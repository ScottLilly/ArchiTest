using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SampleProject
{
    public static class Verify
    {
        public static IEnumerable<Assembly> InAssemblies(params string[] assemblyNamePatterns)
        {
            var allAssemblies =
                AppDomain.CurrentDomain.GetAssemblies().ToList();

            if (assemblyNamePatterns == null)
            {
                return allAssemblies;
            }

            List<Regex> regexPatterns = CreateRegexPatterns(assemblyNamePatterns);

            var matchingAssemblies = new List<Assembly>();

            // Check each assembly against the patterns
            foreach (var assembly in allAssemblies)
            {
                string assemblyName = assembly.GetName().Name;

                foreach (var regex in regexPatterns)
                {
                    if (regex.IsMatch(assemblyName))
                    {
                        matchingAssemblies.Add(assembly);
                        break; // Break to avoid adding the same assembly multiple times
                    }
                }
            }

            return matchingAssemblies;
        }

        #region Private Methods

        private static List<Regex> CreateRegexPatterns(string[] namePatterns)
        {
            var regexPatterns = new List<Regex>();

            foreach (var pattern in namePatterns)
            {
                var regexPattern =
                    new Regex("^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);

                regexPatterns.Add(regexPattern);
            }

            return regexPatterns;
        }

        #endregion
    }
}