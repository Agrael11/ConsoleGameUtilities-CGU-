using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameUtilities
{
    public class ConsoleKeyboard
    {
        public static bool KeyAvailable = false;

        private static ConsoleKey _key;
        private static ConsoleKeyInfo _keyInfo;
        public static ConsoleKey PressedKey
        {
            get { KeyAvailable = false; return _key; }
            set { _key = value; }
        }
        public static ConsoleKeyInfo PressedKeyFull
        {
            get { KeyAvailable = false; return _keyInfo; }
            set { _keyInfo = value; _key = _keyInfo.Key; }
        }

        public static void CheckNow()
        {
            if (Console.KeyAvailable)
            {
                KeyAvailable = true;
                PressedKey = Console.ReadKey(true).Key;
                PressedKeyFull = Console.ReadKey(true);
            }
        }

        public static void CheckKey()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    KeyAvailable = true;
                    //PressedKeyFull = Console.ReadKey(true);
                    PressedKeyFull = Console.ReadKey(true);
                    //PressedKey = Console.ReadKey(true).Key;
                }
            }
        }
    }
}
