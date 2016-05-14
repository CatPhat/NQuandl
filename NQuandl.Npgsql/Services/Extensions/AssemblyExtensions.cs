using System;
using System.IO;
using System.Reflection;

namespace NQuandl.Npgsql.Services.Extensions
{
    public static class AssemblyExtensions
    {
        public static string GetManifestResourceText(this Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) throw new InvalidOperationException(string.Format(
                    "Unable to get stream for embedded resource '{0}' in assembly '{1}'.",
                    resourceName, assembly.FullName));
                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();
            }
        }
    }
}