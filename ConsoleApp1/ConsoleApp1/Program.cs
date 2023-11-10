using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("异步开始!");
            //Task<int> tmp = GetAnswerTolife();
            //Console.WriteLine(tmp.Result); 
            Task<int> tmp = Run(() => {
                Thread.Sleep(5000);
                return 43;
            });
            Console.WriteLine(tmp.Result);
            Console.ReadLine();
        }
        static Task<int> GetAnswerTolife()
        {
            var tcs = new TaskCompletionSource<int>();
            var timer = new System.Timers.Timer(5000) { AutoReset = false };
            //5s后触发setresult，否则调用者一直等待result
            timer.Elapsed += delegate { timer.Dispose(); tcs.SetResult(42); };
            timer.Start();
            return tcs.Task;
        }
        static Task<TResult> Run<TResult>(Func<TResult> func)
        {
            var tcs = new TaskCompletionSource<TResult>();
            new Thread(() =>
            {
                try
                {
                    tcs.SetResult(func());
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }).Start();
            return tcs.Task;
        }
    }
}
