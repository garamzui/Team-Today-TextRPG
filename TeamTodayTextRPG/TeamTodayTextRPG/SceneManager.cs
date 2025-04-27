using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;
using TeamTodayTextRPG;

namespace TeamTodayTextRPG
{
    public class SceneManager
    {
        public Viewer CurrentViewer { get; set; }

        private static readonly Lazy<SceneManager> lazyInstance = new Lazy<SceneManager>(() => new SceneManager());
        public static SceneManager Instance => lazyInstance.Value;

        public SceneManager() 
        {
            CurrentViewer = new MainViewer();
        }


        // 매개변수 : 메세지 , 글자색, 배경색
        public void ColText(string message, ConsoleColor textColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            ConsoleColor prevBackColor = Console.BackgroundColor;

            Console.BackgroundColor = backColor;
            Console.ForegroundColor = textColor;
            Console.Write($"{message}");
            Console.ForegroundColor = prevColor;
            Console.BackgroundColor = prevBackColor;
        }
       
        
        public void SysText(string message, ConsoleColor textColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
        {
            Clear(0, 2);
            ConsoleColor prevColor = Console.ForegroundColor;
            ConsoleColor prevBackColor = Console.BackgroundColor;

            Console.BackgroundColor = backColor;
            Console.ForegroundColor = textColor;
            Console.Write("=========================================================================================\n");
            Console.Write($">> [SYSTEM] {message}\n");
            Console.Write("=========================================================================================\n");

            Console.ForegroundColor = prevColor; 
            Console.BackgroundColor = prevBackColor;
        }
        public void SysDefault()
        {
            SysText("\t\t\t\t\t【Sparta Text RPG  _Team Today Present】", ConsoleColor.Yellow, ConsoleColor.Black);
        }

        // linesToClear 부터 lineCount 까지 삭제
        public void Clear(int linesToClear, int lineCount)
        {
            int currentTop = Console.CursorTop;
            int width = Console.WindowWidth;
            
            if (linesToClear < 0) linesToClear = 0; // 최상단 이상 올라가지 않도록 보정

            for (int i = 0; i < lineCount; i++)
            {
                int line = linesToClear + i;
                if (line >= Console.WindowHeight) break; // 콘솔 범위를 벗어나지 않게 보호

                Console.SetCursorPosition(0, line);
                Console.Write(new string(' ', width));
            }
            Console.SetCursorPosition(0, linesToClear); // 
        }


        // 입력받은 숫자만큼, 현재 커서 위로 삭제
        public void ClearAbove(int linesToClear)
        {
            int currentTop = Console.CursorTop;
            int width = Console.WindowWidth;

            int startLine = currentTop - linesToClear;
            if (startLine < 0) startLine = 0; // 최상단 이상 올라가지 않도록 보정

            for (int i = 0; i < linesToClear; i++)
            {
                int line = startLine + i;
                if (line >= Console.WindowHeight) break; // 콘솔 범위를 벗어나지 않게 보호

                Console.SetCursorPosition(0, line);
                Console.Write(new string(' ', width));
            }

            Console.SetCursorPosition(0, startLine); // 지운 마지막 줄에 커서 위치
        }


        public void SwitchScene(VIEW_TYPE viewType)
        {
            // 새로운 뷰어 할당
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
                    
                case VIEW_TYPE.DUNGEON_SELECT:
                    CurrentViewer = new DungeonSelectViewer();
                    break;
                case VIEW_TYPE.DUNGEON:
                    CurrentViewer = new DungeonViewer();
                    break;
                case VIEW_TYPE.DUNGEON_RESULT:
                    CurrentViewer = new DungeonResultViewer();
                    break;

                case VIEW_TYPE.REST:
                    CurrentViewer = new RestViewer();
                    break;
                case VIEW_TYPE.BACK_STREET:
                    CurrentViewer = new BackStreetViewer();
                    break;
                case VIEW_TYPE.QUEST:
                    CurrentViewer = new QuestViewer();
                    break;
                case VIEW_TYPE.CHECK_QUEST:
                    CurrentViewer = new CheckQuestViewer();
                    break;


                case VIEW_TYPE.BATTLE:
                    CurrentViewer = new BattleViewer();
                    break;
                case VIEW_TYPE.BATTLE_PLAYER:
                    CurrentViewer = new BattlePlayerViewer();
                    break;
                case VIEW_TYPE.CHOOSE_BEHAVIOR:
                    CurrentViewer = new ChooseBehaviorViewer();
                    break;
                case VIEW_TYPE.BATTLE_PLAYER_LOG:
                    CurrentViewer = new BattlePlayerLogViewer();
                    break;
                case VIEW_TYPE.BATTLE_PLAYER_SKILL_LOG:
                    CurrentViewer = new BattlePlayerSkillLogViewer();
                    break;
                case VIEW_TYPE.BATTLE_ENEMY:
                    CurrentViewer = new BattleEnemyViewer();
                    break;
                default:
                    Console.WriteLine("error");
                    break;
            }
            ShowCurrentView();
        }

