using System;
using System.Threading;

namespace ConsoleGameUtilities
{
    public abstract class Game
    {
        public bool Running = true;

        private Thread keyThread;
        private Thread render;

        public Vector2 Resolution = new Vector2(54,25);

        public Game()
        {
            Init();
            keyThread.Start();
            render.Start();
            while (Running)
            {
                Thread.Sleep(33);
                Update();
                Draw();
            }
            NotePlayer.Stop = true;
            keyThread.Abort();
            render.Abort();
        }

        public void InitializeGame()
        {
            Console.Clear();
            Renderer.Init();
            Renderer.WindowSize = Resolution;
            NotePlayer.Init();
            keyThread = new Thread(ConsoleKeyboard.CheckKey);
            render = new Thread(RenderDraw);
        }

        private void RenderDraw()
        {
            while (Running)
            {
                Renderer.Draw();
            }
        }

        public abstract void Init();
        public abstract void Update();
        public abstract void Draw();
    }
}
