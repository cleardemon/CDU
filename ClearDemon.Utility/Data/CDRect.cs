//
// CDRect.cs
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
    /// Declares a cross-platform rectangle structure. 32-bit. In place of System.Drawing.Rectangle.
    /// </summary>
    public struct CDRect : IEquatable<CDRect>
    {
        /// <summary>
        /// Empty rectangle instance.
        /// </summary>
        public static readonly CDRect Empty = new CDRect();

        static CDRect()
        {
        }

        /// <summary>
        /// The x-coordinate in the rectangle (top-left origin).
        /// </summary>
        public int X;
        /// <summary>
        /// The y-coordinate in the rectangle (top-left origin).
        /// </summary>
        public int Y;
        /// <summary>
        /// The width of the rectangle.
        /// </summary>
        public int Width;
        /// <summary>
        /// The height of the rectangle.
        /// </summary>
        public int Height;

        /// <summary>
        /// Initializes a new instance of the <see cref="CDRect"/> struct.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        public CDRect(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        /// <summary>
        /// Constructs a Rect from left/top/right/bottom coordinates.
        /// </summary>
        /// <returns>Rectangle.</returns>
        /// <param name="left">Left coordinate.</param>
        /// <param name="top">Top coordinate.</param>
        /// <param name="right">Right coordinate.</param>
        /// <param name="bottom">Bottom coordinate.</param>
        public static CDRect FromLTRB(int left, int top, int right, int bottom)
        {
            return new CDRect(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CDRect"/> struct.
        /// </summary>
        /// <param name="location">Location vector (X,Y).</param>
        /// <param name="size">Size vector. (Width,Height)</param>
        public CDRect(CDVector2 location, CDVector2 size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }

        /// <summary>
        /// Gets or sets the location of the rectangle in a vector (X,Y).
        /// </summary>
        public CDVector2 Location
        {
            get { return new CDVector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Gets or sets the size of the rectangle in a vector (Width,Height).
        /// </summary>
        /// <value>The size.</value>
        public CDVector2 Size
        {
            get { return new CDVector2(Width, Height); }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// Gets the left corner (top-left origin).
        /// </summary>
        public int Left
        {
            get { return X; }
        }

        /// <summary>
        /// Gets the top corner (top-left origin).
        /// </summary>
        /// <value>The top.</value>
        public int Top
        {
            get { return Y; }
        }

        /// <summary>
        /// Gets the rightmost corner (top-left origin).
        /// </summary>
        public int Right
        {
            get { return X + Width; }
        }

        /// <summary>
        /// Gets the bottommost corner (top-left origin).
        /// </summary>
        public int Bottom
        {
            get { return Y + Height; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return X == 0 && Y == 0 && Width == 0 && Height == 0; }
        }

        #region IEquatable implementation

        public bool Equals(CDRect other)
        {
            return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if(obj is CDRect)
            {
                var r = (CDRect)obj;
                return X == r.X && Y == r.Y && Width == r.Width && Height == r.Height;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Analysis disable NonReadonlyReferencedInGetHashCode
            return X.GetHashCode() ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
            // Analysis restore NonReadonlyReferencedInGetHashCode
        }

        #region Operators

        public static bool operator ==(CDRect left, CDRect right)
        {
            return left.X == right.X && left.Y == right.Y && left.Width == right.Width && left.Height == right.Height;
        }

        public static bool operator !=(CDRect left, CDRect right)
        {
            return !(left == right);
        }

        #endregion

        /// <summary>
        /// Returns true if the specified coordinate appears inside the rectangle.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public bool Contains(int x, int y)
        {
            return X <= x && x < X + Width && Y <= y && y < Y + Height;
        }

        /// <summary>
        /// Returns true if the specified vector coordinate appears inside the rectangle.
        /// </summary>
        /// <param name="xy">The x,y vector.</param>
        public bool Contains(CDVector2 xy)
        {
            return Contains(xy.X, xy.Y);
        }

        /// <summary>
        /// Returns true if the specified rectangle is inside this rectangle instance.
        /// </summary>
        /// <param name="r">The rectangle to test.</param>
        public bool Contains(CDRect r)
        {
            return X <= r.X && r.X + r.Width < X + Width && Y <= r.Y && r.Y + r.Height <= Y + Height;
        }

        /// <summary>
        /// Increases the size of the rectangle by the given width and height.
        /// </summary>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        public void Inflate(int w, int h)
        {
            X -= w;
            Y -= h;
            Width += 2 * w;
            Height += 2 * h;
        }

        /// <summary>
        /// Increases the size of the rectangle by the given vector (width,height).
        /// </summary>
        /// <param name="size">The w,h vector.</param>
        public void Inflate(CDVector2 size)
        {
            Inflate(size.X, size.Y);
        }


        /// <summary>
        /// Returns an intersected rectangle between two rectangles. Empty if not intersected.
        /// </summary>
        /// <param name="a">The first rectangle.</param>
        /// <param name="b">The second rectangle.</param>
        public static CDRect Intersect(CDRect a, CDRect b)
        {
            var x = Math.Max(a.X, b.X);
            var y = Math.Max(a.Y, b.Y);
            var w = Math.Min(a.X + a.Width, b.X + b.Width);
            var h = Math.Min(a.Y + a.Height, b.Y + b.Height);
            if(w >= x && h >= y)
                return new CDRect(x, y, w - x, h - y);
            return Empty;
        }

        /// <summary>
        /// Intersects the current rectangle with the specified rectangle.
        /// </summary>
        /// <param name="r">The rectangle to intersect with.</param>
        public void Intersect(CDRect r)
        {
            var intersected = Intersect(r, this);
            X = intersected.X;
            Y = intersected.Y;
            Width = intersected.Width;
            Height = intersected.Height;
        }

        /// <summary>
        /// Returns true if the specified rectangle intersects with the current rectangle.
        /// </summary>
        /// <param name="r">The rectangle to test collision with.</param>
        public bool IntersectsWith(CDRect r)
        {
            return r.X < X + Width && X < r.X + r.Width && r.Y < Y + Height && Y < r.Y + r.Height;
        }

        public override string ToString()
        {
            return string.Format("{{X={0},Y={1},W={2},H={3}}}", X, Y, Width, Height);
        }
    }

    /// <summary>
    /// Declares a cross-platform floated rectangle structure. 32-bit. In place of System.Drawing.RectangleF.
    /// </summary>
    public struct CDRectF : IEquatable<CDRectF>
    {
        /// <summary>
        /// Empty rectangle instance.
        /// </summary>
        public static readonly CDRectF Empty = new CDRectF();

        static CDRectF()
        {
        }

        /// <summary>
        /// The x-coordinate in the rectangle (top-left origin).
        /// </summary>
        public float X;
        /// <summary>
        /// The y-coordinate in the rectangle (top-left origin).
        /// </summary>
        public float Y;
        /// <summary>
        /// The width of the rectangle.
        /// </summary>
        public float Width;
        /// <summary>
        /// The height of the rectangle.
        /// </summary>
        public float Height;

        /// <summary>
        /// Initializes a new instance of the <see cref="CDRectF"/> struct.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        public CDRectF(float x, float y, float w, float h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        /// <summary>
        /// Constructs a Rect from left/top/right/bottom coordinates.
        /// </summary>
        /// <returns>Rectangle.</returns>
        /// <param name="left">Left coordinate.</param>
        /// <param name="top">Top coordinate.</param>
        /// <param name="right">Right coordinate.</param>
        /// <param name="bottom">Bottom coordinate.</param>
        public static CDRectF FromLTRB(float left, float top, float right, float bottom)
        {
            return new CDRectF(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CDRectF"/> struct.
        /// </summary>
        /// <param name="location">Location vector (X,Y).</param>
        /// <param name="size">Size vector. (Width,Height)</param>
        public CDRectF(CDVector2F location, CDVector2F size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }

        /// <summary>
        /// Gets or sets the location of the rectangle in a vector (X,Y).
        /// </summary>
        public CDVector2F Location
        {
            get { return new CDVector2F(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Gets or sets the size of the rectangle in a vector (Width,Height).
        /// </summary>
        /// <value>The size.</value>
        public CDVector2F Size
        {
            get { return new CDVector2F(Width, Height); }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// Gets the left corner (top-left origin).
        /// </summary>
        public float Left
        {
            get { return X; }
        }

        /// <summary>
        /// Gets the top corner (top-left origin).
        /// </summary>
        /// <value>The top.</value>
        public float Top
        {
            get { return Y; }
        }

        /// <summary>
        /// Gets the rightmost corner (top-left origin).
        /// </summary>
        public float Right
        {
            get { return X + Width; }
        }

        /// <summary>
        /// Gets the bottommost corner (top-left origin).
        /// </summary>
        public float Bottom
        {
            get { return Y + Height; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return Math.Abs(X) < float.Epsilon && Math.Abs(Y) < float.Epsilon && Math.Abs(Width) < float.Epsilon && Math.Abs(Height) < float.Epsilon; }
        }

        #region IEquatable implementation

        public bool Equals(CDRectF other)
        {
            return Math.Abs(X - other.X) < float.Epsilon && Math.Abs(Y - other.Y) < float.Epsilon && Math.Abs(Width - other.Width) < float.Epsilon && Math.Abs(Height - other.Height) < float.Epsilon;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if(obj is CDRect)
            {
                var r = (CDRect)obj;
                return Math.Abs(X - r.X) < float.Epsilon && Math.Abs(Y - r.Y) < float.Epsilon && Math.Abs(Width - r.Width) < float.Epsilon && Math.Abs(Height - r.Height) < float.Epsilon;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Analysis disable NonReadonlyReferencedInGetHashCode
            return X.GetHashCode() ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
            // Analysis restore NonReadonlyReferencedInGetHashCode
        }

        #region Operators

        public static bool operator ==(CDRectF left, CDRectF right)
        {
            return Math.Abs(left.X - right.X) < float.Epsilon && Math.Abs(left.Y - right.Y) < float.Epsilon && Math.Abs(left.Width - right.Width) < float.Epsilon && Math.Abs(left.Height - right.Height) < float.Epsilon;
        }

        public static bool operator !=(CDRectF left, CDRectF right)
        {
            return !(left == right);
        }

        #endregion

        /// <summary>
        /// Returns true if the specified coordinate appears inside the rectangle.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public bool Contains(float x, float y)
        {
            return X <= x && x < X + Width && Y <= y && y < Y + Height;
        }

        /// <summary>
        /// Returns true if the specified vector coordinate appears inside the rectangle.
        /// </summary>
        /// <param name="xy">The x,y vector.</param>
        public bool Contains(CDVector2 xy)
        {
            return Contains(xy.X, xy.Y);
        }

        /// <summary>
        /// Returns true if the specified rectangle is inside this rectangle instance.
        /// </summary>
        /// <param name="r">The rectangle to test.</param>
        public bool Contains(CDRectF r)
        {
            return X <= r.X && r.X + r.Width < X + Width && Y <= r.Y && r.Y + r.Height <= Y + Height;
        }

        /// <summary>
        /// Increases the size of the rectangle by the given width and height.
        /// </summary>
        /// <param name="w">The width.</param>
        /// <param name="h">The height.</param>
        public void Inflate(float w, float h)
        {
            X -= w;
            Y -= h;
            Width += 2 * w;
            Height += 2 * h;
        }

        /// <summary>
        /// Increases the size of the rectangle by the given vector (width,height).
        /// </summary>
        /// <param name="size">The w,h vector.</param>
        public void Inflate(CDVector2 size)
        {
            Inflate(size.X, size.Y);
        }


        /// <summary>
        /// Returns an intersected rectangle between two rectangles. Empty if not intersected.
        /// </summary>
        /// <param name="a">The first rectangle.</param>
        /// <param name="b">The second rectangle.</param>
        public static CDRectF Intersect(CDRectF a, CDRectF b)
        {
            var x = Math.Max(a.X, b.X);
            var y = Math.Max(a.Y, b.Y);
            var w = Math.Min(a.X + a.Width, b.X + b.Width);
            var h = Math.Min(a.Y + a.Height, b.Y + b.Height);
            if(w >= x && h >= y)
                return new CDRectF(x, y, w - x, h - y);
            return Empty;
        }

        /// <summary>
        /// Intersects the current rectangle with the specified rectangle.
        /// </summary>
        /// <param name="r">The rectangle to intersect with.</param>
        public void Intersect(CDRectF r)
        {
            var intersected = Intersect(r, this);
            X = intersected.X;
            Y = intersected.Y;
            Width = intersected.Width;
            Height = intersected.Height;
        }

        /// <summary>
        /// Returns true if the specified rectangle intersects with the current rectangle.
        /// </summary>
        /// <param name="r">The rectangle to test collision with.</param>
        public bool IntersectsWith(CDRectF r)
        {
            return r.X < X + Width && X < r.X + r.Width && r.Y < Y + Height && Y < r.Y + r.Height;
        }

        public override string ToString()
        {
            return string.Format("{{X={0},Y={1},W={2},H={3}}}", X, Y, Width, Height);
        }
    }
}

