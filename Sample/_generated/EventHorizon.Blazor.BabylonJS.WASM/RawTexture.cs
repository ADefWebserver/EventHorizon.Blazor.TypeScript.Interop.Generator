/// Generated - Do Not Edit
namespace BabylonJS
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;
    using Microsoft.JSInterop;

    
    
    [JsonConverter(typeof(CachedEntityConverter))]
    public class RawTexture : Texture
    {
        #region Static Accessors

        #endregion

        #region Static Properties

        #endregion

        #region Static Methods
        public static RawTexture CreateLuminanceTexture(ArrayBufferView data, decimal width, decimal height, Scene scene, System.Nullable<bool> generateMipMaps = null, System.Nullable<bool> invertY = null, System.Nullable<decimal> samplingMode = null)
        {
            return EventHorizonBlazorInteropt.FuncClass<RawTexture>(
                entity => new RawTexture(entity),
                new object[] 
                {
                    new string[] { "BABYLON", "RawTexture", "CreateLuminanceTexture" }, data, width, height, scene, generateMipMaps, invertY, samplingMode
                }
            );
        }

        public static RawTexture CreateLuminanceAlphaTexture(ArrayBufferView data, decimal width, decimal height, Scene scene, System.Nullable<bool> generateMipMaps = null, System.Nullable<bool> invertY = null, System.Nullable<decimal> samplingMode = null)
        {
            return EventHorizonBlazorInteropt.FuncClass<RawTexture>(
                entity => new RawTexture(entity),
                new object[] 
                {
                    new string[] { "BABYLON", "RawTexture", "CreateLuminanceAlphaTexture" }, data, width, height, scene, generateMipMaps, invertY, samplingMode
                }
            );
        }

        public static RawTexture CreateAlphaTexture(ArrayBufferView data, decimal width, decimal height, Scene scene, System.Nullable<bool> generateMipMaps = null, System.Nullable<bool> invertY = null, System.Nullable<decimal> samplingMode = null)
        {
            return EventHorizonBlazorInteropt.FuncClass<RawTexture>(
                entity => new RawTexture(entity),
                new object[] 
                {
                    new string[] { "BABYLON", "RawTexture", "CreateAlphaTexture" }, data, width, height, scene, generateMipMaps, invertY, samplingMode
                }
            );
        }

        public static RawTexture CreateRGBTexture(ArrayBufferView data, decimal width, decimal height, Scene scene, System.Nullable<bool> generateMipMaps = null, System.Nullable<bool> invertY = null, System.Nullable<decimal> samplingMode = null, System.Nullable<decimal> type = null)
        {
            return EventHorizonBlazorInteropt.FuncClass<RawTexture>(
                entity => new RawTexture(entity),
                new object[] 
                {
                    new string[] { "BABYLON", "RawTexture", "CreateRGBTexture" }, data, width, height, scene, generateMipMaps, invertY, samplingMode, type
                }
            );
        }

        public static RawTexture CreateRGBATexture(ArrayBufferView data, decimal width, decimal height, Scene scene, System.Nullable<bool> generateMipMaps = null, System.Nullable<bool> invertY = null, System.Nullable<decimal> samplingMode = null, System.Nullable<decimal> type = null)
        {
            return EventHorizonBlazorInteropt.FuncClass<RawTexture>(
                entity => new RawTexture(entity),
                new object[] 
                {
                    new string[] { "BABYLON", "RawTexture", "CreateRGBATexture" }, data, width, height, scene, generateMipMaps, invertY, samplingMode, type
                }
            );
        }

        public static RawTexture CreateRTexture(ArrayBufferView data, decimal width, decimal height, Scene scene, System.Nullable<bool> generateMipMaps = null, System.Nullable<bool> invertY = null, System.Nullable<decimal> samplingMode = null, System.Nullable<decimal> type = null)
        {
            return EventHorizonBlazorInteropt.FuncClass<RawTexture>(
                entity => new RawTexture(entity),
                new object[] 
                {
                    new string[] { "BABYLON", "RawTexture", "CreateRTexture" }, data, width, height, scene, generateMipMaps, invertY, samplingMode, type
                }
            );
        }
        #endregion

        #region Accessors

        #endregion

        #region Properties
        
        public decimal format
        {
            get
            {
            return EventHorizonBlazorInteropt.Get<decimal>(
                    this.___guid,
                    "format"
                );
            }
            set
            {

                EventHorizonBlazorInteropt.Set(
                    this.___guid,
                    "format",
                    value
                );
            }
        }
        #endregion
        
        #region Constructor
        public RawTexture() : base() { }

        public RawTexture(
            ICachedEntity entity
        ) : base(entity)
        {
        }

        public RawTexture(
            ArrayBufferView data, decimal width, decimal height, decimal format, Scene scene, System.Nullable<bool> generateMipMaps = null, System.Nullable<bool> invertY = null, System.Nullable<decimal> samplingMode = null, System.Nullable<decimal> type = null
        ) : base()
        {
            var entity = EventHorizonBlazorInteropt.New(
                new string[] { "BABYLON", "RawTexture" },
                data, width, height, format, scene, generateMipMaps, invertY, samplingMode, type
            );
            ___guid = entity.___guid;
        }
        #endregion

        #region Methods
        public void update(ArrayBufferView data)
        {
            EventHorizonBlazorInteropt.Func<CachedEntity>(
                new object[] 
                {
                    new string[] { this.___guid, "update" }, data
                }
            );
        }
        #endregion
    }
}