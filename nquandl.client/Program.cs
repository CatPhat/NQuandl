using System;
using System.Threading.Tasks;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var test1 = new QuandlQueueTest();
            var test2 = new QuandlQueueTest();
            var test3 = new QuandlQueueTest();
            var test4 = new QuandlQueueTest();
            var test5 = new QuandlQueueTest();
            var test6 = new QuandlQueueTest();
            var printStatus = new PrintStatus();
            var task = Task.WhenAll(test1.GetTestString(), test2.GetTest2String());
            while (task.IsCompleted == false)
            {
                printStatus.Print();
            }


            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}