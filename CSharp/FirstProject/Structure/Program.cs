using System;

// Structure : 구조체
// 변수와 함수들을 멤버로 가지는 사용자 정의 자료형

struct Stats
{
    public int STR;
    public int DEX;
    public int INT;
    public int LUK;

    public Stats(int STR, int DEX, int INT, int LUK)
    {
        this.STR = STR;
        this.DEX = DEX;
        this.INT = INT;
        this.LUK = LUK;
    }

    public int GetCombatPower()
    {
        return STR + DEX + INT + LUK;
    }
}

class Stats_class
{
    public int STR;
    public int DEX;
    public int INT;
    public int LUK;

    public Stats_class(int STR, int DEX, int INT, int LUK)
    {
        this.STR = STR;
        this.DEX = DEX;
        this.INT = INT;
        this.LUK = LUK;
    }

    public int GetCombatPower()
    {
        return STR + DEX + INT + LUK;
    }
}

// 구조체 vs 클래스
// 한 번에 값을 복사할 수 있는 크기가 16 byte ( .NET 버전에 따라 상이할 수 있음)
// 기본적으로 값형식에다가 바로 값을 할당하는게 참조형식에 값을 할당하는 것 보다 빠르지만
// 대입해야하는 객체의 크기가 16 byte를 넘어서면 두 번 이상 걸쳐서 값을 복사해야함
// 두 번 값을 복사하는 과정이 참조형식을 복사해서 수정하는 것 보다 느림
// 따라서 16 byte를 초과하는 구조에 대해서는 모두 클래스로 정의하는 것을 권장
// 16 byte 이하이면서 값의 복사가 번번한 경우 반드시 구조체로 정의해야함
namespace Structure
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // C#에서 구조체는 값형식
            // 
            Stats stats1 = new Stats(10, 30, 50, 20);
            Stats stats2;

            Stats_class stats3 = new Stats_class(40, 20, 20, 10);

            IsEnterPossible(stats1);
            IsEnterPossible(stats3);

            Console.WriteLine(stats1.STR);
            Console.WriteLine(stats3.STR);
        }


        // 구조체는 값형식이므로 인자로 전달한 값이 파라미터로 대입됨
        // 파라미터를 수정해도 원래 인자로 전달했던 구조체는 수정되지 않는다 (서로 다른 구조체다)
        static bool IsEnterPossible(Stats stats)
        {
            stats.STR = 999;
            if (stats.GetCombatPower() > 100)
                return true;
            return false;
        }


        // 클래스는 참조형식이므로 인자로 전달한 참조가 파라미터로 대입됨
        // 파라미터를 수정하면 원래 인자가 참조하는 객체가 수정됨 (같은 객체를 가지고 있다)
        static bool IsEnterPossible(Stats_class stats)
        {
            stats.STR = 999;
            if (stats.GetCombatPower() > 100)
                return true;
            return false;
        }
    }
}

// 프로그램
// 순서가 정해진 절차적인 작업
//
// 컴퓨터에서 프로그램을 실행하는 순서 :
// 저장공간 확보 -> 데이터 입력 -> 데이터 처리(연산) -> 출력 -> 저장공간 해제
//
// 프로그래밍 언어 : 프로그램을 컴퓨터가 수행하기 위해 컴퓨터가 번역해서 사용할 수 있는 언어
//
// 컴파일 : 소스 코드를 다른 형태의 코드로 변환하는 과정
//
// 컴파일러 : 컴파일을 해주는 프로그램
//
// .NET 에서 프로그램을 실행하면 일어나는 과정
// 소스 코드 컴파일 -> IL(Intermediate Language) -> 실행 파일 생성
// 실행 파일 실행 시 -> IL -> JIT(Just In Time) 컴파일 -> 기계어

//UML