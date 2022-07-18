using System;

namespace OperatorMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(OperatorMethods.Sum(1, 2));
            Console.WriteLine(OperatorMethods.Sum(2, 3));
            Console.WriteLine(OperatorMethods.Sum(3, 4));
            Console.WriteLine(OperatorMethods.Sum(4, 5));
            // 디버그 중 F10 프로시저 단위로 실행
            // 디버그 중 F11 코드 단위로 실행
        }
    }
}
