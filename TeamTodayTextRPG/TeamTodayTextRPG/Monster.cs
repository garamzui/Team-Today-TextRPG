﻿namespace TeamTodayTextRPG
{
    public enum MONSTER_STATE
    {
        IDLE,
        DEAD
    }
    public enum MONSTER_CODE
    {
        SLIME = 0,
        GOBLIN,
        WOLF,
        BOAR,
        ORK,
        ZAKUM
    }
    public enum MONSTER_GRADE
    {
        NORMAL = 0,
        BOSS
    }

    public abstract class Monster
    {
        public MONSTER_CODE Code { get; set; }
        public MONSTER_STATE State { get; set; } = MONSTER_STATE.IDLE;
        public MONSTER_GRADE Grade { get; set; }

        public string? Name { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int PlusAtk { get; set; }
        public int Def { get; set; }
        public int PlusDef { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }
        public string? Text { get; set; }
        public string[]? Parameter { get; set; }

        public void Init(string[] parameter)
        {
            // 0.코드 / 1.이름 / 2.레벨 / 3.공격력 / 4.방어력 / 5.체력 / 6.보상골드 / 7.보상경험치 / 8.몬스터 등급 / 9.텍스트
            Parameter = parameter;

            if (!Enum.TryParse(parameter[0], out MONSTER_CODE code))
                throw new ArgumentException("Invalid MONSTER code.");
            Code = (MONSTER_CODE)(int.Parse(Parameter[0]));
            Name = Parameter[1];
            Level = int.Parse(Parameter[2]);
            Atk = int.Parse(Parameter[3]);
            Def = int.Parse(Parameter[4]);
            Hp = int.Parse(Parameter[5]);
            MaxHp = Hp;
            RewardGold = int.Parse(Parameter[6]);
            RewardExp = int.Parse(Parameter[7]);
            if (!Enum.TryParse(parameter[8], out MONSTER_GRADE grade))
                throw new ArgumentException("Invalid MONSTER grade.");
            Text = Parameter[9];
        }

        //『효빈』 TakeDamage와 Heal 비슷한 역할을 하는 메소드 이기 때문에 ChangeHp로 합쳐서 관리하면 더 편할 것 같아요! 
        public void ManageHp(int HpChange)
        {
            if (HpChange < 0)
            {
                Hp += HpChange; //최종 계산 자료를 음수로 입력해 주어야 합니다.

                //Console.WriteLine($"{Name}이(가){HpChange}의 데미지를 입었습니다. ");
                if (Hp <= 0)
                {
                    Hp = 0;
                    Die();
                }
                /*
                Console.Write($"HP {GameManager.Instance.Dungeon.TargetMonster.Hp-HpChange} -> ");
                if (GameManager.Instance.Dungeon.TargetMonster.State == MONSTER_STATE.DEAD)
                {
                    Console.WriteLine("Dead");
                }
                else Console.WriteLine($"{GameManager.Instance.Dungeon.TargetMonster.Hp}");*/

            }
            else if (HpChange > 0)
            {
                Hp += HpChange;
                if (Hp > MaxHp) Hp = MaxHp;
                //Console.WriteLine($"{Name}이(가){HpChange}만큼 회복했습니다! 현재 HP: {Hp}/{MaxHp}");
            }
            else { Console.WriteLine("아무 일도 일어나지 않았습니다."); }
        }


        public virtual void DefaultAttack()
        {
            switch (GameManager.Instance.Dungeon.Dungeon_Monster[GameManager.Instance.Dungeon.MonsterAtkCounter].Code)
            {
                case MONSTER_CODE.SLIME:
                    GameManager.Instance.Animation.SlimeAnimation();
                    break;
                case MONSTER_CODE.GOBLIN:
                    GameManager.Instance.Animation.GoblinAnimation();
                    break;
                case MONSTER_CODE.WOLF:
                    GameManager.Instance.Animation.WolfAnimation();
                    break;
                case MONSTER_CODE.BOAR:
                    GameManager.Instance.Animation.BoarAnimation();
                    break;
                case MONSTER_CODE.ORK:
                    GameManager.Instance.Animation.OrkAnimation();
                    break;
                case MONSTER_CODE.ZAKUM:
                    GameManager.Instance.Animation.ZakumAnimation();
                    break;
                default:
                    break;

            }
        }


        public void Die()
        {
            State = MONSTER_STATE.DEAD;
            // Console.WriteLine($"{Name}은 쓰러졌다!");    『효빈』이 부분은 SceneManager에서 관리해주면 좋을거 같아요! 대신에 몬스터의 상태를 관리하는 state 변수를 변경시키겠습니다. 
            //                                                       SceneManager 에서 각 몬스터의 State를 체크! if (monster.State == MONSTER_STATE.DEAD) { ... } 
        }

        public void View_Monster_Status()
        {
            if (State == MONSTER_STATE.DEAD)
            {
                Console.WriteLine($"Lv. {Level}\t{Name}\tDead");
            }
            else Console.WriteLine($"Lv. {Level}\t{Name}\tHP {Hp}/{MaxHp}");
        }
    }

    class Slime : Monster
    {
        public Slime()
        {
            Init(DataManager.Instance.MonsterDB.List[(int)MONSTER_CODE.SLIME]);
        }
    }

    class Goblin : Monster
    {
        public Goblin()
        {
            Init(DataManager.Instance.MonsterDB.List[(int)MONSTER_CODE.GOBLIN]);
        }
    }

    class Wolf : Monster
    {
        public Wolf()
        {
            Init(DataManager.Instance.MonsterDB.List[(int)MONSTER_CODE.WOLF]);
        }
    }

    class Boar : Monster
    {
        public Boar()
        {
            Init(DataManager.Instance.MonsterDB.List[(int)MONSTER_CODE.BOAR]);
        }
    }

    class Ork : Monster
    {
        public Ork()
        {
            Init(DataManager.Instance.MonsterDB.List[(int)MONSTER_CODE.ORK]);
        }
    }

    class Zakum : Monster
    {
        public Zakum()
        {
            Init(DataManager.Instance.MonsterDB.List[(int)MONSTER_CODE.ZAKUM]);
        }
    }
}
