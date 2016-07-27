//
// CDColor.cs
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

namespace ClearDemon.Utility.Data
{
    /// <summary>
    /// Cross-platform colour representation. Supports RGBA.
    /// </summary>
    /// <remarks>Named CDColor to help against namespace clashes.</remarks>
    public struct CDColor
    {
        /// <summary>
        /// Defines a new colour.
        /// </summary>
        /// <param name="r">The red component, between 0 and 255.</param>
        /// <param name="g">The green component, between 0 and 255.</param>
        /// <param name="b">The blue component, between 0 and 255.</param>
        /// <param name="a">The alpha component, between 0 and 255.</param>
        public CDColor(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Defines a new colour without an alpha channel.
        /// </summary>
        /// <param name="r">The red component, between 0 and 255.</param>
        /// <param name="g">The green component, between 0 and 255.</param>
        /// <param name="b">The blue component, between 0 and 255.</param>
        public CDColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = 255; // 100% opaque
        }

        /// <summary>
        /// Defines a new colour based on a previous colour and different alpha channel.
        /// </summary>
        /// <param name="a">The alpha component.</param>
        /// <param name="baseColor">Base colour.</param>
        public CDColor(byte a, CDColor baseColor)
        {
            R = baseColor.R;
            G = baseColor.G;
            B = baseColor.B;
            A = a;
        }

        //
        // RGBA properties
        // These values are based around going between 0 and 255 inclusive.
        // Other platforms may work differently and should provide extension methods to translate accordingly.
        //

        /// <summary>
        /// Gets or sets the red component of the colour.
        /// </summary>
        public byte R
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the green component of the colour.
        /// </summary>
        public byte G
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the blue component of the colour.
        /// </summary>
        public byte B
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the alpha component of the colour.
        /// </summary>
        public byte A
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return R == 0 && G == 0 && B == 0 && A == 0; }
        }

        /// <summary>
        /// Computes a new colour from the current colour by use of colour correction. colour correction to the colour.
        /// </summary>
        /// <returns>New colour.</returns>
        /// <param name="factor">Correction factor. 1.5 = lighter by 50%, 0.5 = darker by 50%, 1 = same.</param>
        public CDColor ApplyCorrection(float factor)
        {
            return new CDColor(
                (byte)Math.Max(0, Math.Min(255f, R * factor)),
                (byte)Math.Max(0, Math.Min(255f, G * factor)),
                (byte)Math.Max(0, Math.Min(255f, B * factor)),
                A
            );
        }

        /// <summary>
        /// Converts the colour to an ARGB integer.
        /// </summary>
        /// <returns>Int with ARGB format.</returns>
        public int ToARGB()
        {
            return (A << 24) + (R << 16) + (G << 8) + B;
        }

        /// <summary>
        /// Converts the colour to a RGBA integer.
        /// </summary>
        /// <returns>Int with RGBA format.</returns>
        public int ToRGBA()
        {
            return (R << 24) + (G << 16) + (B << 8) + A;
        }

        /// <summary>
        /// Converts the colour to a BGRA integer.
        /// </summary>
        /// <returns>Int with BRGA format.</returns>
        public int ToBGRA()
        {
            return (B << 24) + (G << 16) + (R << 8) + A;
        }

        //
        // Static colours
        //

        public static CDColor Black
        {
            get { return new CDColor(0, 0, 0); }
        }

        public static CDColor White
        {
            get { return new CDColor(255, 255, 255); }
        }

        public static CDColor Transparent
        {
            get { return new CDColor(255, 255, 255, 0); }
        }

    }
}

