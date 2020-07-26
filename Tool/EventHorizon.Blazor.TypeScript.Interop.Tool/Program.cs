using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.IO;
using System.Net;
using EventHorizon.Blazor.TypeScript.Interop.Generator;
using EventHorizon.Blazor.TypeScript.Interop.Generator.Formatter;
using EventHorizon.Blazor.TypeScript.Interop.Generator.Logging;
using EventHorizon.Blazor.TypeScript.Interop.Generator.Writers.Project;
using EventHorizon.Blazor.TypeScript.Interop.Tool.ToolException;

namespace EventHorizon.Blazor.TypeScript.Interop.Tool
{
    class Program
    {
        private static readonly string SOURCE_FILES_DIRECTORY_NAME = "_sourceFiles";

        static int Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                new Option<IList<string>>(
                    new string[] { "--source", "-s" },
                    description: "TypeScript Definition to generate from, accepts multiple or as Array"
                )
                {
                    IsRequired = true,
                },
                new Option<IList<string>>(
                    new string[] { "--class-to-generate", "-c" },
                    description: "A Class to generate, accepts multiple or as Array"
                )
                {
                    IsRequired = true,
                },
                new Option<string>(
                    new string[] { "--project-assembly", "-a" },
                    getDefaultValue: () => "Generated.WASM",
                    description: ""
                ),
                new Option<string>(
                    new string[] { "--project-generation-location", "-l" },
                    getDefaultValue: () => "_generated",
                    description: ""
                ),
                new Option<bool>(
                    new string[] { "--force", "-f" },
                    getDefaultValue: () => false,
                    description: "This will force generation, by deleting --project-generation-location"
                )
            };

            rootCommand.Description = "Generate a Blazor WASM project from a TypeScript definition file.";

