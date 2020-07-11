﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Sdcb.TypeScript;
using Xunit;

namespace EventHorizon.Blazor.TypeScript.Interop.Generator.Tests.GenerateClassStatementStringTests
{
    public class ButtonTest
    {
        [Fact]
        public void ShouldGenerateButtonString()
        {
            // Given
            var sourceFile = "babylon.gui.d.ts";
            var source = File.ReadAllText($"./SourceFiles/{sourceFile}");
            var ast = new TypeScriptAST(source, sourceFile);

            // When
            var generated = GenerateInteropClassStatement.Generate(
                "ProjectAssembly",
                "Button",
                ast
            );
            var actual = GenerateClassStatementString.Generate(
                generated
            );

            // Then
            actual.Should().Be(expected);

        }

        string expected = @"/// Generated - Do Not Edit
namespace BabylonJS.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;
    using Microsoft.JSInterop;

    
    
    [JsonConverter(typeof(CachedEntityConverter))]
    public class Button : Rectangle
    {
        #region Static Accessors

        #endregion

        #region Static Properties

        #endregion

        #region Static Methods
        public static Button CreateImageButton(string name, string text, string imageUrl)
        {
            return EventHorizonBlazorInteropt.FuncClass<Button>(
                entity => new Button(entity),
                new object[] 
                {
                    new string[] { ""BABYLON"", ""GUI"", ""Button"", ""CreateImageButton"" }, name, text, imageUrl
                }
            );
        }

        public static Button CreateImageOnlyButton(string name, string imageUrl)
        {
            return EventHorizonBlazorInteropt.FuncClass<Button>(
                entity => new Button(entity),
                new object[] 
                {
                    new string[] { ""BABYLON"", ""GUI"", ""Button"", ""CreateImageOnlyButton"" }, name, imageUrl
                }
            );
        }

        public static Button CreateSimpleButton(string name, string text)
        {
            return EventHorizonBlazorInteropt.FuncClass<Button>(
                entity => new Button(entity),
                new object[] 
                {
                    new string[] { ""BABYLON"", ""GUI"", ""Button"", ""CreateSimpleButton"" }, name, text
                }
            );
        }

        public static Button CreateImageWithCenterTextButton(string name, string text, string imageUrl)
        {
            return EventHorizonBlazorInteropt.FuncClass<Button>(
                entity => new Button(entity),
                new object[] 
                {
                    new string[] { ""BABYLON"", ""GUI"", ""Button"", ""CreateImageWithCenterTextButton"" }, name, text, imageUrl
                }
            );
        }
        #endregion

        #region Accessors
        private Image __image;
        public Image image
        {
            get
            {
            if(__image == null)
            {
                __image = EventHorizonBlazorInteropt.GetClass<Image>(
                    this.___guid,
                    ""image"",
                    (entity) =>
                    {
                        return new Image(entity);
                    }
                );
            }
            return __image;
            }
        }

        private TextBlock __textBlock;
        public TextBlock textBlock
        {
            get
            {
            if(__textBlock == null)
            {
                __textBlock = EventHorizonBlazorInteropt.GetClass<TextBlock>(
                    this.___guid,
                    ""textBlock"",
                    (entity) =>
                    {
                        return new TextBlock(entity);
                    }
                );
            }
            return __textBlock;
            }
        }
        #endregion

        #region Properties
        
