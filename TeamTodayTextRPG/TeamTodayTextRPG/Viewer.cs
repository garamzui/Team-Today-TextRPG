using System;
using System.Collections.Concurrent;
using System.Numerics;
using System.Threading;
using System.Xml.Linq;
using TeamTodayTextRPG;

namespace TeamTodayTextRPG
{
    // 뷰어 화면 타입을 정의하는 열거형
    // 가나다라
    public enum VIEW_TYPE
    {
        MAIN,           // 게임 시작 화면
        STATUS,         // 상태 보기 화면
        INVENTORY,      // 인벤토리 화면
        EQUIP,          // 장비 화면
        SHOP,           // 상점 화면
        PURCHASE,       // 아이템 구매 화면
        SALE,           // 아이템 판매 화면
        
        DUNGEON_SELECT,  // 던전 선택화면
        DUNGEON,         // 던전 화면
        DUNGEON_CLEAR,   // 던전 클리어 화면

        BATTLE,
        BATTLE_PLAYER,
        BATTLE_PLAYER_LOG,
        BATTLE_ENEMY,

        REST,           // 휴식 화면
        MONSTER         // 몬스터 화면
    }

    // 모든 뷰어 클래스의 부모가 되는 추상 클래스
    public abstract class Viewer
    {
        public int StartIndex { get; set; }  // 화면에서 입력 가능한 시작 값
        public int EndIndex { get; set; }  // 화면에서 입력 가능한 끝 값
        public int DungeonCode { get; set; }// 던전 코드 (사용할 경우)

        public Player Player => GameManager.Instance.Player;
        
        protected Character Character => Player.Character;

        protected int GetInput()
        {
            return SceneManager.Instance.InputAction(StartIndex, EndIndex);
        }


        // 각 화면에서의 구체적인 액션을 구현하는 추상 메서드
        public abstract void ViewAction();

