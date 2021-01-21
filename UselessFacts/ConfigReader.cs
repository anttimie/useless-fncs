using System;
using System.Collections.Generic;
using System.Text;

namespace UselessFacts
{
    internal static class ConfigReader
    {
        public static string ConnectionString
        {
            get
            {
                return Environment.GetEnvironmentVariable("DB_CONNECTION", EnvironmentVariableTarget.Process);
            }
        }

    }
}
