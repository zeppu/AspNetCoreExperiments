using System;

namespace DelegatesTest
{
    public class VersionAttribute : Attribute
    {
        public int Version { get; }

        public VersionAttribute(int version)
        {
            Version = version;
        }
    }
}