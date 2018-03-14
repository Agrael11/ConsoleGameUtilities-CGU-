using System;

namespace ConsoleGameUtilities
{
    public static class Renderer
    {
        private static Pixel[,] _display = new Pixel[80, 30];
        private static Pixel[,] _displayF = new Pixel[80, 30];
        private static bool Flushing = false;
        private static Vector2 _size = new Vector2(80, 30);
        private static ConsoleColor _backgroundColor = ConsoleColor.Black;
        public static bool StopDrawing = false;
        public static bool DrawEnded = false;
        private static bool _showCursor = false;
        public static bool ShowCursor
        {
            set
            {
                _showCursor = value;
                Console.CursorVisible = _showCursor;
            }
            get { return _showCursor; }
        }

        public static Vector2 WindowSize
        {
            get { return _size; }
            set
            {
                var displayNew = new Pixel[value.X, value.Y];
                for (var x = 0; x < value.X; x++)
                {
                    for (var y = 0; y < value.Y; y++)
                    {
                        if ((x >= _size.X) || (y >= _size.Y))
                        {
                            displayNew[x, y] = new Pixel {Color = _backgroundColor};
                        }
                        else
                        {
                            displayNew[x, y] = _display[x, y];
                        }
                    }
                }
                _display = displayNew;
                _displayF = (Pixel[,])displayNew.Clone();
                _size = value;
            }
        }

        public static void DrawBegin()
        {
            while (Flushing) ;
        }

        public static void DrawEnd()
        {
            Flushing = true;
            for (int x = 0; x < _size.X; x++)
            {
                for (int y = 0; y < _size.Y; y++)
                {
                    bool changed = (_displayF[x, y] != _display[x, y]);
                    if (changed)
                    {
                        _displayF[x, y] = _display[x, y];
                        _displayF[x, y].Changed = changed;
                    }
                }
            }
            Flushing = false;
        }


        public static void Init()
        {
            Console.SetWindowSize(_size.X, _size.Y);
            Console.CursorVisible = ShowCursor;
            for (var x = 0; x < _size.X; x++)
            {
                for (var y = 0; y < _size.Y; y++)
                {
                    _display[x, y] = new Pixel {Color = _backgroundColor, Changed = false};
                }
            }
        }


        public static void Clean()
        {
            for (var x = 0; x < _size.X; x++)
            {
                for (var y = 0; y < _size.Y; y++)
                {
                    var p = new Pixel { Color = _backgroundColor };
                    p.Changed = p != _display[x, y] || _display[x, y].Changed;
                    _display[x, y] = p;
                }
            }
        }

        public static void Clean(bool full)
        {
            if (full)
            {
                Console.Clear();
                for (var x = 0; x < _size.X; x++)
                {
                    for (var y = 0; y < _size.Y; y++)
                    {
                        _display[x, y] = new Pixel { Color = _backgroundColor, Changed = true, Force = true };
                    }
                }
            }
            else Clean();
        }

        public static void Clean(ConsoleColor background)
        {
            _backgroundColor = background;
            Clean();
        }
        public static void Clean(ConsoleColor background, bool full)
        {
            _backgroundColor = background;
            Clean(full);
        }


        public static void DrawPoint(Vector2 point, ConsoleColor color)
        {
            if ((point.X >= 0) && (point.Y >= 0) && (point.X < _size.X) && (point.Y < _size.Y))
            {
                var p = new Pixel {Color = color};
                _display[point.X, point.Y] = p;
            }
        }

        public static void DrawSprite(Vector2 position, Sprite sprite)
        {
            for (var x = 0; x < sprite.Size.X; x++)
            {
                for (var y = 0; y < sprite.Size.Y; y++)
                {
                    int disX = x + position.X;
                    int disY = y + position.Y;
                    if ((disX < _size.X) && (disX >= 0) && (disY < _size.Y) && (disY >= 0))
                    {
                        if (!sprite.GetPixel(new Vector2(x, y)).Empty)
                        {
                            Pixel p = sprite.GetPixel(new Vector2(x, y));
                            _display[disX, disY] = new Pixel() { Changed = p.Changed, Character = p.Character, CharColor = p.CharColor, Color = p.Color, Empty = p.Empty };
                        }
                                
                    }
                }
            }
        }

        public static void DrawRectangle(Rectangle rectangle, ConsoleColor color)
        {
            for (var x = rectangle.X; x < rectangle.X + rectangle.Width; x++)
            {
                for (var y = rectangle.Y; y < rectangle.Y + rectangle.Height; y++)
                {
                    if ((x < _size.X) && (x >= 0) && (y < _size.Y) && (y >= 0))
                    {
                        var p = new Pixel {Color = color};
                        _display[x, y] = p;
                    }
                }
            }
        }

