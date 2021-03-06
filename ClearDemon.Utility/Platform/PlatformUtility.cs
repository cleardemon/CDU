﻿//
// PlatformUtility.cs
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
namespace ClearDemon.Utility.Platform
{
    public static class PlatformUtility
    {
        static IPlatformUtility _current;

        public static IPlatformUtility Default
        {
            get
            {
#if DEBUG
                if(_current == null)
                    throw new InvalidOperationException("PlatformUtility must be set by platform BEFORE calling PlatformUtility.Default!");
#endif
                return _current;
            }
            private set
            {
#if DEBUG
                if(_current != null)
                    throw new InvalidOperationException("PlatformUtility should not be initialised more than once!");
                if(value == null)
                    throw new ArgumentNullException(nameof(Default), "PlatformUtility.Default cannot be set to null!");
#endif
                _current = value;
            }
        }

        public static void Init(IPlatformUtility platform)
        {
            Default = platform;
        }
    }
}

