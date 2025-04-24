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


       


        /* 『효빈』
            꾸준히 호출될 선택지 입력합수입니다.
            매개변수로 선택지의 첫번째 번호와 마지막 번호를 받습니다. 
            ex) 1.아이템구매 2. 아이템판매 0.나가기 
                ...이라면 startIndex = 0, endIndex = 2
            리턴 값으로는 "고른 선택지의 번호"를 반환합니다.
        */
        
    }
}