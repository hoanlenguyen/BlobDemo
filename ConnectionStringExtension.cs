using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace BlobsDemo
{
    public static class ConnectionStringExtension
    {
        public static DbConnectionStringBuilder AsConnectionString(this string connectionString)
        {
            var stringBuilder = new DbConnectionStringBuilder();
            stringBuilder.ConnectionString = connectionString;
            return stringBuilder;
        }
        //public static DbConnectionStringBuilder Bind<T>(this DbConnectionStringBuilder builder, T config);
        public static DbConnectionStringBuilder Get(this DbConnectionStringBuilder builder, string keyword, out string value)
        {
            value = builder[keyword] as string;
            return builder;
        }
        //public static T Read<T>(string connectionString) where T : class, new();
    }
}