        // 입력에 따라 다음 화면을 반환하는 추상 메서드
        public abstract VIEW_TYPE NextView(int choiceNum);
    }
    public class MainViewer : Viewer
    {
        public MainViewer()
        {
            StartIndex = 1;
            EndIndex = 7;
        }
        public override void ViewAction()
        {
            Console.Clear();
            Console.WriteLine("메인 화면");
            Console.WriteLine("====================");
            Console.WriteLine("1. 플레이어 상태 보기");
            Console.WriteLine("2. 인벤토리 보기");
            Console.WriteLine("3. 장비 보기");
            Console.WriteLine("4. 상점");
            Console.WriteLine("5. 던전");
            Console.WriteLine("6. 휴식");
            Console.WriteLine("7. 게임 종료");

            int input = GetInput();
            VIEW_TYPE nextView = NextView(input);
            SceneManager.Instance.SwitchScene(nextView);
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 1:
                    return VIEW_TYPE.STATUS;
                case 2:
                    return VIEW_TYPE.INVENTORY;
                case 3:
                    return VIEW_TYPE.EQUIP;
                case 4:
                    return VIEW_TYPE.SHOP;
                case 5:
                    return VIEW_TYPE.DUNGEON_SELECT;
                case 6:
                    return VIEW_TYPE.REST;
                case 7:
                    Console.WriteLine("게임을 종료합니다...");
                    Environment.Exit(0);
                    return VIEW_TYPE.MAIN;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.MAIN;
            }
        }

    }


    public class StatusViewer : Viewer
    {
        public StatusViewer()
        {
            StartIndex = 1;
            EndIndex = 1;
        }
        public override void ViewAction()
        {
            Console.Clear();
            Console.WriteLine("플레이어 상태 보기");
            Console.WriteLine("====================");


            // 플레이어의 상태를 출력
            Console.WriteLine($"레벨: {Player.Level}");
            Console.WriteLine($"직업: {Character.Jobname}");
            Console.WriteLine($"체력: {Character.Hp}/{Character.MaxHp}");
            Console.WriteLine($"마나: {Character.Mp}/{Character.MaxMp}");
            Console.WriteLine($"공격력: {Character.Attack} (+{Character.PlusAtk}) = {Character.TotalAtk}");
            Console.WriteLine($"방어력: {Character.Defence} (+{Character.PlusDef}) = {Character.TotalDef}");
            Console.WriteLine($"회피율: {Character.Dodge} (+{Character.PlusDodge}) = {Character.TotalDodge}");
            Console.WriteLine($"소지금: {Player.Gold}G");
            Console.WriteLine($"액티브 스킬: {Character.ActskillName}");
            Console.WriteLine($"패시브 스킬: {Character.PasskillName} (레벨 {Character.PassiveSkillLevel}/{Character.MaxPassiveSkillLevel})");

            Console.WriteLine("====================");
            Console.WriteLine("1. 메인으로 돌아가기");

        }

        public override VIEW_TYPE NextView(int input)
        {
            if (input == 1)
            {
                // 메인 화면으로 돌아가기
                return VIEW_TYPE.MAIN;
            }
            else
            {
                // 잘못된 입력 처리
                return VIEW_TYPE.STATUS; // 기본적으로 현재 상태 화면 유지
            }
        }
    }


    public class InventoryViewer : Viewer
    {
        public InventoryViewer()
        {
            StartIndex = 1;
            EndIndex = 2;
        }
        public override void ViewAction()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("====================");

            var player = GameManager.Instance.Player;

            // InventoryItems 목록이나 Item 클래스가 변경될 경우 수정 필요
            for (int i = 0; i < GameManager.Instance.Player.Bag.Count; i++)
            {

                Console.WriteLine($"{i + 1}. {DataManager.Instance.ItemDB.List[GameManager.Instance.Player.Bag[i]][1]} - 공격력: {DataManager.Instance.ItemDB.List[GameManager.Instance.Player.Bag[i]][2]} / 방어력: {DataManager.Instance.ItemDB.List[GameManager.Instance.Player.Bag[i]][3]}");
            }

            Console.WriteLine("====================");
            Console.WriteLine("1. 아이템 사용");
            Console.WriteLine("2. 메인으로 돌아가기");
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(int input)
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
        public EquipViewer()
        {
            StartIndex = 0;
            EndIndex = GameManager.Instance.Player.Bag.Count;
        }

        public override void ViewAction()
        {
            Console.Clear();
            Console.WriteLine("장비");
            Console.WriteLine("====================");

            var player = GameManager.Instance.Player;
            var character = player.Character;

            Console.WriteLine($"직업: {character.Jobname}");
            Console.WriteLine($"총 공격력: {character.TotalAtk} (기본: {character.Attack} + 추가: {character.PlusAtk})");
            Console.WriteLine($"총 방어력: {character.TotalDef} (기본: {character.Defence} + 추가: {character.PlusDef})");
            Console.WriteLine($"총 회피율: {character.TotalDodge} (기본: {character.Dodge} + 추가: {character.PlusDodge})");

            Console.WriteLine("====================");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < player.Bag.Count; i++)
            {
                int itemCode = player.Bag[i];
                var item = DataManager.Instance.ItemDB.List[itemCode];

                string equippedMark = player.CheckEquip(itemCode) ? "[E]" : "";
                Console.WriteLine($"- {i + 1} {equippedMark}{item[1]} " +
                    $"| 공격력 +{item[2]} | 방어력 +{item[3]} | {item[6]}");
            }

            Console.WriteLine("====================");
            Console.WriteLine("1~n. 장비 변경");
            Console.WriteLine("0. 메인으로 돌아가기");
        }

        private int equipedItemCode = 0;
        public override VIEW_TYPE NextView(int input)
        {
            var player = GameManager.Instance.Player;

            if (input == 0)
                return VIEW_TYPE.MAIN;

            int index = input - 1;
            

            if (index >= 0 && index < player.Bag.Count)
            {
                int itemCode = player.Bag[index];  // 실제 아이템 코드 가져오기

                if (player.CheckEquip(itemCode))
                {
                    if (DataManager.Instance.ItemDB.List[equipedItemCode][8] ==
                        DataManager.Instance.ItemDB.List[itemCode][8])
                    {
                        player.ChangeItem(itemCode ,equipedItemCode);
                        player.StatChange(equipedItemCode);
                        player.StatChange(itemCode);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("장비를 교체했습니다!");
                    }
                    else
                    {
                        player.UnEquipItem(itemCode);
                        player.StatChange(itemCode);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("장비를 해제했습니다!");
                    }
                }
                else
                {
                    equipedItemCode = itemCode;
                    player.EquipItem(itemCode);  // 장착 리스트에 추가
                    player.StatChange(itemCode);  // <-- 여기 수정
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("장비를 장착했습니다!");
                }

                Console.ResetColor();
                Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                Console.ReadKey();

                return VIEW_TYPE.EQUIP;
            }

            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadKey();
            return VIEW_TYPE.EQUIP;
        }
    }

    public class ShopViewer : Viewer
    {
        public ShopViewer()
        {
            StartIndex = 1;
            EndIndex = 3;
        }
        public override void ViewAction()
        {
            Console.WriteLine("상점");
            Console.WriteLine("====================");

            Console.WriteLine($"플레이어 금액: {GameManager.Instance.Player.Gold}G");

            Console.WriteLine("상점에서 판매하는 아이템:");

            SceneManager.Instance.ShowShop(VIEW_TYPE.SHOP);

            Console.WriteLine("====================");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("3. 메인으로 돌아가기");
        }

        // NextView 메서드 구현
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
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
        public PurchaseViewer()
        {
            StartIndex = -1;
            EndIndex = DataManager.Instance.ItemDB.List.Count; // 아이템 개수만큼
        }

        public override void ViewAction()
        {
                Console.WriteLine("아이템 구매");
                Console.WriteLine("====================");

                var player = GameManager.Instance.Player;
                Console.WriteLine($"플레이어 금액: {player.Gold}G");

                Console.WriteLine("구매할 아이템을 선택하세요:");

                SceneManager.Instance.ShowShop(VIEW_TYPE.PURCHASE);
                Console.WriteLine("0. 돌아가기");
                Console.WriteLine("-1. 판매 화면으로");

        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0) { return VIEW_TYPE.SHOP; }
            else if (input == -1) { return VIEW_TYPE.SALE; }
            else if (input > 0 && input <= DataManager.Instance.ItemDB.List.Count)
            {
                // 인벤토리에 아이템 존재 여부
                if (!GameManager.Instance.Player.CheckBag(input - 1))
                {
                    // 잔금 여부
                    if (GameManager.Instance.Player.Gold >= int.Parse(DataManager.Instance.ItemDB.List[input - 1][7]))
                    {
                        // 재화감소
                        //GameManager.Instance.Player.Gold -= DataManager.Instance.ItemDB.List[input - 1].Value;
                        // 인벤토리에 아이템 추가
                        GameManager.Instance.Player.InputBag(int.Parse(DataManager.Instance.ItemDB.List[input - 1][0]),VIEW_TYPE.PURCHASE);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(">> 구매를 완료했습니다.\n");
                        Console.ResetColor();
                    }
                    // 구매실패 (잔금 부족)
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(">> Gold가 부족합니다.\n");
                        Console.ResetColor();
                    }
                }
                // 구매실패 (보유중인 물품)
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(">> 이미 구매한 아이템입니다.\n");
                    Console.ResetColor();
                }
                return VIEW_TYPE.PURCHASE;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">> 잘못된 값을 입력하였습니다.\n");
                Console.ResetColor();
                return VIEW_TYPE.PURCHASE;
            }
        }
    }


    public class SaleViewer : Viewer
    {
        public SaleViewer()
        {
            StartIndex = -1;
            EndIndex = GameManager.Instance.Player.Bag.Count; // Bag이 생기면 EndIndex = Bag.Count;
        }
        public override void ViewAction()
        {
            Console.WriteLine("아이템 판매");
            Console.WriteLine("====================");

            //var player = GameManager.Instance.Player;
            //var playerItems = player.GetInventoryItems();  // 플레이어 인벤토리에서 아이템 목록 가져오기

            Console.WriteLine("판매할 아이템을 선택하세요:");

            SceneManager.Instance.ShowShopSale();

            Console.WriteLine("0. 돌아가기");
            Console.WriteLine("-1. 구매 화면으로");
        }

        // NextView 메서드 구현
        public override VIEW_TYPE NextView(int input)
        {
        Console.Clear();
        if (input == 0) { return VIEW_TYPE.SHOP; }
        else if (input == -1) { return VIEW_TYPE.PURCHASE; }
        else if (input > 0 && input <= GameManager.Instance.Player.Bag.Count)
        {
            // 인벤토리에 아이템 존재 여부
            if (GameManager.Instance.Player.CheckBag(GameManager.Instance.Player.Bag[input - 1]))
            {
                // 재화증가
                //GameManager.Instance.Player.Gold += (int)(int.Parse((DataManager.Instance.ItemDB.List[GameManager.Instance.Player.Bag[input - 1]][7])) * 0.85);
                // 장비 중이라면 장비칸에서도 제거
                //if (GameManager.Instance.Player.CheckEquip(GameManager.Instance.Player.Bag[input - 1])) 
                //GameManager.Instance.Player.UnEquipItem(int.Parse(DataManager.Instance.ItemDB.List[GameManager.Instance.Player.Bag[input - 1]][0]));
                // 인벤토리에서 아이템 제거
                GameManager.Instance.Player.RemoveBag(int.Parse(DataManager.Instance.ItemDB.List[GameManager.Instance.Player.Bag[input - 1]][0]), VIEW_TYPE.SALE);
                   // DataManager.Instance.ItemDB.List[GameManager.Instance.Player.Bag[input - 1]][0]

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(">> 판매를 완료했습니다.\n");
                Console.ResetColor();
            }
            // 판매실패 (보유중이지 않은 물품)
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(">> 존재하지 않는 아이템입니다.\n");
                Console.ResetColor();
            }
            return VIEW_TYPE.SALE;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(">> 잘못된 값을 입력하였습니다.\n");
            Console.ResetColor();
            return VIEW_TYPE.SALE;
        }
        }
    }
    public class DungeonSelectViewer : Viewer
    {
        public DungeonSelectViewer()
        {
            StartIndex = 0;
            EndIndex = 4;
        }
        public override void ViewAction()
        {
            Console.WriteLine("던전입장\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            foreach(var dun in DataManager.Instance.DungeonDB.List)
            {
                Console.WriteLine($"{int.Parse(dun[0])+1}. {dun[1]} \t\t| {dun[8]}");
            }

            Console.WriteLine("0. 나가기\n");
        }

        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 0:
                    return VIEW_TYPE.MAIN;  // 메인 화면으로 이동
                case 1:
                case 2:
                case 3:
                case 4:
                    GameManager.Instance.Dungeon = GameManager.Instance.DungeonFactroy(input - 1);
                    return VIEW_TYPE.DUNGEON; // 던전 화면으로 돌아가기
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.DUNGEON; // 기본적으로 던전 화면 유지
            }
        }
    }


    public class DungeonViewer : Viewer
    {
        protected Dungeon dungeon;
        //protected Monster monster;

        public DungeonViewer()
        {
            StartIndex = 1;
            EndIndex = 3;
            dungeon = GameManager.Instance.Dungeon;
        }

        public override void ViewAction()
        {
            Console.Clear();
            Console.WriteLine("던전");
            Console.WriteLine("====================");

            Console.WriteLine($"던전 이름: {dungeon.Name}");
            Console.WriteLine($"던전 코드: {dungeon.Code}");
            Console.WriteLine($"난이도: {dungeon.Diff}");
            Console.WriteLine($"등장 몬스터 레벨: {dungeon.LowLevel} ~ {dungeon.HighLevel}");
            Console.WriteLine($"기본 보상: {dungeon.Reward}G");
            Console.WriteLine($"경험치: {dungeon.Exp}Exp");

            if (dungeon.Diff == DUNGEON_DIFF.Hell)
            {
                Console.WriteLine("!!!!!!!!!!!!!!!!!!! WARNING !!!!!!!!!!!!!!!!!!!");
                Console.WriteLine("[보스 효과] 플레이어 능력치가 10% 감소합니다!\n");
            }

            Console.WriteLine("====================");
            Console.WriteLine("1. 던전 입장 (전투 시작)");
            Console.WriteLine("2. 던전 선택으로 돌아가기");
            Console.WriteLine("3. 메인으로 돌아가기");
        }

        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 1:
                    // 전투 몬스터 등록
                    //GameManager.Instance.BattleEnemy = monster; // 몬스터 설정
                    GameManager.Instance.Dungeon.Enter();
                    return VIEW_TYPE.BATTLE_PLAYER;  // 전투 화면으로 이동
                case 2:
                    return VIEW_TYPE.DUNGEON_SELECT; // 던전 선택화면으로 돌아가기
                case 3:
                    return VIEW_TYPE.MAIN; // 메인 화면으로 돌아가기
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.DUNGEON; // 기본적으로 던전 화면 유지
            }
        }
    }
   

    public class BattleViewer : Viewer
    {
        public override void ViewAction()
        {
            // 몬스터가 모두 죽었다면 던전 CLEAR로 이동
            if (GameManager.Instance.Dungeon.CheckClear())
            {
                SceneManager.Instance.SwitchScene(NextView(0));
            }
            else
            {
                // 플레이어가 죽었다면 던전 CLEAR로 이동
                if (GameManager.Instance.Player.Character.Hp <= 0)
                {
                    SceneManager.Instance.SwitchScene(NextView(0));
                }
                // 배틀플레이어로 이동해서 전투 지속
                else
                {
                    SceneManager.Instance.SwitchScene(NextView(1));
                }
            }
        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
                return VIEW_TYPE.DUNGEON_CLEAR;
            else
                return VIEW_TYPE.BATTLE_PLAYER;
            }
        }
    

    public class BattlePlayerViewer : Viewer
    {
        public BattlePlayerViewer()
        {
            StartIndex = 0;
            EndIndex = GameManager.Instance.Dungeon.MonsterCount;
        }
        public override void ViewAction()
        {
            Console.WriteLine("Battle!!\n");
            int count = 1;
            foreach (var monster in GameManager.Instance.Dungeon.Dungeon_Monster)
            {
                Console.Write($"{ count++ } ");
                monster.View_Monster_Status();
            }

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{GameManager.Instance.Player.Level}\t {GameManager.Instance.Player.Name} ({GameManager.Instance.Player.Character.Jobname})");
            Console.WriteLine($"HP {GameManager.Instance.Player.Character.Hp}/{GameManager.Instance.Player.Character.MaxHp}");

            Console.WriteLine("====================");
            Console.WriteLine("0. 도망");

            Console.WriteLine("대상을 선택해주세요.");
        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
            {
                return VIEW_TYPE.MAIN;
            }
            else if (input > 0 && input <= GameManager.Instance.Dungeon.Dungeon_Monster.Count)
            {
                // 해당 몬스터가 죽은 상태라면
                if (GameManager.Instance.Dungeon.Dungeon_Monster[input-1].State == MONSTER_STATE.DEAD)
                {
                    Console.WriteLine("이미 싸늘한 상태입니다. 다른 적을 선택해주세요.");
                    return VIEW_TYPE.BATTLE_PLAYER;
                }
                // 해당 몬스터가 죽지 않았다면 대미지 처리 화면으로 이동
                else
                {
                    //GameManager.Instance.Animation = new CharaterAnimation();
                  
                    GameManager.Instance.Dungeon.TargetMonster = GameManager.Instance.Dungeon.Dungeon_Monster[input - 1];
                    return VIEW_TYPE.BATTLE_PLAYER_LOG; 
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                return VIEW_TYPE.BATTLE_PLAYER;
            }
        }
    }


    public class BattlePlayerLogViewer : Viewer
    {
        public BattlePlayerLogViewer()
        {
            StartIndex = 0;
            EndIndex = 0;
        }
        public override void ViewAction()
        {
            Console.WriteLine("Battle!!\n");

            Console.WriteLine($"{GameManager.Instance.Player.Name} 의 공격!");
            Console.WriteLine($"Lv.{GameManager.Instance.Dungeon.TargetMonster.Level}{GameManager.Instance.Dungeon.TargetMonster.Name}\n");
            GameManager.Instance.Player.Character.DefaultAttack();
            Console.WriteLine($"Lv.{GameManager.Instance.Dungeon.TargetMonster.Level} {GameManager.Instance.Dungeon.TargetMonster.Name}");
            Console.Write($"HP {GameManager.Instance.Dungeon.TargetMonster.Hp} -> ");

           

            if (GameManager.Instance.Dungeon.TargetMonster.State == MONSTER_STATE.DEAD)
            {
                Console.WriteLine("Dead");
            }
            else Console.WriteLine($"{GameManager.Instance.Dungeon.TargetMonster.Hp}");

            Console.WriteLine("\n0. 다음");
        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 0:
                    //GameManager.Instance.Dungeon.MonsterAtkCounter = GameManager.Instance.Dungeon.Dungeon_Monster.Count;
                    // 공격 횟수를 담당
                    GameManager.Instance.Dungeon.MonsterAtkCounter = 0;
                    return VIEW_TYPE.BATTLE_ENEMY;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.BATTLE_PLAYER_LOG;
            }
        }
    }


    public class BattleEnemyViewer : Viewer
    {
        public BattleEnemyViewer()
        {
            StartIndex = 0;
            EndIndex = 0;
        }
        public override void ViewAction()
        {
            
            // 몬스터가 공격할 수 있는 상태라면 공격 출력
            if (GameManager.Instance.Dungeon.Dungeon_Monster[GameManager.Instance.Dungeon.MonsterAtkCounter].State == MONSTER_STATE.IDLE)
            {
                Console.WriteLine("Battle!!\n");
                GameManager.Instance.Dungeon.TargetMonster.DefaultAttack();
                Console.WriteLine($"Lv.{GameManager.Instance.Dungeon.Dungeon_Monster[GameManager.Instance.Dungeon.MonsterAtkCounter].Level} {GameManager.Instance.Dungeon.Dungeon_Monster[GameManager.Instance.Dungeon.MonsterAtkCounter].Name} 의 공격!");
                

                Console.WriteLine($"Lv.{GameManager.Instance.Player.Level} {GameManager.Instance.Player.Name}");
                //Console.Write($"HP {GameManager.Instance.Player.Character.Hp} -> ");
                
                //Console.WriteLine($"{GameManager.Instance.Player.Character.Hp}\n");

                Console.WriteLine("\n0. 다음");

                GameManager.Instance.Dungeon.MonsterAtkCounter += 1;
                VIEW_TYPE nextView = NextView(SceneManager.Instance.InputAction(StartIndex, EndIndex));
                SceneManager.Instance.SwitchScene(nextView);
            }
            // 공격 할 수 없는 상태라면 바로 다음 화면으로 전환
            else
            {
                GameManager.Instance.Dungeon.MonsterAtkCounter += 1;
                SceneManager.Instance.SwitchScene(NextView(0));
            }
        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if(input == 0)
            {
                // 공격할 몬스터가 남았다면 몬스터 공격화면을 계속 출력
                if (GameManager.Instance.Dungeon.MonsterAtkCounter < GameManager.Instance.Dungeon.Dungeon_Monster.Count)
                {
                    return VIEW_TYPE.BATTLE_ENEMY;
                }
                // 몬스터의 공격이 끝났다. <<<<<<<여기 이어서 작성
                else
                {
                    //배틀 화면으로 이동해서 조건 처리...합쳐도 될거 같은 느낌?
                    return VIEW_TYPE.BATTLE;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                return VIEW_TYPE.BATTLE_ENEMY;
            }
        }
    }


    public class DungeonClearViewer : Viewer
    {
        protected Player player;
        protected Dungeon dungeon;

        public DungeonClearViewer()
        {
            player = GameManager.Instance.Player;
            dungeon = GameManager.Instance.Dungeon;

            StartIndex = 1;
            EndIndex = 1;
        }

        public DungeonClearViewer(Player player, Dungeon dungeon)
        {
            this.player = player;
            this.dungeon = dungeon;

            StartIndex = 1;
            EndIndex = 1;
        }

        public override void ViewAction()
        {
            Console.Clear();
            Console.WriteLine("던전 클리어");
            Console.WriteLine("====================");

            var character = player.Character;

            if (character.Hp <= 0)
            {
                Console.WriteLine("플레이어가 쓰러졌습니다! 던전 클리어 실패!");
            }
            else
            {
                Console.WriteLine($"축하합니다! 던전을 클리어했습니다!");
                Console.WriteLine($"보상: {dungeon.Reward}G");
                Console.WriteLine($"경험치: {dungeon.Exp}Exp");

                // 보상 처리
                player.Gold += dungeon.Reward;
                player.Exp += dungeon.Exp;
                player.LevelUp();

                if (dungeon.Diff == DUNGEON_DIFF.Hell)
                {
                    // 보스 효과 복구
                    character.Attack = (int)(character.Attack / 0.9);
                    character.Defence = (int)(character.Defence / 0.9);
                    Console.WriteLine("\n[보스 효과] 플레이어 능력치 감소 효과가 복구되었습니다!");
                }
            }
            Console.WriteLine("====================");
            Console.WriteLine("1. 메인으로 돌아가기");
        }

        public override VIEW_TYPE NextView(int input)
        {
            switch (input)
            {
                case 1:
                    return VIEW_TYPE.MAIN;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.DUNGEON_CLEAR;
            }
        }
    }


    public class RestViewer : Viewer
    {
        public RestViewer()
        {
            StartIndex = 1;
            EndIndex = 2;
        }
        public override void ViewAction()
        {
            Console.Clear();
            Console.WriteLine("휴식");
            Console.WriteLine("====================");

            var character = GameManager.Instance.Player.Character;
            Console.WriteLine($"체력 회복: {character.Hp}/{character.MaxHp}");

            Console.WriteLine("휴식을 취하시겠습니까?");
            Console.WriteLine("1. 휴식");
            Console.WriteLine("2. 메인으로 돌아가기");

            VIEW_TYPE nextView = NextView(SceneManager.Instance.InputAction(StartIndex, EndIndex));
            SceneManager.Instance.SwitchScene(nextView);
        }

        public override VIEW_TYPE NextView(int input)
        {
            switch (input)
            {
                case 1:
                    var character = GameManager.Instance.Player.Character;
                    character.Hp = character.MaxHp; // 체력 회복 처리
                    Console.WriteLine("휴식을 취했습니다.");
                    return VIEW_TYPE.MAIN;
                case 2:
                    return VIEW_TYPE.MAIN;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.REST;
            }
        }
    }
}
    /*
    public class BattleViewer : Viewer
    {
        public BattleViewer()
        {
            StartIndex = 1;
            EndIndex = 2;
        }

        public override void ViewAction()
        {
            Console.Clear();
            Console.WriteLine(" 전투 시작 ");
            Console.WriteLine("====================");

            var character = GameManager.Instance.Player.Character;
            var enemy = GameManager.Instance.BattleEnemy;

            Console.WriteLine($"플레이어 체력: {character.Hp}/{character.MaxHp}");
            Console.WriteLine($"적 몬스터 체력: {enemy.Hp}/{enemy.MaxHp}");

            Console.WriteLine("====================");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 도망");

            int input = GetInput();
            VIEW_TYPE nextView = NextView(input);
            SceneManager.Instance.SwitchScene(nextView);
        }

        public override VIEW_TYPE NextView(int input)
        {
            var character = GameManager.Instance.Player.Character;
            var enemy = GameManager.Instance.BattleEnemy;

            switch (input)
            {
                case 1:
                    // 플레이어 공격
                    int playerDamage = character.TotalAtk - enemy.Def;
                    if (playerDamage < 0) playerDamage = 0;
                    enemy.ManageHp(-playerDamage);
                    Console.WriteLine($"\n플레이어가 {enemy.Name}에게 {playerDamage}의 데미지를 입혔습니다!");

                    // 적이 죽었는지 확인
                    if (enemy.Hp <= 0)
                    {
                        Console.WriteLine($"{enemy.Name}을(를) 처치했습니다!");
                        Console.ReadKey();
                        return VIEW_TYPE.DUNGEONCLEAR;
                    }

                    // 몬스터 반격
                    int enemyDamage = enemy.Atk - character.TotalDef;
                    if (enemyDamage < 0) enemyDamage = 0;
                    character.Hp -= enemyDamage;
                    Console.WriteLine($"{enemy.Name}이(가) 플레이어에게 {enemyDamage}의 데미지를 입혔습니다!");

                    if (character.Hp <= 0)
                    {
                        Console.WriteLine("플레이어가 쓰러졌습니다...");
                        Console.ReadKey();
                        return VIEW_TYPE.DUNGEONCLEAR;
                    }

                    Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                    Console.ReadKey();
                    return VIEW_TYPE.BATTLE;

                case 2:
                    Console.WriteLine("플레이어가 도망쳤습니다...");
                    Console.ReadKey();
                    return VIEW_TYPE.MAIN;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.BATTLE;
            }
        }
    }
    */
    /*
    public class MonsterViewer : Viewer
    {
        protected Monster currentMonster;

        public MonsterViewer(Monster monster)
        {
            this.currentMonster = monster;
            StartIndex = 1;
            EndIndex = 2;
        }

        public override void ViewAction()
        {
            Console.Clear();
            Console.WriteLine(" 몬스터 정보");
            Console.WriteLine("====================");

            if (currentMonster == null)
            {
                Console.WriteLine("몬스터 정보가 없습니다.");
            }
            else
            {
                Console.WriteLine($"이름: {currentMonster.Name}");
                Console.WriteLine($"레벨: {currentMonster.Level}");
                Console.WriteLine($"체력: {currentMonster.Hp}/{currentMonster.MaxHp}");
                Console.WriteLine($"공격력: {currentMonster.Atk}");
                Console.WriteLine($"보스 여부: {(currentMonster.Grade == MONSTER_GRADE.BOSS ? " 보스" : " 일반")}");
            }

            Console.WriteLine("====================");
            Console.WriteLine("1. 전투 시작");
            Console.WriteLine("2. 메인으로 돌아가기");

            int input = GetInput();
            VIEW_TYPE nextView = NextView(input);
            SceneManager.Instance.SwitchScene(nextView);
        }

        public override VIEW_TYPE NextView(int input)
        {
            switch (input)
            {
                case 1:
                    GameManager.Instance.BattleEnemy = currentMonster; // 전투 대상 설정
                    return VIEW_TYPE.BATTLE;

                case 2:
                    return VIEW_TYPE.MAIN;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    return VIEW_TYPE.MONSTER;
            }
        }
    }*/


    



