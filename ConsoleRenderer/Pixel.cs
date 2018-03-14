using System;

namespace ConsoleGameUtilities
{
    [Serializable]
    public class Pixel
    {
        private readonly string _hashId;
        public ConsoleColor Color = ConsoleColor.Black;
        public bool Changed = true;
        public bool Different = false;
        public char Character = ' ';
        public ConsoleColor CharColor = ConsoleColor.Gray;
        public bool Empty = false;
        public bool Force = false;

        public Pixel()
        {
            _hashId = "PixeL_" + new Random().Next();
        }

        public static bool operator ==(Pixel p1, Pixel p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Pixel p1, Pixel p2)
        {
            return !p1.Equals(p2);
        }

        public override bool Equals(object obj)
        {
            var p2 = (Pixel) obj;
            return (p2.Character == Character) && (p2.CharColor == CharColor) && (p2.Color == Color) && (!Force || !p2.Force);
        }

        public override int GetHashCode()
        {
            var hash = 1001;
            hash = hash*7 + _hashId.GetHashCode();
            return hash;
        }
    }
}