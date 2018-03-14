using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameUtilities
{
    public class Tune
    {
        public List<Note> notes = new List<Note>();
        public bool Loop;

        public List<Note> generateFromText(string Text, int duration=100, int octaveMod=0)
        {
            string[] notes = Text.Split(' ');
            List<Note> generatedNotes = new List<Note>();
            foreach (string note in notes)
            {
                int dur;
                switch(note[0])
                {
                    case 'F': dur = duration; break;
                    case '8':dur = duration * 2; break;
                    case '4':dur = duration * 4; break;
                    case '2':dur = duration * 8; break;
                    case '1':dur = duration * 16; break;
                    default: dur = 0; break;
                }
                if (note[1] == '-')
                {
                    generatedNotes.Add(new Note(dur));
                }
                else
                {
                    int oct = int.Parse(note[1].ToString()) + octaveMod;
                    string not = note.Substring(2);
                    generatedNotes.Add(new Note(not, (uint)dur, (uint)oct));
                }
            }
            return generatedNotes;
        }
    }
}
