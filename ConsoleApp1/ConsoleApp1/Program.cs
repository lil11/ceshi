using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 多线程异步
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("异步开始!");
            //Task<int> tmp = GetAnswerTolife();
            //Console.WriteLine(tmp.Result);
            //
            //Task<int> tmp = Run(() => {
            //    Thread.Sleep(5000);
            //    return 43;
            //});

            //延时5s后输出50
            //for(int n=0;n<1000;n++)
            //{
            //    Delay(2000).GetAwaiter().OnCompleted(() =>
            //    {
            //        Console.WriteLine(50);
            //    });
            //}
            //Console.WriteLine("for循环结束!");
            //Console.WriteLine(tmp.Result);


            var task = PrintAnswerTolife();
            task.GetAwaiter().GetResult();
            Console.WriteLine("exit");
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
        static Task Delay(int time)
        {
            var tcs = new TaskCompletionSource<object>();
            var timer = new System.Timers.Timer(time) { AutoReset = false };
            timer.Elapsed += delegate { timer.Dispose(); tcs.SetResult(null); };
            timer.Start();
            return tcs.Task;
        }

        static Task PrintAnswerTolife()
        {
            var tcs = new TaskCompletionSource<object>();
            var awaiter = Task.Delay(3000).GetAwaiter();
            awaiter.OnCompleted(() =>
            {

                awaiter.GetResult();
                Console.WriteLine("20");
                tcs.SetResult(null);
            });
            return tcs.Task;
        }
    }
}
