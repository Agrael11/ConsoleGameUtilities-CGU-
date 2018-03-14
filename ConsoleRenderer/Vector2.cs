using System;

namespace ConsoleGameUtilities
{
    [Serializable]
    public class Vector2
    {
        private readonly string _hashId = "Vector2" + new Random().Next();
        public int X;
        public int Y;

        internal Vector2()
        {
        }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        #region Náhrada operátorov

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X*v2.X, v1.Y*v2.Y);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X/v2.X, v1.Y/v2.Y);
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return (v1.X == v2.X) && (v1.Y == v2.Y);
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return (v1.X != v2.X) || (v1.Y != v2.Y);
        }

        public override bool Equals(object obj)
        {
            return this == (Vector2) obj;
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