            // Note that the parameters of the handler method are matched according to the names of the options
            rootCommand.Handler = CommandHandler.Create<IList<string>, IList<string>, string, string, bool>(
                GenerateSources
            );

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }

        private static int GenerateSources(
            IList<string> source,
            IList<string> classToGenerate,
            string projectAssembly = "Generated.WASM",
            string projectGenerationLocation = "_generated",
            bool force = false
        )
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                ValidateArguments(
                    source,
                    classToGenerate,
                    projectAssembly,
                    projectGenerationLocation
                );
                GlobalLogger.Info($"projectAssembly: {projectAssembly}");
                GlobalLogger.Info($"projectGenerationLocation: {projectGenerationLocation}");

                GlobalLogger.Info($"classToGenerate.Length: {classToGenerate.Count}");
                foreach (var classToGenerateItem in classToGenerate)
                {
                    GlobalLogger.Info($"classToGenerateItem: {classToGenerateItem}");
                }

                GlobalLogger.Info($"sourceFile.Length: {source.Count}");
                foreach (var sourceFileItem in source)
                {
                    GlobalLogger.Info($"sourceFile: {sourceFileItem}");
                }

                projectGenerationLocation = Path.Combine(
                    ".",
                    projectGenerationLocation
                );

                var sourceDirectory = Path.Combine(
                    ".",
                    SOURCE_FILES_DIRECTORY_NAME
                );
                var sourceFiles = CopyAndDownloadSourceFiles(
                    source
                );
                var generationList = classToGenerate;

                // Check for already Generated Source.
                var projectAssemblyDirectory = Path.Combine(
                    projectGenerationLocation,
                    projectAssembly
                );
                if (Directory.Exists(
                    projectAssemblyDirectory
                ))
                {
                    if (!force)
                    {
                        GlobalLogger.Error(
                            $"Project Assembly Directory was not empty: {projectAssemblyDirectory}"
                        );
                        GlobalLogger.Error(
                            $"Use --force to replace directory."
                        );
                        return 502;
                    }

                    GlobalLogger.Warning(
                        $"Deleting existing projectAssemblyDirectory: {projectAssemblyDirectory}"
                    );
                    Directory.Delete(
                        projectAssemblyDirectory,
                        true
                    );
                }

                var textFormatter = new NoFormattingTextFormatter();
                var writer = new ProjectWriter(
                    projectGenerationLocation,
                    projectAssembly
                );

                new GenerateSource().Run(
                    projectAssembly,
                    sourceDirectory,
                    sourceFiles,
                    generationList,
                    writer,
                    textFormatter,
                    new Dictionary<string, string>
                    {
                    { "BABYLON.PointerInfoBase | type", "int" }
                    }
                );
                stopwatch.Stop();
                GlobalLogger.Success($"Took {stopwatch.ElapsedMilliseconds}ms to Generate Source Project.");

                return 0;
            }
            catch (ArgumentException ex)
            {
                GlobalLogger.Error(
                    $"Argument failure: {ex.ParamName} -> {ex.Message}"
                );
                return 404;
            }
            catch (InvalidSourceFileException ex)
            {
                GlobalLogger.Error(
                    $"Invalid Source File Exception: {ex.Message}"
                );
                return 501;
            }
        }

        private static void ValidateArguments(
            IList<string> sources,
            IList<string> classToGenerate,
            string projectAssembly,
            string projectGenerationLocation
        )
        {
            if (sources == null || sources.Count == 0)
            {
                throw new ToolArgumentException(
                    "--sources",
                    "No sources found."
                );
            }
            if (classToGenerate == null || classToGenerate.Count == 0)
            {
                throw new ToolArgumentException(
                    "--class-to-generate",
                    "Did not found any classes to generate."
                );
            }
            if (string.IsNullOrWhiteSpace(
                projectAssembly
            ))
            {
                throw new ToolArgumentException(
                    "--project-assembly",
                    "Project assembly was null or empty."
                );
            }
            if (string.IsNullOrWhiteSpace(
                projectGenerationLocation
            ))
            {
                throw new ToolArgumentException(
                    "--project-generation-location",
                    "Project Generation Location was null or empty."
                );
            }
        }

        /// <summary>
        /// Copy file or if a URL download the file to a common SOURCE_FILES_DIRECTORY_NAME
        /// </summary>
        /// <param name="sourceFileList">The path or URL to a list of files to use as the Source.</param>
        /// <returns>The filenames of the copied and URLs.</returns>
        private static IList<string> CopyAndDownloadSourceFiles(
            IList<string> sourceFileList
        )
        {
            using var client = new WebClient();
            var filesFound = new List<string>();
            var sourcesFileDirectoryFullName = Path.Combine(
                ".",
                SOURCE_FILES_DIRECTORY_NAME
            );
            // Create SOURCE_FILES_DIRECTORY_NAME
            if (!Directory.Exists(
                sourcesFileDirectoryFullName
            ))
            {
                Directory.CreateDirectory(
                    sourcesFileDirectoryFullName
                );
            }

            foreach (var sourceFile in sourceFileList)
            {
                var sourceFileName = new FileInfo(
                    sourceFile
                ).Name;
                var sourceFileFullName = Path.Combine(
                    sourcesFileDirectoryFullName,
                    sourceFileName
                );

                // Remove the file if already existing
                if (File.Exists(
                    sourceFileFullName
                ))
                {
                    File.Delete(
                        sourceFileFullName
                    );
                }

                // If URL
                if (IsUrl(
                    sourceFile
                ))
                {
                    // Download file, and place into SOURCE_FILES_DIRECTORY_NAME directory
                    client.DownloadFile(
                        sourceFile,
                        sourceFileFullName
                    );
                }
                // If FileSystem
                else if (File.Exists(
                    sourceFile
                ))
                {
                    // Copy file into SOURCE_FILES_DIRECTORY_NAME directory, override already existing.
                    File.Copy(
                        sourceFile,
                        sourceFileFullName
                    );
                }
                // If not either, source was not found throw exception
                else
                {
                    throw new InvalidSourceFileException(
                        $"Invalid source file, was not URL or existing on FileSystem: {sourceFile}"
                    );
                }
                filesFound.Add(
                    sourceFileName
                );
            }

            return filesFound;
        }

        private static bool IsUrl(
            string sourceFile
        ) => sourceFile.StartsWith("http://")
            || sourceFile.StartsWith("https://");
    }
}
