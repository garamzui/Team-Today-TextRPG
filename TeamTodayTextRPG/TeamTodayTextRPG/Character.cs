namespace TeamTodayTextRPG
{
    public enum CHAR_TYPE
    {
        WARRIOR,
        MAGICIAN,
        ASSASSIN
    }
    public enum CHAR_STATE
    {
        IDLE,
        DEAD
    }

    public abstract class Character
    {
        public List<string> ChooseBehavior { get; private set; }

        public Character()
        {
            ChooseBehavior = new List<string>();
            ChooseBehavior.Add("기본 공격");
            ChooseBehavior.Add("스킬");

        }

        public CHAR_TYPE Code { get; set; }
        public CHAR_STATE State { get; set; } = CHAR_STATE.IDLE;
        public string Jobname { get; set; }
        public int Attack { get; set; }
        public int PlusAtk { get; set; } = 0;
        public int TotalAtk { get { return Attack + PlusAtk; } }  //다른 계산에 필요할까 싶어 합산되어 적용될 값을 따로 만들어 보았습니다.
        public int Defence { get; set; }
        public int PlusDef { get; set; } = 0;
        public int TotalDef { get { return Defence + PlusDef; } }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mp { get; set; }
        public int MaxMp { get; set; }
        public int Dodge { get; set; }
        public int PlusDodge { get; set; }
        public int TotalDodge { get { return Dodge + PlusDodge; } }
        // 직업간 차이를 두어 보고자 Dodge 스탯도 추가 해 봤습니다.


        public string[] Parameter { get; set; }

        public string ActskillName { get; set; }
        public string PasskillName { get; set; }

        public int PassiveSkillLevel = 1;
        public int MaxPassiveSkillLevel { get; set; } = 5;  // <= 수정 생각해보기 위치...static


        public void Init(string[] data) //우선은 임의로 매서드로 초기화할 필드를 변경해 놓았습니다.
        {
            //직업이름,공격력,방어력,체력,마력,회피,액티브스킬이름,패시브스킬이름
            Code = (CHAR_TYPE)int.Parse(data[0]);
            Jobname = data[1];
            Attack = int.Parse(data[2]);
            Defence = int.Parse(data[3]);
            Hp = int.Parse(data[4]);
            MaxHp = Hp;
            Mp = int.Parse(data[5]);
            MaxMp = Mp;
            Dodge = int.Parse(data[6]);
            ActskillName = (data[7]);
            PasskillName = (data[8]);
        }

        public virtual string JobDescription() //직업 설명
        {
            return "";
        }
        //영훈) ↓보시면 참조 0개라고 쓰여있어요 그러면 이 메서드는 호출이 안되었다는 뜻이죠
        //Viewer 스크립트의 109번 줄부터 보면 거기에서 이미 비슷한 동작을 하고 있어서
        //ViewStatus() 메서드는 지우셔도 될거같아요!



        //기본공격을 두고, 래밸을 올리면 스킬이
        //해금되는 방식을 적용해 보고 싶었습니다.                <=『효빈』굿 아이디어입니다 :)
        // 추후에 구현이 어려울 시 조건부는 빼 버리는 것으로 하면 될 것 같습니다.
        // 스킬 이름은 스탯과 함께 초기화해서 저장해두게 해놨습니다.


        public abstract int DefaultAttack();

        //active 스킬은 몬스터 체력을 -= 하는 방식으로 
        //passive 스킬은 각각 직업 특성에 맞는 스탯값을 += 하는 방식으로 만들어 보려 합니다.
        public abstract int ActiveSkill();
        public abstract void PassiveSkill();

        public void ManageHp(int HpChange)//Hp관리용 매서드 입니다. 공격 연출시 최종 계산 자료를 음수로 입력하여야합니다
        {
            if (HpChange < 0)
            {
                Hp += HpChange;
                {
                    if (Hp < 0) Hp = 0;
                }

                if (Hp == 0)
                {
                    Die();
                }
            }
            else if (HpChange > 0)
            {
                Hp += HpChange;
                if (Hp > MaxHp) Hp = MaxHp;
            }
            // else { Console.WriteLine("아무 일도 일어나지 않았습니다."); }// 우선 예외처리 때문에 작성해 두었습니다.
        }
        public void Die()
        {
            State = CHAR_STATE.DEAD;
            //Console.WriteLine("눈앞이 깜깜해진다..");
            //GameManager.Instance.Animation.GameOverAnimation();
        }

        public void ManageMp(int ChangeMp) //Mp관리용 메서드입니다. Mp소모 매서드 이용시 최종 계산 자료를 음수로 입력하여야합니다
        {
            if (ChangeMp < 0)
            {
                Mp += ChangeMp;
                //Console.WriteLine($" Mp {ChangeMp}소모 현재 Mp {Mp}/{MaxMp}");
                if (Mp < 0) Mp = 0;
            }

            else if (ChangeMp > 0)
            {
                Mp += ChangeMp;
                if (Mp > MaxMp) Mp = MaxMp;
                //Console.WriteLine($"{ChangeMp}만큼 MP를 회복했습니다! 현재 MP: {Mp}/{MaxMp}");
            }
            else { }

        }
        public bool CheckDodge()
        {
            int DodgeHit = GameManager.Instance.Rand.Next(1, 76);
            if (GameManager.Instance.Player.Character.TotalDodge > DodgeHit)
            {
                return true;
            }
            else return false;
        }
    }




    public class Warrior : Character
    {
        public Warrior()
        {
            Init(DataManager.Instance.CharacterDB.List[(int)CHAR_TYPE.WARRIOR]);
        }
        public override string JobDescription()
        {
            return "높은 방어력,기본 공격력,체력";
        }
        public override int DefaultAttack()
        {
            int attackDamage = GameManager.Instance.Player.Character.TotalAtk - GameManager.Instance.Dungeon.TargetMonster.Def;
            if (attackDamage <= 0)
                attackDamage = 1;



            //GameManager.Instance.Dungeon.TargetMonster.ManageHp(-attackDamage);

            return attackDamage;
        }
        public override int ActiveSkill()
        {
            int skillDamage = (TotalAtk * 3) - GameManager.Instance.Dungeon.TargetMonster.Def;
            if (skillDamage <= 0)
                skillDamage = 1;

            if (Mp >= 10)
            {
                ManageMp(-10);


                //GameManager.Instance.Dungeon.TargetMonster.ManageHp(-skillDamage);

                return skillDamage;
            }
            else
            {
                //Console.WriteLine("MP가 모자랍니다.");
                return skillDamage = 0;
            }
        }

        public override void PassiveSkill()
        {
            if (GameManager.Instance.Player.Level >= 3 && PassiveSkillLevel < MaxPassiveSkillLevel)
            {
                PassiveSkillLevel += 1;

                if (PassiveSkillLevel == MaxPassiveSkillLevel)
                {

                    SceneManager.Instance.ColText($"\t>> 『{PasskillName}』의 Lv이 최대가 되었습니다\n\t\t==☆ 최대 레벨 보상으로 기본 방어도가 20이 됩니다 ☆==\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
                    Defence = 20;
                }
                else
                {
                    SceneManager.Instance.ColText($"\t>> 『{PasskillName}』의 Lv이 1증가 하였습니다 [{PassiveSkillLevel - 1}->{PassiveSkillLevel}]\t| 방어도 +2\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
                    Defence += 2;
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
        public Magician()
        {
            Init(DataManager.Instance.CharacterDB.List[(int)CHAR_TYPE.MAGICIAN]);
        }
        public override string JobDescription()
        {
            return "방어 무시, 높은 마나, 스킬의존성";
        }
        public override int DefaultAttack()
        {
            int attackDamage = GameManager.Instance.Player.Character.TotalAtk - GameManager.Instance.Dungeon.TargetMonster.Def;
            if (attackDamage <= 0)
                attackDamage = 1;



            //GameManager.Instance.Dungeon.TargetMonster.ManageHp(-attackDamage);

            return attackDamage;
        }
        public override int ActiveSkill()
        {
            double itd = Math.Round(GameManager.Instance.Dungeon.TargetMonster.Def / 2.0); //방어무시를 구현하기위해서 방어도를 반으로 나누고 반올림하였습니다.
            if (itd == 1)
            {
                itd = 0;
            }

            int skillDamage = (int)((TotalAtk * 10) - itd);
            if (skillDamage <= 0)
                skillDamage = 1;

            if (Mp >= 10)
            {
                ManageMp(-10);


                //GameManager.Instance.Dungeon.TargetMonster.ManageHp(-skillDamage);

                return skillDamage;
            }
            else
            {
                //Console.WriteLine("MP가 모자랍니다.");
                return skillDamage = 0;
            }
        }
        public override void PassiveSkill()
        {
            if (GameManager.Instance.Player.Level >= 3 && PassiveSkillLevel < MaxPassiveSkillLevel)
            {
                PassiveSkillLevel += 1;

                if (PassiveSkillLevel == MaxPassiveSkillLevel)
                {
                    SceneManager.Instance.ColText($"\t>> 『{PasskillName}』의 Lv이 최대가 되었습니다\n\t\t==☆ 최대 레벨 보상으로 최대 마나가 500이 됩니다 ☆==\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
                    MaxMp = 500;
                    Mp = MaxMp;
                }
                else
                {
                    SceneManager.Instance.ColText($"\t>> 『{PasskillName}』의 Lv이 1증가하였습니다 [ {PassiveSkillLevel - 1} -> {PassiveSkillLevel} ]\t| 최대 마나 +50\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
                    MaxMp += 50;
                    Mp = MaxMp;
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
        public Assassin()
        {
            Init(DataManager.Instance.CharacterDB.List[(int)CHAR_TYPE.ASSASSIN]);
        }
        public override string JobDescription()
        {
            return "높은 회피, 크리티컬 히트";
        }
        public bool Critical(ref int damage)
        {
            int criticalHit = GameManager.Instance.Rand.Next(0, 10);

            if (criticalHit <= 2) //크리티컬 확률 계산
            {
                damage *= 2;
                return true;
            }
            else
                return false;
        }
        public override int DefaultAttack()
        {
            int attackDamage = GameManager.Instance.Player.Character.TotalAtk - GameManager.Instance.Dungeon.TargetMonster.Def;
            if (attackDamage <= 0) attackDamage = 1;



            //GameManager.Instance.Dungeon.TargetMonster.ManageHp(-attackDamage);

            return attackDamage;

        }
        public override int ActiveSkill()
        {
            int skillDamage = (TotalAtk * 2) - GameManager.Instance.Dungeon.TargetMonster.Def;
            if (skillDamage <= 0)
            { skillDamage = 1; }
            if (Mp >= 10)
            {
                ManageMp(-10);


                return skillDamage;
            }
            else
            {
                //Console.WriteLine("MP가 모자랍니다.");
                return 0;
            }
        }
        public override void PassiveSkill() //추후에 Player의 Level UP 메서드에서 호출 해주어야 함
        {
            if (GameManager.Instance.Player.Level >= 3 && PassiveSkillLevel < MaxPassiveSkillLevel)
            {
                PassiveSkillLevel += 1;

                if (PassiveSkillLevel == MaxPassiveSkillLevel)
                {
                    SceneManager.Instance.ColText($"\t>> 『{PasskillName}』의 Lv이 최대가 되었습니다\n\t\t==☆ 최대 레벨 보상으로 기본 회피율이 25가 됩니다 ☆==\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
                    Dodge = 25;
                }
                else
                {
                    SceneManager.Instance.ColText($"\t>> 『{PasskillName}』의 Lv이 1증가 하였습니다 [ {PassiveSkillLevel - 1} -> {PassiveSkillLevel} ]\t| 공격력 +1 회피율 +2\n", ConsoleColor.DarkCyan, ConsoleColor.Black);
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
















