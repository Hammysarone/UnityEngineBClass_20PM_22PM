using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritance
{
    // 클래스는 단일 상속만 가능
    // 인터페이스는 다중 상속이 가능
    internal class Human : Creature, ITwoLeggedWalker, IFourLeggedWalker
    {
        public float height;
        public string name;

        public override void Breath()
        {
            Console.WriteLine($"{name}이(가) 숨을 쉬다.");
        }

        // virtual : 자식 클래스가 재정의 할 수 있도록 하는 키워드
        public virtual void Grow()
        {
            mass += 10.0f;
            height += 5.0f;
            Console.WriteLine($"{name}이가 자랐다.{mass}, {height}");
        }

        public void TwoLeggedWalk()
        {
            Console.WriteLine($"{name}이(가) 이족보행 한다");
        }
        public void FourLeggedWalk()
        {
            Console.WriteLine($"{name}이(가) 사족보행 한다");
        }
    }
}
