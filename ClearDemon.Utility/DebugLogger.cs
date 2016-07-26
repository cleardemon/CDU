//
// Logger.cs
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

namespace ClearDemon.Utility
{
    public static class DebugLogger
    {
        static void WriteMessage(string level, string msg)
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")}: [{level}] {msg}");
        }

        public static void Debug(string msg)
        {
            WriteMessage("D", msg);
        }

        public static void Info(string msg)
        {
            WriteMessage("I", msg);
        }

        public static void Warn(string msg)
        {
            WriteMessage("W", msg);
        }

        public static void Crit(string msg)
        {
            WriteMessage("C", msg);
        }
    }
}

