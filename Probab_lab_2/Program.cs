using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Probab_lab_2
{
    class Program
    {
        private static int _totalCalculations = 1000000;
        private static ThreadLocal<Random> _tlRng;
        private static ThreadLocal<double> _tlFunctionSum;
        private static double _functionSum = 0;
        private static double _integralResult = 0;
        
        
        static void Main(string[] args)
        {
            Console.Write("Dati numarul de calcule (n): ");
            _totalCalculations = int.Parse(Console.ReadLine());
            
            _tlRng = new ThreadLocal<Random>(() => new Random());
            _tlFunctionSum = new ThreadLocal<double>(() => 0, true);
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.For(0, _totalCalculations, i =>
            {
                FunctionAsync();
            });
            
            foreach (var result in _tlFunctionSum.Values)
            {
                _functionSum += result;
            }

            stopwatch.Stop();

            _integralResult = (0.5 * 0.5) * _functionSum / _totalCalculations;


            Console.WriteLine("I = " + _integralResult);
            Console.WriteLine("Calculated in " + stopwatch.Elapsed);
        }
        
        private static void FunctionAsync()
        {
            double x = _tlRng.Value.NextDouble() * 0.5;
            double y = _tlRng.Value.NextDouble() * 0.5;

            if (x + y <= 0.5 && y > x)
            {
                _tlFunctionSum.Value += (2 - Math.Log(1 + x * x * y)) / (2 * x * y * y + 1);
            }
        }
    }
}