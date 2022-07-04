using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassInheritance
{
    // abstract : 클래스를 추상화 클래스로 지정하는 키워드
    // 추상화 클래스 : 추상화 클래스가 가지고 있는 멤버변수 및 함수를 상속받는 자식들이 무조건 재정의해야 하는 클래스
    internal abstract class Creature
    {
        public string DNA;
        public int age;
        public float mass;

        public abstract void Breath();
    }
}
