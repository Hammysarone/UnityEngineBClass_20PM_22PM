using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorMethods
{
    internal class OperatorMethods
    {
        public static int Sum(int a, int b) //매개변수도 지역변수
        {
            return a + b;
        }
        // 함수 오버로딩
        // 같은 기능을 수행하는 함수의 이름을 똑같이 하면서
        // 파라미터가 다르면 동일한 이름의 함수도 여러개 정의할 수 있는 기능
        public static float Sum(float a, float b)
        {
            return a + b;
        }
        public static int Sub(int a, int b)
        {
            return a - b;
        }
        public static int Div(int a, int b)
        {
            return a / b;
        }
        public static int Mul(int a, int b)
        {
            return a * b;
        }
        public static int Mod(int a, int b)
        {
            return a % b;
        }
        public static int Increase(int a)
        {
            return ++a;
        }
        public static int Decrease(int a)
        {
            return --a;
        }
        public static bool IsSame(int a, int b)
        {
            return a == b;
        }
        public static bool IsDifferent(int a, int b)
        {
            return a != b;
        }
        public static bool IsBigger(int a, int b)
        {
            return a > b;
        }
        public static bool IsSmaller(int a, int b)
        {
            return a < b;
        }
        public static bool IsBiggerOrSame(int a, int b)
        {
            return a >= b;
        }
        public static bool IsSmallerOrSame(int a, int b)
        {
            return a <= b;
        }
        public static int PlusBToA(int a, int b)
        {
            a += b;
            return a;
        }
        public static int MinusBToA(int a, int b)
        {
            a -= b;
            return a;
        }
        public static int MultiplyBToA(int a, int b)
        {
            a *= b;
            return a;
        }
        public static int DivideBToA(int a, int b)
        {
            a /= b;
            return a;
        }
        public static int ModBToA(int a, int b)
        {
            a %= b;
            return a;
        }
        public static bool LogicOr(bool a, bool b)
        {
            return a | b;
        }
        public static bool LogicAnd(bool a, bool b)
        {
            return a & b;
        }
        public static bool LogicXOR(bool a, bool b)
        {
            return a ^ b;
        }
        public static bool LogicNOT(bool a)
        {
            return !a;
        }
        public static bool ConditionalLogicOr(bool a, bool b)
        {
            return a || b;
        }
        public static bool ConditionalLogicAND(bool a, bool b)
        {
            return a && b;
        }
        static public int BitLogicOR(int a, int b)
        {
            return a | b;
        }
        static public int BitLogicAND(int a, int b)
        {
            return a & b;
        }
        static public int BitLogicXOR(int a, int b)
        {
            return a ^ b;
        }
        static public int BitShiftLeft(int a, int howManyBitYouWantToShift)
        {
            return a >> howManyBitYouWantToShift;
        }
        static public int BitShiftRight(int a, int howManyBitYouWantToShift)
        {
            return a >> howManyBitYouWantToShift;
        }
        static public int BitComplement(int a)
        {
            return ~a;
        }
    }
}
