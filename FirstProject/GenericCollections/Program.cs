using System;
using System.Collections.Generic;

// Generic : 일반화
// 다양한 자료형에 대해서 유동적으로 갖다 쓸 수 있도록 만드는 형태

namespace GenericCollections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ArrayList
            // ====================================================
            // 박싱   : 자료형을 객체 타입으로 변환하는 과정
            // 언박싱 : 객체 타입을 원래 자료형으로 변환하는 과정
            // System.Collections를 더 이상 사용하지 않는 이유는
            // 박싱은 기본형 단순 할당 과정보다 20배 정도 느리고
            // 언박싱은 4배정도 느리기 때문에 Generic을 사용할 것을 권장하고 있다.
            System.Collections.ArrayList arrListr = new System.Collections.ArrayList();
            arrListr.Add(3);
            arrListr.Add("철수");
            arrListr.Add('A');

            // List
            // ====================================================
            List<int> list_int = new List<int>();
            List<float> list_float = new List<float>();
            List<List<int>> list_list_int = new List<List<int>>();

            // 추가
            list_int.Add(0);
            list_float.Add(1.0f);
            list_list_int.Add(list_int);
            list_list_int.Add(new List<int>());

            // 삭제
            list_int.Remove(0);
            list_list_int.RemoveAt(1);

            // 검색
            //list_int.Find(x => x == 0);
            list_int.Contains(0);

            for (int i = 0; i < list_int.Count; i++)
            {
                Console.WriteLine(list_int[i]);
            }

            // LinkedList
            // ====================================================
            // 노드 형식
            // 배열 대신 노드를 사용하고 노드는 자료형의 맞는 데이터와 이전 노드 및 다음 노드의 주소를 가지고 있다
            // 단방향 : Singly LinkedList
            // 양방향 : Doubly LinkedList
            LinkedList<int> linkedList = new LinkedList<int>();

            // 삽입
            linkedList.AddFirst(1);                     // 첫번째 노드에 삽입
            linkedList.AddLast(0);                      // 마지막 노드에 삽입
            linkedList.AddAfter(linkedList.Find(0), 1); // node 매개변수에 넣은 노드 뒤에 새로운 노드를 추가한다.
            linkedList.AddBefore(linkedList.First, 1);  // node 매개변수에 넣은 노드 앞에 새로운 노드를 추가한다.

            // 검색
            linkedList.Find(1);     // 입력한 값을 첫번째 노드부터 찾으며 먼저 나온 노드를 반환
            linkedList.FindLast(1); // 입력한 값을 마지막 노드부터 찾으며 먼저 나온 노드를 반환
            linkedList.Contains(1); // 매개변수에 입력한 값이 있는지 찾고 있으면 true, 없으면 false 반환

            // 삭제
            linkedList.Remove(1);                // 매개변수에 입력한 값을 찾아 삭제
            linkedList.Remove(linkedList.First); // 매개변수에 입력한 노드 삭제
            linkedList.RemoveFirst();            // 첫번째 노드 삭제
            linkedList.RemoveLast();             // 마지막 노드 삭제

            // HashTable
            // ====================================================
            // 중복되는 value 값은 넣을 수 없다
            // Hash : 고유 키 값
            // Hash 함수 : Hash를 뽑아내는 함수 ex) 문자열 "사과" -> Hash 함수 -> 12
            // 문자열 키 값으로 해시를 뽑아내는 해시 함수 구현 방식
            // 1. 입력 문자열의 각 문자들을 정수 형태로 전부 더한다.            ex) "사과" 입력, "사과"의 문자들을 ASCII 코드의 정수 값으로 더한다. (12가 나왔다고 가정)
            // (부가적으로 충돌을 줄이기 위해서 자릿수를 곱하거나 자릿수의 승수를 곱하는 등의 추가 연산을 할 수 있다.)
            // 2. 1의 값에 해시테이블 크기로 모듈러(나머지 % 연산)연산을 한다.  ex) 나온 값을 해시테이블 크기로 나머지 연산을 한다. (12 % 1000(해시테이블 크기라고 가정)) 
            // 3. 2의 결과를 해시로 반환한다.                                   ex) 나온 값을 "사과"의 해시로 지정

            // 체이닝
            // Hash 충돌이 일어난 value들을 LinkedList 형태로 관리하는 방법
            // 오픈어드레싱
            // Linear Probing    : 충돌이 일어난 인덱스에 +1해서 value의 해시를 지정한다.
            // Quadratic Probing : 제곱 뭐시기

            // 박싱 / 언박싱 때문에 사용하지 않음
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            ht.Add("철수", 90);

            // Dictionary
            // ====================================================
            Dictionary<string, char> grades = new Dictionary<string, char>();
            
            // 추가
            grades.Add("철수", 'A');
            grades.Add("영희", 'B');
            if (grades.TryAdd("영희", 'C'))
            {
                Console.WriteLine("영희 점수 C 추가");
            }
            else
            {
                Console.WriteLine($"영희 점수 이미 있음{grades["영희"]}");
            }

            // 삭제
            grades.Remove("철수");

            // 검색
            if(grades.ContainsKey("영희"))
            {
                Console.WriteLine("영희 점수 있음");
            }

            if(grades.TryGetValue("영희", out char grade))
            {
                Console.WriteLine(grade);
            }

            foreach (var sub in grades)
            {
                Console.WriteLine(sub.Key);
                Console.WriteLine(sub.Value);
            }

            foreach (var sub in grades.Keys)
            {
                Console.WriteLine(sub);
                Console.WriteLine(grades[sub]);
            }

            // Queue
            // FIFO (First Input First Output) 선입선출
            // 먼저 넣은 메모리가 먼저 나감
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1); // queue 제일 뒤에 아이템 추가
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Dequeue();  // queue 제일 앞에 있는 아이템 반환 및 제거
            Console.WriteLine(queue.Dequeue());
            queue.Peek();     // queue 제일 앞에 있는 아이템 반환
            queue.TryPeek(out int peek);

            // Stack
            // LIFO (Last Input First Output) 후입선출
            // 나중에 넣은 메모리가 먼저 나감
            Stack<int> stack = new Stack<int>();
            stack.Push(0); // Stack 제일 뒤에 아이템 추가
            stack.Push(1); 
            stack.Pop();   // Stack 제일 뒤에 추가된 아이템 반환 및 제거
            stack.Peek();  // Stack 제일 뒤에 추가된 아이템 반환
            stack.TryPeek(out int result);
        }
    }
}