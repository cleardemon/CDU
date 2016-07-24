using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClearDemon.Utility.FileSystem;

namespace ClearDemon.Utility.Platform
{
    /// <summary>
    /// Base utility functions and implementation shared between all platform implementations.
    /// </summary>
    public abstract class PlatformUtilityBase : IPlatformUtility
    {
        /// <summary>
        /// Gets the path to the application's cache directory.
        /// </summary>
        public abstract string CachePath { get; }
        /// <summary>
        /// Provides information about the current platform and its executing environment.
        /// </summary>
        public abstract IPlatformInfo CurrentPlatform { get; }
        /// <summary>
        /// Gets the path to the application's data directory.
        /// </summary>
        public abstract string DataPath { get; }
        /// <summary>
        /// Gets the name of the device, typically specified by the device owner.
        /// </summary>
        public abstract string DeviceName { get; }
        /// <summary>
        /// Gets a unique identifier that may be used to identify the current device.
        /// </summary>
        public abstract string DeviceUniqueId { get; }
        /// <summary>
        /// Gets the path to the executable of the application.
        /// </summary>
        public abstract string ExecutablePath { get; }
        /// <summary>
        /// Provides access to the local file system, to read and write files and directories.
        /// </summary>
        public abstract IFileSystem LocalFileSystem { get; }
        /// <summary>
        /// Gets the Assembly of the current executable.
        /// </summary>
        public abstract Assembly ExecutableAssembly { get; }
        /// <summary>
        /// Gets the defined name of the application, from the manifest.
        /// </summary>
        public abstract string ApplicationName { get; }
        /// <summary>
        /// Gets the defined version of the application, from the manifest.
        /// </summary>
        public abstract Version ApplicationVersion { get; }
        /// <summary>
        /// Provides details and access to specific features on a platform.
        /// </summary>
        public abstract IPlatformFeature Features { get; }
        /// <summary>
        /// Starts work that is guaranteed to be executed on the main thread.
        /// </summary>
        /// <param name="work">Work to perform on the main thread.</param>
        public abstract void BeginInvokeOnMainThread(Action work);

        //
        // Utility functions for implementations
        //

        string _formattedVersion;

        /// <summary>
        /// Gets a formatted, readable version of the device and its operating system.
        /// </summary>
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
