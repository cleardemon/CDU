using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClearDemon.Utility.FileSystem;

namespace ClearDemon.Utility.Platform
{
    public enum PlatformFeatureSystemBarType
    {
        /// <summary>
        /// Normal system/statusbar visible.
        /// </summary>
        Default,
        /// <summary>
        /// No system/status bar visible.
        /// </summary>
        Hidden,
        /// <summary>
        /// Dark theme on macOS/iOS.
        /// </summary>
        Alternate
    }

    public interface IPlatformFeature
    {
        int ApplicationBadgeNumber { get; set; }
        void SetSystemBarType(PlatformFeatureSystemBarType type);
    }

    public interface IPlatformUtility
    {
        string CachePath { get; }
        string DataPath { get; }

        IPlatformInfo CurrentPlatform { get; }

        string ExecutablePath { get; }
        string DeviceUniqueId { get; }
        string DeviceName { get; }

        IFileSystem LocalFileSystem { get; }

        Assembly ExecutableAssembly { get; }
        string ApplicationName { get; }
        Version ApplicationVersion { get; }

        void BeginInvokeOnMainThread(Action work);

        IPlatformFeature Features { get; }
    }
}
