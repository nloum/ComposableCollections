using System;

namespace DebuggableSourceGenerators.Read
{
    public class NugetPackageSpecifier
    {
        public NugetPackageSpecifier(string packageName, string packageVersion, DotNetFramework targetFramework)
        {
            PackageName = packageName;
            PackageVersion = packageVersion;
            TargetFramework = targetFramework;
        }

        public string PackageName { get; }
        public string PackageVersion { get; }
        public DotNetFramework TargetFramework { get; }

        protected bool Equals(NugetPackageSpecifier other)
        {
            return PackageName == other.PackageName && PackageVersion == other.PackageVersion && TargetFramework == other.TargetFramework;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NugetPackageSpecifier) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PackageName, PackageVersion, TargetFramework);
        }
    }
}