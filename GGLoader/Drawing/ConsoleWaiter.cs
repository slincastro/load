using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GGLoader.Drawing
{
    class ConsoleWaiter : IDisposable
    {
        private const string Sequence = @"/-\|";
        List<string> _animation = new List<string> { "<(o_o)>", "^(O.o)>", "^(o.o)^", "v(o.O)^", "^(O.O)v", "<(o.o)^" };
        int _counter;
        private readonly int left;
        private readonly int top;
        private readonly int delay;
        private bool active;
        private readonly Thread thread;

        public ConsoleWaiter(int left, int top, int delay)
        {

            this.left = left;
            this.top = top;
            this.delay = delay;
            thread = new Thread(Spin);
        }

        public ConsoleWaiter(int left, int top, int delay, List<string> animation)
        {

            this.left = left;
            this.top = top;
            this.delay = delay;
            this._animation = animation;
            thread = new Thread(Spin);
        }

        public void Start()
        {
            active = true;
            if (!thread.IsAlive)
                thread.Start();
        }

        public void Stop()
        {
            active = false;
            Draw("                                      ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void Spin()
        {
            _counter = 0;
            while (active)
            {
                Turn();
                Thread.Sleep(delay);
            }
        }

        private void Draw(string c)
        {
            Console.SetCursorPosition(left, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(c);
        }

        private void Turn()
        {

            _counter++;
            Draw(_animation.ToArray()[_counter]);
            _counter = (_counter == _animation.Count() - 1) ? 0 : _counter;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
