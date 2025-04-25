using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace TeamTodayTextRPG
{
    public enum DUNGEON_DIFF { Easy = 0, Normal, Hard, Hell }

    class BattleLog
    {
        private List<string> logs = new List<string>();

        public void Add(string message) => logs.Add(message);

        public void Print()
        {
            Console.WriteLine("\n 전투 로그 요약:");
            foreach (var log in logs)
            {
                Console.WriteLine("- " + log);
            }
        }
    }

    public abstract class Dungeon
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Reward { get; set; }
        public int Exp { get; set; }

        public int LowLevel { get; set; }
        public int HighLevel { get; set; }
        public int MonsterCount { get; set; }

        public Monster? TargetMonster { get; set; }
        public int MonsterAtkCounter { get; set; } 

        public DUNGEON_DIFF Diff { get; set; }

        // 던전 진입시 등장할 몬스터 리스트
        public List<Monster> Dungeon_Monster { get; set; } = new List<Monster>();

        //public Dungeon(int code, string name, int reward, int exp, int defLevel, DUNGEON_DIFF diff)

        public void Init(string[] parameter)
        {
            Code = int.Parse(parameter[0]);
            Name = parameter[1];
            Reward = int.Parse(parameter[2]);
            Exp = int.Parse(parameter[3]);
            LowLevel = int.Parse(parameter[4]);
            HighLevel = int.Parse(parameter[5]);
            MonsterCount = int.Parse(parameter[6]);
            Diff = (DUNGEON_DIFF)(int.Parse(parameter[7]));
        }

        public void Enter()
        {
            if(Diff == DUNGEON_DIFF.Hell)
            {
                Dungeon_Monster.Add(GameManager.Instance.MonsterFactory(4));
            }
            {
                int randNum = GameManager.Instance.Rand.Next(1, MonsterCount + 1);
                for (int i = 0; i < randNum; i++)
                {
                    Random randCode = new Random();

                    Dungeon_Monster.Add(GameManager.Instance.MonsterFactory(randCode.Next(0, 4)));
                }
            }
        }
        
        public bool CheckClear()
        {
            bool retBool = true;

            foreach(var monster in Dungeon_Monster)
            {
                if (monster.State == MONSTER_STATE.IDLE)
                {
                    retBool = false;
                    break;
                }
            }
            return retBool;
        }
        /*
        public void Enter(Player player, Monster monster)
        {
            

            BattleLog log = new BattleLog();

            Console.WriteLine($"\n{monster.Name} 등장! {(monster.IsBoss ? "[보스]" : "")}");

            int originalAtk = player.BaseAttack;
            int originalDef = player.BaseDefense;

            if (monster.IsBoss)
            {
                player.BaseAttack = (int)(player.BaseAttack * 0.9);
                player.BaseDefense = (int)(player.BaseDefense * 0.9);
                Console.WriteLine("보스 효과로 능력치 10% 감소!");
                log.Add("보스 효과로 능력치 10% 감소");
            }

            bool fled = false;

            while (monster.Hp > 0 && player.Hp > 0)
            {
                Console.WriteLine("\n1. 공격하기\n2. 도망가기\n>> ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    monster.Hp -= player.TotalAttack;
                    Console.WriteLine($"{monster.Name}에게 {player.TotalAttack} 데미지!");
                    log.Add($"{monster.Name}에게 {player.TotalAttack} 데미지");

                    if (monster.Hp <= 0)
                    {
                        Console.WriteLine($"{monster.Name} 처치!");
                        log.Add($"{monster.Name} 처치!");
                        player.Gold += monster.Reward;
                        log.Add($"{monster.Reward}G 획득");

                        if (monster.IsBoss && monster.Name == "자쿰")
                        {
                            player.Title = "Zakum Slayer";
                            log.Add("칭호 'Zakum Slayer' 획득!");
                        }

                        break;
                    }

                    player.Hp -= monster.Attack;
                    Console.WriteLine($"{monster.Name}의 반격! {monster.Attack} 데미지");
                    log.Add($"반격 피해: {monster.Attack}");
                }
                else if (input == "2")
                {
                    Console.WriteLine("도망쳤습니다.");
                    log.Add("도망 시도 성공");
                    fled = true;
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

            if (monster.IsBoss)
            {
                player.BaseAttack = originalAtk;
                player.BaseDefense = originalDef;
                Console.WriteLine("보스 효과 종료 (능력치 복구)");
            }

            log.Print();

            if (player.Hp > 0 && !fled && monster.Hp <= 0)
            {
                Console.WriteLine("\n 던전을 클리어했습니다!");
            }
        }*/
    }


    public class Dungeon_Easy : Dungeon
    {
        public Dungeon_Easy()
        {
            Init(DataManager.Instance.DungeonDB.List[0]);
        }
    }

    public class Dungeon_Normal : Dungeon
    {
        public Dungeon_Normal()
        {
            Init(DataManager.Instance.DungeonDB.List[1]);
        }
    }
    public class Dungeon_Hard : Dungeon
    {
        public Dungeon_Hard()
        {
            Init(DataManager.Instance.DungeonDB.List[2]);
        }
    }
    public class Dungeon_Hell : Dungeon
    {
        public Dungeon_Hell()
        {
            Init(DataManager.Instance.DungeonDB.List[3]);
        }
    }
    /* 
       능력치 감소
       헬던전은 보스가 고정으로 등장
    /* 
    public class Dungeon_Hell : Dungeon
    {

    }
    /*
    class Program
    {
        static void Main()
        {
          
            string[] monsterNames = { "슬라임", "고블린", "늑대", "오크" };

            Random rand = new Random();
          
            int index = rand.Next(monsterNames.Length);       
            string selectedMonster = monsterNames[index];         
            Console.WriteLine($"몬스터 등장! ▶ {selectedMonster}");
        }
    }


    class Program2
    {
        static void Main(string[] args)
        {
            Player player = new Player(name: "Jaehun", level: 3, atk: 15, def: 8, hp: 100, gold: 1000);

           
            Monster boss = new Monster("자쿰", 200, 20, 1000, isBoss: true);   

            Dungeon dungeon = new Dungeon(1, "불의 신전", 1000, 500, 3, DUNGEON_DIFF.Hard);
            dungeon.Enter(player, boss);
        }
    }*/
}
