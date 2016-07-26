using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using ClearDemon.Utility.FileSystem;

namespace ClearDemon.Utility
{
	/// <summary>
	/// Implementation of FileDetail that uses the .NET Framework System.IO namespace.
	/// </summary>
	public sealed class FileDetailShared : FileDetail
	{
		// wrap around FileInfo
		readonly FileInfo _info;

		public override long Size
		{
			get
			{
				return _info.Length;
			}
		}

		public override DateTime DateCreated
		{
			get
			{
				return _info.CreationTimeUtc;
			}
		}

		public override DateTime DateModified
		{
			get
			{
				return _info.LastWriteTimeUtc;
			}
		}

		public override bool Exists
		{
			get
			{
				return _info.Exists;
			}
		}

		internal FileDetailShared(string fullPath, bool isDirectory = false) : base(fullPath, isDirectory) 
		{
			_info = new FileInfo(fullPath);
		}
	}

	/// <summary>
	/// FileSystem implementation that uses the .NET Framework System.IO namespace.
	/// </summary>
	public sealed class FileSystemShared : FileSystemBase
	{
		public override async Task<IEnumerable<FileDetail>> GetFileListAsync(string path, bool recursive = false)
		{
			if(path == null)
				throw new ArgumentNullException(nameof (path));

			await AwaitHelper.EnsureNotMainThread();

			var di = new DirectoryInfo(path);
			if(di.Exists)
			{
				var details = new List<FileDetail>();
				foreach(var fi in di.EnumerateFiles("*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
					details.Add(new FileDetailShared(fi.FullName));
				return details;
			}
			// return null if no folder exists at the path
			return null;
		}

		public override async Task<IEnumerable<string>> GetDirectoryListAsync(string path, bool recursive = false)
		{
			if(path == null)
				throw new ArgumentNullException(nameof (path));

			await AwaitHelper.EnsureNotMainThread();

			var root = new DirectoryInfo(path);
			if(root.Exists)
			{
				var list = new List<string>();
				foreach(var di in root.EnumerateDirectories("*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
					list.Add(di.FullName);
				return list;
			}

			return null;
		}

		public override async Task<FileDetail> GetFileDetailAsync(string path)
		{
			if(path == null)
				throw new ArgumentNullException(nameof (path));

			await AwaitHelper.EnsureNotMainThread();

			return new FileDetailShared(path);
		}

		public override async Task<Stream> OpenFileAsync (string path, AccessMode accessMode = AccessMode.ReadOnly, CollisionMethod collisionMethod = CollisionMethod.NormalOpen)
		{
			if(path == null)
				throw new ArgumentNullException(nameof (path));

			await AwaitHelper.EnsureNotMainThread();

			var fi = new FileInfo(path);

			// set some default enums
			FileMode mode = FileMode.Open;
			FileAccess access = FileAccess.Read;
			FileShare sharing = FileShare.Read;
			FileOptions opts = FileOptions.RandomAccess | FileOptions.Asynchronous;

			// parse the supplied options
			switch(accessMode)
			{
				case AccessMode.ReadWrite:
					mode = FileMode.OpenOrCreate;
					access = FileAccess.ReadWrite;
					break;
				case AccessMode.ReadWriteTemporary:
					mode = FileMode.OpenOrCreate;
					access = FileAccess.ReadWrite;
					opts |= FileOptions.DeleteOnClose;
					break;
				case AccessMode.ReadWriteExclusive:
					mode = FileMode.OpenOrCreate;
					access = FileAccess.ReadWrite;
					sharing = FileShare.None;
					break;
			}

			// check collision options
			if(fi.Exists)
			{
				if(collisionMethod == CollisionMethod.FailIfExists)
					return null;
				else if(collisionMethod == CollisionMethod.RenameIfExists)
				{
					// append numbers until we find a unique one
					var fPath = fi.DirectoryName;
					var fName = Path.GetFileNameWithoutExtension(fi.Name);
					for(int num = 2;; num++)
					{
						var name = Path.Combine(fPath, string.Format("{0}-{1}.{2}", fName, num, fi.Extension));
						if(!File.Exists(name))
						{
							path = name; // use this new name
							break;
						}
					}
				}
				else if(collisionMethod == CollisionMethod.OverwriteIfExists && accessMode != AccessMode.ReadOnly)
					mode = FileMode.Create;
			}

			// try creating the file stream
			try
			{
				return new FileStream(path, mode, access, sharing, 16 * 1024, opts);
			}
			catch(ArgumentException)
			{
				// bad file name
			}
			catch(NotSupportedException)
			{
				// something weird, like lpt1:
			}
			catch(FileNotFoundException)
			{
				// file doesn't exist for read
			}
			catch(IOException)
			{
				// stream broke
			}
			catch(UnauthorizedAccessException)
			{
				// not permitted
			}
			return null;
		}

		public override async Task<bool> DeleteFileAsync(string path)
		{
			if(path == null)
				throw new ArgumentNullException(nameof (path));

			await AwaitHelper.EnsureNotMainThread();

			if(File.Exists(path))
			{
				try
				{
					File.Delete(path);
				}
				catch(Exception)
				{
					return false;
				}
			}

			// return success if doesn't exist in the first place
			return true;
		}

		public override async Task<bool> CreateDirectoryAsync(string path, bool allowExisting = true)
		{
			if(path == null)
				throw new ArgumentNullException(nameof (path));
			
			await AwaitHelper.EnsureNotMainThread();

			if(Directory.Exists(path))
				return allowExisting;

			Directory.CreateDirectory(path);
			return true;
		}

		public override async Task<bool> DeleteDirectoryAsync(string path, bool recursive)
		{
			if(path == null)
				throw new ArgumentNullException(nameof (path));

			await AwaitHelper.EnsureNotMainThread();

			try
			{
				Directory.Delete(path, recursive);
			}
			catch(Exception)
			{
				return false;
			}

			return true;
		
		}

		public override async Task<string> GetTemporaryFilePathAsync(bool createFile = false, string useExtension = null)
		{
			await AwaitHelper.EnsureNotMainThread();

			if(!createFile)
				return Path.GetTempPath();
			if(useExtension == null || useExtension.Length < 2)
				return Path.GetTempFileName();

			if(!useExtension.StartsWith (".", StringComparison.Ordinal))
				useExtension = "." + useExtension;
			var path = Path.Combine(Path.GetTempPath(), Path.GetFileName(Path.GetRandomFileName()) + useExtension);
			if(createFile)
				File.WriteAllText(path, string.Empty);
			return path;
		}

		public override async Task<bool> CheckExistsAsync(string path)
		{
				await AwaitHelper.EnsureNotMainThread();

			return File.Exists(path) || Directory.Exists(path);
		}

		public async override Task<bool> RenameFileAsync(string oldPath, string newPath)
		{
			await AwaitHelper.EnsureNotMainThread();

			File.Move(oldPath, newPath);
			return true;
		}

		internal FileSystemShared() { }
	}
}

