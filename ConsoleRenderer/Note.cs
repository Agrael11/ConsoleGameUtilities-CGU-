using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameUtilities
{
    public class Note
    {
        public Note(Notes note, uint Duration, uint Octave)
        {
            NoteType = note;
            this.Duration = Duration;
            this.Octave = Octave;
        }

        public Note(string note, uint Duration, uint Octave)
        {
            note = note.ToUpper();
            switch (note)
            {
                case "C": NoteType = Notes.C;break;
                case "C#":
                case "CS": NoteType = Notes.CS;break;

                case "D": NoteType = Notes.D; break;
                case "D#":
                case "DS": NoteType = Notes.DS; break;

                case "E": NoteType = Notes.E; break;

                case "F": NoteType = Notes.F; break;
                case "F#":
                case "FS": NoteType = Notes.FS; break;

                case "G": NoteType = Notes.G; break;
                case "GS":
                case "G#": NoteType = Notes.GS; break;

                case "A": NoteType = Notes.A; break;
                case "AS":
                case "A#": NoteType = Notes.AS; break;

                case "B":
                case "H": NoteType = Notes.B; break;

                case "PAUSE":
                case "-":
                case " ": NoteType = Notes.PAUSE; break;
            }
            this.Duration = Duration;
            this.Octave = Octave;
        }

        public Note(int duration)
        {
            NoteType = Notes.PAUSE;
            Duration = 1000;
        }

        public enum Notes { C, CS, D, DS, E, F, FS, G, GS, A, AS, B, PAUSE}

        public Notes NoteType;
        public uint Duration;
        public uint Octave = 1;
    }
}
