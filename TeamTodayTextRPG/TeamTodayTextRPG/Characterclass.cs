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
            public int mp { get; set; } // 새로운 스탯 mp추가 했습니다
            public int maxMp { get; set; }
            public int dodge { get; set; }
            public int plusDodge { get; set; }
            // 직업간 차이를 두어 보고자 dodge 스탯도 추가 해 봤습니다.
            // 전투가 어떻게 이루어질지에 따라 추가해야 할 계산식이 달라질 것 같습니다.
            // 예를 들면 
            //int num = new Random().Next(0, 26); 또는 GameManager에 static Random하나 만들어두고 돌려쓰기
            //if ((num += Charater.dodge > 20)
            //{공격을 무효화하기}
            // 이런식으로 설계하면 어떨까 합니다.
            public int gold { get; set; }
            public string[] initstr { get; set; }
            public string[] strary { get; set; }
            public string actskillName { get; set; }
            public string passkillName { get; set; }

            public int passiveSkillLevel = 0;

            public void init(string data) //우선은 임의로 매서드로 초기화할 필드를 변경해 놓았습니다.
            {
                //직업이름,공격력,방어력,체력,마력,회피,골드,액티브스킬이름,패시브스킬이름
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


            //기본공격을 두고, 래밸을 올리면 스킬이
            //해금되는 방식을 적용해 보고 싶었습니다.
            // 추후에 구현이 어려울 시 조건부는 빼 버리는 것으로 하면 될 것 같습니다.
            // 스킬 이름은 스탯과 함께 초기화해서 저장해두게 해놨습니다.
            //Player의 필드들이 private되어있어 접근이 안됩니다. 요 부분은 회의 때 조율 해야 할 것 같아요.
            public virtual void DefaultAttack()
            {
                Console.WriteLine($"{jobname}의 기본 공격");
            }


            //active 스킬은 몬스터 체력을 -= 하는 방식으로 
            //passive 스킬은 각각 직업 특성에 맞는 스탯값을 += 하는 방식으로 만들어 보려 합니다.
            public virtual void ActiveSkill(Monster m)

            {



                Console.WriteLine($"{jobname}의 기술 {actskillName}");
            }

            public virtual void PassiveSkill(Player p)
            {
                
                Console.WriteLine($"{jobname}의 기술 {passkillName}");
            }
        }






        public class Worrior : Character
        {
            public static Worrior Default()
            {
                Worrior w = new Worrior();
                //직업이름,공격력,방어력,체력,마력,회피,골드,액티브스킬이름,패시브스킬이름
                w.init("전사,10,5,100,40,1,1000,쾅 내려치기,전사의 피부");
                return w;
            }
            public override void ActiveSkill(Monster m)
            {
                mp -= 10;
                int SkillDamage = (attack * 3) - (m.Def);
                m.Hp -= SkillDamage;
                Console.WriteLine($"{actskillName}을 사용하여 {m.Name}이(가) {SkillDamage}의 피해를 입었습니다.");
            }

            public override void PassiveSkill(Player p)
            {
                if (p.level >= 5)
                {
                    def += 2;
                    
                    passiveSkillLevel += 1;
                    if (passiveSkillLevel >= 5)
                    { def = 20; }
                }

            }

        }
        public class Megician : Character
        {
            public static Megician Default()
            {
                Megician m = new Megician();
                //직업이름,공격력,방어력,체력,마력,회피,골드,액티브스킬이름,패시브스킬이름
                m.init("마법사,3,3,50,100,3,1000,썬더볼트,마력개방");
                return m;
            }
            public override void ActiveSkill(Monster m)
            {
                mp -= 10;
                int SkillDamage = (attack * 10) - (m.Def);
                m.Hp -= SkillDamage;
                Console.WriteLine($"{actskillName}을 사용하여 {m.Name}이(가) {SkillDamage}의 피해를 입었습니다.");
            }
            public override void PassiveSkill(Player p)
            {
                if (p.level >= 5)
                {
                    attack += 1;
                    mp += 50;

                    passiveSkillLevel += 1;
                    if (passiveSkillLevel >= 5)
                    { mp = 500; }
                }

            }
        }
        public class Assassin : Character
        {
            public static Assassin Default()
            {
                Assassin a = new Assassin();
                //직업이름,공격력,방어력,체력,마력,회피,골드,액티브스킬이름,패시브스킬이름
                a.init("암살자,8,1,75,75,10,1000,비열한습격,날쌘 몸놀림");
                return a;
            }
            public override void ActiveSkill(Monster m)
            {
                mp -= 10;
                int SkillDamage = (attack * 3) - (m.Def);


               
               
                
                Console.WriteLine($"{actskillName}을 사용하여 {m.Name}이(가) {SkillDamage}의 피해를 입었습니다.");

                 m.Hp -= SkillDamage;

            }
            public override void PassiveSkill(Player p)
            {
                if (p.level >= 5)
                {
                    dodge += 2;

                    passiveSkillLevel += 1;
                    if (passiveSkillLevel >= 5)
                    { dodge = 25; }
                }

            }
        }


























    }
    public class StartIdleScene
    {
        public void StartIdleScenePaint()
        {
            Console.WriteLine(@"            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⡤⠤⠶⠖⠒⠒⢲⡶⠦⢤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠞⠝⢊⠀⠀⠀⠀⠀⠒⠂⠀⠀⠭⠀⣙⠲⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⠞⠐⠂⠀⠤⠀⠐⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠤⣈⠳⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣠⣤⣤⠤⢤⣤⣄⣀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⠴⠒⠋⠉⠉⠉⣉⣭⠭⠶⠆⠀⠀⠀⠀⢠⢇⣒⣥⠤⠶⣒⣚⣉⠭⠭⠭⠭⠭⢭⣙⣒⣲⠦⠤⣴⣁⣹⡄⠀⠀⠀⠀⠀⠀⠐⠛⠻⠷⠶⣖⡠⠤⣄⡀⠀⠈⠙⠲⣄⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⣠⠖⠋⠀⠀⢀⠤⣲⠾⠋⠁⠀⠀⠀⠀⠀⢀⡤⠶⣚⠉⢀⡠⠨⠕⢒⣀⣤⣤⠤⠤⠤⠤⣤⣄⣀⡀⠈⠁⢀⠈⠉⣓⠦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠑⠦⣌⠑⠢⡀⠀⠈⠳⡄⠀⠀⠀
            ⠀⠀⠀⠀⢠⠞⠁⠀⠀⡠⢊⡴⠋⠀⠀⠀⠀⠀⠀⢀⡴⢚⠥⢒⡡⠀⠀⣀⠴⠚⠋⠁⠀⠀⠀⠒⣒⠒⠐⠂⠀⠀⠈⠙⠒⢦⣀⠑⠢⣉⠢⢝⠲⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠳⣄⠈⠳⡀⠀⠙⡆⠀⠀
            ⠀⠀⠀⣰⠋⠀⠀⡠⢊⡴⠋⠀⠀⠀⠀⠀⠀⢀⡔⡩⠚⠁⠐⢁⠀⣠⢾⡃⠀⠀⠀⠈⠉⠉⠁⠁⠀⠈⠉⠀⠀⠀⠀⠀⠀⠀⢘⡷⣄⠀⠀⠀⡁⢈⠳⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠳⣄⠈⢆⠀⠸⡆⠀
            ⠀⠀⡰⠃⠀⠀⡔⣡⠋⠀⠀⠀⠀⠀⠀⠀⢠⠏⡔⠁⢠⠂⠠⠁⣼⠁⠾⠀⠀⣠⣴⠶⠖⠂⠀⠀⠀⠀⠀⠐⠒⠲⠶⣦⡀⠀⢨⡜⠈⣧⠈⠆⠙⠄⠱⡈⢧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⣆⠈⡆⠀⣷⠀
            ⠀⠸⣃⠀⠀⡜⣸⠃⠀⠀⠀⠀⠀⠀⠀⠀⣿⠀⠀⠀⠃⠀⠀⠈⢹⠀⠆⠀⠀⠉⠀⠀⠀⠀⠠⡀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠀⠀⡕⠀⡿⠃⠈⠀⠘⠀⠁⠀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⡆⢸⠀⡿⠀
            ⠀⠀⠈⢧⣰⠥⡃⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⠀⢄⠀⢆⠀⢂⢀⣼⣀⡆⢀⡀⢀⣠⠶⠶⠄⠀⢳⠀⠀⠀⢠⠶⠶⢦⡀⠀⠀⠀⢷⣠⠧⣄⢀⠀⡰⠀⡀⢀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡇⡸⢠⠇⠀
            ⠀⠀⠀⠸⡅⠀⢹⡀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠣⣈⠢⠈⠂⠀⣯⠖⣽⠃⠐⠂⠈⠀⠤⠐⠀⠀⡤⠀⠀⠀⠀⠒⠀⠀⠀⠈⠁⠀⢸⣧⠺⣼⡆⠐⣁⠜⢀⠞⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡴⠓⣧⠎⠀⠀
            ⠀⠀⠀⠀⢣⡠⠶⢇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠓⠦⣄⣀⡇⢰⣿⠀⠀⠀⠀⠀⠀⠀⢠⠄⠀⠀⠀⠀⢤⠀⠀⠀⠀⠀⠀⠀⢸⡏⡆⢸⠇⣈⡥⠞⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⡞⠀⠀⡞⠀⠀⠀
            ⠀⠀⠀⠀⠈⢷⠀⠸⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢳⠘⢽⠀⠀⠀⠀⠀⢀⠔⠻⠠⠤⠀⠴⠦⠰⠑⢄⠀⠀⠀⠀⠀⢸⠟⢁⡞⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠙⢲⠞⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠈⣆⠀⢻⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢣⣸⡄⠀⠀⠀⡜⢁⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠙⡆⠀⠀⠀⣼⢀⡞⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⢠⠏⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⢸⡄⠈⢧⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⢧⠀⠀⠀⠣⠘⢿⣛⠒⠒⠒⠒⠒⢚⣻⠟⠀⠃⢠⠀⢀⠏⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⢠⠏⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⢣⠀⠘⡆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣇⠀⢣⠀⠀⠀⠈⠉⠒⠒⠒⠊⠉⠀⠀⠀⢠⠇⢀⡞⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⢠⠏⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠈⣇⠀⠹⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⠣⡀⠳⡀⠀⠀⠈⠙⠛⠛⠉⠀⠀⠀⠠⠂⣠⠎⠱⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⢠⠏⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⢹⡄⠀⢳⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⢾⠃⢰⢿⠳⠄⢤⡀⠀⠀⠀⠀⠀⠀⣀⠔⠴⠊⡿⡆⠀⢹⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠁⢠⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⢣⠀⢘⡧⠤⠤⣄⡤⢤⡀⠀⠀⠀⢀⣠⡴⠃⡎⠀⢸⢸⣇⠀⠀⠉⠙⠒⠒⠒⠚⠋⠁⠀⠀⣠⡇⢹⠀⠀⠏⢳⣀⠀⠀⠀⠀⠀⠀⣀⣠⣀⣀⡼⠁⢀⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⠷⠉⠀⠀⣀⣤⣧⡀⣷⡤⠶⠚⠉⣸⠁⢠⠃⠀⠀⢸⠘⢦⡀⠈⠐⠢⠤⠤⠤⠂⠀⢀⡴⠁⡇⢸⠀⠀⠰⠀⢯⠉⠲⠦⣤⠞⣹⡁⠀⠀⠉⠳⢤⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⣰⠋⠀⠠⠖⠋⠀⠀⠀⢹⠋⠀⠀⠀⢠⠇⠀⠈⠀⠀⠀⢸⠀⠀⠳⢤⡀⠑⠒⠂⠀⣀⠴⠋⠀⠀⡇⢸⡄⠀⠀⠀⢸⡄⠀⠀⢸⠞⠁⠈⠉⠓⠦⡀⠀⠙⢆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⡇⡀⠀⠀⢀⡤⠖⠛⠉⠙⣆⠀⠀⠀⡾⠀⠀⠀⠀⠀⠀⢸⠖⠛⠳⣌⡙⢦⣀⡴⠚⢁⡏⠉⠓⢦⡇⠀⡇⠀⠀⠀⠀⡇⠀⠀⡼⠒⠒⠲⢤⣀⠀⠈⠀⠀⠘⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⡇⠀⠀⠀⠀⠀⣀⡤⠤⠤⣿⠀⠀⢀⡇⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⠉⣶⢫⠔⡋⠉⠀⠀⠀⢸⠇⠀⡇⠀⠀⠀⠀⣿⠀⢀⣧⠤⣄⡀⠀⠈⠀⠀⠀⠀⠀⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⣧⡀⠀⠀⠀⠈⠁⠀⠀⣀⢸⠀⠀⢸⠁⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⠀⣧⠤⡄⡇⠀⠀⠀⠀⢸⠀⠀⠀⠀⠀⠀⠀⢸⠀⢸⠀⠀⠀⠈⠃⠀⠀⠀⢀⢨⣱⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⢹⠈⠈⠄⠀⠀⠀⣶⠋⣷⢋⠀⠀⡎⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⢸⠈⠉⠁⠇⠀⠀⠀⠀⡸⠀⠀⠀⠀⠀⠀⠀⢸⠀⠘⢮⠟⠲⠄⠀⠀⠀⠀⡀⠰⢸⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⡟⢨⡀⡈⡠⠀⠀⣏⠀⠘⣼⠀⢠⡇⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⢸⠀⠀⢸⠀⠀⠀⠀⠀⡇⠀⠀⠀⠀⠀⠀⠀⠸⡀⢀⠟⠀⣸⠀⠠⠀⠀⢀⢐⠀⣾⣱⡀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⢀⣼⠡⠠⢁⠧⣂⠀⢁⣻⡄⠀⢻⠀⢸⠀⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⣸⠀⠀⢸⠀⠀⠀⠀⢠⡇⠀⠀⠀⠀⠀⠀⠀⠀⣧⠎⠀⣰⡃⡀⡀⠀⡀⢂⡕⠃⠨⢳⢧⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⢀⠏⠈⢧⡂⠎⡸⠠⢠⢠⢠⢷⠀⠈⢇⢸⠀⠀⣀⣀⣀⠀⠀⠀⠈⡄⠀⠀⠀⣿⢀⡀⢸⠀⠀⠀⠀⢸⠁⠀⠀⠀⠀⠀⠀⠀⢀⠟⠀⣰⠃⣷⠂⠠⠖⠒⡁⡀⠆⢃⡞⢻⣧⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⢀⣼⠀⠀⠀⠙⠒⠧⠤⠦⠴⢿⠘⡇⠀⠘⡞⠿⠿⠷⠦⢌⣙⡒⠲⣶⠇⠀⠀⠀⡿⠙⠜⢸⠀⠀⠀⠀⢸⣀⣀⣀⡤⠴⣚⣉⢭⡏⠀⣰⢻⠀⠘⣎⡀⠄⠆⠃⣀⡴⠋⠀⠈⡿⣇⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠊⠘⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠀⠙⠀⠀⠛⠀⠀⠀⠀⠀⠈⠉⠉⠁⠀⠀⠀⠀⠃⠀⠀⠘⠀⠀⠀⠀⠘⠓⠒⠒⠚⠋⠉⠀⠊⠀⠐⠃⠘⠂⠀⠃⠉⠙⠛⠉⠁⠀⠀⠀⠀⠃⠈⠂⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
            ⠀⢀⣀⣄⣀⠀⣀⣀⣀⡀⠀⠀⢀⣀⡀⠀⠀⣀⣀⣀⣀⠀⣀⣀⣀⣀⣀⠀⠀⣀⣀⠀⠀⠀⠀⣀⣀⣀⣀⣀⢀⣀⣀⣀⡀⣀⡀⠀⢀⡀⣀⣀⣀⣀⣀⠀⠀⢀⣀⣀⣀⡀⠀⣀⣀⣀⣀⠀⠀⢀⣀⣄⣀⡀⠀
            ⠀⢾⣏⡉⠉⠀⣿⡏⠙⣿⡆⠀⣾⠿⣷⡀⠀⣿⡏⠉⣿⡇⠉⢹⣿⠉⠉⠀⣸⡟⣿⡄⠀⠀⠀⠉⠉⣿⠉⠉⢸⣿⠉⠉⠀⠹⣷⣰⡿⠁⠉⠉⣿⠉⠉⠀⠀⢸⣿⠉⢹⣿⠀⣿⡏⠉⣿⡆⣴⡿⠉⠉⠉⠃⠀
            ⠀⠈⠙⢿⣷⡀⣿⡷⠾⠟⠁⢸⣿⣤⣿⣧⠀⣿⡟⢿⣯⠀⠀⢸⣿⠀⠀⢠⣿⣥⣽⣷⡀⠀⠀⠀⠀⣿⠀⠀⢸⣿⠛⠛⠀⢀⣿⢿⣇⠀⠀⠀⣿⠀⠀⠀⠀⢸⣿⠻⣿⡅⠀⣿⡷⠾⠟⠁⣿⣇⠘⠛⣿⡇⠀
            ⠀⠷⠶⠾⠟⠀⠿⠇⠀⠀⠀⠿⠃⠀⠈⠿⠆⠿⠇⠀⠻⠧⠀⠸⠿⠀⠀⠾⠏⠀⠀⠻⠇⠀⠀⠀⠀⠿⠀⠀⠸⠿⠶⠶⠄⠾⠏⠈⠻⠧⠀⠀⠿⠀⠀⠀⠀⠸⠿⠀⠘⠿⠄⠿⠇⠀⠀⠀⠘⠿⠶⠶⠿⠇⠀");
        }

    }

}