        public string name
        {
            get
            {
            return EventHorizonBlazorInteropt.Get<string>(
                    this.___guid,
                    ""name""
                );
            }
            set
            {

                EventHorizonBlazorInteropt.Set(
                    this.___guid,
                    ""name"",
                    value
                );
            }
        }

        
        public bool delegatePickingToChildren
        {
            get
            {
            return EventHorizonBlazorInteropt.Get<bool>(
                    this.___guid,
                    ""delegatePickingToChildren""
                );
            }
            set
            {

                EventHorizonBlazorInteropt.Set(
                    this.___guid,
                    ""delegatePickingToChildren"",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public Button() : base() { }

        public Button(
            ICachedEntity entity
        ) : base(entity)
        {
        }

        public Button(
            string name = null
        ) : base()
        {
            var entity = EventHorizonBlazorInteropt.New(
                new string[] { ""BABYLON"", ""GUI"", ""Button"" },
                name
            );
            ___guid = entity.___guid;
        }
        #endregion

        #region Methods
        #region pointerEnterAnimation TODO: Get Comments as metadata identification
        private bool _isPointerEnterAnimationEnabled = false;
        private readonly IDictionary<string, Func<Task>> _pointerEnterAnimationActionMap = new Dictionary<string, Func<Task>>();

        public string pointerEnterAnimation(
            Func<Task> callback
        )
        {
            SetupPointerEnterAnimationLoop();

            var handle = Guid.NewGuid().ToString();
            _pointerEnterAnimationActionMap.Add(
                handle,
                callback
            );

            return handle;
        }

        private void SetupPointerEnterAnimationLoop()
        {
            if (_isPointerEnterAnimationEnabled)
            {
                return;
            }
            EventHorizonBlazorInteropt.FuncCallback(
                this,
                ""pointerEnterAnimation"",
                ""CallPointerEnterAnimationActions"",
                _invokableReference
            );
            _isPointerEnterAnimationEnabled = true;
        }

        [JSInvokable]
        public async Task CallPointerEnterAnimationActions()
        {
            foreach (var action in _pointerEnterAnimationActionMap.Values)
            {
                await action();
            }
        }
        #endregion

        #region pointerOutAnimation TODO: Get Comments as metadata identification
        private bool _isPointerOutAnimationEnabled = false;
        private readonly IDictionary<string, Func<Task>> _pointerOutAnimationActionMap = new Dictionary<string, Func<Task>>();

        public string pointerOutAnimation(
            Func<Task> callback
        )
        {
            SetupPointerOutAnimationLoop();

            var handle = Guid.NewGuid().ToString();
            _pointerOutAnimationActionMap.Add(
                handle,
                callback
            );

            return handle;
        }

        private void SetupPointerOutAnimationLoop()
        {
            if (_isPointerOutAnimationEnabled)
            {
                return;
            }
            EventHorizonBlazorInteropt.FuncCallback(
                this,
                ""pointerOutAnimation"",
                ""CallPointerOutAnimationActions"",
                _invokableReference
            );
            _isPointerOutAnimationEnabled = true;
        }

        [JSInvokable]
        public async Task CallPointerOutAnimationActions()
        {
            foreach (var action in _pointerOutAnimationActionMap.Values)
            {
                await action();
            }
        }
        #endregion

        #region pointerDownAnimation TODO: Get Comments as metadata identification
        private bool _isPointerDownAnimationEnabled = false;
        private readonly IDictionary<string, Func<Task>> _pointerDownAnimationActionMap = new Dictionary<string, Func<Task>>();

        public string pointerDownAnimation(
            Func<Task> callback
        )
        {
            SetupPointerDownAnimationLoop();

            var handle = Guid.NewGuid().ToString();
            _pointerDownAnimationActionMap.Add(
                handle,
                callback
            );

            return handle;
        }

        private void SetupPointerDownAnimationLoop()
        {
            if (_isPointerDownAnimationEnabled)
            {
                return;
            }
            EventHorizonBlazorInteropt.FuncCallback(
                this,
                ""pointerDownAnimation"",
                ""CallPointerDownAnimationActions"",
                _invokableReference
            );
            _isPointerDownAnimationEnabled = true;
        }

        [JSInvokable]
        public async Task CallPointerDownAnimationActions()
        {
            foreach (var action in _pointerDownAnimationActionMap.Values)
            {
                await action();
            }
        }
        #endregion

        #region pointerUpAnimation TODO: Get Comments as metadata identification
        private bool _isPointerUpAnimationEnabled = false;
        private readonly IDictionary<string, Func<Task>> _pointerUpAnimationActionMap = new Dictionary<string, Func<Task>>();

        public string pointerUpAnimation(
            Func<Task> callback
        )
        {
            SetupPointerUpAnimationLoop();

            var handle = Guid.NewGuid().ToString();
            _pointerUpAnimationActionMap.Add(
                handle,
                callback
            );

            return handle;
        }

        private void SetupPointerUpAnimationLoop()
        {
            if (_isPointerUpAnimationEnabled)
            {
                return;
            }
            EventHorizonBlazorInteropt.FuncCallback(
                this,
                ""pointerUpAnimation"",
                ""CallPointerUpAnimationActions"",
                _invokableReference
            );
            _isPointerUpAnimationEnabled = true;
        }

        [JSInvokable]
        public async Task CallPointerUpAnimationActions()
        {
            foreach (var action in _pointerUpAnimationActionMap.Values)
            {
                await action();
            }
        }
        #endregion
        #endregion
    }
}";
    }
}