        public static void DrawWindow(Rectangle rectangle, string Title, ConsoleColor frame, ConsoleColor background, ConsoleColor fontColor)
        {
            DrawRectangle(rectangle, frame);
            int frmWdth = rectangle.Width / 2;
            int ttlWdth = Title.Length / 2;
            DrawString("╔".PadRight(rectangle.Width-1, '═') + "╗", new Vector2(rectangle.X, rectangle.Y), fontColor);
            for (int y = 0; y < rectangle.Height-2; y++)
            {
                DrawString("║".PadRight(rectangle.Width - 1, ' ') + "║", new Vector2(rectangle.X, rectangle.Y + y+1), fontColor);
            }
            DrawString("╚".PadRight(rectangle.Width - 1, '═') + "╝", new Vector2(rectangle.X, rectangle.Y + rectangle.Height - 1), fontColor);
            DrawString(Title, new Vector2(rectangle.X + frmWdth - ttlWdth, rectangle.Y), fontColor);
            DrawRectangle(new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height - 2), background);
        }
        public static void DrawButton(Rectangle rectangle, string Text, ConsoleColor background, ConsoleColor fontColor, bool border = true)
        {
            DrawRectangle(rectangle, background);
            string[] Lines = Text.Split('\n');

            int frmWdth = rectangle.Width / 2;
            int frmHght = rectangle.Height / 2;
            int ttlHght = Lines.Length / 2;
            if (border)
            {
                DrawString("╔".PadRight(rectangle.Width - 1, '═') + "╗", new Vector2(rectangle.X, rectangle.Y), fontColor);
                for (int y = 0; y < rectangle.Height - 2; y++)
                {
                    DrawString("║".PadRight(rectangle.Width - 1, ' ') + "║", new Vector2(rectangle.X, rectangle.Y + y + 1), fontColor);
                }
                DrawString("╚".PadRight(rectangle.Width - 1, '═') + "╝", new Vector2(rectangle.X, rectangle.Y + rectangle.Height - 1), fontColor);
            }
            for (int i = 0; i < Lines.Length; i++)
            {
                int ttlWdth = Lines[i].Length / 2;
                DrawString(Lines[i], new Vector2(rectangle.X + frmWdth - ttlWdth, rectangle.Y + frmHght - ttlHght + i), fontColor);
            }
        }


        public static void DrawString(string str, Vector2 point, ConsoleColor fontColor, bool Force = false)
        {
            for (var i = 0; i < str.Length; i++)
            {
                if ((point.X + i >= 0) && (point.Y >= 0) && (point.X + i < _size.X) && (point.Y < _size.Y))
                {
                    _display[point.X + i, point.Y].Character = str[i];
                    _display[point.X + i, point.Y].CharColor = fontColor;
                    _display[point.X + i, point.Y].Changed = true;
                    _display[point.X + 1, point.Y].Force = Force;
                }
            }
        }

        public static void DrawString(string str, Vector2 point, ConsoleColor fontColor, ConsoleColor backColor)
        {
            for (var i = 0; i < str.Length; i++)
            {
                if ((point.X + i >= 0) && (point.Y >= 0) && (point.X + i < _size.X) && (point.Y < _size.Y))
                {
                    _display[point.X + i, point.Y].Character = str[i];
                    _display[point.X + i, point.Y].CharColor = fontColor;
                    _display[point.X + i, point.Y].Color = backColor;
                    _display[point.X + i, point.Y].Changed = true;
                }
            }
        }

        public static string GetVerInfo()
        {
            return "CGU.NET ver 1.0\nby Tachi23 (c)2016";
        }

        public static void Draw()
        {
            while (StopDrawing) ;
            while (Flushing) ;

            DrawEnded = false;

            Console.SetWindowSize(_size.X, _size.Y);
            Console.CursorTop = 0;
            Console.CursorLeft = 0;

            for (var x = 0; x < _size.X; x++)
            {
                for (var y = 0; y < _size.Y; y++)
                {
                    if (_displayF[x, y].Changed)
                    {
                        Console.BackgroundColor = _displayF[x, y].Color;
                        Console.ForegroundColor = _displayF[x, y].CharColor;
                        Console.CursorTop = y;
                        Console.CursorLeft = x;
                        Console.Write(_displayF[x, y].Character);
                        _displayF[x, y].Changed = false;
                    }
                }
            }

            Console.CursorTop = 0;
            Console.CursorLeft = 0;

            DrawEnded = true;
        }
    }
}