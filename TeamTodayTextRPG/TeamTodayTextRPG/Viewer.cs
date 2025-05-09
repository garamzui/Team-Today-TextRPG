﻿namespace TeamTodayTextRPG
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

        DUNGEON_SELECT,  // 던전 선택화면
        DUNGEON,         // 던전 화면
        DUNGEON_RESULT,   // 던전 클리어 화면

        BATTLE,
        BATTLE_PLAYER,
        CHOOSE_BEHAVIOR,
        BATTLE_PLAYER_LOG,
        BATTLE_PLAYER_SKILL_LOG,
        BATTLE_ENEMY,

        REST,           // 휴식 화면
        MONSTER,         // 몬스터 화면
        BACK_STREET,
        QUEST,
        CHECK_QUEST
    }

    // 모든 뷰어 클래스의 부모가 되는 추상 클래스
    public abstract class Viewer
    {
        public int StartIndex { get; set; }  // 화면에서 입력 가능한 시작 값
        public int EndIndex { get; set; }  // 화면에서 입력 가능한 끝 값
        public VIEW_TYPE Type { get; set; }
        public int MaxPage { get; set; }  // 최대페이지

        protected Dictionary<int, Func<VIEW_TYPE>> inputActions;

        protected Viewer()
        {
            inputActions = new Dictionary<int, Func<VIEW_TYPE>>();
        }
        protected Player Player => GameManager.Instance.Player;
        protected Character Character => Player.Character;
        protected Dungeon Dungeon => GameManager.Instance.Dungeon;
        protected Animation Animation => GameManager.Instance.Animation;

        // 탭 페이지 전환 관련
        protected void ClampTabPage(int maxPage)
        {
            if (SceneManager.Instance.TabPage < 0)
            {
                SceneManager.Instance.TabPage = 0;
                SceneManager.Instance.SysText("첫 페이지입니다", ConsoleColor.Red, ConsoleColor.Black);
            }
            else if (SceneManager.Instance.TabPage > maxPage-1)
            {
                SceneManager.Instance.TabPage = maxPage - 1;
                SceneManager.Instance.SysText("마지막 페이지입니다", ConsoleColor.Red, ConsoleColor.Black);
            }
            else
            {
                SceneManager.Instance.SysDefault();
            }
        }
        protected VIEW_TYPE NextTab()
        {
            SceneManager.Instance.TabPage++;
            ClampTabPage(2);
            return Type;
        }
        protected VIEW_TYPE PreviousTab()
        {
            SceneManager.Instance.TabPage--;
            ClampTabPage(2);
            return Type;
        }
        protected void ResetTab()
        {
            SceneManager.Instance.SysDefault();
            SceneManager.Instance.TabPage = 0;
        }

        // 단순 출력용 함수들
        protected void ViewNameLv()
        {
            Console.WriteLine($"\t이름     : {Player.Name}   ({Character.Jobname})");
            Console.WriteLine($"\t레벨     : Lv.{Player.Level}");
        }
        protected void ViewStatus()
        {
            Console.Write("\t체력     : ");
            SceneManager.Instance.ColText($"{Character.Hp}", ConsoleColor.Red, ConsoleColor.Black);
            Console.Write(" / ");
            SceneManager.Instance.ColText($"{Character.MaxHp}", ConsoleColor.Red, ConsoleColor.Black);
            ViewGuage10(Character.Hp, Character.MaxHp, ConsoleColor.Red);

            Console.Write("\t마나     : ");
            SceneManager.Instance.ColText($"{Character.Mp}", ConsoleColor.Blue, ConsoleColor.Black);
            Console.Write(" / ");
            SceneManager.Instance.ColText($"{Character.MaxMp}", ConsoleColor.Blue, ConsoleColor.Black);
            ViewGuage10(Character.Mp, Character.MaxMp, ConsoleColor.Blue);

            Console.Write($"\t공격력   : {Character.TotalAtk}");
            Console.Write(" (");
            if (Character.PlusAtk > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{Character.PlusAtk}");
            }
            else if (Character.PlusAtk < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"-{Character.PlusAtk}");
            }
            else
                Console.Write($"{Character.PlusAtk}");
            Console.ResetColor();
            Console.WriteLine(")");

            Console.Write($"\t방어력   : {Character.TotalDef}");
            Console.Write(" (");
            if (Character.PlusDef > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{Character.PlusDef}");
            }
            else if (Character.PlusDef < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"-{Character.PlusDef}");
            }
            else
                Console.Write($"{Character.PlusDef}");
            Console.ResetColor();
            Console.WriteLine(")");

            Console.Write($"\t회피율   : {Character.TotalDodge}");
            Console.Write(" (");
            if (Character.PlusDodge > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{Character.PlusDodge}");
            }
            else if (Character.PlusDodge < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"-{Character.PlusDodge}");
            }
            else
                Console.Write($"{Character.PlusDodge}");
            Console.ResetColor();
            Console.WriteLine(")");
        }
        protected void ViewStatusRest()
        {
            Console.Write("\t체력     : ");
            SceneManager.Instance.ColText($"{Character.Hp}", ConsoleColor.Red, ConsoleColor.Black);
            Console.Write(" / ");
            SceneManager.Instance.ColText($"{Character.MaxHp}", ConsoleColor.Red, ConsoleColor.Black);
            ViewGuage20(Character.Hp, Character.MaxHp, ConsoleColor.Red);

            Console.Write("\t마나     : ");
            SceneManager.Instance.ColText($"{Character.Mp}", ConsoleColor.Blue, ConsoleColor.Black);
            Console.Write(" / ");
            SceneManager.Instance.ColText($"{Character.MaxMp}", ConsoleColor.Blue, ConsoleColor.Black);
            ViewGuage20(Character.Mp, Character.MaxMp, ConsoleColor.Blue);
        }
        protected void ViewStatusEquip()
        {
            Console.Write($"\t공격력   : {Character.TotalAtk}");
            Console.Write(" (");
            if (Character.PlusAtk > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{Character.PlusAtk}");
            }
            else if (Character.PlusAtk < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{Character.PlusAtk}");
            }
            else
                Console.Write($"{Character.PlusAtk}");
            Console.ResetColor();
            Console.WriteLine(")");

            Console.Write($"\t방어력   : {Character.TotalDef}");
            Console.Write(" (");
            if (Character.PlusDef > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{Character.PlusDef}");
            }
            else if (Character.PlusDef < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{Character.PlusDef}");
            }
            else
                Console.Write($"{Character.PlusDef}");
            Console.ResetColor();
            Console.WriteLine(")");

            Console.Write($"\t회피율   : {Character.TotalDodge}");
            Console.Write(" (");
            if (Character.PlusDodge > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{Character.PlusDodge}");
            }
            else if (Character.PlusDodge < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{Character.PlusDodge}");
            }
            else
                Console.Write($"{Character.PlusDodge}");
            Console.ResetColor();
            Console.WriteLine(")");
        }
        protected void ViewStatusDun()
        {
            Console.WriteLine($"\tLv.{Player.Level} | {Player.Name}   ({Character.Jobname})");
            Console.Write("\t체력     : ");
            SceneManager.Instance.ColText($"{Character.Hp}", ConsoleColor.Red, ConsoleColor.Black);
            Console.Write(" / ");
            SceneManager.Instance.ColText($"{Character.MaxHp}", ConsoleColor.Red, ConsoleColor.Black);
            ViewGuage20(Character.Hp, Character.MaxHp, ConsoleColor.Red);

            Console.Write("\t마나     : ");
            SceneManager.Instance.ColText($"{Character.Mp}", ConsoleColor.Blue, ConsoleColor.Black);
            Console.Write(" / ");
            SceneManager.Instance.ColText($"{Character.MaxMp}", ConsoleColor.Blue, ConsoleColor.Black);
            ViewGuage20(Character.Mp, Character.MaxMp, ConsoleColor.Blue);

            Console.Write($"\t공격력   : {Character.TotalAtk}");
            Console.Write(" (");
            if (Character.PlusAtk > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{Character.PlusAtk}");
            }
            else if (Character.PlusAtk < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"-{Character.PlusAtk}");
            }
            else
                Console.Write($"{Character.PlusAtk}");
            Console.ResetColor();
            Console.Write(")");

            Console.Write($"\t방어력   : {Character.TotalDef}");
            Console.Write(" (");
            if (Character.PlusDef > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{Character.PlusDef}");
            }
            else if (Character.PlusDef < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"-{Character.PlusDef}");
            }
            else
                Console.Write($"{Character.PlusDef}");
            Console.ResetColor();
            Console.Write(")");

            Console.Write($"\t회피율   : {Character.TotalDodge}");
            Console.Write(" (");
            if (Character.PlusDodge > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"+{Character.PlusDodge}");
            }
            else if (Character.PlusDodge < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"-{Character.PlusDodge}");
            }
            else
                Console.Write($"{Character.PlusDodge}");
            Console.ResetColor();
            Console.WriteLine(")");
        }
        protected void ViewEquipItem()
        {
            if (Player.equipedWpCode != -1)
            {
                SceneManager.Instance.ColText($"\t[ 무  기 ]  -  『{DataManager.Instance.ItemDB.List[Player.equipedWpCode][1]}』\n", ConsoleColor.DarkGreen, ConsoleColor.Black);
                SceneManager.Instance.ShowAtk(int.Parse(DataManager.Instance.ItemDB.List[Player.equipedWpCode][2]));
                SceneManager.Instance.ShowDef(int.Parse(DataManager.Instance.ItemDB.List[Player.equipedWpCode][3]));
                Console.WriteLine("\n\t  [ " + DataManager.Instance.ItemDB.List[Player.equipedWpCode][6] + " ]");
                Console.WriteLine();
            }
            if (Player.equipedAmCode != -1)
            {
                SceneManager.Instance.ColText($"\t[ 방어구 ]  -  『{DataManager.Instance.ItemDB.List[Player.equipedAmCode][1]}』\n", ConsoleColor.DarkGreen, ConsoleColor.Black);
                SceneManager.Instance.ShowAtk(int.Parse(DataManager.Instance.ItemDB.List[Player.equipedAmCode][2]));
                SceneManager.Instance.ShowDef(int.Parse(DataManager.Instance.ItemDB.List[Player.equipedAmCode][3]));
                Console.WriteLine("\n\t  [ " + DataManager.Instance.ItemDB.List[Player.equipedAmCode][6] + " ]");
            }
        }

        // 장착중이라면 [E]붙여주는 함수
        public void Attach_E_Mark(int itemCode)
        {
            if (Player.CheckEquip(itemCode, ITEM_TYPE.WEAPON) || Player.CheckEquip(itemCode, ITEM_TYPE.ARMOR))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[E]");
                Console.ResetColor();
            }
        }

        // 비례 게이지 표시 10칸, 20칸 짜리
        protected void ViewGuage10(int value, int maxValue, ConsoleColor color)
        {
            int guage = (int)(((float)value / (float)maxValue) * 100) / 10;
            int guageDetail = (int)(((float)value / (float)maxValue) * 100) % 10;

            Console.Write("\t[");
            Console.ForegroundColor = color;
            for (int i = 0; i < guage; i++)
            {
                Console.Write("█");
            }
            if (guageDetail >= 5)
            {
                Console.Write("█");
                guage++;
            }
            Console.ResetColor();
            for (int i = guage; i < 10; i++)
            {
                Console.Write("░");
            }
            Console.Write("]\n");

        }
        protected void ViewGuage20(int value, int maxValue, ConsoleColor color)
        {
            int guage = (int)(((float)value / (float)maxValue) * 100) / 5;
            int guageDetail = (int)(((float)value / (float)maxValue) * 100) % 5;

            Console.Write("\t[");
            Console.ForegroundColor = color;
            for (int i = 0; i < guage; i++)
            {
                Console.Write("█");
            }
            if (guageDetail >= 3)
            {
                Console.Write("█");
                guage++;
            }
            Console.ResetColor();
            for (int i = guage; i < 20; i++)
            {
                Console.Write("░");
            }
            Console.Write("]\n");

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
            StartIndex = 0;
            EndIndex = 7;
        }
        public override void ViewAction()
        {
            SceneManager.Instance.ColText("    『마을』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 여러가지 활동을 할 수 있습니다.\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.WriteLine("\t━━━━━ ✦ 캐릭터 ✦ ━━━━━━━━━━━━━━━━━━━━\n");
            Console.WriteLine("\t【 1 】 플레이어\n\t【 2 】 인벤토리\n\t【 3 】 장비\n");
            Console.WriteLine("\t━━━━━ ✦ 행선지 ✦ ━━━━━━━━━━━━━━━━━━━━\n");
            Console.WriteLine("\t【 4 】 던전\n\t【 5 】 상점\n\t【 6 】 여관\n\t【 7 】 뒷골목");
            Console.WriteLine("\n\t━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Console.WriteLine("\n\t>> 0. 게임 종료\n\n");
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 1:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.STATUS;
                case 2:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.INVENTORY;
                case 3:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.EQUIP;
                case 4:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.DUNGEON_SELECT;
                case 5:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.SHOP;
                case 6:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.REST;
                case 7:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.BACK_STREET;
                case 0:
                    SceneManager.Instance.SysText("게임을 종료합니다...", ConsoleColor.Red, ConsoleColor.Black);
                    Environment.Exit(0);
                    return VIEW_TYPE.MAIN;
                default:
                    return VIEW_TYPE.MAIN;
            }
        }

    }


    public class StatusViewer : Viewer
    {
        public StatusViewer()
        {
            StartIndex = 0;
            EndIndex = 0;
        }
        public override void ViewAction()
        {
            SceneManager.Instance.ColText("    『플레이어』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 플레이어의 정보가 표시됩니다.\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);

            Console.WriteLine("\t━━━━━ ✦ 플레이어 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            ViewNameLv();
            Console.Write($"\t경험치   : {Player.Exp}/{Player.RequiredExp}");
            ViewGuage10(Player.Exp, Player.RequiredExp, ConsoleColor.Green);
            Console.Write("\t소지금   : ");
            SceneManager.Instance.ColText($"{Player.Gold}", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine(" G\n");

            Console.WriteLine("\n\t━━━━━ ✦  능력치  ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            ViewStatus();
            Console.WriteLine("\n\t━━━━━ ✦  장  비  ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            ViewEquipItem();
            Console.WriteLine("\n\t━━━━━ ✦  스  킬  ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            Console.WriteLine($"\t 액티브 스킬 | {Character.ActskillName}");
            Console.WriteLine($"\t 패시브 스킬 | {Character.PasskillName} (레벨 {Character.PassiveSkillLevel}/{Character.MaxPassiveSkillLevel})");
            Console.WriteLine("\n\t━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Console.WriteLine("\n\t0. 닫기\n\n");
        }

        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
            {
                // 메인 화면으로 돌아가기
                SceneManager.Instance.SysDefault();
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
            StartIndex = 0;
            EndIndex = 1;
        }
        public override void ViewAction()
        {
            SceneManager.Instance.ColText("    『인벤토리』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 보유중인 아이템을 확인하거나, 소모품을 사용할 수 있습니다.\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);

            Console.WriteLine($"  ━━━━━ ✦ 아이템 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            //Console.WriteLine($"  ━━━━━ ✦ 무  기 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            SceneManager.Instance.ShowInventory(VIEW_TYPE.INVENTORY);
            //Console.WriteLine($"  ━━━━━ ✦ 방어구 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            //Console.WriteLine($"  ━━━━━ ✦ 소모품 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Console.WriteLine("\t1. 아이템 장착");
            Console.WriteLine("\t0. 닫기\n\n");
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 1:
                    // 아이템 사용 화면으로 이동
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.EQUIP;  // 아이템 사용 화면
                case 0:
                    SceneManager.Instance.SysDefault();
                    // 메인 화면으로 돌아가기
                    return VIEW_TYPE.MAIN;  // 메인 화면으로 전환
                default:
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
            var player = GameManager.Instance.Player;
            var character = player.Character;
            //SceneManager.Instance.ColText("스파르타 마을에 오신 여러분 환영합니다.", 0, -1, ConsoleColor.Yellow, ConsoleColor.Black, true);
            SceneManager.Instance.ColText("    『장비』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 장비를 장착하거나, 교체할 수 있습니다.\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);

            Console.WriteLine($"  ====직업: {character.Jobname}");
            Console.WriteLine($"      총 공격력: {character.TotalAtk} (기본: {character.Attack} + 추가: {character.PlusAtk})");
            Console.WriteLine($"      총 방어력: {character.TotalDef} (기본: {character.Defence} + 추가: {character.PlusDef})");
            Console.WriteLine($"      총 회피율: {character.TotalDodge} (기본: {character.Dodge} + 추가: {character.PlusDodge})");
            Console.WriteLine("");
            Console.WriteLine("  =====[목록]=====================================================================");
            SceneManager.Instance.ShowEquip(VIEW_TYPE.EQUIP);
            Console.WriteLine("  ================================================================================\n");

            Console.WriteLine("\t━━━━━ ✦ 장비목록 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            SceneManager.Instance.ShowInventory(VIEW_TYPE.EQUIP);
            Console.WriteLine("\n\t━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Console.WriteLine($"\t1~{EndIndex}. 장비 변경");
            Console.WriteLine("\n\t0. 메인으로 돌아가기\n\n");
        }


        public override VIEW_TYPE NextView(int input)
        {
            var player = GameManager.Instance.Player;
            

            if (input == 0)
                return VIEW_TYPE.MAIN;

            int index = input - 1;

            if (index >= 0 && index < player.Bag.Count)
            {
                int itemCode = player.Bag[index];  // 실제 아이템 코드 가져오기
                int Itype = int.Parse(DataManager.Instance.ItemDB.List[itemCode][8]);

                switch (Itype)
                {
                    case (int)(ITEM_TYPE.WEAPON):

                        if (player.equipedWpCode == -1)
                        {
                            Player.EquipItem(itemCode, ITEM_TYPE.WEAPON);
                            SceneManager.Instance.SysText($"{DataManager.Instance.ItemDB.List[itemCode][1]} 을(를) 장착 했습니다", ConsoleColor.DarkCyan, ConsoleColor.Black);
                        }

                        else if (player.equipedWpCode == itemCode)
                        {
                            player.UnEquipItem(itemCode);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("장비를 해제했습니다!");
                        }

                        else
                        {
                            player.ChangeItem(itemCode, ITEM_TYPE.WEAPON);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("장비를 교체했습니다!");
                        }
                        break;

                    case (int)(ITEM_TYPE.ARMOR):

                        if (player.equipedAmCode == -1)
                        {
                            player.EquipItem(itemCode, ITEM_TYPE.ARMOR);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("장비를 장착했습니다!");
                        }

                        else if (player.equipedAmCode == itemCode)
                        {
                            player.UnEquipItem(itemCode);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("장비를 해제했습니다!");
                        }

                        else
                        {
                            player.ChangeItem(itemCode, ITEM_TYPE.ARMOR);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("장비를 교체했습니다!");
                        }
                        break;
                }

                Console.ResetColor();
                Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
                Console.ReadKey();
                Console.Clear();

                return VIEW_TYPE.EQUIP;
            }

            Console.WriteLine("잘못된 입력입니다.");
            Console.ReadKey();
            return VIEW_TYPE.EQUIP;
        }
    }

    public class ShopViewer : Viewer
    {
        private readonly Dictionary<int, Func<VIEW_TYPE>> inputActions;
        public ShopViewer()
        {
            StartIndex = -2;
            EndIndex = 2;
            Type = VIEW_TYPE.SHOP;

            inputActions = new Dictionary<int, Func<VIEW_TYPE>>()
            {
                { -1, PreviousTab },
                { -2, NextTab },
                { 0, () => { ResetTab(); return VIEW_TYPE.MAIN; } },
                { 1, () => { ResetTab(); return VIEW_TYPE.PURCHASE; } },
                { 2, () => { ResetTab(); return VIEW_TYPE.SALE; } }
            };
        }
        private VIEW_TYPE PreviousTab()
        {
            SceneManager.Instance.TabPage--;
            ClampTabPage(2);
            return VIEW_TYPE.SHOP;
        }
        private void DisplayHeader()
        {
            SceneManager.Instance.ColText("    『상점』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 필요한 아이템을 구매하거나 판매합니다.\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
        }
        private void DisplayGold()
        {
            Console.Write("\t\t\t\t\t소지금 : ");
            SceneManager.Instance.ColText($"{Player.Gold}", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine(" G");
        }
        private void DisplayTabs()
        {
            if (SceneManager.Instance.TabPage == 0)
            {
                Console.Write($"  ━━━━━");
                SceneManager.Instance.ColText(" ✦ 무  기 ✦ ", ConsoleColor.Yellow, ConsoleColor.Black);
                Console.WriteLine($"━ ✦ 방어구 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
                SceneManager.Instance.ShowShop(VIEW_TYPE.SHOP, ITEM_TYPE.WEAPON);
            }
            else if (SceneManager.Instance.TabPage == 1)
            {
                Console.Write($"  ━━━━━ ✦ 무  기 ✦ ━");
                SceneManager.Instance.ColText(" ✦ 방어구 ✦ ", ConsoleColor.Yellow, ConsoleColor.Black);
                Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
                SceneManager.Instance.ShowShop(VIEW_TYPE.SHOP, ITEM_TYPE.ARMOR);
                Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            }
        }
        private void DisplayMenu()
        {
            Console.WriteLine("\t-1. << 이전\t-2. 다음 >>\n");
            Console.WriteLine("\t1. 아이템 구매");
            Console.WriteLine("\t2. 아이템 판매");
            Console.WriteLine("\n\t0. 마을로 돌아가기\n\n");
        }
        
        public override void ViewAction()
        {
            DisplayHeader();
            DisplayGold();
            DisplayTabs();
            DisplayMenu();
        }
        // NextView 메서드 구현
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (inputActions.TryGetValue(input, out var action))
            {
                return action();
            }
            return VIEW_TYPE.SHOP;
            
        }
    }


    public class PurchaseViewer : Viewer
    {
        public PurchaseViewer()
        {
            StartIndex = -3;
            EndIndex = DataManager.Instance.ItemDB.List.Count; // 아이템 개수만큼
        }

        public override void ViewAction()
        {
            SceneManager.Instance.ColText("    『상점 - 구매』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 필요한 아이템을 골드를 주고 구매합니다.\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);

            Console.Write("\t\t\t\t\t소지금 : ");
            SceneManager.Instance.ColText($"{Player.Gold}", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine(" G");

            if (SceneManager.Instance.TabPage == 0)
            {
                Console.Write($"  ━━━━━");
                SceneManager.Instance.ColText(" ✦ 무  기 ✦ ", ConsoleColor.Yellow, ConsoleColor.Black);
                Console.WriteLine($"━ ✦ 방어구 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
                SceneManager.Instance.ShowShop(VIEW_TYPE.PURCHASE, ITEM_TYPE.WEAPON);
            }
            else if (SceneManager.Instance.TabPage == 1)
            {
                Console.Write($"  ━━━━━ ✦ 무  기 ✦ ━");
                SceneManager.Instance.ColText(" ✦ 방어구 ✦ ", ConsoleColor.Yellow, ConsoleColor.Black);
                Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
                SceneManager.Instance.ShowShop(VIEW_TYPE.PURCHASE, ITEM_TYPE.ARMOR);
            }
            //Console.WriteLine($"  ━━━━━ ✦ 아이템 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            //Console.WriteLine($"  ━━━━━ ✦ 무  기 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            //SceneManager.Instance.ShowShop(VIEW_TYPE.PURCHASE, ITEM_TYPE.WEAPON);
            //Console.WriteLine($"  ━━━━━ ✦ 방어구 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            //SceneManager.Instance.ShowShop(VIEW_TYPE.PURCHASE, ITEM_TYPE.ARMOR);
            //Console.WriteLine($"  ━━━━━ ✦ 소모품 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Console.WriteLine("\t-1. << 이전\t-2. 다음 >>\n");
            Console.WriteLine($"\t1 ~ {DataManager.Instance.ItemDB.List.Count}. 아이템 구매\n");
            Console.WriteLine("\t0. 상점으로");
            Console.WriteLine("\t-3. 판매 화면으로\n\n");
        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
            {
                SceneManager.Instance.SysDefault();
                SceneManager.Instance.TabPage = 0;
                return VIEW_TYPE.SHOP;
            }
            else if (input == -1)
            {
                SceneManager.Instance.TabPage--;
                if (SceneManager.Instance.TabPage < 0)
                {
                    SceneManager.Instance.TabPage = 0;
                    SceneManager.Instance.SysText("첫 페이지입니다", ConsoleColor.Red, ConsoleColor.Black);
                }
                else
                {
                    SceneManager.Instance.SysDefault();
                }
                return VIEW_TYPE.PURCHASE;
            }
            else if (input == -2)
            {
                SceneManager.Instance.TabPage++;
                if (SceneManager.Instance.TabPage > 1)
                {
                    SceneManager.Instance.TabPage = 1;
                    SceneManager.Instance.SysText("마지막 페이지입니다", ConsoleColor.Red, ConsoleColor.Black);
                }
                else
                {
                    SceneManager.Instance.SysDefault();
                }
                return VIEW_TYPE.PURCHASE;
            }
            else if (input == -3)
            {
                SceneManager.Instance.SysDefault();
                SceneManager.Instance.TabPage = 0;
                return VIEW_TYPE.SALE;
            }
            else if (input > 0 && input <= DataManager.Instance.ItemDB.List.Count)
            {
                // 인벤토리에 아이템 존재 여부
                if (!GameManager.Instance.Player.CheckBag(input - 1))
                {
                    // 잔금 여부
                    if (GameManager.Instance.Player.Gold >= int.Parse(DataManager.Instance.ItemDB.List[input - 1][7]))
                    {
                        // 인벤토리에 아이템 추가
                        GameManager.Instance.Player.InputBag(int.Parse(DataManager.Instance.ItemDB.List[input - 1][0]), VIEW_TYPE.PURCHASE);
                        SceneManager.Instance.SysText($"{DataManager.Instance.ItemDB.List[input - 1][1]} 을(를) 구매 했습니다", ConsoleColor.DarkCyan, ConsoleColor.Black);
                    }
                    // 구매실패 (잔금 부족)
                    else
                    {
                        SceneManager.Instance.SysText("Gold가 부족합니다", ConsoleColor.Red, ConsoleColor.Black);
                    }
                }
                // 구매실패 (보유중인 물품)
                else
                {
                    SceneManager.Instance.SysText("이미 구매한 아이템입니다", ConsoleColor.Red, ConsoleColor.Black);
                }
                return VIEW_TYPE.PURCHASE;
            }
            else
            {
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
            SceneManager.Instance.ColText("    『상점 - 판매』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 필요없는 아이템을 골드를 받고 판매합니다.\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);

            Console.Write("\t\t\t\t\t소지금 : ");
            SceneManager.Instance.ColText($"{Player.Gold}", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine(" G");

            Console.WriteLine($"  ━━━━━ ✦ 아이템 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            //Console.WriteLine($"  ━━━━━ ✦ 무  기 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            SceneManager.Instance.ShowShopSale();
            //Console.WriteLine($"  ━━━━━ ✦ 방어구 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            //SceneManager.Instance.ShowShopSale();
            //Console.WriteLine($"  ━━━━━ ✦ 소모품 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Console.WriteLine($"\t1 ~ {GameManager.Instance.Player.Bag.Count}. 아이템 판매\n");
            Console.WriteLine("\t0. 상점으로");
            Console.WriteLine("\t-1. 구매 화면으로\n\n");
        }

        // NextView 메서드 구현
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
            {
                SceneManager.Instance.SysDefault();
                return VIEW_TYPE.SHOP;
            }
            else if (input == -1)
            {
                SceneManager.Instance.SysDefault();
                return VIEW_TYPE.PURCHASE;
            }
            else if (input > 0 && input <= Player.Bag.Count)
            {
                // 인벤토리에 아이템 존재 여부
                if (GameManager.Instance.Player.CheckBag(GameManager.Instance.Player.Bag[input - 1]))
                {
                    SceneManager.Instance.SysText($"{DataManager.Instance.ItemDB.List[Player.Bag[input - 1]][1]} 을(를) 판매 했습니다", ConsoleColor.DarkCyan, ConsoleColor.Black);

                    Player.RemoveBag(int.Parse(DataManager.Instance.ItemDB.List[Player.Bag[input - 1]][0]), VIEW_TYPE.SALE);

                }
                // 판매실패 (보유중이지 않은 물품)
                else
                {
                    SceneManager.Instance.SysText("존재하지 않는 아이템입니다", ConsoleColor.Red, ConsoleColor.Black);
                }
                return VIEW_TYPE.SALE;
            }
            else
            {
                return VIEW_TYPE.SALE;
            }
        }
    }


    public class DungeonSelectViewer : Viewer
    {
        public DungeonSelectViewer()
        {
            StartIndex = 0;
            EndIndex = DataManager.Instance.DungeonDB.List.Count;
        }
        public override void ViewAction()
        {
            SceneManager.Instance.ColText("    『던전 입구』", ConsoleColor.Magenta, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 던전을 선택하여 진입합니다.\n\n", ConsoleColor.DarkMagenta, ConsoleColor.Black);

            Console.WriteLine($"  ━━━━━ ✦ 던  전 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            foreach (var dun in DataManager.Instance.DungeonDB.List)
            {
                Console.WriteLine($"【 {int.Parse(dun[0]) + 1} 】 {dun[1]} \t\t|   {dun[8]}\n");
            }
            Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");


            Console.WriteLine($"\t1 ~ {DataManager.Instance.DungeonDB.List.Count}. 던전 입장\n");
            Console.WriteLine("\t0. 나가기\n\n");
        }

        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 0:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.MAIN;  // 메인 화면으로 이동
                case 1:
                case 2:
                case 3:
                case 4:
                    SceneManager.Instance.SysDefault();
                    GameManager.Instance.Dungeon = GameManager.Instance.DungeonFactroy(input - 1);
                    return VIEW_TYPE.DUNGEON; // 던전 화면으로 돌아가기
                default:
                    return VIEW_TYPE.DUNGEON; // 기본적으로 던전 화면 유지
            }
        }
    }


    public class DungeonViewer : Viewer
    {
        public DungeonViewer()
        {
            StartIndex = 0;
            EndIndex = 2;
        }

        public override void ViewAction()
        {
            SceneManager.Instance.ColText("    『던전 정보』", ConsoleColor.Magenta, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 던전입장 전 정보를 확인합니다.\n\n", ConsoleColor.DarkMagenta, ConsoleColor.Black);

            Console.WriteLine($"  ━━━━━ ✦ 정  보 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            if (Dungeon.Diff == DUNGEON_DIFF.Hell)
            {
                //SceneManager.Instance.ColText("\t\t!!!!!!!!!!!!!!!!! WARNING !!!!!!!!!!!!!!!!!\n\t\t     플레이어 능력치가 10% 감소합니다!     \n\t\t!!!!!!!!!!!!!!!!! WARNING !!!!!!!!!!!!!!!!!\n",ConsoleColor.Red,ConsoleColor.Black);
                SceneManager.Instance.ColText("\t\t!!!!!!!!!!!!!!!!! WARNING !!!!!!!!!!!!!!!!!\n\t\t              기저귀를 착용하세요!!            \n\t\t!!!!!!!!!!!!!!!!! WARNING !!!!!!!!!!!!!!!!!\n", ConsoleColor.Red, ConsoleColor.Black);
            }

            Console.WriteLine($"\t던전 이름            : {Dungeon.Name}\n");
            Console.WriteLine($"\t난이도               : {Dungeon.Diff}\n");
            Console.WriteLine($"\t등장 몬스터 레벨     : {Dungeon.LowLevel} ~ {Dungeon.HighLevel}\n");
            Console.WriteLine($"\t기본 보상 골드       : {Dungeon.Reward} G\n");
            Console.WriteLine($"\t기본 보상 경험치     : {Dungeon.Exp}Exp\n");
            Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");


            Console.WriteLine("\t1. 전투 시작");
            Console.WriteLine("\t2. 던전 재선택");
            Console.WriteLine("\n\t0. 마을로 돌아가기\n\n");
        }

        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 1:
                    // 전투 몬스터 등록
                    //GameManager.Instance.BattleEnemy = monster; // 몬스터 설정
                    Dungeon.Enter();
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.BATTLE_PLAYER;  // 전투 화면으로 이동
                case 2:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.DUNGEON_SELECT; // 던전 선택화면으로 돌아가기
                case 0:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.MAIN; // 메인 화면으로 돌아가기
                default:
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
            {
                SceneManager.Instance.SysDefault();
                return VIEW_TYPE.DUNGEON_RESULT;
            }
            else
            {
                SceneManager.Instance.SysDefault();
                return VIEW_TYPE.BATTLE_PLAYER;
            }

        }
    }


    public class BattlePlayerViewer : Viewer
    {
        public BattlePlayerViewer()
        {
            StartIndex = 0;
            EndIndex = Dungeon.MonsterCount;
            Dungeon.Turn++;
        }
        public override void ViewAction()
        {
            SceneManager.Instance.ColText($"    『{Dungeon.Name}』", ConsoleColor.Magenta, ConsoleColor.Black);
            SceneManager.Instance.ColText($" {Dungeon.Text}\n\n", ConsoleColor.DarkMagenta, ConsoleColor.Black);

            SceneManager.Instance.ColText($" >> TURN [{Dungeon.Turn}]  Battle!!!\n", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine($"  ━━━━━ ✦ 몬스터 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            int count = 1;
            foreach (var monster in Dungeon.Dungeon_Monster)
            {
                if (monster.State == MONSTER_STATE.DEAD)
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write($"\t【 {count++} 】 ");
                monster.View_Monster_Status();
                Console.WriteLine();
            }
            Console.WriteLine("");
            Console.WriteLine($"  ━━━━━ ✦ 플레이어 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            ViewStatusDun();
            Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Console.WriteLine($"\t1~{count - 1}. 공격 대상 선택(번호 입력)");
            Console.WriteLine("\n\t0. 도망\n\n");

        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
            {
                SceneManager.Instance.SysText("☆★☆★☆ 당신은 한심하게 빤스런을 했습니다 ☆★☆★☆", ConsoleColor.Red, ConsoleColor.Black);
                return VIEW_TYPE.MAIN;
            }
            else if (input > 0 && input <= Dungeon.Dungeon_Monster.Count)
            {

                //행동 선택 뷰가 들어갈 자리

                // 해당 몬스터가 죽은 상태라면
                if (Dungeon.Dungeon_Monster[input - 1].State == MONSTER_STATE.DEAD)
                {
                    SceneManager.Instance.SysText("이미 싸늘한 상태입니다... 시체를 배려해주세요", ConsoleColor.Red, ConsoleColor.Black);

                    return VIEW_TYPE.BATTLE_PLAYER;
                }
                // 해당 몬스터가 죽지 않았다면 대미지 처리 화면으로 이동
                else
                {
                    Dungeon.TargetMonsterIndex = input - 1;
                    Dungeon.TargetMonster = Dungeon.Dungeon_Monster[Dungeon.TargetMonsterIndex];
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.CHOOSE_BEHAVIOR;
                }
            }
            else
            {
                return VIEW_TYPE.BATTLE_PLAYER;
            }
        }
    }

    public class ChooseBehaviorViewer : Viewer
    {
        public ChooseBehaviorViewer()
        {
            StartIndex = 0;
            EndIndex = Character.ChooseBehavior.Count;
        }

        public override void ViewAction()
        {
            SceneManager.Instance.ColText($"    『{Dungeon.Name}』", ConsoleColor.Magenta, ConsoleColor.Black);
            SceneManager.Instance.ColText($" {Dungeon.Text}\n\n", ConsoleColor.DarkMagenta, ConsoleColor.Black);

            SceneManager.Instance.ColText($"  >> TURN [{Dungeon.Turn}]  Target Selected!!!\n", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine($"  ━━━━━ ✦ 몬스터 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            int count = 1;
            for (int i = 0; i < Dungeon.Dungeon_Monster.Count; i++)
            {
                if (i == Dungeon.TargetMonsterIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"    >>  【 {count++} 】 ");
                    Dungeon.Dungeon_Monster[i].View_Monster_Status();
                    Console.ResetColor();
                    ViewGuage10(Dungeon.Dungeon_Monster[i].Hp, Dungeon.Dungeon_Monster[i].MaxHp, ConsoleColor.Red);
                    Console.WriteLine();
                }
                else
                {
                    Console.Write($"\t【 {count++} 】 ");
                    Dungeon.Dungeon_Monster[i].View_Monster_Status();
                    Console.WriteLine();
                }
            }
            Console.WriteLine("");
            Console.WriteLine($"  ━━━━━ ✦ 플레이어 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            ViewStatusDun();
            Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Console.Write("\t1. 기본공격");
            if (Character.Mp < 10)
            {
                SceneManager.Instance.ColText("\n\t2. 스킬\n", ConsoleColor.Red, ConsoleColor.Black);
            }
            else
                Console.WriteLine("\n\t2. 스킬\n");
            Console.WriteLine("\n\t0. 대상 재선택\n\n");
        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
            {
                SceneManager.Instance.SysDefault();
                return VIEW_TYPE.BATTLE_PLAYER;
            }
            else if (input > 0 && input <= Character.ChooseBehavior.Count)
            {
                if (input == 1)
                {
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.BATTLE_PLAYER_LOG;
                }
                else
                {
                    if (Character.Mp < 10)
                    {
                        SceneManager.Instance.SysText("MP가 부족하여 스킬을 사용 할 수 없습니다", ConsoleColor.Red, ConsoleColor.Black);
                        return VIEW_TYPE.CHOOSE_BEHAVIOR;
                    }
                    else
                    {
                        SceneManager.Instance.SysDefault();
                        return VIEW_TYPE.BATTLE_PLAYER_SKILL_LOG;
                    }
                }
            }
            else
            {
                return VIEW_TYPE.CHOOSE_BEHAVIOR;
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
            int attackDamage = Character.DefaultAttack();

            int rand = GameManager.Instance.Rand.Next(0, 10);
            switch (GameManager.Instance.Player.Character.Code)
            {
                case CHAR_TYPE.WARRIOR:
                    if (rand == 0) { GameManager.Instance.Animation.WARRIORATK1(); }
                    else if (rand == 1) { GameManager.Instance.Animation.WARRIORATK2(); }
                    else { GameManager.Instance.Animation.WarriorNomAtk(); }
                    break;
                case CHAR_TYPE.MAGICIAN:

                    if (rand == 0) { GameManager.Instance.Animation.MAGICIANATK1(); }
                    else if (rand == 1) { GameManager.Instance.Animation.MAGICIANATK2(); }
                    else { GameManager.Instance.Animation.MagicianNomAtk(); }
                    break;
                case CHAR_TYPE.ASSASSIN:

                    if (rand == 0) { GameManager.Instance.Animation.ASSASSINATK(); }
                    else { GameManager.Instance.Animation.AssassinNomAtk(); }
                    break;

            }



            SceneManager.Instance.ColText($"    『{Dungeon.Name}』", ConsoleColor.Magenta, ConsoleColor.Black);
            SceneManager.Instance.ColText($" {Dungeon.Text}\n\n", ConsoleColor.DarkMagenta, ConsoleColor.Black);

            SceneManager.Instance.ColText($"  >> TURN [{Dungeon.Turn}]  Battle!!!\n", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine($"  ━━━━━ ✦ 배틀로그 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            Console.WriteLine($"\n\t>> 『{Player.Name}』 의 일반 공격!!");

            Console.WriteLine($"\t>> 『{Dungeon.TargetMonster.Name}』이(가) 『{attackDamage}』의 데미지를 입었습니다.\n");
            Dungeon.TargetMonster.ManageHp(-attackDamage);
            Console.Write($"\t>> HP {Dungeon.TargetMonster.Hp + attackDamage} -> ");
            if (GameManager.Instance.Dungeon.TargetMonster.State == MONSTER_STATE.DEAD)
            {
                Console.WriteLine("Dead");
            }
            else Console.WriteLine($"{Dungeon.TargetMonster.Hp}\n");
            Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Dungeon.SaveLog($"\t- TURN[{Dungeon.Turn}]  |  {Player.Name}의 일반 공격으로 {Dungeon.TargetMonster.Name} 이(가) {attackDamage}의 데미지를 받음 ");

            Console.WriteLine("\n\t>> 다음(Enter)");
            Console.ReadKey();
            VIEW_TYPE nextView = NextView(0);
            SceneManager.Instance.SwitchScene(nextView);
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
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.BATTLE_ENEMY;
                default:
                    return VIEW_TYPE.BATTLE_PLAYER_LOG;
            }
        }
    }

    public class BattlePlayerSkillLogViewer : Viewer
    {
        public BattlePlayerSkillLogViewer()
        {
            StartIndex = 0;
            EndIndex = 0;
        }
        public override void ViewAction()
        {
            int skillDamage = Character.ActiveSkill();


            switch (GameManager.Instance.Player.Character.Code)
            {
                case CHAR_TYPE.WARRIOR:
                    GameManager.Instance.Animation.WarriorAnimation();
                    break;
                case CHAR_TYPE.MAGICIAN:
                    GameManager.Instance.Animation.MagicianAnimation();

                    break;
                case CHAR_TYPE.ASSASSIN:
                    GameManager.Instance.Animation.AssassinAnimation();

                    break;

            }
            SceneManager.Instance.ColText($"    『{Dungeon.Name}』", ConsoleColor.Magenta, ConsoleColor.Black);
            SceneManager.Instance.ColText($" {Dungeon.Text}\n\n", ConsoleColor.DarkMagenta, ConsoleColor.Black);

            SceneManager.Instance.ColText($"  >> TURN [{Dungeon.Turn}]  Battle!!!\n", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.WriteLine($"  ━━━━━ ✦ 배틀로그 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            Console.WriteLine($"\n\t>> 『{Player.Name}』 의 스킬 공격!!");
            Console.Write($"\t>> 『");
            GameManager.Instance.SceneManager.ColText($"{Character.ActskillName}", ConsoleColor.Blue, ConsoleColor.Cyan);
            Console.WriteLine($"』!!!");

            // 『효빈』애니메이션 출력 위치 바꾸고 추후 어새신:캐릭터 쪽으로 이동시켜야함 {연격 스킬}
            int totalDamage = 0;
            if (Character.Code == CHAR_TYPE.ASSASSIN)
            {

                Random rand = new Random();
                for (int i = 0; i < 2; i++)
                {
                    Console.Write("\t>> ");
                    int criticalHit = rand.Next(0, 10);
                    if (criticalHit <= 2) //크리티컬 확률 계산
                    {
                        SceneManager.Instance.ColText("[치명타] ", ConsoleColor.Red, ConsoleColor.Black);
                        skillDamage *= 2;
                    }

                    Console.WriteLine($"『{Dungeon.TargetMonster.Name}』이(가) 『{skillDamage}』의 데미지를 입었습니다.");
                    Dungeon.TargetMonster.ManageHp(-skillDamage);
                    Console.Write($"\t>> HP {Dungeon.TargetMonster.Hp + skillDamage} -> ");
                    if (GameManager.Instance.Dungeon.TargetMonster.State == MONSTER_STATE.DEAD)
                    {
                        Console.WriteLine("Dead");
                        break;
                    }
                    else Console.WriteLine($"{Dungeon.TargetMonster.Hp}\n");
                    totalDamage += skillDamage;
                    skillDamage /= 2;
                }
            }// 여기까지가 연격 구현
            else
            {
                Console.WriteLine($"\t>> 『{Dungeon.TargetMonster.Name}』이(가) 『{skillDamage}』의 데미지를 입었습니다.");
                Dungeon.TargetMonster.ManageHp(-skillDamage);
                Console.Write($"\t>> HP {Dungeon.TargetMonster.Hp + skillDamage} -> ");
                if (GameManager.Instance.Dungeon.TargetMonster.State == MONSTER_STATE.DEAD)
                {
                    Console.WriteLine("Dead");
                }
                else Console.WriteLine($"{Dungeon.TargetMonster.Hp}\n");
            }
            Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            if (Character.Code == CHAR_TYPE.ASSASSIN)
                Dungeon.SaveLog($"\t- TURN[{Dungeon.Turn}]  |  {Player.Name}의 스킬 공격으로 {Dungeon.TargetMonster.Name} 이(가) {totalDamage}의 데미지를 받음 ");
            else
                Dungeon.SaveLog($"\t- TURN[{Dungeon.Turn}]  |  {Player.Name}의 스킬 공격으로 {Dungeon.TargetMonster.Name} 이(가) {skillDamage}의 데미지를 받음 ");

            Console.WriteLine("\n\t>> 다음(Enter)");
            Console.ReadKey();
            VIEW_TYPE nextView = NextView(0);
            SceneManager.Instance.SwitchScene(nextView);
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
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.BATTLE_ENEMY;
                default:
                    return VIEW_TYPE.BATTLE_PLAYER_SKILL_LOG;
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
            if (Dungeon.Dungeon_Monster[Dungeon.MonsterAtkCounter].State == MONSTER_STATE.IDLE)
            {
                Dungeon.Dungeon_Monster[Dungeon.MonsterAtkCounter].DefaultAttack();
                SceneManager.Instance.SysDefault();
                SceneManager.Instance.ColText($"    『{Dungeon.Name}』", ConsoleColor.Magenta, ConsoleColor.Black);
                SceneManager.Instance.ColText($" {Dungeon.Text}\n\n", ConsoleColor.DarkMagenta, ConsoleColor.Black);

                SceneManager.Instance.ColText($"  >> TURN [{Dungeon.Turn}]  Battle!!!\n", ConsoleColor.Yellow, ConsoleColor.Black);
                Console.WriteLine($"  ━━━━━ ✦ 배틀로그 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
                Console.WriteLine($"\n\t>> 『{Dungeon.Dungeon_Monster[Dungeon.MonsterAtkCounter].Name}』 의 공격!!");
                int attackDamage = 0;
                if (Character.CheckDodge())
                {
                    SceneManager.Instance.ColText($"\t>> 『{Player.Name}』이(가) 공격을 회피했습니다!\n", ConsoleColor.Green, ConsoleColor.Black);
                    attackDamage = 0;
                }
                else
                {
                    attackDamage = Dungeon.Dungeon_Monster[Dungeon.MonsterAtkCounter].Atk - Character.Defence;
                    if (attackDamage <= 0) attackDamage = 1;
                    Console.WriteLine($"\t>> 『{Player.Name}』이(가) 『{attackDamage}』의 데미지를 입었습니다.\n");
                    Character.ManageHp(-attackDamage);
                    Console.Write($"\t>> HP {Character.Hp+attackDamage} -> ");
                    if (Character.State == CHAR_STATE.DEAD)
                    {
                        Console.WriteLine("Dead");
                    }
                    else Console.WriteLine($"{Character.Hp}\n");
                }

                Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

                if (Character.CheckDodge())
                    Dungeon.SaveLog($"\t- TURN[{Dungeon.Turn}]  |  {Dungeon.Dungeon_Monster[Dungeon.MonsterAtkCounter].Name}의 일반 공격을 {Player.Name} 이(가) 회피함 ");
                Dungeon.SaveLog($"\t- TURN[{Dungeon.Turn}]  |  {Dungeon.Dungeon_Monster[Dungeon.MonsterAtkCounter].Name}의 일반 공격으로 {Player.Name} 이(가) {attackDamage}의 데미지를 받음 ");



                Console.WriteLine("\n\t>> 다음(Enter)");
                Console.ReadKey();

                if (Character.State == CHAR_STATE.DEAD)
                {
                    Animation.GameOverAnimation();
                }

                Dungeon.MonsterAtkCounter += 1;
                VIEW_TYPE nextView = NextView(0);
                //VIEW_TYPE nextView = NextView(SceneManager.Instance.InputAction(StartIndex, EndIndex, Console.CursorTop));
                SceneManager.Instance.SwitchScene(nextView);
            }
            // 공격 할 수 없는 상태라면 바로 다음 화면으로 전환
            else
            {
                Dungeon.MonsterAtkCounter += 1;
                SceneManager.Instance.SwitchScene(NextView(0));
            }
        }

        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
            {
                // 공격할 몬스터가 남았다면 몬스터 공격화면을 계속 출력
                if (GameManager.Instance.Dungeon.MonsterAtkCounter < GameManager.Instance.Dungeon.Dungeon_Monster.Count)
                {
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.BATTLE_ENEMY;
                }
                // 몬스터의 공격이 끝났다. <<<<<<<여기 이어서 작성
                else
                {
                    return VIEW_TYPE.BATTLE;
                }
            }
            else
            {
                return VIEW_TYPE.BATTLE_ENEMY;
            }
        }
    }


    public class DungeonResultViewer : Viewer
    {
        public DungeonResultViewer()
        {
            StartIndex = 0;
            EndIndex = 1;
        }
        public override void ViewAction()
        {
            if (Character.Hp <= 0)
            {
                GameManager.Instance.Animation.GameOverAnimation();
            }
            else
            {
                SceneManager.Instance.ColText("    『던전 결과』", ConsoleColor.Cyan, ConsoleColor.Black);
                SceneManager.Instance.ColText(" 던전 플레이 결과를 출력합니다.\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);

                Console.WriteLine($"  ━━━━━ ✦ 결  과 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

                SceneManager.Instance.ColText("\t\t\t☆ 축하합니다! 던전을 클리어했습니다 ☆\n\n", ConsoleColor.Yellow, ConsoleColor.Black);

                Console.WriteLine($"\t보상 골드       : {Dungeon.Reward} G\n");
                foreach (var monster in Dungeon.Dungeon_Monster)
                {
                    Console.WriteLine($"\t\t\t\t+{monster.RewardGold} G …………{monster.Name} 토벌 추가 보상");
                    Dungeon.Reward += monster.RewardGold;
                }
                Console.WriteLine($"\t--------------------------------------------------------------------------\n");
                SceneManager.Instance.ColText($"\t\t\t\t\t\t\t최종 보상 골드   :  {Dungeon.Reward} G\n\n", ConsoleColor.Green, ConsoleColor.Black);

                Console.WriteLine($"\n\t보상 경험치     : {Dungeon.Exp} Exp\n");
                foreach (var monster in Dungeon.Dungeon_Monster)
                {
                    Console.WriteLine($"\t\t\t\t+{monster.RewardExp} Exp …………{monster.Name} 토벌 추가 보상");
                    Dungeon.Exp += monster.RewardExp;
                }
                Console.WriteLine($"\t--------------------------------------------------------------------------\n");
                SceneManager.Instance.ColText($"\t\t\t\t\t\t\t최종 보상 경험치  :  {Dungeon.Exp} Exp\n\n", ConsoleColor.Green, ConsoleColor.Black);


                Player.GetReward(Dungeon.Reward, Dungeon.Exp);
                if (Player.LevelUp() > 0)
                {
                    //Console.WriteLine($"  ━━━━━ ✦ 레벨 업 ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
                }

                //if (dungeon.Diff == DUNGEON_DIFF.Hell)
                Console.WriteLine();
                Console.WriteLine($"  ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            }
            Console.WriteLine("\t1. 던전 입구로 돌아가기");
            Console.WriteLine("\n\t0. 마을로 돌아가기\n\n");
        }

        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 1:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.DUNGEON_SELECT;
                case 0:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.MAIN;
                default:
                    return VIEW_TYPE.DUNGEON_RESULT;
            }
        }
    }
    public class RestViewer : Viewer
    {
        public RestViewer()
        {
            StartIndex = 0;
            EndIndex = 2;
        }
        public override void ViewAction()
        {
            SceneManager.Instance.ColText("    『여관』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 골드를 지불하고 휴식합니다.\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);

            Console.WriteLine("\n\t━━━━━ ✦  능력치  ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━\n");
            ViewStatusRest();
            Console.WriteLine("\n\t━━━━━ ✦  요금표  ✦ ━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Console.Write("\n\t【 1 】 대실    | ");
            SceneManager.Instance.ColText("500", ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.WriteLine(" G    |  HP +50    |  MP +20");

            Console.Write("\n\t【 2 】 숙박    | ");
            SceneManager.Instance.ColText("2000", ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.WriteLine(" G   |  HP +FULL  |  MP +FULL");

            Console.WriteLine("\n\t━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

            Console.WriteLine("\t1. 대실");
            Console.WriteLine("\t2. 숙박");
            Console.WriteLine("\n\t0. 마을로 돌아가기\n\n");
        }

        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();

            switch (input)
            {
                case 1:
                    if (Character.Hp == Character.MaxHp)
                    {
                        SceneManager.Instance.SysText($"이미 최대 체력입니다", ConsoleColor.Green, ConsoleColor.Black);
                    }
                    else
                    {
                        if (Player.Gold >= 500)
                        {
                            Character.ManageHp(50);
                            Character.ManageMp(50);
                            Player.Gold -= 500;
                            SceneManager.Instance.SysText($"잠시동안 휴식을 취했습니다", ConsoleColor.DarkCyan, ConsoleColor.Black);
                        }
                        else
                            SceneManager.Instance.SysText($"Gold가 부족합니다", ConsoleColor.DarkCyan, ConsoleColor.Black);
                    }

                    return VIEW_TYPE.REST;
                case 2:
                    if (Character.Hp == Character.MaxHp)
                    {
                        SceneManager.Instance.SysText($"이미 최대 체력입니다", ConsoleColor.Green, ConsoleColor.Black);
                    }
                    else
                    {
                        if (Player.Gold >= 2000)
                        {
                            Character.ManageHp(Character.MaxHp);
                            Character.ManageMp(Character.MaxMp);
                            Player.Gold -= 2000;
                            SceneManager.Instance.SysText($"오랫동안 휴식을 취했습니다", ConsoleColor.DarkCyan, ConsoleColor.Black);
                        }
                        else
                            SceneManager.Instance.SysText($"Gold가 부족합니다", ConsoleColor.DarkCyan, ConsoleColor.Black);
                    }
                    return VIEW_TYPE.REST;
                case 0:
                    SceneManager.Instance.SysDefault();
                    return VIEW_TYPE.MAIN;
                default:
                    return VIEW_TYPE.REST;
            }
        }
    }
    //잔혹하고도 냉정한 뒷골목입니다.
    public class BackStreetViewer : Viewer
    {
        public BackStreetViewer()
        {
           
            StartIndex = 0;
            EndIndex = GameManager.Instance.NonePlayableCharacter.NPC.Count;
        }
        public override void ViewAction()
        {
            SceneManager.Instance.ColText("    『뒷골목』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText(" 사람들과 대화를 나눌 수 있습니다..\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.WriteLine("\t━━━━━ ✦ N P C ✦ ━━━━━━━━━━━━━━━━━━━━\n");
            Console.WriteLine("\t【 1 】 노인\n\t【 2 】 소녀\n\t【 3 】 대장장이\n\t【 4 】 수상한 사람");


            Console.WriteLine("\n\t>> 0. 돌아가기\n\n");
        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
            {
                SceneManager.Instance.SysText("마을로 돌아갑니다.", ConsoleColor.Red, ConsoleColor.Black);
                return VIEW_TYPE.MAIN;
            }
            else if (input > 0 && input <= GameManager.Instance.NonePlayableCharacter.NPC.Count)
            {
                GameManager.Instance.NonePlayableCharacter.Code = (NPC_TYPE)(input - 1);

                //Qcod를 이용해 다음 Viewer에서 각기 다르게 출력하기 위해 선택지마다 맞는 enum값을 Qcode에 저장해 줍니다.
                switch (GameManager.Instance.NonePlayableCharacter.Code)
                {
                    case NPC_TYPE.OLDMAN:
                        if (!GameManager.Instance.OldMan.IsQuest)//퀘스트를 받았는지 확인할 불리언값 속성 IsQuest 초기값 false
                        {
                            GameManager.Instance.NonePlayableCharacter = GameManager.Instance.OldMan;
                           
                            GameManager.Instance.NonePlayableCharacter.QCode = QUEST_TYPE.OLDMAN;
                            return VIEW_TYPE.QUEST;
                        }
                        else if (GameManager.Instance.OldMan.IsQuest)
                        {
                            GameManager.Instance.NonePlayableCharacter = GameManager.Instance.OldMan;
                            GameManager.Instance.NonePlayableCharacter.QCode = QUEST_TYPE.OLDMAN;
                            return VIEW_TYPE.CHECK_QUEST;
                        }
                        else if (GameManager.Instance.OldMan.CompletQuest)
                        {
                            Console.WriteLine("어디에 갔는지 보이지 않는다.");
                            return VIEW_TYPE.BACK_STREET;
                        }
                        else
                        {
                            return VIEW_TYPE.BACK_STREET;
                        }



                    case NPC_TYPE.YOUNGGIRL:
                        if (!GameManager.Instance.YoungGirl.IsQuest)
                        {
                            GameManager.Instance.NonePlayableCharacter = GameManager.Instance.YoungGirl;
                            GameManager.Instance.NonePlayableCharacter.QCode = QUEST_TYPE.YOUNGGIRL;
                            return VIEW_TYPE.QUEST;
                        }
                        else if (GameManager.Instance.YoungGirl.IsQuest)
                        {
                            GameManager.Instance.NonePlayableCharacter = GameManager.Instance.YoungGirl;
                            GameManager.Instance.NonePlayableCharacter.QCode = QUEST_TYPE.YOUNGGIRL;
                            return VIEW_TYPE.CHECK_QUEST;
                        }
                        else if (GameManager.Instance.YoungGirl.CompletQuest)
                        {
                            Console.WriteLine("어디에 갔는지 보이지 않는다.");
                            return VIEW_TYPE.BACK_STREET;
                        }
                        else
                        {
                            return VIEW_TYPE.BACK_STREET;
                        }

                    case NPC_TYPE.BLACKSMITH:
                        if (!GameManager.Instance.BlackSmith.IsQuest)
                        {
                            GameManager.Instance.NonePlayableCharacter = GameManager.Instance.BlackSmith;
                            GameManager.Instance.NonePlayableCharacter.QCode = QUEST_TYPE.BLACKSMITH;
                            return VIEW_TYPE.QUEST;
                        }
                        else if (GameManager.Instance.BlackSmith.IsQuest)
                        {
                            GameManager.Instance.NonePlayableCharacter = GameManager.Instance.BlackSmith;
                            GameManager.Instance.NonePlayableCharacter.QCode = QUEST_TYPE.BLACKSMITH;
                            return VIEW_TYPE.CHECK_QUEST;
                        }
                        else if (GameManager.Instance.BlackSmith.CompletQuest)
                        {
                            Console.WriteLine("어디에 갔는지 보이지 않는다.");
                            return VIEW_TYPE.BACK_STREET;
                        }
                        else
                        {
                            return VIEW_TYPE.BACK_STREET;
                        }
                    
                    case NPC_TYPE.STRANGER:
                        if (!GameManager.Instance.Stranger.IsQuest)
                        {
                            GameManager.Instance.NonePlayableCharacter = GameManager.Instance.Stranger;
                            GameManager.Instance.NonePlayableCharacter.QCode = QUEST_TYPE.STRANGER;
                            return VIEW_TYPE.QUEST;
                        }
                        else if (GameManager.Instance.Stranger.IsQuest)
                        {
                            GameManager.Instance.NonePlayableCharacter = GameManager.Instance.Stranger;
                            GameManager.Instance.NonePlayableCharacter.QCode = QUEST_TYPE.STRANGER;
                            return VIEW_TYPE.CHECK_QUEST;
                        }
                        else if (GameManager.Instance.Stranger.CompletQuest)
                        {
                            Console.WriteLine("어디에 갔는지 보이지 않는다.");
                            return VIEW_TYPE.BACK_STREET;
                        }
                        else
                        {
                            return VIEW_TYPE.BACK_STREET;
                        }

                    default:
                                return VIEW_TYPE.BACK_STREET;

                            }

            }
            else
            {
                return VIEW_TYPE.BACK_STREET;
            }
        }
    }

    public class QuestViewer : Viewer //NPC가 등장해 퀘스트 문구를 보여주는 Viewer
    {
        string npcname = string.Empty; //NPC 이름을 QCode를이용해 알맞게 저장하기위한 빈 string
        string nopequest = string.Empty;//퀘스트 거절 시 나올 문구를 NPC에 맞게 저장 하기 위한 빈 string
        public QuestViewer()
        {
            StartIndex = 0;
            EndIndex = 1;
        }
        public override void ViewAction()
        {

            switch (GameManager.Instance.NonePlayableCharacter.QCode)//QCode이용해 각기 string 초기화 해줄 switch문
            {
                case QUEST_TYPE.OLDMAN:
                    GameManager.Instance.NonePlayableCharacter = GameManager.Instance.OldMan;
                    nopequest = GameManager.Instance.OldMan.NopeQuest;
                    npcname = GameManager.Instance.OldMan.Name;
                    break;
                case QUEST_TYPE.YOUNGGIRL:
                    GameManager.Instance.NonePlayableCharacter = GameManager.Instance.YoungGirl;
                    nopequest = GameManager.Instance.YoungGirl.NopeQuest;
                    npcname = GameManager.Instance.YoungGirl.Name;
                    break;
                case QUEST_TYPE.BLACKSMITH:
                    GameManager.Instance.NonePlayableCharacter = GameManager.Instance.BlackSmith;
                    nopequest = GameManager.Instance.BlackSmith.NopeQuest;
                    npcname = GameManager.Instance.BlackSmith.Name;
                    break;
                case QUEST_TYPE.STRANGER:
                    GameManager.Instance.NonePlayableCharacter = GameManager.Instance.Stranger;
                    nopequest = GameManager.Instance.Stranger.NopeQuest;
                    npcname = GameManager.Instance.Stranger.Name;
                    break;

            }


            SceneManager.Instance.ColText($"    『{npcname}』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText($" {npcname}이 말을 걸어옵니다....\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.WriteLine("\t━━━━━ ✦ N P C ✦ ━━━━━━━━━━━━━━━━━━━━\n");
            Console.WriteLine("\t【 1 】 수락하기");


            Console.WriteLine("\n\t>> 0. 거절하고 면박주기\n\n");
        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
            {
                SceneManager.Instance.SysText(nopequest, ConsoleColor.Red, ConsoleColor.Black);
                return VIEW_TYPE.BACK_STREET;
            }
            else if (input > 0 && input <= 1)
            {
                Console.WriteLine($"{GameManager.Instance.NonePlayableCharacter.QuestDialog}");
                GameManager.Instance.OldMan.IsQuest = true; //퀘스트 수락시 IsQuest true로 저장
                Console.ReadLine();
                return VIEW_TYPE.BACK_STREET;
            }
            else
            {
                return VIEW_TYPE.BACK_STREET;
            }
        }
    }

    public class CheckQuestViewer : Viewer
    {
        string npcname = string.Empty; 
        bool questcomplete;
        bool questgoals;
        string questfinished = string.Empty; //퀘스트 완료 문구 저장
        string questunfinished = string.Empty;//퀘스트 미완 문구 저장
        public CheckQuestViewer()
        {
            StartIndex = 0;
            EndIndex = 0;
        }
        public override void ViewAction()
        {

            switch (GameManager.Instance.NonePlayableCharacter.QCode)
            {
                case QUEST_TYPE.OLDMAN:
                    npcname = GameManager.Instance.OldMan.Name;
                    questcomplete = GameManager.Instance.OldMan.CompletQuest;
                    questgoals = GameManager.Instance.OldMan.QuestGoals;
                    questfinished = GameManager.Instance.OldMan.QuestFinished;
                    questunfinished = GameManager.Instance.OldMan.QuestUnFinished;
                    break;
                case QUEST_TYPE.YOUNGGIRL:
                    npcname = GameManager.Instance.YoungGirl.Name;
                    questcomplete = GameManager.Instance.YoungGirl.CompletQuest;
                    questgoals = GameManager.Instance.YoungGirl.QuestGoals;
                    questfinished = GameManager.Instance.YoungGirl.QuestFinished;
                    questunfinished = GameManager.Instance.YoungGirl.QuestUnFinished;
                    break;
                case QUEST_TYPE.BLACKSMITH:
                    npcname = GameManager.Instance.BlackSmith.Name;
                    questcomplete = GameManager.Instance.BlackSmith.CompletQuest;
                    questgoals = GameManager.Instance.BlackSmith.QuestGoals;
                    questfinished = GameManager.Instance.BlackSmith.QuestFinished;
                    questunfinished = GameManager.Instance.BlackSmith.QuestUnFinished;
                    break;
                case QUEST_TYPE.STRANGER:
                    npcname = GameManager.Instance.Stranger.Name;
                    questcomplete = GameManager.Instance.Stranger.CompletQuest;
                    questgoals = GameManager.Instance.Stranger.QuestGoals;
                    questfinished = GameManager.Instance.Stranger.QuestFinished;
                    questunfinished = GameManager.Instance.Stranger.QuestUnFinished;
                    break;

            }


            SceneManager.Instance.ColText("    『"+npcname+"』", ConsoleColor.Cyan, ConsoleColor.Black);
            SceneManager.Instance.ColText($" 어디보자....\n\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.WriteLine("\t━━━━━ ✦ N P C ✦ ━━━━━━━━━━━━━━━━━━━━\n");



            Console.WriteLine("\n\t>> 0. 퀘스트 검사 받기\n\n");
        }
        public override VIEW_TYPE NextView(int input)
        {
            Console.Clear();
            if (input == 0)
            {
                if (questgoals)
                {
                    Console.WriteLine(questfinished);
                    questcomplete = true;
                    return VIEW_TYPE.BACK_STREET;
                }
                else 
                {
                    Console.WriteLine(questunfinished);
                    Console.ReadLine();
                    return VIEW_TYPE.BACK_STREET;
                }
            }
          
            else
            {
                return VIEW_TYPE.CHECK_QUEST;
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






