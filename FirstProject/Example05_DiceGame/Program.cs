using System;
using System.Collections.Generic;

namespace Example05_DiceGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int totalDiceCount = 20;     // 주사위 최대 개수
            int totalTileCount = 20;     // 타일 최대 개수
            int diceValue;               // 주사위 눈금 수

            int myStarCount = 0;         // 샛별 개수
            int starGrantCount = 1;      // 부여하는 샛별 개수
            int currentLocation = 1;     // 현 위치
            int currentDiceCount;        // 현재 주사위 개수

            List<TileMap> tiles = new List<TileMap>();
            // a 타입으로 리스트를 만들었지만 만약 다른 클래스가 a를 상속 받는다면 그 클래스 타입도 현재 리스트에 추가는 가능하나
            // 만든 리스트 타입 클래스가 가지고 있는 멤버 변수 함수만 사용할 수 있다
            // (기본적으로 a를 상속 받는 클래스의 멤버 변수 함수 사용 불가능하지만 다른 방법으로 사용 가능)
            // 하지만 a를 상속 받는 클래스의 오버라이드된 함수는 사용 가능하다

            // 클래스 a와 b가 있고 b가 a를 상속받는 클래스이며 List를 a 클래스 타입으로 만들었고 b 클래스를 추가함
            // 이 때 a 클래스 타입으로 만든 List를 통해 b 클래스의 멤버 변수, 함수를 접근하고 싶으면 "(리스트이름 as b클래스이름)"을 사용해도 되고
            // (b클래스이름)리스트이름을 사용해서 접근할 수 있다.(캐스팅)

            CreateTiles(totalTileCount, tiles);
            currentDiceCount = totalDiceCount;

            while(currentDiceCount > 0)
            {
                // 시작
                Console.WriteLine($"엔터를 눌러 주사위를 굴리세요.");
                Console.WriteLine($"현재 위치 : {currentLocation}번째 칸, 남은 주사위 : {currentDiceCount}");
                Console.ReadLine();
                Console.WriteLine("========== 주사위 굴림! ==========");

                diceValue = RollDice();

                // 주사위 눈금 수 만큼 앞으로 이동하고 샛별칸을 지나가면 콘솔 출력
                // 현재칸 / 5의 값과 이전칸 / 5의 값을 구하고 빼서 나온 수가 총 지나간 샛별칸 수(5의 배수 칸이 샛별칸인 경우)
                // 지나간 타일 중 샛별칸이 있을 때 그 칸의 인덱스는 For(int i = 0; i < 총 지나간 샛별칸 수; i++)문에서 (현재칸 / 5 - i) * 5
                
                for (int i = 1; i < diceValue + 1; i++)
                {
                    bool isOverTotalTile = false;
                    if (currentLocation + 1 > totalTileCount)
                    {
                        isOverTotalTile = true;
                        currentLocation = 1;
                    }

                    if (tiles[currentLocation - 1].IsStarTile())
                    {
                        myStarCount += starGrantCount;
                        Console.WriteLine($"샛별 칸 지나감 +{starGrantCount}. 현재 샛별 수 : {myStarCount}");
                        starGrantCount++;
                    }
                    if(!isOverTotalTile)
                        currentLocation++;
                }
                Console.WriteLine();

                // 눈금 수 만큼 이동 후 콘솔 출력
                Console.Write($"{diceValue}칸 이동했으며 ");
                if(tiles[currentLocation - 1].IsStarTile() == true)
                    Console.WriteLine($"샛별 칸인 {tiles[currentLocation - 1].GetTileNum()}번 칸에 도달하였습니다.");
                else if(tiles[currentLocation - 1].IsStarTile() == false)
                    Console.WriteLine($"샛별 칸이 아닌 {tiles[currentLocation - 1].GetTileNum()}번 칸에 도달하였습니다.");
                Console.WriteLine("=================================");

                // 초기화 후 while문 처음으로
                currentDiceCount--;
                Console.WriteLine();
            }

            Console.WriteLine("========== 게임 끝! ==========");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"최종 샛별 수 : {myStarCount}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("=============================");
        }

        static int RollDice()
        {
            Random random = new Random();
            int result = random.Next(1, 6 + 1);


            switch (result)
            {
                case 1:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("└───────────┘");
                    break;
                case 2:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│         ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●        │");
                    Console.WriteLine("└───────────┘");
                    break;
                case 3:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│         ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●        │");
                    Console.WriteLine("└───────────┘");
                    break;
                case 4:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 5:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│     ●    │");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                case 6:
                    Console.WriteLine("┌───────────┐");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("│           │");
                    Console.WriteLine("│ ●      ●│");
                    Console.WriteLine("└───────────┘");
                    break;
                default:
                    throw new Exception("주사위 눈금이 잘못됨");
            }

            return result;
        }

        static void CreateTiles(int tileNum, List<TileMap> tiles)
        {
            int curTileNum = 1;
            for (int i = 0; i < tileNum; i++)
            {
                if(curTileNum % 5 == 0)
                    tiles.Add(new TileMap(true, curTileNum));
                else if (curTileNum % 5 != 0)
                    tiles.Add(new TileMap(false, curTileNum));

                curTileNum++;
            }
        }
    }
}