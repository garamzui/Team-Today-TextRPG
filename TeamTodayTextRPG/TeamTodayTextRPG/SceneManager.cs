using System;
using TeamTodayTextRPG;

namespace TeamTodayTextRPG
{
    //**객체 받고/ 속성 받기** To.재우
    // SceneManager 클래스: 씬 전환을 관리
    public class SceneManager
    {
        private Viewer currentViewer;
        private GameManager gameManager;  // GameManager 객체를 멤버로 추가

        public SceneManager(GameManager gameManager) //*GameManager 객체를 생성자에서 받아야함
        {
            this.gameManager = gameManager;  // 생성자에서 gameManager 객체 받기
        }

        public void SwitchScene(VIEW_TYPE viewType)
        {
            // 새로운 뷰어 할당
            switch (viewType)
            {
                case VIEW_TYPE.MAIN:
                    currentViewer = new MainViewer();
                    break;
                case VIEW_TYPE.STATUS:
                    currentViewer = new StatusViewer();
                    break;
                case VIEW_TYPE.INVENTORY:
                    currentViewer = new InventoryViewer();
                    break;
                case VIEW_TYPE.EQUIP:
                    currentViewer = new EquipViewer();
                    break;
                case VIEW_TYPE.SHOP:
                    currentViewer = new ShopViewer();
                    break;
                case VIEW_TYPE.PURCHASE:
                    currentViewer = new PurchaseViewer();
                    break;
                case VIEW_TYPE.SALE:
                    currentViewer = new SaleViewer();
                    break;
                case VIEW_TYPE.DUNGEON:
                    currentViewer = new DungeonViewer();
                    break;
                case VIEW_TYPE.DUNGEONCLEAR:
                    currentViewer = new DungeonClearViewer();
                    break;
                case VIEW_TYPE.REST:
                    currentViewer = new RestViewer();
                    break;
                case VIEW_TYPE.BATTLE:
                    currentViewer = new BattleViewer();
                    break;
                case VIEW_TYPE.MONSTER:
                    // GameManager에서 직접적으로 몬스터 객체를 가져오는 방식으로 수정
                    Monster currentMonster = gameManager.CurrentMonster;//*GameManager에서 속성을 추가 후 받아야함
                    currentViewer = new MonsterViewer(currentMonster);
                    break;
            }

            // 새로운 뷰어의 화면 출력
            ShowCurrentView();
        }

        public void ShowCurrentView()
        {
            if (currentViewer != null)
            {
                currentViewer.ViewAction(gameManager);  // gameManager 객체를 넘기기
            }
        }
    }
}
