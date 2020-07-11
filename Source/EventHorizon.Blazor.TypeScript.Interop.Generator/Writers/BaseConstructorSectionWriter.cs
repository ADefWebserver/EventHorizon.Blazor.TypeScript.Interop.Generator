﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventHorizon.Blazor.TypeScript.Interop.Generator.Model;
using EventHorizon.Blazor.TypeScript.Interop.Generator.Model.Statements;

namespace EventHorizon.Blazor.TypeScript.Interop.Generator.Writers
{
    public class BaseConstructorSectionWriter
    {
        internal static string Write(
            ClassStatement classStatement,
            ConstructorStatement constructorDetails,
            ClassGenerationTemplates templates
        )
        {
            var template = templates.Constructor;
            var extendsClass = classStatement.ExtendedClassNames.Any();

            if (extendsClass)
            {
                template = templates.ConstructorToBase;
            }

            return template.Replace(
                "[[CLASS_NAME]]",
                classStatement.Name
            ).Replace(
                "[[BASE_CLASS_CALL]]",
                extendsClass ? " : base()" : string.Empty
            );
        }
    }
}
