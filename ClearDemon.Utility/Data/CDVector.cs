//
// CDVector.cs
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
    /// A two-dimensional vector structure. Cross-platform 32-bit vector to use in place of System.Drawing.Point.
    /// </summary>
    public struct CDVector2 : IEquatable<CDVector2>
    {
        /// <summary>
        /// Empty vector structure.
        /// </summary>
        public static readonly CDVector2 Empty = new CDVector2();

        static CDVector2()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CDVector2"/> struct.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public CDVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The x-coordinate of the vector.
        /// </summary>
        public int X;
        /// <summary>
        /// The y-coordinate of the vector.
        /// </summary>
        public int Y;

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get
            {
                return X == 0 && Y == 0;
            }
        }

        /// <summary>
        /// Offsets the vector by another vector.
        /// </summary>
        /// <param name="other">Other vector that will be added to the instance.</param>
        public void Offset(CDVector2 other)
        {
            X += other.X;
            Y += other.Y;
        }

        #region IEquatable implementation

        public bool Equals(CDVector2 other)
        {
            return other.X == X && other.Y == Y;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if(obj is CDVector2)
            {
                var v = (CDVector2)obj;
                return v.X == X && v.Y == Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Analysis disable NonReadonlyReferencedInGetHashCode
            return X ^ Y;
            // Analysis restore NonReadonlyReferencedInGetHashCode
        }

        public override string ToString()
        {
            return string.Format("{{X={0},Y={1}}}", X, Y);
        }

        #region Operators

        public static bool operator ==(CDVector2 left, CDVector2 right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(CDVector2 left, CDVector2 right)
        {
            return !(left == right);
        }

        public static CDVector2 operator +(CDVector2 left, CDVector2 right)
        {
            return new CDVector2(left.X + right.X, left.Y + right.Y);
        }

        public static CDVector2 operator -(CDVector2 left, CDVector2 right)
        {
            return new CDVector2(left.X - right.X, left.Y - right.Y);
        }

        #endregion
    }

    /// <summary>
    /// A two-dimensional floating-point vector structure. Cross-platform 32-bit vector to use in place of System.Drawing.PointF.
    /// </summary>
    public struct CDVector2F : IEquatable<CDVector2F>
    {
        /// <summary>
        /// Empty vector structure.
        /// </summary>
        public static readonly CDVector2F Empty = new CDVector2F();

        static CDVector2F()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CDVector2F"/> struct.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public CDVector2F(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// The x-coordinate of the vector.
        /// </summary>
        public float X;
        /// <summary>
        /// The y-coordinate of the vector.
        /// </summary>
        public float Y;

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get { return Math.Abs(X) < float.Epsilon && Math.Abs(Y) < float.Epsilon; }
        }

        /// <summary>
        /// Offsets the vector by another vector.
        /// </summary>
        /// <param name="other">Other vector that will be added to the instance.</param>
        public void Offset(CDVector2F other)
        {
            X += other.X;
            Y += other.Y;
        }

        #region IEquatable implementation

        public bool Equals(CDVector2F other)
        {
            return Math.Abs(other.X - X) < float.Epsilon && Math.Abs(other.Y - Y) < float.Epsilon;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if(obj is CDVector2F)
            {
                var v = (CDVector2F)obj;
                return Math.Abs(v.X - X) < float.Epsilon && Math.Abs(v.Y - Y) < float.Epsilon;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Analysis disable NonReadonlyReferencedInGetHashCode
            return X.GetHashCode() ^ Y.GetHashCode();
            // Analysis restore NonReadonlyReferencedInGetHashCode
        }

        public override string ToString()
        {
            return string.Format("{{X={0},Y={1}}}", X, Y);
        }

        #region Operators

        public static bool operator ==(CDVector2F left, CDVector2F right)
        {
            return Math.Abs(left.X - right.X) < float.Epsilon && Math.Abs(left.Y - right.Y) < float.Epsilon;
        }

        public static bool operator !=(CDVector2F left, CDVector2F right)
        {
            return !(left == right);
        }

        public static CDVector2F operator +(CDVector2F left, CDVector2F right)
        {
            return new CDVector2F(left.X + right.X, left.Y + right.Y);
        }

        public static CDVector2F operator -(CDVector2F left, CDVector2F right)
        {
            return new CDVector2F(left.X - right.X, left.Y - right.Y);
        }

        #endregion

    }
}