        //『효빈』선택지 입력 시 다음 화면으로의 전환
        public VIEW_TYPE ChangeNextView()
        {
            return CurrentViewer.NextView(InputAction(CurrentViewer.StartIndex, CurrentViewer.EndIndex, Console.CursorTop));
        }

        // 새로운 뷰어의 화면 출력
       public void ShowCurrentView()
        {
            if (CurrentViewer != null)
                CurrentViewer.ViewAction();  // gameManager 객체를 넘기기
        }

        // 『효빈』초기 캐릭터 설정 (플레이어 이름, 플레이어할 캐릭터의 직업)을 도와주는 함수 입니다.
        public void Intro()
        {
            string? name = string.Empty;
            int cursorPos = 0;

            SysText("스파르타 마을에 오신 여러분 환영합니다.", ConsoleColor.Yellow, ConsoleColor.Black);
            while (name == string.Empty)
            {
                Console.WriteLine();
                Clear(4, 10);
                Console.SetCursorPosition(0, 4);
                ColText("\t[이름 설정] 원하시는 이름을 설정해주세요.\n", ConsoleColor.Yellow, ConsoleColor.Black);
                ColText("\t이름 입력 ☞ ", ConsoleColor.White, ConsoleColor.Black);

                name = Console.ReadLine();
                if (name == string.Empty)
                {
                    //cursorPos = Console.CursorTop;
                    SysText("빈칸은 이름으로 사용할 수 없습니다", ConsoleColor.Red, ConsoleColor.Black);
                }
                else
                {
                    SysText("스파르타 마을에 오신 여러분 환영합니다.", ConsoleColor.Yellow, ConsoleColor.Black);
                    bool check = true;
                    Console.SetCursorPosition(0, 7);
                    while (check&&name!=string.Empty)
                    {
                        int num = 0;
                        Console.Write("\n\t입력하신 이름은 『 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(name);
                        Console.ResetColor();
                        Console.WriteLine(" 』입니다.\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\t1. 저장");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t2. 취소\n\n");
                        Console.ResetColor();

                        cursorPos = Console.CursorTop;
                        num = InputAction(1, 2, cursorPos);

                        if (num == 1) check = false;
                        else if (num == 2)
                        {
                            ClearAbove(3);
                            check = false;
                            name = string.Empty;
                        }
                    }
                }
            }
            Console.Clear();
            SysText("스파르타 마을에 오신 여러분 환영합니다.", ConsoleColor.Yellow, ConsoleColor.Black);

            int classCode = 0;
            while (classCode == 0)
            {
                Console.WriteLine();
                ColText("\t[직업 선택] 원하시는 직업을 골라 주세요.\n\n",ConsoleColor.Yellow, ConsoleColor.Black);
                ColText("\t【 1 】 전사     | 높은 방어력, 기본 공격력, 체력\n\n", ConsoleColor.Yellow, ConsoleColor.Black);
                ColText("\t【 2 】 마법사  | 방어 무시, 높은 마나, 스킬의존성\n\n", ConsoleColor.Yellow, ConsoleColor.Black);
                ColText("\t【 3 】 도적     | 높은 회피, 크리티컬 히트\n\n", ConsoleColor.Yellow, ConsoleColor.Black);
                Console.WriteLine();

                cursorPos = Console.CursorTop;
                classCode = InputAction(1, 3, cursorPos);
                GameManager.Instance.Player.SetCharacter(classCode, name);
            }
            Console.Clear();
        }

        /* 『효빈』
            꾸준히 호출될 선택지 입력합수입니다.
            매개변수로 선택지의 첫번째 번호와 마지막 번호를 받습니다. 
            ex) 1.아이템구매 2. 아이템판매 0.나가기 
                ...이라면 startIndex = 0, endIndex = 2
            리턴 값으로는 "고른 선택지의 번호"를 반환합니다.
        */
        public int InputAction(int startIndex, int endIndex, int y)
        {
            string rtnStr = string.Empty;
            int num = -1;
            bool check = false;
            while (!check)
            {
                Console.Write("\t선택지 입력 ☞ ");
                rtnStr = Console.ReadLine();
                if (rtnStr == string.Empty)
                {
                    //Clear(0, 2);
                    SysText("아무 행동도 입력하지 않으셨습니다",ConsoleColor.Red, ConsoleColor.Black);
                    Console.SetCursorPosition(0, y);
                }
                else
                {
                    if (int.TryParse(rtnStr, out num))
                    {
                        if (num < startIndex || num > endIndex)
                        {
                            //Clear(0, 2);
                            SysText("선택지 내에서 입력해주세요", ConsoleColor.Red, ConsoleColor.Black);
                            Console.SetCursorPosition(0, y);
                        }
                        else check = true;
                    }
                    else
                    {
                        //Clear(0, 2);
                        SysText("숫자만 입력해주세요", ConsoleColor.Red, ConsoleColor.Black);
                        Console.SetCursorPosition(0, y);
                    }
                }
            }
            return num;
        }

        public void ShowName(string name)
        {
            Console.Write(name + "\t| ");
        }
        public void ShowAtk(int atk)
        {
            if (atk > 0) Console.Write("\t  공격력 +" + atk + "\t| ");
            else if (atk == 0) Console.Write("\t  공격력 -\t| ");
            else Console.Write("\t  공격력 -" + atk + "\t| ");
        }
        public void ShowDef(int def)
        {
            if (def > 0) Console.Write("\t  방어력 +" + def + "\t| ");
            else if (def == 0) Console.Write("\t  방어력 -\t| ");
            else Console.Write("\t  방어력 -" + def + "\t| ");
        }

        public void ShowInventory(VIEW_TYPE view)
        {
            int count = 0;
            if (GameManager.Instance.Player.Bag != null)
            {
                foreach (var item in GameManager.Instance.Player.Bag)
                {
                    Console.WriteLine("   -----------------------------------------------------------------------------");
                    if (view == VIEW_TYPE.EQUIP) Console.Write($"     -{++count} ");
                    else Console.Write("     -");

                    CurrentViewer.Attach_E_Mark(item);

                    Console.WriteLine(" 『" + DataManager.Instance.ItemDB.List[item][1] + "』");
                    ShowAtk(int.Parse(DataManager.Instance.ItemDB.List[item][2]));
                    ShowDef(int.Parse(DataManager.Instance.ItemDB.List[item][3]));
                    Console.WriteLine("\t  [ " + DataManager.Instance.ItemDB.List[item][6] + " ]");
                }
                Console.WriteLine("   -----------------------------------------------------------------------------");
            }
        }

        public void ShowShop(VIEW_TYPE view)
        {
            if (DataManager.Instance.ItemDB.List != null)
            {
                foreach (var item in DataManager.Instance.ItemDB.List)
                {

                    Console.WriteLine("   -----------------------------------------------------------------------------");
                    if (GameManager.Instance.Player.CheckBag(int.Parse(item[0]))) Console.ForegroundColor = ConsoleColor.DarkGray;

                    string idText = view == VIEW_TYPE.PURCHASE ? "-"+ (int.Parse(item[0]) + 1).ToString() : "-";
                    Console.WriteLine($"     {idText} 『{item[1]}』");
                    ShowAtk(int.Parse(item[2]));
                    ShowDef(int.Parse(item[3]));
                    if (GameManager.Instance.Player.CheckBag(int.Parse(item[0])))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t [구매 완료]");
                        Console.ResetColor();
                    }
                    else Console.WriteLine("\t [" + int.Parse(item[7]) + " G]");
                    if (GameManager.Instance.Player.CheckBag(int.Parse(item[0]))) Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\t  [ "+item[6]+" ]");
                    Console.ResetColor();
                }
                Console.WriteLine("   -----------------------------------------------------------------------------");
            }
        }

