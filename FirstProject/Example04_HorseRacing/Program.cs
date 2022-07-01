using System;
using System.Collections.Generic;
using System.Threading;

// 진행방식
// 말 클래스 만들고 5마리 생성
// 다섯마리는 초당 10 ~ 20범위 거리를 무작위로 움직임
// 200에 도달하면 이름 말함


namespace Example04_HorseRacing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int horseCount = 5;
            List<Horse> horses = new List<Horse>();

            List<int> usedNum = new List<int>();
            for (int i = 0; i < horseCount; i++)
            {
                Random random = new Random();
                int rndNum = random.Next(50) + random.Next(50);

                usedNum.Add(rndNum);
                Horse horse = new Horse($"말 번호 {rndNum}번");
                horses.Add(horse);
            }

            int turn = 1;
            int curRank = 1;
            bool isAnyHorseFinished = false;
            int finishedHorseCount = 0;
            bool isEveryHorseFinished = false;
            while(true)
            {
                Console.WriteLine();
                Console.WriteLine($"라운드 {turn}");
                Console.WriteLine();
                Random random = new Random();

                foreach (var horse in horses)
                {
                    if(!horse.GetFinishState())
                    {
                        horse.Run(random.Next(1, 21));
                    }
                }
                Console.WriteLine();
                foreach (var horse in horses)
                {
                    if (!horse.GetFinishState())
                    {
                        horse.CheckFinish(curRank);
                        if(horse.GetFinishState())
                        {
                            isAnyHorseFinished = true;
                            finishedHorseCount++;
                        }
                    }
                }

                if (finishedHorseCount >= horseCount)
                    break;
                if (isAnyHorseFinished) curRank++;
                isAnyHorseFinished = false;
                turn++;
                Thread.Sleep(1000);
            }

            Console.WriteLine("");
            Console.WriteLine("게임이 끝났습니다!");
            Console.WriteLine($"걸린 라운드 : {turn}");

            foreach (var horse in horses)
                horse.GetRank();

        }
    }

    public class Horse
    {
        string name;
        bool isFinished = false;
        int ranDistance;
        int rankNum;

        public void Run(int value)
        {
            ranDistance += value;
            Console.WriteLine($"{name}이(가) {value}만큼 이동했습니다. (현재 거리 : {ranDistance})");
        }
        public bool GetFinishState() { return isFinished; }
        public void GetRank() {Console.WriteLine($"{name}은(는) {rankNum}등 입니다!"); }
        public void CheckFinish(int rank)
        {
            if(ranDistance >= 200)
            {
                rankNum = rank;
                GetRank();
                isFinished = true;
            }
        }

        public Horse(string name)
        {
            this.name = name;
        }
    }
}
