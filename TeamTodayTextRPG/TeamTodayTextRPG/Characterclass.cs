using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static TeamTodayTextRPG.Characterclass;

namespace TeamTodayTextRPG
{
    internal class Characterclass
    {
        public abstract class Character
        {

            public string jobname { get; set; }
            public int attack { get; set; }
            public int plusAtk { get; set; }
            public int def { get; set; }
            public int plusDef { get; set; }
            public int hp { get; set; }
            public int maxHp { get; set; }
            public int mp { get; set; }
            public int maxMp { get; set; }
            public int dodge { get; set; }
            public int gold { get; set; }
            public string[] initstr { get; set; }
            public string[] strary { get; set; }
            public string actskillName { get; set; }
            public string passkillName {  get; set; }

            public void init(string data)
            {
                initstr = data.Split(',');
                jobname = initstr[0];
                attack = int.Parse(initstr[1]);
                def = int.Parse(initstr[2]);
                hp = int.Parse(initstr[3]);
                mp = int.Parse(initstr[4]);
                dodge = int.Parse(initstr[5]);
                gold = int.Parse(initstr[6]);
                actskillName = string.Empty;
                passkillName = string.Empty;



            }

            public void ViewStatus()
            {
                Console.WriteLine($"{jobname} - 공격력 {attack} (+{plusAtk}), 방어력 {def} (+{plusDef}), HP {hp}/{maxHp}, Gold {gold}");
            }

            public virtual void DefaultAttack()
            {
                Console.WriteLine($"{jobname}의 기본 공격");
            }

            public virtual void ActiveSkill()

            {
                

                if (Player.level >= 2)
                {

                }
                Console.WriteLine($"{jobname}의 기술 {actskillName}");
            }

            public virtual void PassiveSkill()
            {
                if (Player.level >= 5)
                {

                }
                Console.WriteLine($"{jobname}의 기술 {passkillName}");
            }
        }






        public class Worrior : Character
        {
            public static Worrior Default()
            {
                Worrior w = new Worrior();
                w.init("전사,10,5,100,20,1,1000,쾅 내려치기,전사의 피부");
                return w;
            }
            public override void ActiveSkill()
            {
                base.ActiveSkill();
            }

        }
        public class Megician : Character
        {
            public static Megician Default()
            {
                Megician m = new Megician();
                m.init("마법사,1,3,50,100,3,1000,썬더볼트,마력개방");
                return m;
            }
            public override void ActiveSkill()
            {
                base.ActiveSkill();
            }
        }
        public class Assassin : Character
        {
            public static Assassin Default()
            {
                Assassin a = new Assassin();
                a.init("애쓰애쓰인,8,1,75,75,10,1000,비열한습격,날쌘 몸놀림");
                return a;
            }
            public override void ActiveSkill()
            {
                base.ActiveSkill();
            }
        }
    }







}



