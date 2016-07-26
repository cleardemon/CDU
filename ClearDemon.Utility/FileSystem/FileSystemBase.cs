//
// FileSystemBase.cs
//
// Author:
//       Bart King <github@cleardemon.com>
//
// Copyright (c) 2016 Bart King
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ClearDemon.Utility.FileSystem
{
    public abstract class FileSystemBase : IFileSystem
    {
        public abstract Task<FileDetail> GetFileDetailAsync(string path);

        public abstract Task<Stream> OpenFileAsync(string path, AccessMode accessMode = AccessMode.ReadOnly, CollisionMethod collisionMethod = CollisionMethod.NormalOpen);

        public abstract Task<bool> DeleteFileAsync(string path);

        public abstract Task<bool> CreateDirectoryAsync(string path, bool allowExisting = true);

        public abstract Task<bool> DeleteDirectoryAsync(string path, bool recursive = false);

        public abstract Task<string> GetTemporaryFilePathAsync(bool createFile = false, string useExtension = null);

        public abstract Task<IEnumerable<string>> GetDirectoryListAsync(string path, bool recursive = false);

        public abstract Task<IEnumerable<FileDetail>> GetFileListAsync(string path, bool recursive = false);

        public abstract Task<bool> CheckExistsAsync(string path);

        public abstract Task<bool> RenameFileAsync(string oldPath, string newPath);

        protected bool CanOpenFile(AccessMode requestedMode, CollisionMethod requestedCollision, bool doesAlreadyExist)
        {
            if(requestedCollision == CollisionMethod.FailIfExists && doesAlreadyExist)
                return false;
            if(requestedMode == AccessMode.ReadOnly && !doesAlreadyExist)
                return false;
            return true;
        }
    }
}

