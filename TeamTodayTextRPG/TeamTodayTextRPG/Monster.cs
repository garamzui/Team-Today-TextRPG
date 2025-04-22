using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTodayTextRPG
{
    enum MONSTER_STATE
    {
        IDLE,
        DEAD
    }

    abstract class Monster()
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int PlusAtk { get; set; }
        public int Def { get; set; }
        public int PlusDef { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int rewardGold { get; set; }
        public int rewardExp { get; set; }
        public string text { get; set; }
        public string[] Strary { get; set; }

        public void Init(string[] parameter)
        {
            // 0.몬스터코드 / 1.이름 / 2.레벨 / 3.공격력 / 4.방어력 / 5.체력 / 6.보상골드 / 7.보상경험치 / 8.텍스트
            Strary = parameter;
            Name = Strary[1];
            Level = int.Parse(Strary[2]);
            Atk = int.Parse(Strary[3]);
            Def = int.Parse(Strary[4]);
            Hp = int.Parse(Strary[5]);
            MaxHp = Hp;
            rewardGold = int.Parse(Strary[6]);
            rewardExp = int.Parse(Strary[7]);
            text = Strary[8];
        }
    }

    class Monster_1 : Monster
    {
        public Monster_1(string name)
        {
            Code = 0;
            Init(DataManager.Instance.MonsterDB.List[Code]);
        }
    }

    class Monster_2 : Monster
    {
        public Monster_2(string name)
        {
            Code = 1;
            Init(DataManager.Instance.MonsterDB.List[Code]);
        }
    }

    class Monster_3 : Monster
    {
        public Monster_3(string name)
        {
            Code = 2;
            Init(DataManager.Instance.MonsterDB.List[Code]);
        }
    }
    class Monster_4 : Monster
    {
        public Monster_4(string name)
        {
            Code = 3;
            Init(DataManager.Instance.MonsterDB.List[Code]);
        }
    }
    class Monster_5 : Monster
    {
        public Monster_5(string name)
        {
            Code = 4;
            Init(DataManager.Instance.MonsterDB.List[Code]);
        }
    }
}
