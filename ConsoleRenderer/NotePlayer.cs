using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleGameUtilities
{
    public class NotePlayer
    {
        public static bool Stop = false;
        public static bool Stopped = true;
        private static bool Stopped2 = true;
        public static double[][] Freqs;
        public static bool Paused = false;

        public static void Init()
        {
            XmlSerializer ser = new XmlSerializer(typeof(double[][]));
            StreamReader reader = new StreamReader("Notes.xml");
            Freqs = (double[][])ser.Deserialize(reader);
            reader.Close();
        }

        public static void PlayNote(Note note)
        {
            if (note.NoteType == Note.Notes.PAUSE)
            {
                Thread.Sleep((int)note.Duration);
            }
            else
            {
                double freq = Freqs[note.Octave][(int)note.NoteType];
                if (freq < 37) freq = 37;
                if (freq > 32767) freq = 32767;
                Console.Beep((int)freq, (int)note.Duration);
            }
        }

        private static Thread tuneThread;
        private static Thread tuneThread2;

        public static void PlayTune(Tune tune)
        {
            Stop = true;
            while (!Stopped) ;
            Stop = false;
            Stopped = false;

            tuneThread = new Thread(new ParameterizedThreadStart(TunePlay));
            tuneThread.Start(tune);
        }
        public static void PlayTuneSec(Tune tune)
        {
            while (!Stopped2) ;

            Paused = true;

            tuneThread2 = new Thread(new ParameterizedThreadStart(TunePlay2));
            tuneThread2.Start(tune);
        }


        private static void TunePlay(object tuneObj)
        {
            Tune tune = (Tune)tuneObj;
            if (tune.Loop)
                while (!Stop)
                {
                    foreach (Note note in tune.notes)
                    {
                        while (Paused) ;
                        PlayNote(note);
                        if (Stop) break;
                    }
                }
            else
            {
                foreach (Note note in tune.notes)
                {
                    while (Paused) ;
                    PlayNote(note);
                    if (Stop) break;
                }
            }
            Stopped = true;
        }
        private static void TunePlay2(object tuneObj)
        {
            Tune tune = (Tune)tuneObj;
            foreach (Note note in tune.notes)
            {
                PlayNote(note);
                if (Stop) break;
            }
            Paused = false;
            Stopped2 = true;
        }
    }
}
