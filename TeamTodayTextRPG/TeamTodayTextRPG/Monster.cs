namespace TeamTodayTextRPG
{
    public enum MONSTER_STATE
    {
        IDLE,
        DEAD
    }
    public enum MONSTER_CODE
    {
        SLIME,
        GOBLIN,
        WOLF,
        BOAR,
        ORK,
        ZAKUM
    }
    public enum MONSTER_GRADE
    {
        NORMAL,
        BOSS
    }

    public abstract class Monster
    {
        public MONSTER_CODE Code { get; set; }
        public MONSTER_STATE State { get; set; } = MONSTER_STATE.IDLE;
        public MONSTER_GRADE Grade { get; set; }

        public string? Name { get; set; }
        public int Level { get; set; }
        public int Attack { get; set; }
        public int PlusAtk { get; set; }
        public int Defence { get; set; }
        public int PlusDef { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Mp { get; set; }
        public int MaxMp { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }
        public string? Text { get; set; }
        public string[]? Parameter { get; set; }

        //『효빈』 TakeDamage와 Heal 비슷한 역할을 하는 메소드이기 때문에 ChangeHp로 합쳐서 관리하면 더 편할 것 같아요! 
        public void ManageHp(int HpChange)
        {
            if (HpChange < 0)
            {
                Hp += HpChange;

                if (Hp <= 0)
                {
                    Hp = 0;
                    Die();
                }
            }
            else if (HpChange > 0)
            {
                Hp += HpChange;
                if (Hp > MaxHp) Hp = MaxHp;
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
        }

        public void View_Monster_Status()
        {
            if (State == MONSTER_STATE.DEAD)
            {
                Console.Write($"Lv. {Level}\t{Name}\tDead\t\t");
            }
            else Console.Write($"Lv. {Level}\t{Name}\tHP {Hp}/{MaxHp}\t");
        }
    }

    class Slime : Monster
    {
        public Slime()
        {
        }
    }

    class Goblin : Monster
    {
        public Goblin()
        {
        }
    }

    class Wolf : Monster
    {
        public Wolf()
        {
        }
    }

    class Boar : Monster
    {
        public Boar()
        {
        }
    }

    class Ork : Monster
    {
        public Ork()
        {
        }
    }

    class Zakum : Monster
    {
        public Zakum()
        {
        }
    }
}
