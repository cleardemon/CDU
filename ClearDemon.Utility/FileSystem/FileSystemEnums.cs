//
// AccessMode.cs
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

namespace ClearDemon.Utility.FileSystem
{
    /// <summary>
    /// Defines the file access mode, when opening.
    /// </summary>
    public enum AccessMode
    {
        /// <summary>
        /// Open a file with read/write access. Default.
        /// </summary>
        ReadWrite,
        /// <summary>
        /// Open a file with read-only access, not allowed to write.
        /// </summary>
        ReadOnly,
        /// <summary>
        /// Open a file with read/write access, but delete it when it is closed.
        /// </summary>
        ReadWriteTemporary,
        /// <summary>
        /// Open a file with read/write access, but attempt to obtain an exclusive lock so no other process can access the file at the same time.
        /// </summary>
        ReadWriteExclusive
    }

    /// <summary>
    /// Defines what to do when a file exists during its creation (or similar operation).
    /// </summary>
    public enum CollisionMethod
    {
        /// <summary>
        /// Open the file as normal, if it exists, and create if it doesn't with a read/write access mode.
        /// </summary>
        NormalOpen,
        /// <summary>
        /// Throw exception if opening the file and it already exists. If read/write, create the file.
        /// </summary>
        FailIfExists,
        /// <summary>
        /// Overwrite file if it already exists, if read/write. Throws exception if read-only and exists.
        /// </summary>
        OverwriteIfExists,
        /// <summary>
        /// Renames the existing file, if read/write. Throws exception if read-only and exists.
        /// </summary>
        RenameIfExists
    }
}

