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
            Console.WriteLine($"액세서리: {player.Accessory.Name}");

            // Player 클래스의 Weapon, Armor, Accessory가 변경될 경우, 이 부분 수정 필요
            Console.WriteLine("====================");
            Console.WriteLine("1. 장비 변경");
            Console.WriteLine("2. 메인으로 돌아가기");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
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

            // currentMonster가 변경될 경우, 이 부분 수정 필요
            if (currentMonster == null)
            {
                Console.WriteLine("몬스터 객체가 null입니다.");
            }
            else
            {
                Console.WriteLine($"이름: {currentMonster.Name}");
                Console.WriteLine($"레벨: {currentMonster.Level}");
                Console.WriteLine($"체력: {currentMonster.Hp}/{currentMonster.MaxHp}");
                Console.WriteLine($"공격력: {currentMonster.Attack}");
            }

            Console.WriteLine("====================");
            Console.WriteLine("1. 전투");
            Console.WriteLine("2. 돌아가기");

            int input = gameManager.InputAction(startIndex, endIndex);

            VIEW_TYPE nextView = NextView(gameManager, input);
            gameManager.SceneManager.SwitchScene(nextView);
        }
    }
}
