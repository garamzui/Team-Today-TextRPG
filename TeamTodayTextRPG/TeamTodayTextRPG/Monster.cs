using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTodayTextRPG
{
    enum Monster_State
    {
        IDLE,
        DEAD
    }

    class Monster
    {
        Monster_State State { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int Atk { get; set; }
        public int PlusAtk { get; set; }
        public int Def { get; set; }
        public int PlusDef { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Gold { get; set; }
        public string Initstr { get; set; }
        public string[] Strary { get; set; }



        public void Init(string name, string info)
        {
            Strary = info.Split(',');

            Name = name;
            Job = Strary[0];
            Atk = int.Parse(Strary[1]);
            Def = int.Parse(Strary[2]);
            Hp = int.Parse(Strary[3]);
            MaxHp = Hp;
            Gold = int.Parse(Strary[4]);
        }


    }

    class Monster_1 : Monster
    {

    }
    class Monster_2 : Monster
    {

    }
    class Monster_3 : Monster
    {

    }



}
