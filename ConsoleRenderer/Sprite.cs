using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleGameUtilities
{
    [Serializable]
    public class Sprite
    {
        public Pixel[][] _pixels;
        private Vector2 _size;
        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }

        internal Sprite()
        {

        }

        public Sprite(Vector2 size)
        {
            this.Size = size;
            _pixels = new Pixel[size.X][];
            for (int x = 0; x < size.X; x++)
            {
                _pixels[x] = new Pixel[size.Y];
                for (int y = 0; y < size.Y; y++)
                {
                    _pixels[x][y] = new Pixel() { Empty = true};
                }
            }
        }
        
        public void SetPixel(Vector2 position, Pixel pixel)
        {
            if (((position.X >= 0) && (position.Y >= 0)) && ((position.X < Size.X) && (position.Y < Size.Y)))
            {
                _pixels[position.X][position.Y] = pixel;
            }
            else throw new IndexOutOfRangeException();
        }

        public Pixel GetPixel(Vector2 position)
        {
            if (((position.X >= 0) && (position.Y >= 0)) && ((position.X < Size.X) && (position.Y < Size.Y)))
            {
                return _pixels[position.X][position.Y];
            }
            else throw new IndexOutOfRangeException();
        }

        public static Sprite Load(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Sprite));
            StreamReader reader = new StreamReader(file);
            Sprite sprite = (Sprite)serializer.Deserialize(reader);
            reader.Close();
            return sprite;
        }
        public static bool TryLoad(string file)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Sprite));
                StreamReader reader = new StreamReader(file);
                Sprite sprite = (Sprite)serializer.Deserialize(reader);
                reader.Close();
                if (sprite != null) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static void Save(string file, Sprite sprite)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Sprite));
            StreamWriter writer = new StreamWriter(file);
            serializer.Serialize(writer, sprite);
            writer.Close();
        }

        public static bool TrySave(string file, Sprite sprite)
        {
            bool ex = File.Exists(file);
            string tmppath = Path.GetTempFileName();
            if (ex)
            {
                File.Delete(tmppath);
                File.Move(file, tmppath);
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Sprite));
                StreamWriter writer = new StreamWriter(file);
                serializer.Serialize(writer, sprite);
                writer.Close();
                File.Delete(file);
                File.Move(tmppath, file);
                return true;
            }
            catch
            {
                try {
                    File.Move(tmppath, file); }
                catch { }
                return false;
            }
        }
    }
}
