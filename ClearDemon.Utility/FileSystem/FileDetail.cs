//
// FileDetail.cs
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
namespace ClearDemon.Utility.FileSystem
{
    public abstract class FileDetail
    {
        public abstract long Size
        {
            get;
        }

        public abstract DateTime DateCreated
        {
            get;
        }

        public abstract DateTime DateModified
        {
            get;
        }

        public abstract bool Exists
        {
            get;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Extension
        {
            get;
            private set;
        }

        public string FullName
        {
            get { return string.IsNullOrEmpty(Extension) ? Name : (Name + Extension); }
        }

        public string Path
        {
            get;
            private set;
        }

        public string FullPath
        {
            get;
            private set;
        }

        public bool IsDirectory
        {
            get;
            private set;
        }

        protected void SetName(string fullPath)
        {
            if(fullPath == null)
                throw new ArgumentNullException(nameof(fullPath));

            FullPath = fullPath;
            Name = System.IO.Path.GetFileNameWithoutExtension(fullPath);
            Path = System.IO.Path.GetDirectoryName(fullPath);
            // get extension, but ensure it has a period at the start
            Extension = System.IO.Path.GetExtension(fullPath);
            if(string.IsNullOrEmpty(Extension) && !Extension.StartsWith(".", StringComparison.Ordinal))
                Extension = "." + Extension;
        }

        protected FileDetail(string fullPath, bool isDirectory = false)
        {
            IsDirectory = isDirectory;
            SetName(fullPath);
        }
    }
}

