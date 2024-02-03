using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SampleProject
{
    public class Verify
    {
        private List<Assembly> _assemblies = new List<Assembly>();

        #region Constructors

        private Verify()
        {
        }

        public static Verify That()
        {
            return new Verify();
        }

        #endregion

        public IEnumerable<Assembly> Assemblies(params string[] namePatterns)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            if (namePatterns == null)
            {
                _assemblies = assemblies;
            }
            else
            {
                // Convert wildcard patterns to regular expressions
                var regexPatterns = new List<Regex>();

                foreach (var pattern in namePatterns)
                {
                    regexPatterns.Add(ConvertWildcardToRegex(pattern));
                }

                // Check each assembly against the patterns
                foreach (var assembly in assemblies)
                {
                    if (_assemblies.Contains(assembly))
                    {
                        continue; // Skip already added assemblies   
                    }

                    foreach (var regex in regexPatterns)
                    {
                        if (regex.IsMatch(assembly.GetName().Name))
                        {
                            _assemblies.Add(assembly);
                            break; // Break to avoid adding the same assembly multiple times
                        }
                    }
                }

            }

            return _assemblies;
        }

        public static Regex ConvertWildcardToRegex(string pattern)
        {
            string regexPattern = 
                "^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";

            return new Regex(regexPattern, RegexOptions.IgnoreCase);
        }
    }
}