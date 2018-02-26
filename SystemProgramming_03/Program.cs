using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemProgramming_03
{
    class Program
    {
        static object locker = new object();
        static void Main()
        {
            //Вариант 1 для запуска потока
            //Thread saydate = new Thread(new ThreadStart(SayDateTime));
            //saydate.Start();

            //Вариант 2 для запуска потока
            //new Thread (SayDateTime).Start();

            //int n = 1000;
            //unsafe
            //{
            //    Console.WriteLine(new IntPtr(n));
            //}

            //List<Thread> threads = new List<Thread>()
            //{
            //    new Thread(()=>WriteInt(n)),
            //    new Thread(()=>WriteInt(n)),
            //    new Thread(()=>WriteInt(n)),
            //    new Thread(()=>WriteInt(n))
            //};
            //threads.ForEach(p => p.Start());
            double c = 0.0;
            double x = 0.0, y = 0.0;
            double n;
            double count;

            double pi = 0.0;
            string input;

            Console.WriteLine("Please input a number of dots for Monte Carlo to calculate pi.");
            input = Console.ReadLine();
            n = double.Parse(input);

            Random rand = new Random();


            for (int i = 1; i < n; i++)
            {
                new Thread(() => GetX(ref x)).Start();
                //x = rand.NextDouble
                //    () * 2 - 1;
                //y = rand.NextDouble() * 2 - 1;
                new Thread(() => GetY(ref y)).Start();

                if (((x * x) + (y * y) <= 1))
                {
                    c++;
                }
                pi = 4.0 * (c / i);
                Console.WriteLine("pi: {0,-10:0.00} Dots in square: {1,-15:0} Dots in circle: {2,-20:0}", pi, i, c);
            }
            Console.ReadLine();
        }
        static void GetX(ref double x)
        {

            Random rnd = new Random();
            x = rnd.NextDouble() * 2.0 - 1.0;
        }
        static void GetY(ref double y)
        {
            lock (locker)
            {
                Random rnd = new Random();
                y = rnd.NextDouble() * 2.0 - 1.0;

            }
        }
        static void WriteCharForever(char c)
        {
            while (true)
            {
                Console.WriteLine(c);
            }
        }
        static void WriteInt(int n)
        {
            unsafe
            {

                int* nPtr = &n;
                while (true)
                {
                    n++;
                    Console.WriteLine(n);
                    Console.WriteLine(new IntPtr(nPtr));
                    Thread.Sleep(1000);
                }

            }
        }
        static void SayDateTime()
        {
            Console.WriteLine(DateTime.Now);
        }

    }
}