        public void ShowShopSale()
        {
            int count = 0;
            if (GameManager.Instance.Player.Bag != null)
            {
                foreach (var item in GameManager.Instance.Player.Bag)
                {
                    Console.WriteLine("   -----------------------------------------------------------------------------");
                    Console.WriteLine("     -" + (++count) + " 『" + DataManager.Instance.ItemDB.List[item][1] + "』");
                    ShowAtk(int.Parse(DataManager.Instance.ItemDB.List[item][2]));
                    ShowDef(int.Parse(DataManager.Instance.ItemDB.List[item][3]));
                    Console.WriteLine("\t [" + (int)(int.Parse(DataManager.Instance.ItemDB.List[item][7]) * 0.85) + " G]");
                    Console.WriteLine("\t  [ "+DataManager.Instance.ItemDB.List[item][6] + " ]");
                }
                Console.WriteLine("   -----------------------------------------------------------------------------");
            }
        }
        //일단은 복붙만
        public void ShowEquip(VIEW_TYPE view)
        {
            int count = 0;
            if (GameManager.Instance.Player.Bag != null)
            {
                foreach (var item in GameManager.Instance.Player.Bag)
                {
                    //Console.WriteLine("   ------------------------------------------------------------------------------");
                    if (view == VIEW_TYPE.EQUIP) Console.Write($"     -{++count} ");
                    else Console.Write("     -");

                    CurrentViewer.Attach_E_Mark(item);

                    Console.WriteLine(" 『" + DataManager.Instance.ItemDB.List[item][1] + "』");
                    ShowAtk(int.Parse(DataManager.Instance.ItemDB.List[item][2]));
                    ShowDef(int.Parse(DataManager.Instance.ItemDB.List[item][3]));
                    Console.WriteLine("\t  [ " + DataManager.Instance.ItemDB.List[item][6] + " ]");
                }
                //Console.WriteLine("   ------------------------------------------------------------------------------");
            }
        }
    }
}
