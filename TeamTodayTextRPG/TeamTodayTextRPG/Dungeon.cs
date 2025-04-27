using System;
using System.Numerics;
using System.Threading;
using System.Xml.Linq;
using TeamTodayTextRPG;

namespace TeamTodayTextRPG
{
    public enum DUNGEON_DIFF { Easy = 0, Normal, Hard, Hell }

    public abstract class Dungeon
    {

        public int Code { get; set; }
        public string Name { get; set; }
        public int Reward { get; set; }
        public int Exp { get; set; }
        public string Text { get; set; }

        public int LowLevel { get; set; }
        public int HighLevel { get; set; }
        public int MonsterCount { get; set; }
        public int Turn { get; set; } = 0;

        private List<string> Logs = new List<string>();

        public Monster? TargetMonster { get; set; }
        public int TargetMonsterIndex { get; set; }
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
            Text = parameter[8];
        }

        public void Enter()
        {
            if (Diff == DUNGEON_DIFF.Hell)
            {
                Dungeon_Monster.Add(GameManager.Instance.MonsterFactory(4));
            }
            else
            {

                int randNum = GameManager.Instance.Rand.Next(1, MonsterCount + 1);
                for (int i = 0; i < randNum; i++)
                {
                    int weakMon = 0;
                    int strongMon = 0;
                    Random randCode = new Random();
                    // 던전 난이도 별로 switch case 문 혹은 if문
                    if (Diff == DUNGEON_DIFF.Easy)
                    {
                        weakMon = (int)MONSTER_CODE.Slime;
                        strongMon = (int)MONSTER_CODE.Wolf;
                    }

                    else if (Diff == DUNGEON_DIFF.Normal)
                    {
                        weakMon = (int)MONSTER_CODE.Slime;
                        strongMon = (int)MONSTER_CODE.Ork;
                    }

                    else if (Diff == DUNGEON_DIFF.Hard)
                    {
                        weakMon = (int)MONSTER_CODE.Goblin;
                        strongMon = (int)MONSTER_CODE.Zakum;
                    }

                    Dungeon_Monster.Add(GameManager.Instance.MonsterFactory
                        (randCode.Next(weakMon, strongMon)));
                }
            }
        }

        public bool CheckClear()
        {
            bool retBool = true;

            foreach (var monster in Dungeon_Monster)
            {
                if (monster.State == MONSTER_STATE.IDLE)
                {
                    retBool = false;
                    break;
                }
            }
            return retBool;
        }


        public void SaveLog(string message)
        {
            Logs.Add(message);
        }
        public void PrintLog()
        {
            Console.WriteLine("\n 전투 로그 요약:");
            foreach (var log in Logs)
            {
                Console.WriteLine("- " + log);
            }
        }

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
}
