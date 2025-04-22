using System;
using System.Collections.Generic;
using TeamTodayTextRPG;


// 게임매니저, 플레이어, 몬스터, 아이템 클래스에서 맞는 프로퍼티가 있어야 문제없이 처리됨
// 예: player.Name, player.Hp, monster.Name, item.Name 등


namespace TeamTodayTextRPG
{
    // 뷰어 화면 타입을 정의하는 열거형
    public enum VIEW_TYPE
    {
        MAIN,           // 게임 시작 화면
        STATUS,         // 상태 보기 화면
        INVENTORY,      // 인벤토리 화면
        EQUIP,          // 장비 화면
        SHOP,           // 상점 화면
        PURCHASE,       // 아이템 구매 화면
        SALE,           // 아이템 판매 화면
        DUNGEON,        // 던전 화면
        DUNGEONCLEAR,   // 던전 클리어 화면
        REST,           // 휴식 화면
        BATTLE,         // 전투 화면
    }

    // 화면 전환과 관련된 처리를 담당하는 씬 매니저 클래스
    public class SceneManager
    {
        // 현재 화면을 나타내는 프로퍼티
        public Viewer CurrentViewer { get; set; }

        // 생성자에서 기본 뷰어를 MainViewer로 설정
        public SceneManager()
        {
            CurrentViewer = new MainViewer();
        }

        // 주어진 화면 타입으로 화면을 전환
        public void SwitchScene(VIEW_TYPE viewType)
        {
            switch (viewType)
            {
                case VIEW_TYPE.MAIN:
                    CurrentViewer = new MainViewer();
                    break;
                case VIEW_TYPE.STATUS:
                    CurrentViewer = new StatusViewer();
                    break;
                case VIEW_TYPE.INVENTORY:
                    CurrentViewer = new InventoryViewer();
                    break;
                case VIEW_TYPE.EQUIP:
                    CurrentViewer = new EquipViewer();
                    break;
                case VIEW_TYPE.SHOP:
                    CurrentViewer = new ShopViewer();
                    break;
                case VIEW_TYPE.PURCHASE:
                    CurrentViewer = new PurchaseViewer();
                    break;
                case VIEW_TYPE.SALE:
                    CurrentViewer = new SaleViewer();
                    break;
                case VIEW_TYPE.DUNGEON:
                    CurrentViewer = new DungeonViewer();
                    break;
                case VIEW_TYPE.BATTLE:
                    CurrentViewer = new BattleViewer();
                    break;
                default:
                    throw new ArgumentException("Unknown view type");
            }
        }

        // 현재 화면을 표시하는 메서드
        public void ShowCurrentView(GameManager gameManager)
        {
            CurrentViewer?.ViewAction(gameManager);
        }

        // 입력을 처리하고 다음 화면으로 전환하는 메서드
        public void HandleInput(GameManager gameManager, int input)
        {
            VIEW_TYPE nextView = CurrentViewer.NextView(gameManager, input);
            SwitchScene(nextView);
        }
    }

    // 모든 뷰어 클래스의 부모가 되는 추상 클래스
    public abstract class Viewer
    {
        protected int startIndex;  // 화면에서 입력 가능한 시작 값
        protected int endIndex;    // 화면에서 입력 가능한 끝 값
        protected int dungeonCode; // 던전 코드 (사용할 경우)

        // 각 화면에서의 구체적인 액션을 구현하는 추상 메서드
        public abstract void ViewAction(GameManager gameManager);

        // 입력에 따라 다음 화면을 반환하는 추상 메서드
        public abstract VIEW_TYPE NextView(GameManager gameManager, int input);
    }

    // MainViewer 클래스: 게임 시작 화면을 담당
    public class MainViewer : Viewer
    {
        // 생성자에서 startIndex, endIndex, dungeonCode 초기화
        public MainViewer()
        {
            startIndex = 1;
            endIndex = 6;
            dungeonCode = 0;
        }

