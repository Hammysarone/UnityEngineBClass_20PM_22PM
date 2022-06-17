using System;

namespace Example01_ClassObjectInstance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Orc orc1 = new Orc("상급오크", 240.2f, 200f, 140, '남', false);
            Orc orc2 = new Orc("하급오크", 140.4f, 120f, 60, '여', true);

            orc1.GetResult();
            orc2.GetResult();
        }

        class Orc
        {
            string name;
            float height;
            float weight;
            int age;
            char genderChar;
            bool isResting;

            public string GetName() { return name; }
            public float GetHeight() { return height; }
            public float GetWeight() { return weight; }
            public int GetAge() { return age; }
            public char GetGenderChar() { return genderChar; }
            public bool GetRestingState() { return isResting; }


            public void GetResult()
            {
                // this 키워드
                // 객체 자기 자신을 참조하는 키워드
                if (isResting)
                {
                    Smite();
                    Jump();
                }
                else
                {
                    Console.WriteLine(name + "(이)가 바쁘다.");
                    Console.WriteLine("");
                }
            }

            public void Smite()
            {
                Console.WriteLine(name + "(이)가 강타했다!");
            }
            public void Jump()
            {
                Console.WriteLine(name + "(이)가 도약했다!");
            }

            public Orc(string name, float height, float weight, int age, char genderChar, bool isResting)
            {
                this.name = name;
                this.height = height;
                this.weight = weight;
                this.age = age;
                this.genderChar = genderChar;
                this.isResting = isResting;
            }
        }
    }
}
