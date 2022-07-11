using System;
using System.Collections.Generic;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int totalDiceCount = 20;     // 주사위 최대 개수
            int totalTileCount = 20;     // 타일 최대 개수
            int diceValue = 1;           // 주사위 눈금 수

            int myStarCount = 0;         // 샛별 개수
            int currentLocation = 1;     // 현 위치
            int currentDiceCount = 0;    // 현재 주사위 개수

            List<TileMap> tiles = new List<TileMap>();
            CreateTiles(totalTileCount, tiles);
            currentDiceCount = totalDiceCount;

            while(currentDiceCount > 0)
            {
                Console.WriteLine($"엔터를 눌러 주사위를 굴리세요.");
                Console.WriteLine($"현재 위치 : {currentLocation}번째 칸, 남은 주사위 : {currentDiceCount}");
                Console.ReadLine();
                Console.WriteLine("========== 주사위 굴림! ==========");

                diceValue = RollDice();

                // 주사위 눈금 수 만큼 앞으로 이동하고 샛별칸을 지나가면 콘솔 출력
                for (int i = 1; i < diceValue + 1; i++)
                {
                    bool isOverTotalTile = false;
                    if (currentLocation + 1 > totalTileCount)
                    {
                        isOverTotalTile = true;
                        currentLocation = 1;
                    }

                    if (tiles[currentLocation - 1].IsStarTile())
                        Console.WriteLine($"샛별 칸 지나감. 현재 샛별 수 : {++myStarCount}");
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