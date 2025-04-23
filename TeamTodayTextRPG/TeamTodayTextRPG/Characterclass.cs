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

            public string Jobname { get; set; }
            public int attack { get; set; }
            public int plusAtk { get; set; } = 0;
            public int totalAtk { get { return attack + plusAtk; } }  //다른 계산에 필요할까 싶어 합산되어 적용될 값을 따로 만들어 보았습니다.
            public int def { get; set; }
            public int plusDef { get; set; } = 0;
            public int totalDef { get { return def + plusDef; } }
            private int Hp {  get; set; }
            //Hp,Mp예외처리 속성쪽으로 옮겼습니다.
            public int hp { get { return Hp; } set { if (value < 0) Hp = 0; else if (value > maxHp) Hp = maxHp; else Hp = value; } }
            public int maxHp { get; set; }
            private int Mp { get; set; }// 새로운 스탯 mp추가 했습니다
            public int mp { get { return Mp; } set { if (value < 0) Mp = 0; else if (value > maxHp) Mp = maxHp; else Mp = value; } } 
            public int maxMp { get; set; }
            public int dodge { get; set; }
            public int plusDodge { get; set; } 
            public int totalDodge { get { return dodge + plusDodge; } }
            // 직업간 차이를 두어 보고자 dodge 스탯도 추가 해 봤습니다.
            // 전투가 어떻게 이루어질지에 따라 추가해야 할 계산식이 달라질 것 같습니다.
            // 예를 들면 
            //int num = new Random().Next(0, 26); 또는 GameManager에 static Random하나 만들어두고 돌려쓰기
            //if ((num += Charater.dodge > 20)
            //{공격을 무효화하기}
            // 이런식으로 설계하면 어떨까 합니다.
            public int gold { get; set; }
            public string[] Parameter { get; set; }
            
            public string actskillName { get; set; }
            public string passkillName { get; set; }

            public int passiveSkillLevel = 0;

            public void init(string[] data) //우선은 임의로 매서드로 초기화할 필드를 변경해 놓았습니다.
            {
                //직업이름,공격력,방어력,체력,마력,회피,골드,액티브스킬이름,패시브스킬이름
                Parameter = data.Split('/');
                Jobname = data[0];
                attack = data[1];
                def = int.Parse(initstr[2]);
                maxHp = int.Parse(initstr[3]);
                hp = maxHp;
                 maxMp = int.Parse(initstr[4]);
                mp = maxMp;
                dodge = int.Parse(initstr[5]);
                gold = int.Parse(initstr[6]);
                actskillName = (initstr[7]);
                passkillName = (initstr[8]);



            }

            public virtual string jobDescription() //직업 설명
            {
                return"";
            }
            public void ViewStatus()
            {
                Console.WriteLine($"{Jobname} {jobDescription}- 공격력 {attack} (+{plusAtk}), 방어력 {def} (+{plusDef}), HP {hp}/{maxHp}, Gold {gold}");
            }


            //기본공격을 두고, 래밸을 올리면 스킬이
            //해금되는 방식을 적용해 보고 싶었습니다.                <=『효빈』굿 아이디어입니다 :)
            // 추후에 구현이 어려울 시 조건부는 빼 버리는 것으로 하면 될 것 같습니다.
            // 스킬 이름은 스탯과 함께 초기화해서 저장해두게 해놨습니다.
            //Player의 필드들이 private되어있어 접근이 안됩니다. 요 부분은 회의 때 조율 해야 할 것 같아요.
            public virtual void DefaultAttack()
            {
                Console.WriteLine($"{Jobname}의 기본 공격");
            }


            //active 스킬은 몬스터 체력을 -= 하는 방식으로 
            //passive 스킬은 각각 직업 특성에 맞는 스탯값을 += 하는 방식으로 만들어 보려 합니다.
            public virtual void ActiveSkill(Monster m)

            {
                Console.WriteLine($"{Jobname}의 기술 {actskillName}");
            }

            public virtual void PassiveSkill(Player p)
            {
                 Console.WriteLine($"{Jobname}의 기술 {passkillName}");
            }

            public void TakeDamage(int damage)
            { 
                hp -= damage;
                if (hp <= 0)
                {
                    Die();
                }
            }
            public void Die()
            {
                Console.WriteLine("눈앞이 깜깜해진다..");
            }
            public void Heal(int heal)
            {
                hp += heal; 
            }
        }


        enum CHAR_TYPE
        { 
            WARRIOR,
            MAGICIAN,
            ASSASSIN
        }



        public class Worrior : Character
        {
            public  Worrior ()
            {
               
                //직업이름,공격력,방어력,체력,마력,회피,골드,액티브스킬이름,패시브스킬이름
                init(DataManager.Instance.CharacterDB.List[0]);
                
            }
            public override string jobDescription()
            {
               return"";
            }
            public override void ActiveSkill(Monster m)
            {
                mp -= 10;
                int SkillDamage = (totalAtk * 3) - m.Def;
                if (SkillDamage < 0)
                { SkillDamage = 1; }
                m.TakeDamage(SkillDamage);
                Console.WriteLine($"{actskillName}을 사용하여 {m.Name}이(가) {SkillDamage}의 피해를 입었습니다.");
            }

            public override void PassiveSkill(Player p)
            {
                if (p.Level >= 5 && passiveSkillLevel < 5)
                {
                    def += 2;
                    
                    passiveSkillLevel += 1;
                    if (passiveSkillLevel >= 5)
                    { def = 20; }
                }

            }

        }
        public class Magician : Character
        {
            public static Magician Default()
            {
                Magician m = new Magician();
                //직업이름,공격력,방어력,체력,마력,회피,골드,액티브스킬이름,패시브스킬이름
                m.init("마법사,3,3,50,100,3,1000,썬더볼트,마력개방");
                return m;
            }
            public override void ActiveSkill(Monster m)
            {
                mp -= 10;
                int SkillDamage = (int)((totalAtk * 10) - Math.Round(m.Def / 2.0));
                if (SkillDamage < 0)
                { SkillDamage = 1; }
                m.TakeDamage(SkillDamage);
                Console.WriteLine($"{actskillName}을 사용하여 {m.Name}이(가) {SkillDamage}의 피해를 입었습니다.");
            }
            public override void PassiveSkill(Player p)
            {
                if (p.Level >= 5 && passiveSkillLevel< 5)
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
            public Assassin Default()
            {
                Assassin a = new Assassin();
                //직업이름,공격력,방어력,체력,마력,회피,골드,액티브스킬이름,패시브스킬이름
                a.init("암살자,7,1,75,75,10,1000,연격,날쌘 몸놀림");
                return a;
            }
            public override void ActiveSkill(Monster m)
            {
                mp -= 10;
                int SkillDamage = (totalAtk * 2) - m.Def;
                
                Random critical = new Random();
                int criticalHit =critical.Next(0,10);
                
                if (criticalHit <= 2)
                {
                    SkillDamage *= 2;
                    Console.WriteLine("치명타!");
                }

                if (SkillDamage < 0)
                { SkillDamage = 1; }
               
                for (int i = 0; i < 2; i++)
                { 
                    m.TakeDamage(SkillDamage); 
                    Console.WriteLine($"{actskillName}을 사용하여 {m.Name}이(가) {SkillDamage}의 피해를 입었습니다.");
                }
                

                

            }
            public override void PassiveSkill(Player p)
            {
                if (p.Level >= 5 && passiveSkillLevel < 5)
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



