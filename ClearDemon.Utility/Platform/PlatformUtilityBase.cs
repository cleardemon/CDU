using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClearDemon.Utility.FileSystem;

namespace ClearDemon.Utility.Platform
{
    public abstract class PlatformUtilityBase : IPlatformUtility
    {
        public abstract string CachePath { get; }
        public abstract IPlatformInfo CurrentPlatform { get; }
        public abstract string DataPath { get; }
        public abstract string DeviceName { get; }
        public abstract string DeviceUniqueId { get; }
        public abstract string ExecutablePath { get; }
        public abstract IFileSystem LocalFileSystem { get; }
        public abstract Assembly ExecutableAssembly { get; }
        public abstract string ApplicationName { get; }
        public abstract Version ApplicationVersion { get; }
        public abstract IPlatformFeature Features { get; }
        public abstract void BeginInvokeOnMainThread(Action work);

        //
        // Utility functions for implementations
        //

        string _formattedVersion;

        public string FormattedPlatformVersion
        {
            get
            {
                if(_formattedVersion == null) {
                    // format the type of the platform
                    string type;
                    switch(CurrentPlatform.PlatformType) {
                        case PlatformInfoType.Android:
                            type = "Android";
                            break;
                        case PlatformInfoType.AppleIOS:
                            type = "iOS";
                            break;
                        case PlatformInfoType.AppleMacOS:
                            type = "macOS";
                            break;
                        case PlatformInfoType.AppleTVOS:
                            type = "tvOS";
                            break;
                        case PlatformInfoType.AppleWatchOS:
                            type = "watchOS";
                            break;
                        case PlatformInfoType.Linux:
                            type = "Linux";
                            break;
                        case PlatformInfoType.Windows:
                            type = "Windows";
                            break;
                        case PlatformInfoType.WindowsPhone:
                            type = "Windows Phone";
                            break;
                        default:
                            type = "Unknown";
                            break;
                    }

                    _formattedVersion = $"{CurrentPlatform.ManufacturerName} {type} {CurrentPlatform.ReleaseVersion.ToString(2)}";
                }
                return _formattedVersion;
            }
        }
    }
}