        // 게임 시작 화면을 출력하는 메서드
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("게임 시작 화면");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 장비");
            Console.WriteLine("4. 상점");
            Console.WriteLine("5. 던전");
            Console.WriteLine("6. 종료");

            int input = gameManager.InputAction(startIndex, endIndex);
            gameManager.SceneManager.HandleInput(gameManager, input);
        }

        // 입력에 따라 다음 화면을 결정하는 메서드
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1: return VIEW_TYPE.STATUS;
                case 2: return VIEW_TYPE.INVENTORY;
                case 3: return VIEW_TYPE.EQUIP;
                case 4: return VIEW_TYPE.SHOP;
                case 5: return VIEW_TYPE.DUNGEON;
                case 6: ExitGame(); return VIEW_TYPE.MAIN;
                default: return VIEW_TYPE.MAIN;
            }
        }

        // 게임 종료 처리 메서드
        private void ExitGame()
        {
            Console.WriteLine("게임 종료 중...");
            SaveGameProgress();
            Environment.Exit(0);
        }

        // 게임 진행 상태를 저장하는 메서드
        private void SaveGameProgress()
        {
            Console.WriteLine("게임 진행 상태가 저장되었습니다.");
        }
    }

    // StatusViewer 클래스: 플레이어 상태 보기 화면을 담당
    public class StatusViewer : Viewer
    {
        public StatusViewer()
        {
            startIndex = 1;
            endIndex = 1;
            dungeonCode = 0;
        }

        // 플레이어 상태를 출력하는 메서드
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("플레이어 상태 보기");
            Console.WriteLine("1. 메인으로");

            int input = gameManager.InputAction(startIndex, endIndex);
            gameManager.SceneManager.HandleInput(gameManager, input);
        }

        // 입력에 따라 다음 화면을 결정하는 메서드
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            if (input == 1)
                return VIEW_TYPE.MAIN;

            return VIEW_TYPE.STATUS;
        }
    }

    // InventoryViewer 클래스: 인벤토리 화면을 담당
    public class InventoryViewer : Viewer
    {
        public InventoryViewer()
        {
            startIndex = 1;
            endIndex = 3;
            dungeonCode = 0;
        }

        // 인벤토리 화면을 출력하는 메서드
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 보기");
            Console.WriteLine("1. 장비 장착");
            Console.WriteLine("2. 장비 해제");
            Console.WriteLine("3. 메인으로");

            int input = gameManager.InputAction(startIndex, endIndex);
            gameManager.SceneManager.HandleInput(gameManager, input);
        }

        // 입력에 따라 다음 화면을 결정하는 메서드
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1: return VIEW_TYPE.INVENTORY;
                case 2: return VIEW_TYPE.INVENTORY;
                case 3: return VIEW_TYPE.MAIN;
                default: return VIEW_TYPE.INVENTORY;
            }
        }
    }

    // EquipViewer 클래스: 장비 화면을 담당
    public class EquipViewer : Viewer
    {
        public EquipViewer()
        {
            startIndex = 1;
            endIndex = 3;
            dungeonCode = 0;
        }

        // 장비 화면을 출력하는 메서드
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("장비 메뉴");
            Console.WriteLine("1. 장비 장착");
            Console.WriteLine("2. 장비 해제");
            Console.WriteLine("3. 메인으로");

            int input = gameManager.InputAction(startIndex, endIndex);
            gameManager.SceneManager.HandleInput(gameManager, input);
        }

        // 입력에 따라 다음 화면을 결정하는 메서드
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1: return VIEW_TYPE.EQUIP;
                case 2: return VIEW_TYPE.EQUIP;
                case 3: return VIEW_TYPE.MAIN;
                default: return VIEW_TYPE.EQUIP;
            }
        }
    }

    // ShopViewer 클래스: 상점 화면을 담당
    public class ShopViewer : Viewer
    {
        public ShopViewer()
        {
            startIndex = 1;
            endIndex = 3;
            dungeonCode = 0;
        }

        // 상점 화면을 출력하는 메서드
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("상점 메뉴");
            Console.WriteLine("1. 구매");
            Console.WriteLine("2. 판매");
            Console.WriteLine("3. 메인으로");

            int input = gameManager.InputAction(startIndex, endIndex);
            gameManager.SceneManager.HandleInput(gameManager, input);
        }

        // 입력에 따라 다음 화면을 결정하는 메서드
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1: return VIEW_TYPE.PURCHASE;
                case 2: return VIEW_TYPE.SALE;
                case 3: return VIEW_TYPE.MAIN;
                default: return VIEW_TYPE.SHOP;
            }
        }
    }

    // PurchaseViewer 클래스: 아이템 구매 화면을 담당
    public class PurchaseViewer : Viewer
    {
        public PurchaseViewer()
        {
            startIndex = 1;
            endIndex = 3;
            dungeonCode = 0;
        }

        // 구매 화면을 출력하는 메서드
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("구매 메뉴");
            Console.WriteLine("1. 무기 구매");
            Console.WriteLine("2. 방어구 구매");
            Console.WriteLine("3. 메인으로");

            int input = gameManager.InputAction(startIndex, endIndex);
            gameManager.SceneManager.HandleInput(gameManager, input);
        }

        // 입력에 따라 다음 화면을 결정하는 메서드
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1: return VIEW_TYPE.PURCHASE;
                case 2: return VIEW_TYPE.SALE;
                case 3: return VIEW_TYPE.SHOP;
                default: return VIEW_TYPE.PURCHASE;
            }
        }
    }

    // SaleViewer 클래스: 아이템 판매 화면을 담당
    public class SaleViewer : Viewer
    {
        public SaleViewer()
        {
            startIndex = 1;
            endIndex = 3;
            dungeonCode = 0;
        }

        // 판매 화면을 출력하는 메서드
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("판매 메뉴");
            Console.WriteLine("1. 무기 판매");
            Console.WriteLine("2. 방어구 판매");
            Console.WriteLine("3. 메인으로");

            int input = gameManager.InputAction(startIndex, endIndex);
            gameManager.SceneManager.HandleInput(gameManager, input);
        }

        // 입력에 따라 다음 화면을 결정하는 메서드
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1: return VIEW_TYPE.SALE;
                case 2: return VIEW_TYPE.SALE;
                case 3: return VIEW_TYPE.SHOP;
                default: return VIEW_TYPE.SALE;
            }
        }
    }

    // DungeonViewer 클래스: 던전 화면을 담당
    public class DungeonViewer : Viewer
    {
        public DungeonViewer()
        {
            startIndex = 1;
            endIndex = 2;
            dungeonCode = 1;
        }

        // 던전 입장 화면을 출력하는 메서드
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("던전 입장");
            Console.WriteLine("1. 쉬움 던전");
            Console.WriteLine("2. 메인으로");

            int input = gameManager.InputAction(startIndex, endIndex);
            gameManager.SceneManager.HandleInput(gameManager, input);
        }

        // 입력에 따라 다음 화면을 결정하는 메서드
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1: return VIEW_TYPE.BATTLE;
                case 2: return VIEW_TYPE.MAIN;
                default: return VIEW_TYPE.DUNGEON;
            }
        }
    }

    // BattleViewer 클래스: 전투 화면을 담당
    public class BattleViewer : Viewer
    {
        public BattleViewer()
        {
            startIndex = 1;
            endIndex = 4;
            dungeonCode = 0;
        }

        // 전투 화면을 출력하는 메서드
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("전투 화면");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 방어");
            Console.WriteLine("3. 아이템 사용");
            Console.WriteLine("4. 도망");

            int input = gameManager.InputAction(startIndex, endIndex);
            gameManager.SceneManager.HandleInput(gameManager, input);
        }

        // 입력에 따라 다음 화면을 결정하는 메서드
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1: return VIEW_TYPE.BATTLE;
                case 2: return VIEW_TYPE.BATTLE;
                case 3: return VIEW_TYPE.BATTLE;
                case 4: return VIEW_TYPE.MAIN;
                default: return VIEW_TYPE.BATTLE;
            }
        }
    }
}
