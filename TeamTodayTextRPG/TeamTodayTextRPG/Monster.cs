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
    enum MONSTER_CODE
    {
        // 추후에 몬스터 이름으로 변경 될 예정입니다. 
        M1 = 0,
        M2,
        M3,
        M4,
        M5
    }

    abstract class Monster
    {
        public MONSTER_CODE Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int PlusAtk { get; set; }
        public int Def { get; set; }
        public int PlusDef { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }
        public string Text { get; set; }
        public string[] Parameter { get; set; }

        public void Init(string[] parameter)
        {
            // 0.몬스터코드 / 1.이름 / 2.레벨 / 3.공격력 / 4.방어력 / 5.체력 / 6.보상골드 / 7.보상경험치 / 8.텍스트
            Parameter = parameter;

            if (!Enum.TryParse(parameter[0], out MONSTER_CODE code))
                throw new ArgumentException("Invalid MONSTER code.");
            Code = code;
            Name = Parameter[1];
            Level = int.Parse(Parameter[2]);
            Atk = int.Parse(Parameter[3]);
            Def = int.Parse(Parameter[4]);
            Hp = int.Parse(Parameter[5]);
            MaxHp = Hp;
            RewardGold = int.Parse(Parameter[6]);
            RewardExp = int.Parse(Parameter[7]);
            Text = Parameter[8];
        }

        class Monster_1 : Monster
        {
            public Monster_1()
            {
                Init(DataManager.Instance.MonsterDB.List[(int)Code]);
            }
        }

        class Monster_2 : Monster
        {
            public Monster_2()
            {
                Init(DataManager.Instance.MonsterDB.List[(int)Code]);
            }
        }

        class Monster_3 : Monster
        {
            public Monster_3()
            {
                Init(DataManager.Instance.MonsterDB.List[(int)Code]);
            }
        }
        class Monster_4 : Monster
        {
            public Monster_4()
            {
                Init(DataManager.Instance.MonsterDB.List[(int)Code]);
            }
        }
        class Monster_5 : Monster
        {
            public Monster_5()
            {
                Init(DataManager.Instance.MonsterDB.List[(int)Code]);
            }
        }
    }
}
