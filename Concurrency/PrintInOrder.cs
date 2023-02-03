using System;

using System.Threading;

namespace DataStructureRepo.Concurrency
{


    /// <summary>
    /// The same instance of Foo will be passed to three different threads.
    /// Thread A will call first(), thread B will call second(), and thread C will call third().
    /// Design a mechanism and modify the program to ensure that second() is executed after first(), and third() is executed after second().
    /// https://leetcode.com/problems/print-in-order/
    /// </summary>
    ///   
    

    public class PrintInOrder
    {
        private EventWaitHandle waitFirst;
        private EventWaitHandle waitSecond;
        public PrintInOrder()
        {
            waitFirst = new AutoResetEvent(false);
            waitSecond = new AutoResetEvent(false);
        }
        public void First(Action printFirst)
        {
            // printFirst() outputs "first". Do not change or remove this line.
            printFirst();
            waitFirst.Set();
        }

        public void Second(Action printSecond)
        {
            waitFirst.WaitOne(1000);
            // printSecond() outputs "second". Do not change or remove this line.
            printSecond();
            waitSecond.Set();
        }

        public void Third(Action printThird)
        {
            waitSecond.WaitOne(1000);
            // printThird() outputs "third". Do not change or remove this line.
            printThird();
        }
    }
}