//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⡤⠤⠶⠖⠒⠒⢲⡶⠦⢤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠞⠝⢊⠀⠀⠀⠀⠀⠒⠂⠀⠀⠭⠀⣙⠲⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⠞⠐⠂⠀⠤⠀⠐⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠤⣈⠳⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣠⣤⣤⠤⢤⣤⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⠴⠒⠋⠉⠉⠉⣉⣭⠭⠶⠆⠀⠀⠀⠀⢠⢇⣒⣥⠤⠶⣒⣚⣉⠭⠭⠭⠭⠭⢭⣙⣒⣲⠦⠤⣴⣁⣹⡄⠀⠀⠀⠀⠀⠀⠐⠛⠻⠷⠶⣖⡠⠤⣄⡀⠀⠈⠙⠲⣄⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⣠⠖⠋⠀⠀⢀⠤⣲⠾⠋⠁⠀⠀⠀⠀⠀⢀⡤⠶⣚⠉⢀⡠⠨⠕⢒⣀⣤⣤⠤⠤⠤⠤⣤⣄⣀⡀⠈⠁⢀⠈⠉⣓⠦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠑⠦⣌⠑⠢⡀⠀⠈⠳⡄⠀⠀⠀
//⠀⠀⠀⠀⢠⠞⠁⠀⠀⡠⢊⡴⠋⠀⠀⠀⠀⠀⠀⢀⡴⢚⠥⢒⡡⠀⠀⣀⠴⠚⠋⠁⠀⠀⠀⠒⣒⠒⠐⠂⠀⠀⠈⠙⠒⢦⣀⠑⠢⣉⠢⢝⠲⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠳⣄⠈⠳⡀⠀⠙⡆⠀⠀
//⠀⠀⠀⣰⠋⠀⠀⡠⢊⡴⠋⠀⠀⠀⠀⠀⠀⢀⡔⡩⠚⠁⠐⢁⠀⣠⢾⡃⠀⠀⠀⠈⠉⠉⠁⠁⠀⠈⠉⠀⠀⠀⠀⠀⠀⠀⢘⡷⣄⠀⠀⠀⡁⢈⠳⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠳⣄⠈⢆⠀⠸⡆⠀
//⠀⠀⡰⠃⠀⠀⡔⣡⠋⠀⠀⠀⠀⠀⠀⠀⢠⠏⡔⠁⢠⠂⠠⠁⣼⠁⠾⠀⠀⣠⣴⠶⠖⠂⠀⠀⠀⠀⠀⠐⠒⠲⠶⣦⡀⠀⢨⡜⠈⣧⠈⠆⠙⠄⠱⡈⢧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣆⠈⡆⠀⣷⠀
//⠀⠸⣃⠀⠀⡜⣸⠃⠀⠀⠀⠀⠀⠀⠀⠀⣿⠀⠀⠀⠃⠀⠀⠈⢹⠀⠆⠀⠀⠉⠀⠀⠀⠀⠠⡀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠀⠀⡕⠀⡿⠃⠈⠀⠘⠀⠁⠀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡆⢸⠀⡿⠀
//⠀⠀⠈⢧⣰⠥⡃⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⠀⢄⠀⢆⠀⢂⢀⣼⣀⡆⢀⡀⢀⣠⠶⠶⠄⠀⢳⠀⠀⠀⢠⠶⠶⢦⡀⠀⠀⠀⢷⣠⠧⣄⢀⠀⡰⠀⡀⢀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡇⡸⢠⠇⠀
//⠀⠀⠀⠸⡅⠀⢹⡀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠣⣈⠢⠈⠂⠀⣯⠖⣽⠃⠐⠂⠈⠀⠤⠐⠀⠀⡤⠀⠀⠀⠀⠒⠀⠀⠀⠈⠁⠀⢸⣧⠺⣼⡆⠐⣁⠜⢀⠞⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡴⠓⣧⠎⠀⠀
//⠀⠀⠀⠀⢣⡠⠶⢇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠓⠦⣄⣀⡇⢰⣿⠀⠀⠀⠀⠀⠀⠀⢠⠄⠀⠀⠀⠀⢤⠀⠀⠀⠀⠀⠀⠀⢸⡏⡆⢸⠇⣈⡥⠞⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡞⠀⠀⡞⠀⠀⠀
//⠀⠀⠀⠀⠈⢷⠀⠸⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢳⠘⢽⠀⠀⠀⠀⠀⢀⠔⠻⠠⠤⠀⠴⠦⠰⠑⢄⠀⠀⠀⠀⠀⢸⠟⢁⡞⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠙⢲⠞⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠈⣆⠀⢻⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢣⣸⡄⠀⠀⠀⡜⢁⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠙⡆⠀⠀⠀⣼⢀⡞⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⢠⠏⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⢸⡄⠈⢧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⢧⠀⠀⠀⠣⠘⢿⣛⠒⠒⠒⠒⠒⢚⣻⠟⠀⠃⢠⠀⢀⠏⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⢠⠏⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⢣⠀⠘⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣇⠀⢣⠀⠀⠀⠈⠉⠒⠒⠒⠊⠉⠀⠀⠀⢠⠇⢀⡞⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⢠⠏⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠈⣇⠀⠹⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⠣⡀⠳⡀⠀⠀⠈⠙⠛⠛⠉⠀⠀⠀⠠⠂⣠⠎⠱⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⢠⠏⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⢹⡄⠀⢳⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⢾⠃⢰⢿⠳⠄⢤⡀⠀⠀⠀⠀⠀⠀⣀⠔⠴⠊⡿⡆⠀⢹⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⢠⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⢣⠀⢘⡧⠤⠤⣄⡤⢤⡀⠀⠀⠀⢀⣠⡴⠃⡎⠀⢸⢸⣇⠀⠀⠉⠙⠒⠒⠒⠚⠋⠁⠀⠀⣠⡇⢹⠀⠀⠏⢳⣀⠀⠀⠀⠀⠀⠀⣀⣠⣀⣀⡼⠁⢀⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠷⠉⠀⠀⣀⣤⣧⡀⣷⡤⠶⠚⠉⣸⠁⢠⠃⠀⠀⢸⠘⢦⡀⠈⠐⠢⠤⠤⠤⠂⠀⢀⡴⠁⡇⢸⠀⠀⠰⠀⢯⠉⠲⠦⣤⠞⣹⡁⠀⠀⠉⠳⢤⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⣰⠋⠀⠠⠖⠋⠀⠀⠀⢹⠋⠀⠀⠀⢠⠇⠀⠈⠀⠀⠀⢸⠀⠀⠳⢤⡀⠑⠒⠂⠀⣀⠴⠋⠀⠀⡇⢸⡄⠀⠀⠀⢸⡄⠀⠀⢸⠞⠁⠈⠉⠓⠦⡀⠀⠙⢆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⡇⡀⠀⠀⢀⡤⠖⠛⠉⠙⣆⠀⠀⠀⡾⠀⠀⠀⠀⠀⠀⢸⠖⠛⠳⣌⡙⢦⣀⡴⠚⢁⡏⠉⠓⢦⡇⠀⡇⠀⠀⠀⠀⡇⠀⠀⡼⠒⠒⠲⢤⣀⠀⠈⠀⠀⠘⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⠀⠀⠀⠀⣀⡤⠤⠤⣿⠀⠀⢀⡇⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⠉⣶⢫⠔⡋⠉⠀⠀⠀⢸⠇⠀⡇⠀⠀⠀⠀⣿⠀⢀⣧⠤⣄⡀⠀⠈⠀⠀⠀⠀⠀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⣧⡀⠀⠀⠀⠈⠁⠀⠀⣀⢸⠀⠀⢸⠁⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⠀⣧⠤⡄⡇⠀⠀⠀⠀⢸⠀⠀⠀⠀⠀⠀⠀⢸⠀⢸⠀⠀⠀⠈⠃⠀⠀⠀⢀⢨⣱⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⢹⠈⠈⠄⠀⠀⠀⣶⠋⣷⢋⠀⠀⡎⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⢸⠈⠉⠁⠇⠀⠀⠀⠀⡸⠀⠀⠀⠀⠀⠀⠀⢸⠀⠘⢮⠟⠲⠄⠀⠀⠀⠀⡀⠰⢸⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⡟⢨⡀⡈⡠⠀⠀⣏⠀⠘⣼⠀⢠⡇⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⢸⠀⠀⢸⠀⠀⠀⠀⠀⡇⠀⠀⠀⠀⠀⠀⠀⠸⡀⢀⠟⠀⣸⠀⠠⠀⠀⢀⢐⠀⣾⣱⡀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⢀⣼⠡⠠⢁⠧⣂⠀⢁⣻⡄⠀⢻⠀⢸⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⣸⠀⠀⢸⠀⠀⠀⠀⢠⡇⠀⠀⠀⠀⠀⠀⠀⠀⣧⠎⠀⣰⡃⡀⡀⠀⡀⢂⡕⠃⠨⢳⢧⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⢀⠏⠈⢧⡂⠎⡸⠠⢠⢠⢠⢷⠀⠈⢇⢸⠀⠀⣀⣀⣀⠀⠀⠀⠈⡄⠀⠀⠀⣿⢀⡀⢸⠀⠀⠀⠀⢸⠁⠀⠀⠀⠀⠀⠀⠀⢀⠟⠀⣰⠃⣷⠂⠠⠖⠒⡁⡀⠆⢃⡞⢻⣧⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⢀⣼⠀⠀⠀⠙⠒⠧⠤⠦⠴⢿⠘⡇⠀⠘⡞⠿⠿⠷⠦⢌⣙⡒⠲⣶⠇⠀⠀⠀⡿⠙⠜⢸⠀⠀⠀⠀⢸⣀⣀⣀⡤⠴⣚⣉⢭⡏⠀⣰⢻⠀⠘⣎⡀⠄⠆⠃⣀⡴⠋⠀⠈⡿⣇⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠊⠘⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠀⠙⠀⠀⠛⠀⠀⠀⠀⠀⠈⠉⠉⠁⠀⠀⠀⠀⠃⠀⠀⠘⠀⠀⠀⠀⠘⠓⠒⠒⠚⠋⠉⠀⠊⠀⠐⠃⠘⠂⠀⠃⠉⠙⠛⠉⠁⠀⠀⠀⠀⠃⠈⠂⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⢀⣀⣄⣀⠀⣀⣀⣀⡀⠀⠀⢀⣀⡀⠀⠀⣀⣀⣀⣀⠀⣀⣀⣀⣀⣀⠀⠀⣀⣀⠀⠀⠀⠀⣀⣀⣀⣀⣀⢀⣀⣀⣀⡀⣀⡀⠀⢀⡀⣀⣀⣀⣀⣀⠀⠀⢀⣀⣀⣀⡀⠀⣀⣀⣀⣀⠀⠀⢀⣀⣄⣀⡀⠀
//⠀⢾⣏⡉⠉⠀⣿⡏⠙⣿⡆⠀⣾⠿⣷⡀⠀⣿⡏⠉⣿⡇⠉⢹⣿⠉⠉⠀⣸⡟⣿⡄⠀⠀⠀⠉⠉⣿⠉⠉⢸⣿⠉⠉⠀⠹⣷⣰⡿⠁⠉⠉⣿⠉⠉⠀⠀⢸⣿⠉⢹⣿⠀⣿⡏⠉⣿⡆⣴⡿⠉⠉⠉⠃⠀
//⠀⠈⠙⢿⣷⡀⣿⡷⠾⠟⠁⢸⣿⣤⣿⣧⠀⣿⡟⢿⣯⠀⠀⢸⣿⠀⠀⢠⣿⣥⣽⣷⡀⠀⠀⠀⠀⣿⠀⠀⢸⣿⠛⠛⠀⢀⣿⢿⣇⠀⠀⠀⣿⠀⠀⠀⠀⢸⣿⠻⣿⡅⠀⣿⡷⠾⠟⠁⣿⣇⠘⠛⣿⡇⠀
//⠀⠷⠶⠾⠟⠀⠿⠇⠀⠀⠀⠿⠃⠀⠈⠿⠆⠿⠇⠀⠻⠧⠀⠸⠿⠀⠀⠾⠏⠀⠀⠻⠇⠀⠀⠀⠀⠿⠀⠀⠸⠿⠶⠶⠄⠾⠏⠈⠻⠧⠀⠀⠿⠀⠀⠀⠀⠸⠿⠀⠘⠿⠄⠿⠇⠀⠀⠀⠘⠿⠶⠶⠿⠇⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀


