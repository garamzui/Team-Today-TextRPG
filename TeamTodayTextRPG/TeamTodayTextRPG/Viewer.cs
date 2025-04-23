using System;
using TeamTodayTextRPG;

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
        MONSTER         // 몬스터 화면
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

    public class StatusViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("플레이어 상태 보기");
            Console.WriteLine("====================");

            var player = gameManager.Player;  // Player 객체 가져오기

            // 플레이어의 상태를 출력
            Console.WriteLine($"이름: {player.Name}");
            Console.WriteLine($"체력: {player.Health}/{player.MaxHealth}");
            Console.WriteLine($"공격력: {player.Attack}");
            Console.WriteLine($"방어력: {player.Defense}");
            Console.WriteLine($"금액: {player.Gold}G");

            Console.WriteLine("====================");
            Console.WriteLine("1. 메인으로 돌아가기");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }

        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            if (input == 1)
            {
                // 메인 화면으로 돌아가기
                return VIEW_TYPE.MAIN;
            }
            else
            {
                // 잘못된 입력 처리
                return VIEW_TYPE.STATUS;  // 기본적으로 현재 상태 화면 유지
            }
        }
    }

    public class MainViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("메인 화면");
            Console.WriteLine("====================");
            Console.WriteLine("1. 플레이어 상태 보기");
            Console.WriteLine("2. 인벤토리 보기");
            Console.WriteLine("3. 장비 보기");
            Console.WriteLine("4. 상점");
            Console.WriteLine("5. 던전");
            Console.WriteLine("6. 게임 종료");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1:
                    // 플레이어 상태 보기 화면
                    return VIEW_TYPE.STATUS;
                case 2:
                    // 인벤토리 보기 화면
                    return VIEW_TYPE.INVENTORY;
                case 3:
                    // 장비 보기 화면
                    return VIEW_TYPE.EQUIP;
                case 4:
                    // 상점 화면
                    return VIEW_TYPE.SHOP;
                case 5:
                    // 던전 화면
                    return VIEW_TYPE.DUNGEON;
                case 6:
                    // 게임 종료 (이 부분은 실제 종료 로직을 추가해야 할 수 있음)
                    Console.WriteLine("게임 종료");
                    return VIEW_TYPE.MAIN;  // 또는 종료 화면을 정의할 수 있음
                default:
                    // 잘못된 입력 처리
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.MAIN;  // 잘못된 입력 시 메인 화면을 다시 표시
            }
        }
    }

    public class InventoryViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("====================");

            var player = gameManager.Player;
            var inventory = player.GetInventoryItems();

            // InventoryItems 목록이나 Item 클래스가 변경될 경우 수정 필요
            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {inventory[i].Name} - 공격력: {inventory[i].Atk} / 방어력: {inventory[i].Def}");
            }

            Console.WriteLine("====================");
            Console.WriteLine("1. 아이템 사용");
            Console.WriteLine("2. 메인으로 돌아가기");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1:
                    // 아이템 사용 화면으로 이동
                    return VIEW_TYPE.INVENTORY;  // 아이템 사용 화면
                case 2:
                    // 메인 화면으로 돌아가기
                    return VIEW_TYPE.MAIN;  // 메인 화면으로 전환
                default:
                    // 잘못된 입력 처리
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.INVENTORY;  // 잘못된 입력 시 인벤토리 화면을 다시 표시
            }
        }
    }

    public class EquipViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("장비");
            Console.WriteLine("====================");

            var player = gameManager.Player;
            Console.WriteLine($"무기: {player.Weapon.Name}");
            Console.WriteLine($"방어구: {player.Armor.Name}");

            // Player 클래스의 Weapon, Armor,가 변경될 경우, 이 부분 수정 필요
            Console.WriteLine("====================");
            Console.WriteLine("1. 장비 변경");
            Console.WriteLine("2. 메인으로 돌아가기");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1:
                    // 장비 변경 화면으로 이동
                    return VIEW_TYPE.EQUIP;  // 장비 변경 화면
                case 2:
                    // 메인 화면으로 돌아가기
                    return VIEW_TYPE.MAIN;  // 메인 화면으로 전환
                default:
                    // 잘못된 입력 처리
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.EQUIP;  // 잘못된 입력 시 장비 화면을 다시 표시
            }
        }
    }

    public class ShopViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("====================");

            var player = gameManager.Player;
            var shopItems = gameManager.ShopItems;

            // ShopItems나 아이템 구매/판매 로직이 변경되면 이 부분 수정 필요
            Console.WriteLine($"플레이어 금액: {player.Gold}G");

            Console.WriteLine("상점에서 판매하는 아이템:");
            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                Console.WriteLine($"{i + 1}. {item.Name} - {item.Price}G");
            }

            Console.WriteLine("====================");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("3. 메인으로 돌아가기");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1:
                    // 아이템 구매 화면으로 이동
                    return VIEW_TYPE.PURCHASE;
                case 2:
                    // 아이템 판매 화면으로 이동
                    return VIEW_TYPE.SALE;
                case 3:
                    // 메인 화면으로 돌아가기
                    return VIEW_TYPE.MAIN;
                default:
                    // 잘못된 입력 처리
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.SHOP;  // 다시 상점 화면을 보여줌
            }
        }
    }

    public class PurchaseViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("아이템 구매");
            Console.WriteLine("====================");

            var player = gameManager.Player;
            var shopItems = gameManager.ShopItems;  // 상점 아이템 목록

            Console.WriteLine($"플레이어 금액: {player.Gold}G");

            // 상점에서 판매하는 아이템 출력
            Console.WriteLine("구매할 아이템을 선택하세요:");
            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                Console.WriteLine($"{i + 1}. {item.Name} - {item.Price}G");
            }

            Console.WriteLine("0. 돌아가기");
            Console.WriteLine("9. 판매 화면으로");

            int input = gameManager.InputAction(startIndex, endIndex);

            if (input == 0)
            {
                NextView(gameManager, input);
            }
            else if (input == 9)
            {
                // 판매 화면으로 이동
                gameManager.SceneManager.SwitchScene(VIEW_TYPE.SALE);
            }
            else if (input > 0 && input <= shopItems.Count)
            {
                // 아이템 구매 처리
                var item = shopItems[input - 1];
                if (player.Gold >= item.Price)
                {
                    player.Gold -= item.Price;
                    player.AddItem(item);
                    Console.WriteLine($"{item.Name} 아이템을 구매했습니다.");
                }
                else
                {
                    Console.WriteLine("금액이 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            gameManager.SceneManager.SwitchScene(VIEW_TYPE.SHOP);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 0:
                    // 돌아가기: SHOP 화면으로 돌아갑니다.
                    return VIEW_TYPE.SHOP;
                case 9:
                    // 판매 화면으로 이동
                    return VIEW_TYPE.SALE;
                default:
                    // 잘못된 입력: 다시 구매 화면으로 돌아갑니다.
                    return VIEW_TYPE.PURCHASE;
            }
        }
    }


    public class SaleViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("아이템 판매");
            Console.WriteLine("====================");

            var player = gameManager.Player;
            var playerItems = player.GetInventoryItems();  // 플레이어 인벤토리에서 아이템 목록 가져오기

            Console.WriteLine("판매할 아이템을 선택하세요:");
            for (int i = 0; i < playerItems.Count; i++)
            {
                var item = playerItems[i];
                Console.WriteLine($"{i + 1}. {item.Name} - {item.Price}G");
            }

            Console.WriteLine("0. 돌아가기");
            Console.WriteLine("9. 구매 화면으로");

            int input = gameManager.InputAction(startIndex, endIndex);

            if (input == 0)
            {
                NextView(gameManager, input);
            }
            else if (input == 9)
            {
                // 구매 화면으로 이동
                gameManager.SceneManager.SwitchScene(VIEW_TYPE.PURCHASE);
            }
            else if (input > 0 && input <= playerItems.Count)
            {
                // 아이템 판매 처리
                var item = playerItems[input - 1];
                player.Gold += item.Price;
                player.RemoveItem(item);
                Console.WriteLine($"{item.Name} 아이템을 판매했습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            gameManager.SceneManager.SwitchScene(VIEW_TYPE.SHOP);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 0:
                    // 돌아가기: 상점 화면으로 이동
                    return VIEW_TYPE.SHOP;
                case 9:
                    // 구매 화면으로 이동
                    return VIEW_TYPE.PURCHASE;
                default:
                    var player = gameManager.Player;
                    var playerItems = player.GetInventoryItems();

                    if (input > 0 && input <= playerItems.Count)
                    {
                        // 아이템 판매 처리
                        var item = playerItems[input - 1];
                        player.Gold += item.Price;
                        player.RemoveItem(item);
                        Console.WriteLine($"{item.Name} 아이템을 판매했습니다.");
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }

                    // 판매 후에는 다시 상점 화면으로 이동
                    return VIEW_TYPE.SHOP;
            }
        }
    }


    public class DungeonViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("던전");
            Console.WriteLine("====================");

            Console.WriteLine("던전으로 들어가시겠습니까?");
            Console.WriteLine("1. 던전 입장");
            Console.WriteLine("2. 메인으로 돌아가기");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1:
                    // 던전 입장 화면으로 이동
                    return VIEW_TYPE.DUNGEON;  // 던전으로 입장
                case 2:
                    // 메인 화면으로 돌아가기
                    return VIEW_TYPE.MAIN;  // 메인 화면으로 전환
                default:
                    // 잘못된 입력 처리
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.DUNGEON;  // 잘못된 입력시 던전 화면을 다시 표시
            }
        }
    }

    public class DungeonClearViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("던전 클리어");
            Console.WriteLine("====================");

            Console.WriteLine("축하합니다! 던전을 클리어했습니다.");
            Console.WriteLine("1. 메인으로 돌아가기");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1:
                    // 메인 화면으로 돌아가기
                    return VIEW_TYPE.MAIN;  // 던전 클리어 후 메인 화면으로 전환
                default:
                    // 잘못된 입력 처리
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.DUNGEONCLEAR;  // 잘못된 입력시 던전 클리어 화면을 다시 보여줌
            }
        }
    }

    public class RestViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("휴식");
            Console.WriteLine("====================");

            var player = gameManager.Player;
            Console.WriteLine($"체력 회복: {player.Health}/{player.MaxHealth}");

            Console.WriteLine("휴식을 취하시겠습니까?");
            Console.WriteLine("1. 휴식");
            Console.WriteLine("2. 메인으로 돌아가기");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1:
                    // 휴식을 취하는 경우: 체력 회복 처리 후 메인 화면으로 이동
                    var player = gameManager.Player;
                    player.Rest(); // Rest 메서드를 호출하여 체력 회복 로직을 처리
                    Console.WriteLine("휴식을 취했습니다.");
                    return VIEW_TYPE.MAIN; // 메인 화면으로 이동
                case 2:
                    // 메인으로 돌아가는 경우
                    return VIEW_TYPE.MAIN;
                default:
                    // 잘못된 입력 처리
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.REST; // 다시 휴식 화면으로 돌아감
            }
        }
    }

    public class BattleViewer : Viewer
    {
        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("배틀");
            Console.WriteLine("====================");

            var player = gameManager.Player;
            var enemy = gameManager.BattleEnemy;

            // 플레이어와 적의 체력이 갱신될 때마다 출력
            Console.WriteLine($"플레이어 체력: {player.Health}/{player.MaxHealth}");
            Console.WriteLine($"적 몬스터 체력: {enemy.Health}/{enemy.MaxHealth}");

            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 도망");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1:
                    // 공격을 선택한 경우
                    // 전투 로직을 처리한 후, 다음 화면을 선택
                    return VIEW_TYPE.BATTLE;  // 계속 전투 화면을 유지하거나, 다른 화면으로 전환할 수 있음
                case 2:
                    // 도망을 선택한 경우
                    // 도망 성공 후 메인 화면 등으로 전환
                    return VIEW_TYPE.MAIN;  // 예시로 메인 화면으로 전환
                default:
                    // 잘못된 입력 처리
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.BATTLE;  // 다시 전투 화면으로 돌아갑니다.
            }
        }
    }

    public class MonsterViewer : Viewer
    {
        private Monster currentMonster;

        public MonsterViewer(Monster monster)
        {
            this.currentMonster = monster;
        }

        public override void ViewAction(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine("몬스터 정보 화면");
            Console.WriteLine("====================");

            // currentMonster가 null일 때의 처리
            if (currentMonster == null)
            {
                Console.WriteLine("몬스터 객체가 null입니다.");
            }
            else
            {
                // 몬스터의 속성 출력
                Console.WriteLine($"이름: {currentMonster.Name}");
                Console.WriteLine($"레벨: {currentMonster.Level}");
                Console.WriteLine($"체력: {currentMonster.Hp}/{currentMonster.MaxHp}");
                Console.WriteLine($"공격력: {currentMonster.Atk}");
            }

            Console.WriteLine("====================");
            Console.WriteLine("1. 전투");
            Console.WriteLine("2. 돌아가기");

            // 사용자 입력 받기
            int input = gameManager.InputAction(startIndex, endIndex);

            // 다음 화면 결정
            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }

        // 다음 화면을 결정하는 메서드
        public override VIEW_TYPE NextView(GameManager gameManager, int input)
        {
            switch (input)
            {
                case 1:
                    // 전투 화면으로 이동
                    return VIEW_TYPE.BATTLE;
                case 2:
                    // 이전 화면으로 돌아가기
                    return VIEW_TYPE.MAIN;  // 또는 다른 화면으로 돌아갈 수 있음
                default:
                    // 잘못된 입력 처리
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.MONSTER;  // 다시 몬스터 정보 화면을 보여줌
            }
        }
    }
}
