using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearDemon.Utility.Platform
{
    [Flags]
    public enum PlatformInfoType
    {
        Unknown = 0,
        AppleIOS = 1,
        AppleTVOS = 2,
        AppleWatchOS = 4,
        AppleMacOS = 8,
        Android = 16,
        Windows = 32,
        WindowsPhone = 64,
        Linux = 128
    }

    public enum PlatformInfoFormFactor
    {
        Unknown,
        Desktop,
        Phone,
        Tablet
    }

    public interface IPlatformInfo
    {
        string ManufacturerName { get; }
        PlatformInfoType PlatformType { get; }
        Version ReleaseVersion { get; }
        bool IsSimulated { get; }
        PlatformInfoFormFactor FormFactor { get; }
    }
}
