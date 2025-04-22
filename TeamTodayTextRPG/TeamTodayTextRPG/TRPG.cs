using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTodayTextRPG
{
    class TRPG()
    {
        static void Main(string[] args)
        {
            // 『효빈』VIEW_TYPE의 변수생성
            VIEW_TYPE currentView = VIEW_TYPE.MAIN;

            /* 『효빈』
                게임 전체를 관리해줄 GameManager 인스턴스
                이후 모든 메소드의 접근을 gm을 통해 행합니다!!
            */
            var gm = GameManager.Instance;

            gm.Intro();


            // 『효빈』스테이트 머신
            while (true)
            {
                switch (currentView)
                {
                    case VIEW_TYPE.MAIN:
                        gm.Viewer = new Main();
                        break;
                    case VIEW_TYPE.STATUS:
                        gm.Viewer = new Status();
                        break;
                    case VIEW_TYPE.INVENTORY:
                        gm.Viewer = new Inventory();
                        break;
                    case VIEW_TYPE.EQUIP:
                        gm.Viewer = new Equip();
                        break;
                    case VIEW_TYPE.SHOP:
                        gm.Viewer = new Shop();
                        break;
                    case VIEW_TYPE.PURCHASE:
                        gm.Viewer = new Purchase();
                        break;
                    case VIEW_TYPE.SALE:
                        gm.Viewer = new Sale();
                        break;
                    case VIEW_TYPE.DUNGEONSELECT:
                        gm.Viewer = new DungeonSelect();
                        break;
                    case VIEW_TYPE.DUNGEONCLEAR:
                        gm.Viewer = new DungeonClear();
                        break;
                    case VIEW_TYPE.REST:
                        gm.Viewer = new Rest();
                        break;
                }

                //『효빈』화면 UI 호출
                gm.Viewer.ViewAction(gm);

                //『효빈』선택지 입력 시 다음 화면으로의 전환
                currentView = gm.Viewer.NextView(gm, gm.InputAction(gm.Viewer.StartIndex, gm.Viewer.EndIndex));
            }
        }
    }
}