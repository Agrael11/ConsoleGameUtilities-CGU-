using System;

namespace ConsoleGameUtilities
{
    [Serializable]
    public class Rectangle
    {
        private readonly string _hashId = "Rectangle" + new Random().Next();
        public int Height;
        public int Width;
        public int X;
        public int Y;

        internal Rectangle()
        {
        }

        public Rectangle(int x, int y, int width, int height)
        {
            SetupHelper(x, y, width, height);
        }

        public Rectangle(Vector2 position, int width, int height)
        {
            SetupHelper(position.X, position.Y, width, height);
        }

        public Rectangle(int x, int y, Vector2 size)
        {
            SetupHelper(x, y, size.X, size.Y);
        }

        public Rectangle(Vector2 position, Vector2 size)
        {
            SetupHelper(position.X, position.Y, size.X, size.Y);
        }

        private void SetupHelper(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool Intersects(Rectangle rectangle)
        {
            if (X >= rectangle.X)
            {
                if (X >= rectangle.X + rectangle.Width) return false;
            }
            else if (X <= rectangle.X)
            {
                if (X + Width <= rectangle.X) return false;
            }

            if (Y >= rectangle.Y)
            {
                if (Y >= rectangle.Y + rectangle.Height) return false;
            }
            else if (Y <= rectangle.Y)
            {
                if (Y + Height <= rectangle.Y) return false;
            }

            return true;
        }

        public static bool Intersects(Rectangle r1, Rectangle r2)
        {
            return r1.Intersects(r2);
        }

        #region Náhrada operátorov

        public static implicit operator Rectangle(Vector2 value)
        {
            return new Rectangle(value, 1, 1);
        }

        public static Rectangle operator +(Rectangle v1, Rectangle v2)
        {
            return new Rectangle(v1.X + v2.X, v1.Y + v2.Y, v1.Width + v2.Width, v1.Height + v2.Height);
        }

        public static Rectangle operator -(Rectangle v1, Rectangle v2)
        {
            return new Rectangle(v1.X - v2.X, v1.Y - v2.Y, v1.Width - v1.Width, v1.Height - v2.Height);
        }

        public static Rectangle operator *(Rectangle v1, Rectangle v2)
        {
            return new Rectangle(v1.X*v2.X, v1.Y*v2.Y, v1.Width*v2.Width, v1.Height*v2.Height);
        }

        public static Rectangle operator /(Rectangle v1, Rectangle v2)
        {
            return new Rectangle(v1.X/v2.X, v1.Y/v2.Y, v1.Width/v2.Width, v1.Height/v2.Height);
        }

        public static bool operator ==(Rectangle v1, Rectangle v2)
        {
            return v2 != null && v1 != null && (v1.X == v2.X) && (v1.Y == v2.Y) && (v1.Width == v2.Width) &&
                   (v1.Height == v2.Height);
        }

        public static bool operator !=(Rectangle v1, Rectangle v2)
        {
            return !(v1 == v2);
        }

        public override bool Equals(object obj)
        {
            return this == (Rectangle) obj;
        }

        public override int GetHashCode()
        {
            var hash = 1001;
            hash = hash*7 + _hashId.GetHashCode();
            return hash;
        }

        #endregion
    }
}