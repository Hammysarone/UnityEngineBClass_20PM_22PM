using System;

namespace Statement_Loops
{
    internal class Program
    {
        static int i = 0;
        static int j = 0;
        // 반복문의 종류
        // while, do while, for, foreach
        // while    : 조건이 참이면 해당 내용 반복 수행
        // do while : 일단 한 번 수행하고 조건이 참이면 해당 내용 반복 수행
        // for      : 조건이 참이면 해당 내용을 반복 수행하며, 반본 수행이 끝날 때마다 특정 연산이 가능 (대부분 인덱스 증감연산)
        // foreach  : collection의 또는 배열의 모든 요소에 접근해서 반복 수행
        static void Main(string[] args)
        {
            int[] tmpArr = new int[5];

            // 반복문의 조건이 항상 참인 경우 무한 루프라고 함
            // 무한 루프를 사용할 때 주의해야 할 점은 도중에 빠져나올 수 있는 명령이 있어야 한다는 것

            int count = 0;
            while (count < tmpArr.Length)
            {
                Console.WriteLine(tmpArr[count]);
                count++;
            }

            while (false)
            {
                Console.WriteLine(1);
            }

            do
            {
                Console.WriteLine(1);
            } while (false);

            //for (인덱스용 변수 초기화; for문 내용 반복할 조건; 루프가 한 번 실행되고나서 수행할 연산)
            //{
            //
            //}

            // while(true)랑 똑같이 작동
            //for(;;)
            //{
            //
            //}

            // 변수의 이름이 같다면 하위의 변수가 상위의 변수를 가린다.
            i = 1;
            for (int i = 0; 
                i < tmpArr.Length; 
                i++)
            {
                Console.WriteLine(tmpArr[i]);
            }

            // 다차원 배열
            int[,] mat2D = new int[3, 5] // 행이 3개, 열이 5개
            {
                { 1,2,3,4,5},
                { 6,7,8,9,1},
                { 2,3,4,5,6}
            };

            for (int i = 0; i < mat2D.GetLength(0); i++)
            {
                for (int j = 0; j  < mat2D.GetLength(1); j++)
                {
                    Console.Write(mat2D[i, j] + ", ");
                }
                Console.WriteLine();
            }

            int[][] arr2D = new int[5][];
            for (int i = 0; i < arr2D.Length; i++)
            {
                arr2D[i] = new int[3];
            }
            // collection 내의 모든 요소들에 접근
            // 접근 시 해당 요소는 var item 지역 변수에 담음
            //foreach (var item in collection)
            //{

            //}

            foreach (int item in tmpArr)
            {
                Console.WriteLine(item);
            }
        }
    }
}