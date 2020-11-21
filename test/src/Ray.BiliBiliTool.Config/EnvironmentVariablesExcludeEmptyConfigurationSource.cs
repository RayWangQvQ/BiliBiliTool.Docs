using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace Ray.BiliBiliTool.Config
{
    public class EnvironmentVariablesExcludeEmptyConfigurationSource : IConfigurationSource//: EnvironmentVariablesConfigurationSource
    {
        public string Prefix { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EnvironmentVariablesExcludeEmptyConfigurationProvider(this.Prefix);
        }
    }
}
