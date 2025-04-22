using System;
using TeamTodayTextRPG;

namespace TeamTodayTextRPG
{
    // 각 View 상태를 구분하기 위한 열거형 정의
    public enum VIEW_TYPE
    {
        MAIN,
        STATUS,
        INVENTORY,
        EQUIP,
        SHOP,
        PURCHASE,
        SALE,
        DUNGEON,
        DUNGEONCLEAR,
        REST,
        BATTLE,
        MONSTER
    }

    // 현재 화면(Viewer)을 관리하고 전환하는 클래스
    public class SceneManager
    {
        public Viewer CurrentViewer { get; set; }

        public SceneManager()
        {
            // 초기 화면은 MainViewer로 설정
            CurrentViewer = new MainViewer();
        }

        // VIEW_TYPE에 따라 Viewer 인스턴스를 전환
        public void SwitchScene(VIEW_TYPE viewType)
        {
            CurrentViewer = viewType switch
            {
                VIEW_TYPE.MAIN => new MainViewer(),
                VIEW_TYPE.STATUS => new StatusViewer(),
                VIEW_TYPE.INVENTORY => new InventoryViewer(),
                VIEW_TYPE.EQUIP => new EquipViewer(),
                VIEW_TYPE.SHOP => new ShopViewer(),
                VIEW_TYPE.PURCHASE => new PurchaseViewer(),
                VIEW_TYPE.SALE => new SaleViewer(),
                VIEW_TYPE.DUNGEON => new DungeonViewer(),
                VIEW_TYPE.DUNGEONCLEAR => new DungeonClearViewer(),
                VIEW_TYPE.REST => new RestViewer(),
                VIEW_TYPE.BATTLE => new BattleViewer(),
                VIEW_TYPE.MONSTER => new MonsterViewer(),
                _ => throw new ArgumentException("Unknown view type")
            };
        }

        // 현재 Viewer의 화면을 출력
        public void ShowCurrentView(GameManager gameManager)
        {
            CurrentViewer?.ViewAction(gameManager);
        }

        // 입력을 받아 다음 화면으로 전환
        public void HandleInput(GameManager gameManager, int input)
        {
            VIEW_TYPE nextView = CurrentViewer.NextView(gameManager, input);
            SwitchScene(nextView);
        }
    }
}
