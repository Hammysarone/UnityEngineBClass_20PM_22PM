using System;

namespace Delegate
{
    internal class Program
    {
        public delegate int MyDelegate(int a, int b);
        public static event MyDelegate opDelegate;
        // event 한정자
        // event가 선언된 클래스 내에서만 해당 대리자를 호출할 수 있으며
        // 외부 클래스에서는 구독 및 구독 취소 (+= 또는 -=)만 가능하다

        public Action<int, int> opAction;
        static void Main(string[] args)
        {
            OPs.RefreshOP(OPs.OP.SUB, ref opDelegate);

            Console.WriteLine(opDelegate(3, 5));

            OPs.AddOP(OPs.OP.MUL, ref opDelegate);

            Console.WriteLine(opDelegate(3, 6));

        }
    }
}