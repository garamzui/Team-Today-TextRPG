using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamTodayTextRPG.Characterclass;

namespace TeamTodayTextRPG
{
    internal class Characterclass
    {
        public abstract class Character
        {
            string name { get; set; }
            string job { get; set; }
            int attack { get; set; }
            int plusAtk { get; set; }
            int def { get; set; }
            int plusDef { get; set; }
            int hp { get; set; }

            int maxHp { get; set; }
            int gold { get; set; }
            string[] inistr { get; set; }
            string[] stray { get; set; }

            void init(string instring)
            { }

            void ViewStatus(GameManager)
            { }

            void Skill()

            { }
        }






        public class Worrior : Character
        { }
        public class megician : Character 
        { }
        public class Assassing : Character
        { }
    }







}


