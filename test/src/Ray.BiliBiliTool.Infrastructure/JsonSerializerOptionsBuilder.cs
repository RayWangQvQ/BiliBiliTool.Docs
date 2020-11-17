using System;
using System.Text.Json;
using System.Text.Unicode;

namespace Ray.BiliBiliTool.Infrastructure
{
    /// <summary>
    /// System.Text.Json的序列化OptionsBuilder
    /// </summary>
    public class JsonSerializerOptionsBuilder
    {
        /// <summary>
        /// 默认配置
        /// </summary>
        public static JsonSerializerOptions DefaultOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        public static JsonSerializerOptions Builder(Action<JsonSerializerOptions> build)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            build(options);
            return options;
        }
    }
}
