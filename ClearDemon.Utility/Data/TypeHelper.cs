//
// TypeHelper.cs
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
using System.Text.RegularExpressions;

namespace ClearDemon.Utility.Data
{
    /// <summary>
    /// Methods for ensuring that a passed in object is of a specific type, converting if necessary.
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// Converts the specified object to a string. Guaranteed to always return a string.
        /// </summary>
        /// <returns>String from the conversion.</returns>
        /// <param name="obj">Object to convert.</param>
        /// <param name="clean">If set to <c>true</c>, removes unprintable characters from the 
        /// resulting string. These are defined as characters below ASCII 32, but excluding tab 
        /// and linefeeds.</param>
        /// <param name="cleanXml">If set to <c>true</c>, removes XML tags from the string. Note that
        /// this also implies <paramref name="clean"/>.</param>
        public static string AsString(object obj, bool clean = false, bool cleanXml = false)
        {
            const string Unprintables = "[\u0000-\u0006]|[\u0008-\u0009]|[\u000B-\u000C]|[\u000E-\u001F]";
            const string NoXml = "<(.|\\n)*?>";

            string s;

            // check for special cases of type so they can be appropiately formatted as a string
            if(obj == null)
                s = string.Empty;
            else if(obj is int)
                s = ((int)obj).ToString("N0");
            else if(obj is long)
                s = ((long)obj).ToString("N0");
            else if(obj is double || obj is float)
                s = ((double)obj).ToString("N");
            else if(obj is decimal)
                s = ((decimal)obj).ToString("N");
            else if(obj is bool)
                s = ((bool)obj).ToString();
            else if(obj is Guid)
                s = ((Guid)obj).ToString();
            else if(obj is DateTime)
                s = ((DateTime)obj).ToString();
            // if not any of the above primitive types, plain old type conversion
            else
                s = Convert.ToString(obj).Trim();

            // perform cleaning?
            if(s.Length > 0)
            {
                // remove non-printables. implied if cleaning XML
                if(clean || cleanXml)
                    s = Regex.Replace(s, Unprintables, string.Empty);
                // remove XML tags from string
                if(cleanXml)
                    s = Regex.Replace(s, NoXml, string.Empty);
            }

            return s;
        }

        /// <summary>
        /// Converts the specified object to a guaranteed 'clean' string.
        /// </summary>
        /// <returns>A string, cleaned to remove characters or XML.</returns>
        /// <param name="s">Object to convert.</param>
        /// <param name="cleanXml">If set to <c>true</c>, as well as removing non-printable characters,
        /// will also remove XML tags.</param>
        /// <remarks>Convenience function to call <see cref="AsString"/> with <c>clean = true</c>.</remarks>
        public static string AsCleanString(object s, bool cleanXml = false)
        {
            return AsString(s, true, cleanXml);
        }

        /// <summary>
        /// Converts the specified object to a guaranteed boolean value.
        /// </summary>
        /// <returns>True or false, especially false if could not be converted.</returns>
        /// <param name="obj">Object to convert.</param>
        public static bool AsBool(object obj)
        {
            if(obj == null)
                return false;
            if(obj is string)
            {
                // special check to see if a string is something that could be
                // loosely interpreted as 'true'. this is more suited to use cases
                // where values are stored in configuration files, for example.
                var s = AsString(obj).ToLowerInvariant();
                return s == "1" || s == "true" || s == "on" || s == "yes";
            }
            // plain-old conversion
            return Convert.ToBoolean(obj);
        }

        /// <summary>
        /// Converts the specified object to a guaranteed double value.
        /// </summary>
        /// <returns>The double.</returns>
        /// <param name="obj">Object to convert.</param>
        public static double AsDouble(object obj)
        {
            if(obj == null)
                return 0.0d;
            if(obj is string)
            {
                var s = AsString(obj);
                double num;
                // try and convert the string to a double.
                // if failed or NaN, return zero
                if(s == string.Empty || !double.TryParse(s, out num) || double.IsNaN(num))
                    return 0.0d;
                return num;
            }
            return Convert.ToDouble(obj);
        }

