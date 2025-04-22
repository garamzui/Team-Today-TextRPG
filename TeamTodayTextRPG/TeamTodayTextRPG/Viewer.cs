using TeamTodayTextRPG;

// 게임매니저, 플레이어, 몬스터, 아이템 클래스에서 맞는 프로퍼티가 있어야 문제없이 처리됨
// 예: player.Name, player.Hp, monster.Name, item.Name 등


namespace TeamTodayTextRPG
{
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

    public class SceneManager
    {
        // 뷰어를 관리하는 프로퍼티
        public Viewer CurrentViewer { get; set; }

        public SceneManager()
        {
            // 기본 뷰어는 MainViewer로 설정
            CurrentViewer = new MainViewer();
        }

        // 씬 전환 메서드
        public void SwitchScene(VIEW_TYPE viewType)
        {
            // 씬 전환시 각 뷰어를 설정하는 부분
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

        // 현재 뷰어에서 화면을 출력하는 메서드
        public void ShowCurrentView(GameManager gameManager)
        {
            // 현재 뷰어의 화면을 출력하는 부분
            CurrentViewer?.ViewAction(gameManager);
        }

        // 입력을 처리하여 씬을 전환하는 메서드
        public void HandleInput(GameManager gameManager, int input)
        {
            // 입력에 따라 화면을 전환하는 부분
            VIEW_TYPE nextView = CurrentViewer.NextView(gameManager, input);
            SwitchScene(nextView);  // 화면 전환
        }
    }

    // Viewer 추상 클래스
    public abstract class Viewer
    {
        public abstract void ViewAction(GameManager gameManager);  // 화면 출력 메서드 (각 뷰어에서 구현)
        public abstract VIEW_TYPE NextView(GameManager gameManager, int input);  // 입력 처리 후 다음 뷰 반환 메서드 (각 뷰어에서 구현)
    }

    // MainViewer 클래스
    public class MainViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();

            // 게임 시작 화면 출력
            Console.WriteLine("게임 시작 화면");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 장비");
            Console.WriteLine("4. 상점");
            Console.WriteLine("5. 던전");
            Console.WriteLine("6. 종료");
        }

        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            // 사용자 입력에 따른 뷰 전환
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

        // 게임 종료 메서드
        private void ExitGame()
        {
            // 게임 종료 시 진행 상태 저장 및 종료 처리
            Console.WriteLine("게임 종료 중...");
            SaveGameProgress();
            Environment.Exit(0);  // 게임 종료
        }

