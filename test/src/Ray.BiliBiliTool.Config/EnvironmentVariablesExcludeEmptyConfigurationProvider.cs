using System;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Ray.BiliBiliTool.Infrastructure;

namespace Ray.BiliBiliTool.Config
{
    public class EnvironmentVariablesExcludeEmptyConfigurationProvider : EnvironmentVariablesConfigurationProvider
    {
        private readonly string prefix;

        public EnvironmentVariablesExcludeEmptyConfigurationProvider(string prefix = null) : base(prefix)
        {
            this.prefix = prefix ?? string.Empty;
        }

        public override void Load()
        {
            var dictionary = Environment.GetEnvironmentVariables()
                .Cast<DictionaryEntry>()
                .Where(it => it.Key.ToString().StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                             && !string.IsNullOrWhiteSpace(it.Value.ToString()))//过滤掉空值的（使用GitHub Actions的脚本传入环境变量，空值会覆盖，本地并不会，所以这里做了特殊处理）
                .ToDictionary(it => it.Key.ToString().Substring(prefix.Length), it => it.Value.ToString());

            System.Console.WriteLine(JsonSerializer.Serialize(dictionary, JsonSerializerOptionsBuilder.Builder(x => x.WriteIndented = true)));

            this.Data = new Dictionary<string, string>(dictionary, StringComparer.OrdinalIgnoreCase);
        }
    }
}