        /// <summary>
        /// Converts the specified object to a guaranteed integer value (32-bit signed).
        /// </summary>
        /// <returns>The integer.</returns>
        /// <param name="obj">Object to convert.</param>
        public static int AsInt(object obj)
        {
            if(obj == null)
                return 0;
            if(obj is string)
            {
                var s = AsString(obj);
                int num;
                if(s == string.Empty || !int.TryParse(s, out num))
                    return 0;
                return num;
            }
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// Converts the specified object to a guaranteed unsigned integer value (32-bit unsigned).
        /// </summary>
        /// <returns>The unsigned integer.</returns>
        /// <param name="obj">Object to convert.</param>
        public static uint AsUInt(object obj)
        {
            if(obj == null)
                return 0u;
            if(obj is string)
            {
                var s = AsString(obj);
                uint num;
                if(s == string.Empty || !uint.TryParse(s, out num))
                    return 0u;
                return num;
            }
            return Convert.ToUInt32(obj);
        }

        /// <summary>
        /// Converts the specified object to a guaranteed long value (64-bit signed).
        /// </summary>
        /// <returns>The long value.</returns>
        /// <param name="obj">Object to convert.</param>
        public static long AsLong(object obj)
        {
            if(obj == null)
                return 0L;
            if(obj is string)
            {
                var s = AsString(obj);
                long num;
                if(s == string.Empty || !long.TryParse(s, out num))
                    return 0L;
                return num;
            }
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// Converts the specified object to a guaranteed unsigned long value (64-bit unsigned).
        /// </summary>
        /// <returns>The unsigned long value.</returns>
        /// <param name="obj">Object to convert.</param>
        public static ulong AsULong(object obj)
        {
            if(obj == null)
                return 0UL;
            if(obj is string)
            {
                var s = AsString(obj);
                ulong num;
                if(s == string.Empty || !ulong.TryParse(s, out num))
                    return 0UL;
                return num;
            }
            return Convert.ToUInt64(obj);
        }

        /// <summary>
        /// Converts the specified object to a guaranteed DateTime value.
        /// </summary>
        /// <returns>A <c>DateTime</c> value. If failed, will return <c>DateTime.MinValue</c>.</returns>
        /// <param name="obj">Object to convert.</param>
        /// <remarks>Note that if the passed in object is a long, it will be taken as ticks.</remarks>
        public static DateTime AsDateTime(object obj)
        {
            if(obj == null)
                return DateTime.MinValue;
            if(obj is DateTime)
                return (DateTime)obj;
            if(obj is string)
            {
                // attempt to parse the DateTime from the string using default values
                var s = AsString(obj);
                DateTime d;
                if(s == string.Empty || !DateTime.TryParse(s, out d))
                    return DateTime.MinValue;
            }
            // use DateTime ticks if a long value
            if(obj is long)
                return new DateTime((long)obj);
            // otherwise, no idea
            return DateTime.MinValue;
        }

        /// <summary>
        /// Converts the specified object to an enum, with a flag to indicate success on conversion.
        /// </summary>
        /// <returns>A value in the requested enumerable type.</returns>
        /// <param name="obj">Object to convert. Typically, this would be a string.</param>
        /// <param name="success">Out value set to <c>true</c> if successfully converted.</param>
        /// <typeparam name="T">Type of the enum to convert.</typeparam>
        /// <remarks>While this accepts a <c>struct</c> as a type (because all Enums are structs),
        /// undefined responses will occur if a type is not explicitly used as an Enum.</remarks>
        public static T AsEnum<T>(object obj, out bool success) where T : struct
        {
            success = true;
            if(obj == null)
                return default(T);
            if(obj is T)
                return (T)obj;
            T result;
            success = Enum.TryParse(AsString(obj), out result);
            return result;
        }

        /// <summary>
        /// Converts the specified object to an enum.
        /// </summary>
        /// <returns>A value in the requested enumerable type.</returns>
        /// <param name="obj">Object to convert. Typically, this would be a string.</param>
        /// <typeparam name="T">Type of the enum to convert.</typeparam>
        /// <remarks>Convenience method for <see cref="this.AsEnum(obj, success)"/>. Provides no way of checking
        /// if the conversion was successful, so the result may not be always correct.</remarks>
        public static T AsEnum<T>(object obj) where T : struct
        {
            bool dummy;
            return AsEnum<T>(obj, out dummy);
        }
    }
}