        // 게임 진행 상태 저장 메서드
        private void SaveGameProgress()
        {
            // 게임 진행 상태 저장하는 로직 (현재는 단순 출력)
            Console.WriteLine("게임 진행 상태가 저장되었습니다.");
        }
    }

    // StatusViewer 클래스
    public class StatusViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            // 플레이어 상태 출력
            Console.WriteLine("상태 보기 화면");
            Console.WriteLine($"체력: {gameManager.Player.Hp}");
            Console.WriteLine($"마나: {gameManager.Player.Mp}");
            Console.WriteLine($"레벨: {gameManager.Player.Level}");
            Console.WriteLine($"경험치: {gameManager.Player.Exp}");
            Console.WriteLine("1. 메인 화면으로 돌아가기");
        }

        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            // 메인 화면으로 돌아가는 경우
            if (input == 1)
                return VIEW_TYPE.MAIN;

            return VIEW_TYPE.STATUS;
        }
    }

    // InventoryViewer 클래스
    // Player 클래스에서 인벤토리, 장비를 처리하는 뷰어
    // Player 클래스에서 구현할 기능을 미리 생각하고 생성해둠
    public class InventoryViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();

            // player 객체를 Player 클래스에서 가져오는 기반으로 미리 생성해둠
            Player player = gameManager.Player;


            Console.WriteLine("인벤토리 화면\n");

            // 인벤토리 비어있을 때 처리
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("  (인벤토리에 아이템이 없습니다)");
            }
            else
            {
                Console.WriteLine("  보유 아이템 목록:");
                foreach (var item in player.Inventory)
                {
                    Console.WriteLine($"  - {item.Name}");
                }
            }

            // 사용자 행동을 선택하게 하는 부분
            Console.WriteLine("\n행동을 선택하세요:");
            Console.WriteLine(" 1. 아이템 사용");
            Console.WriteLine(" 2. 아이템 버리기");
            Console.WriteLine(" 3. 메인 화면으로 돌아가기");
        }

        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            // 행동에 따른 화면 전환
            switch (input)
            {
                case 1: return VIEW_TYPE.INVENTORY;
                case 2: return VIEW_TYPE.INVENTORY;
                case 3: return VIEW_TYPE.MAIN;
                default: return VIEW_TYPE.INVENTORY;
            }
        }
    }

    // EquipViewer 클래스
    public class EquipViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();

            // player 객체를 Player 클래스에서 가져오는 기반으로 미리 생성해둠
            Player player = gameManager.Player;

            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("           [ 장비 화면 ]");
            Console.WriteLine("==================================\n");

            Console.WriteLine(" ▶ 현재 착용 중인 장비:");

            // 장비가 없을 경우 처리
            if (player.Equipments.Count == 0)
            {
                Console.WriteLine("  (장비 없음)");
            }
            else
            {
                foreach (var equip in player.Equipments)
                {
                    Console.WriteLine($"  - {equip.Name}");
                }
            }    

            // 행동 선택
            Console.WriteLine("\n▼ 행동을 선택하세요:");
            Console.WriteLine(" 1. 장비 착용");
            Console.WriteLine(" 2. 장비 해제");
            Console.WriteLine(" 3. 메인 화면으로 돌아가기");
        }

        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            // 장비 관련 선택지 처리
            switch (input)
            {
                case 1: return VIEW_TYPE.EQUIP;
                case 2: return VIEW_TYPE.EQUIP;
                case 3: return VIEW_TYPE.MAIN;
                default: return VIEW_TYPE.EQUIP;
            }
        }
    }

    // ShopViewer 클래스
    public class ShopViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();

            // 상점 화면 출력
            Console.WriteLine("상점 화면");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("3. 메인 화면으로 돌아가기");
        }

        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            // 상점에서의 선택지 처리
            switch (input)
            {
                case 1: return VIEW_TYPE.PURCHASE;
                case 2: return VIEW_TYPE.SALE;
                case 3: return VIEW_TYPE.MAIN;
                default: return VIEW_TYPE.SHOP;
            }
        }
    }

    // PurchaseViewer 클래스
    public class PurchaseViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();

            // 아이템 구매 화면 출력
            Console.WriteLine("아이템 구매 화면");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 판매로 가기");
            Console.WriteLine("3. 상점으로 돌아가기");
        }

        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            // 아이템 구매 관련 선택지 처리
            switch (input)
            {
                case 1: return VIEW_TYPE.PURCHASE;
                case 2: return VIEW_TYPE.SALE;
                case 3: return VIEW_TYPE.SHOP;
                default: return VIEW_TYPE.PURCHASE;
            }
        }
    }

    // SaleViewer 클래스
    public class SaleViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();

            // 아이템 판매 화면 출력
            Console.WriteLine("아이템 판매 화면");
            Console.WriteLine("1. 아이템 판매");
            Console.WriteLine("2. 구매로 가기");
            Console.WriteLine("3. 상점으로 돌아가기");
        }

        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            // 판매와 관련된 선택지 처리
            switch (input)
            {
                case 1: return VIEW_TYPE.SALE;
                case 2: return VIEW_TYPE.PURCHASE;
                case 3: return VIEW_TYPE.SHOP;
                default: return VIEW_TYPE.SALE;
            }
        }
    }

    // DungeonViewer 클래스
    public class DungeonViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();

            // 던전 화면 출력
            Console.WriteLine("던전 화면");
            Console.WriteLine("1. 던전 탐험");
            Console.WriteLine("2. 메인 화면으로 돌아가기");
        }

        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            // 던전 선택지 처리
            switch (input)
            {

                case 1: return VIEW_TYPE.DUNGEON;
                case 2: return VIEW_TYPE.MAIN;
                default: return VIEW_TYPE.DUNGEON;
            }
        }
    }

    // BattleViewer 클래스
    public class BattleViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            // 전투 화면 출력
            Console.Clear();
            Console.WriteLine("==== 전투 화면 ====");

            // 플레이어 정보 출력
            Console.WriteLine($"플레이어: {gameManager.Player.Name} (레벨 {gameManager.Player.Level})");
            Console.WriteLine($"체력: {gameManager.Player.Hp}/{gameManager.Player.MaxHp}");
            Console.WriteLine($"마나: {gameManager.Player.Mp}/{gameManager.Player.MaxMp}");
            Console.WriteLine($"경험치: {gameManager.Player.Exp}/{gameManager.Player.ExpToNextLevel}");

            // 몬스터 정보 출력
            var monster = gameManager.CurrentMonster;
            Console.WriteLine($"\n몬스터: {monster.Name} (레벨 {monster.Level})");
            Console.WriteLine($"체력: {monster.Hp}/{monster.MaxHp}");

            // 전투 선택지 출력
            Console.WriteLine("\n1. 공격");
            Console.WriteLine("2. 방어");
            Console.WriteLine("3. 도망");
            Console.WriteLine("4. 메인 화면으로 돌아가기");
        }

        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1: return VIEW_TYPE.BATTLE;  // 공격 선택
                case 2: return VIEW_TYPE.BATTLE;  // 방어 선택
                case 3: return VIEW_TYPE.BATTLE;  // 도망 선택
                case 4: return VIEW_TYPE.MAIN;    // 메인 화면으로 돌아가기
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 선택하세요.");
                    return VIEW_TYPE.BATTLE;
            }
        }
    }
}
