using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using TeamTodayTextRPG;

namespace TeamTodayTextRPG
{
    class GameManager
    {
        /* 『효빈』
            Lazy<T> 형식의 싱글톤 방법입니다.
            인스턴스의 생성 시기를 직접 정할 수 있기 때문에 리소스 낭비를 줄일 수 있고, 스레드 안정성 문제도 해결 가능합니다. 
        */
        private static readonly Lazy<GameManager> lazyInstance = new Lazy<GameManager>(() => new GameManager());
        public static GameManager Instance => lazyInstance.Value;


        // 『효빈』GameManager 생성자 입니다.
        private GameManager()
        {
            Player = new Player();
        }


        // 『효빈』GameMananger 클래스의 프로퍼티 입니다.
        public Random rand { get; set; }
        public Player Player { get; set; }


        // 『효빈』초기 캐릭터 설정 (플레이어 이름, 플레이어할 캐릭터의 직업)을 도와주는 함수 입니다.
        public void Intro()
        {
            string? name = string.Empty;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.ResetColor();

            while (name == string.Empty)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("원하시는 이름을 설정해주세요.");
                Console.ResetColor();
                Console.Write("\n입력 >> ");
                name = Console.ReadLine();
                if (name == string.Empty)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("이름을 제대로 입력해주세요.\n");
                    Console.ResetColor();
                }
                else
                {
                    bool check = true;

                    while (check)
                    {
                        int num = 0;
                        Console.Write("입력하신 이름은 『 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(name);
                        Console.ResetColor();
                        Console.WriteLine(" 』입니다.\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("1. 저장");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("2. 취소\n");
                        Console.ResetColor();

                        num = SceneManager.Instance.InputAction(1, 2);

                        if (num == 1) check = false;
                        else if (num == 2)
                        {
                            check = false;
                            name = string.Empty;
                        }
                    }
                }
            }
            Console.Clear();

            int classCode = 0;
            while (classCode == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("원하시는 직업을 설정해주세요.\n");
                Console.ResetColor();
                Console.WriteLine("1. 전사\n2. 마법사\n3. 도적\n");

                classCode = SceneManager.Instance.InputAction(1, 3);
                Player.SetCharacter(classCode, name);
            }
            Console.Clear();
        }


        /* 『효빈』
            꾸준히 호출될 선택지 입력합수입니다.
            매개변수로 선택지의 첫번째 번호와 마지막 번호를 받습니다. 
            ex) 1.아이템구매 2. 아이템판매 0.나가기 
                ...이라면 startIndex = 0, endIndex = 2
            리턴 값으로는 "고른 선택지의 번호"를 반환합니다.
        */
        
    }
}