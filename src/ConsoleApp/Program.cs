using System;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            

            var parallelOption = new ParallelOptions
            {
                MaxDegreeOfParallelism = 2,
                CancellationToken = cts.Token,
            };

            var t = Task.Run(() =>
            {
                Thread.Sleep(5000);
                cts.Cancel();
            });

            try
            {
                var ret = Parallel.For(0, 100, parallelOption, x =>
                {
                    Console.WriteLine(cts.IsCancellationRequested);
                    Thread.Sleep(1000);
                });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
