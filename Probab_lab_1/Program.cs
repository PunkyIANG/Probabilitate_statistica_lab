using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Probab_lab_1
{
    class Program
    {
        private static int totalCalculations = 1000000;
        private static int totalHits = 0;
        private static ThreadLocal<Random> _tlRng;
        
        static void Main(string[] args)
        {
            Console.Write("Dati numarul de calcule (n): ");
            totalCalculations = int.Parse(Console.ReadLine());

            _tlRng = new ThreadLocal<Random>(() => new Random());
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.For(0, totalCalculations, i =>
            {
                CalcPosition();
            });

            stopwatch.Stop();
            
            Console.WriteLine("P = " + (double)totalHits/totalCalculations);
            Console.WriteLine("Calculated in " + stopwatch.Elapsed);
        }

        private static void CalcPosition()
        {
            double firstAngle = _tlRng.Value.NextDouble() * 2 * Math.PI;
            double secondAngle = _tlRng.Value.NextDouble() * 2 * Math.PI;

            if ((firstAngle <= Math.PI / 2 && secondAngle <= Math.PI / 2) 
                || (firstAngle >= 3 * Math.PI / 2 && secondAngle >= Math.PI && secondAngle <= 3 * Math.PI / 2))
            {
                Interlocked.Increment(ref totalHits);
            }
        }
    }
}