using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using SemVersion;

namespace DebuggableSourceGenerators.Read
{
    public class DotNetFramework
    {
        private static Regex VersionRegex = new Regex(@"([0-9]+\.[0-9]*)");

        public static DotNetFramework GetCurrentlyExecutingTargetFramework()
        {
            object[] list = Assembly.GetExecutingAssembly().GetCustomAttributes(true);
            var attribute = list.OfType<TargetFrameworkAttribute>().FirstOrDefault();
            return Parse(attribute.FrameworkName);
        }
        
        public static DotNetFramework Parse(string targetFrameworkText)
        {
            DotNetFrameworkType type;
            
            if (targetFrameworkText.Contains("Core", StringComparison.OrdinalIgnoreCase))
            {
                type = DotNetFrameworkType.DotNetCore;
            }
            else if (targetFrameworkText.Contains("Standard", StringComparison.OrdinalIgnoreCase))
            {
                type = DotNetFrameworkType.DotNetStandard;
            }
            else if (targetFrameworkText.Contains("Framework", StringComparison.OrdinalIgnoreCase))
            {
                type = DotNetFrameworkType.DotNetFramework;
            }
            else
            {
                throw new ArgumentException(nameof(targetFrameworkText));
            }

            var version = VersionRegex.Match(targetFrameworkText);
            if (!version.Success)
            {
                throw new ArgumentException(nameof(targetFrameworkText));
            }

            return new DotNetFramework(type, SemanticVersion.Parse(version.Value));
        }
        
        public DotNetFramework(DotNetFrameworkType type, SemanticVersion version)
        {
            Type = type;
            Version = version;
        }

        public DotNetFrameworkType Type { get; }
        public SemanticVersion Version { get; }

        public bool CanDependOn(DotNetFramework dotNetFramework)
        {
            if (Type == dotNetFramework.Type)
            {
                return Version >= dotNetFramework.Version;
            }

            if (Type == DotNetFrameworkType.DotNetFramework &&
                dotNetFramework.Type == DotNetFrameworkType.DotNetStandard)
            {
                if (dotNetFramework.Version == "2.1")
                {
                    return false;
                }
                
                if (dotNetFramework.Version >= "1.4")
                {
                    return Version >= "4.6.1";
                }
                
                if (dotNetFramework.Version == "1.3")
                {
                    return Version >= "4.6";
                }
                
                if (dotNetFramework.Version == "1.2")
                {
                    return Version >= "4.5.1";
                }

                return Version >= "4.5";
            }

            if (Type == DotNetFrameworkType.DotNetCore &&
                dotNetFramework.Type == DotNetFrameworkType.DotNetStandard)
            {
                if (dotNetFramework.Version == "2.1")
                {
                    return Version >= "3.0";
                }
                
                if (dotNetFramework.Version >= "2.0")
                {
                    return Version >= "2.0";
                }
                
                return true;
            }

            return false;
        }
        
        protected bool Equals(DotNetFramework other)
        {
            return Type == other.Type && Equals(Version, other.Version);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DotNetFramework) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) Type, Version);
        }
    }
}