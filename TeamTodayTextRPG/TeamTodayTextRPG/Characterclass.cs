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
            public int Attack { get; set; }
            public int PlusAtk { get; set; } = 0;
            public int TotalAtk { get { return Attack + PlusAtk; } }  //다른 계산에 필요할까 싶어 합산되어 적용될 값을 따로 만들어 보았습니다.
            public int Def { get; set; }
            public int PlusDef { get; set; } = 0;
            public int TotalDef { get { return Def + PlusDef; } }
            public int Hp { get; set; }
            public int MaxHp { get; set; }
            public int Mp { get;set; }
            public int MaxMp { get; set; }
            public int Dodge { get; set; }
            public int PlusDodge { get; set; } 
            public int TotalDodge { get { return Dodge + PlusDodge; } }
            // 직업간 차이를 두어 보고자 Dodge 스탯도 추가 해 봤습니다.
            // 전투가 어떻게 이루어질지에 따라 추가해야 할 계산식이 달라질 것 같습니다.
            // 예를 들면 
            //int num = new Random().Next(0, 26); 또는 GameManager에 static Random하나 만들어두고 돌려쓰기
            //if ((num += Charater.Dodge > 20)
            //{공격을 무효화하기}
            // 이런식으로 설계하면 어떨까 합니다.
            public int gold { get; set; }
            public string[] Parameter { get; set; }
            
            public string ActskillName { get; set; }
            public string PasskillName { get; set; }

            public int PassiveSkillLevel = 0;
            protected const int MaxPassiveSkillLevel = 5;
            public void init(string[] data) //우선은 임의로 매서드로 초기화할 필드를 변경해 놓았습니다.
            {
                //직업이름,공격력,방어력,체력,마력,회피,골드,액티브스킬이름,패시브스킬이름
                Parameter = data;
                Jobname = data[0];
                Attack = int.Parse(data[1]);
                Def = int.Parse(data[2]);
                Hp = int.Parse(data[3]);
                MaxHp = Hp;
                Mp = int.Parse(data[4]);
                MaxMp = Mp;
                Dodge = int.Parse(data[5]);
                gold = int.Parse(data[6]);
                ActskillName = (data[7]);
                PasskillName = (data[8]);



            }

            public virtual string JobDescription() //직업 설명
            {
                return"";
            }
            public void ViewStatus()
            {
                Console.WriteLine($"{Jobname} {JobDescription()}\n- 공격력 {Attack} (+{PlusAtk}), 방어력 {Def} (+{PlusDef}), HP {Hp}/{MaxHp}, Gold {gold}");
            }


            //기본공격을 두고, 래밸을 올리면 스킬이
            //해금되는 방식을 적용해 보고 싶었습니다.                <=『효빈』굿 아이디어입니다 :)
            // 추후에 구현이 어려울 시 조건부는 빼 버리는 것으로 하면 될 것 같습니다.
            // 스킬 이름은 스탯과 함께 초기화해서 저장해두게 해놨습니다.
            
            public virtual void DefaultAttack()
            {
                Console.WriteLine($"{Jobname}의 기본 공격");
            }


            //active 스킬은 몬스터 체력을 -= 하는 방식으로 
            //passive 스킬은 각각 직업 특성에 맞는 스탯값을 += 하는 방식으로 만들어 보려 합니다.
            public virtual void ActiveSkill(Monster m)

            {
                Console.WriteLine($"{Jobname}의 기술 {ActskillName}");
            }

            public virtual void PassiveSkill(Player p)
            {
                 Console.WriteLine($"{Jobname}의 기술 {PasskillName}");
            }

            public void TakeDamage(int damage)
            {

                int DodgeHit = Characterclass.rng.Next(1, 76);// 피격 메서드에 회피를 구현 해봤습니다.
                if (TotalDodge > DodgeHit)
                {
                    Console.WriteLine("공격을 회피했습니다!");
                    return;
                }
                else
                { 
                    Hp -= damage;
                    if (Hp < 0) Hp = 0; 
                    Console.WriteLine($"{damage}의 피해를 입었습니다! \n현재 Hp : {Hp}/{MaxHp}");

                }
                            
                if (Hp == 0)
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
                Hp += heal;
                if (Hp > MaxHp) Hp = MaxHp;
                Console.WriteLine($"{heal}만큼 회복했습니다! 현재 HP: {Hp}/{MaxHp}");
            }
            public void UsingMp(int use) //Mp관리용 메서드입니다.
            { 
                Mp -= use;
                if (Mp < 0) Mp = 0;
            }
            public void RecoverMp(int Recover)
            {
                Mp += Recover;
                if (Mp > MaxMp) Mp = MaxMp;
                Console.WriteLine($"{Recover}만큼 MP를 회복했습니다! 현재 MP: {Mp}/{MaxMp}");
            }
        }


        enum CHAR_TYPE
        { 
            WARRIOR,
            MAGICIAN,
            ASSASSIN
        }

        public static Random rng = new Random();

        public class Worrior : Character
        {
            public  Worrior ()
            {
                init(DataManager.Instance.CharacterDB.List[(int)CHAR_TYPE.WARRIOR]);
            }
            public override string JobDescription()
            {
               return"높은 방어력,기본 공격력,체력";
            }
            public override void ActiveSkill(Monster m)
            {
                if (Mp >= 10)
                {
                    UsingMp(10);
                    int SkillDamage = (TotalAtk * 3) - m.Def;
                    if (SkillDamage < 0)
                    { SkillDamage = 1; }
                    m.TakeDamage(SkillDamage);
                    Console.WriteLine($"{ActskillName}을 사용하여 {m.Name}이(가) {SkillDamage}의 피해를 입었습니다.");
                }
                else
                {
                    Console.WriteLine("MP가 모자랍니다.");

                }
            }

            public override void PassiveSkill(Player p)
            {
                if (p.Level >= 3 && PassiveSkillLevel < MaxPassiveSkillLevel)
                {
                   PassiveSkillLevel += 1;

                    if (PassiveSkillLevel == MaxPassiveSkillLevel)
                    {
                        Console.WriteLine($"{PasskillName}의 Lv이 최대가 되었습니다. 최대 래벨 보상으로 방어도가 20이 됩니다.");
                        Def = 20;
                    }
                    else
                    {
                        Console.WriteLine($"{PasskillName}의 Lv이 1증가하였습니다. 방어도 +2");
                        Def += 2;
                    }
                }
                else if (PassiveSkillLevel > MaxPassiveSkillLevel)
                { 
                 //아무 동작도 안하려고 비워뒀습니다. 
                }
               
            }

        }
        public class Magician : Character
        {
            public  Magician()
            {
                init(DataManager.Instance.CharacterDB.List[(int)CHAR_TYPE.MAGICIAN]);
            }
            public override string JobDescription()
            {
                return "방어 무시, 높은 마나, 스킬의존성";
            } 
           
        public override void ActiveSkill(Monster m)
            {
                if (Mp >= 10)
                {
                    UsingMp(10);
                    int SkillDamage = (int)((TotalAtk * 10) - Math.Round(m.Def / 2.0)); //방어무시를 구현하기위해서 방어도를 반으로 나누고 반올림하였습니다.
                    if (SkillDamage < 0)
                    { SkillDamage = 1; }
                    m.TakeDamage(SkillDamage);
                    Console.WriteLine($"{ActskillName}을 사용하여 {m.Name}이(가) {SkillDamage}의 피해를 입었습니다.");
                }
                else 
                {
                    Console.WriteLine("MP가 모자랍니다.");

                }
            }
            public override void PassiveSkill(Player p)
            {
                if (p.Level >= 3 && PassiveSkillLevel < MaxPassiveSkillLevel)
                {
                    PassiveSkillLevel += 1;

                    if (PassiveSkillLevel == MaxPassiveSkillLevel)
                    {
                        Console.WriteLine($"{PasskillName}의 Lv이 최대가 되었습니다. 최대 래벨 보상으로 최대마나가 500이 됩니다.");
                        MaxMp = 500;
                        Mp += 500;
                    }
                    else
                    {
                        Console.WriteLine($"{PasskillName}의 Lv이 1증가하였습니다. 최대 마나 + 50");
                        MaxMp += 50;
                        Mp += 50;
                    }
                }
                else if (PassiveSkillLevel > MaxPassiveSkillLevel)
                {
                    //아무 동작도 안하려고 비워뒀습니다. 
                }

            }
        }
        public class Assassin : Character
        {
            public Assassin ()
            {
            init(DataManager.Instance.CharacterDB.List[(int)CHAR_TYPE.ASSASSIN]);
            }
            public override string JobDescription()
            {
                return "높은 회피, 크리티컬 히트";
            }
            public override void ActiveSkill(Monster m)
            {
                if (Mp >= 10)
                {
                    UsingMp(10);
                    int SkillDamage = (TotalAtk * 2) - m.Def;
                    if (SkillDamage < 0)
                    { SkillDamage = 1; }

                    int criticalHit = Characterclass.rng.Next(0, 10); // 추후 게임매니저 공용 랜덤으로 전환


                    if (criticalHit <= 2) //크리티컬 확률 계산
                    {
                        SkillDamage *= 2;
                        Console.WriteLine("치명타!");
                    }


                    for (int i = 0; i < 2; i++) //2연격 구현
                    {
                        m.TakeDamage(SkillDamage);
                        Console.WriteLine($"{ActskillName}을 사용하여 {m.Name}이(가) {SkillDamage}의 피해를 입었습니다.");
                    }
                }
                else
                {
                    Console.WriteLine("MP가 모자랍니다.");

                }




            }
            public override void PassiveSkill(Player p) //추후에 Player의 Level UP 메서드에서 호출 해주어야 함
            {
                if (p.Level >= 3 && PassiveSkillLevel < MaxPassiveSkillLevel)
                {
                    PassiveSkillLevel += 1;

                    if (PassiveSkillLevel == MaxPassiveSkillLevel)
                    {
                        Console.WriteLine($"{PasskillName}의 Lv이 최대가 되었습니다. 최대 래벨 보상으로 회피가 25가 됩니다.");
                        Dodge = 25;
                        
                    }
                    else
                    {
                        Console.WriteLine($"{PasskillName}의 Lv이 1증가하였습니다. 회피 +2 공격력+1");
                        Dodge += 2;
                        Attack += 1;
                        
                    }
                }
                else if (PassiveSkillLevel > MaxPassiveSkillLevel)
                {
                    //아무 동작도 안하려고 비워뒀습니다. 
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



