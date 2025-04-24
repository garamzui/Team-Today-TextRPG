using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using static TeamTodayTextRPG.Characterclass;

namespace TeamTodayTextRPG
{
    //프로퍼티 관련 스크립트
    partial class Player
    {
        public Character Character { get; set; }
        public DataManager DataManager { get; set; }
        public GameManager GameManager { get; set; }
        public SceneManager SceneManager { get; set; } 
        public List<int> bag { get; set; }
        public List<int> equip { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }
        public int MaxHP { get; set; }
        public int CurHP { get; set; }
        public int MaxMP { get; set; }
        public int CurMP { get; set; }
        public int Dodge { get; set; }
        public int Gold { get; set; }
        public string Name { get; set; }

        public int CharacterCode { get; set; }
        public int ItemCode { get; set; }
    }

    //스탯 관련 스크립트
    partial class Player
    {
        public void SetCharacter(int classNum, string name)
        {
            //이렇게 하면 될까요?
            Name = name;

            //데이터매니저에서 직업별 스탯을 '파싱'해서 가져온다
            //ㄴ 이제 데이터매니저에서 잘 되어있어서 이렇게 안해도 됨
            string[][] settingCharacter = DataManager.CharacterDB.
                        Parsing(DataManager.CharacterDB.Data);

            //그러면 입력값 저장하는 변수도 끌어와야 함
            switch (classNum)
            {
                case 0:
                    CharacterCode = 0;
                    break;
                case 1:
                    CharacterCode = 1;
                    break;
                case 2:
                    CharacterCode = 2;
                    break;
                case 3:
                    CharacterCode = 3;
                    break;
            }

            //직업.Default가 Characterclass에서 스탯을 세팅하는 메서드
//Characterclass의 CHAR_TYPE public 선언, '= 0' 부탁
            switch (CharacterCode)
            {
                case 0: Character = DataManager.Instance.
                        CharacterDB.List[(int)CHAR_TYPE.WARRIOR];
                    SetStat();
                    break;
                case 1: Character = DataManager.Instance.
                        CharacterDB.List[(int)CHAR_TYPE.MAGICIAN];
                    SetStat();
                    break;
                case 2: Character = DataManager.Instance.
                        CharacterDB.List[(int)CHAR_TYPE.ASSASSIN];
                    SetStat();
                    break;
                //case 3:
                //    break;
            }

            void SetStat()
            {
                Attack = Character.Attack;
                Defense = Character.Def;
                CurHP = Character.Hp;
                MaxHP = Character.MaxHp;
                CurMP = Character.Mp;
                MaxMP = Character.MaxMp;
                Dodge = Character.Dodge;
                Gold = Character.gold;
            }

            //초기 소지 장비를 bag과 equip 리스트에 저장한다
            //직업별 초기 장비가 다르다면 수정
            //이것도 마찬가지로 쉽게 접근 가능
            bag.Add(int.Parse((DataManager.ItemDB.Parsing(DataManager.ItemDB.Data)[0][0])));
            bag.Add(int.Parse((DataManager.ItemDB.Parsing(DataManager.ItemDB.Data)[3][0])));

            //초기 장비를 가지고 있되 장착은 되어있지 않은 상태로 시작해서
            //인벤토리를 처음 열면 장착&해제 튜토리얼 구현해 보는 것 괜찮을지도
        }

        public void LevelUp()
        {
            int requiredExp = 100;

            //경험치가 요구 경험치보다 크거나 같아진다.
            if (Exp >= requiredExp)
            {
                //경험치에서 요구 경험치 만큼 빼고 초과량은 현재 경험치로 남는다.
                Exp = Exp - requiredExp;

                //레벨 및 요구 경험치가 늘어난다.
                Level++;
                requiredExp += 25;

                //스탯 증가량 화면에 표시
                Character.Attack += 1;
                Character.Def += 2;

                //Console.WriteLine("축하합니다! 레벨이 올랐습니다.");
                //Console.WriteLine("공격력 +1, 방어력 +2");
                //Viewer로
            }
        }

        //특정 레벨에 스킬이 해금되는 구조
        public void UnLockSkill()
        {
            //데이터베이스에는 이미 스킬을 가지고 있으니까
            //스킬 정보를 확인할 수 있는 
            if (Level == 5)
            {

            }
        }

        //휴식 기능
        public void Rest()
        {
            Character.Hp += 50;
            Character.gold -= 500;

            //Console.WriteLine("휴식을 취했다!");
            //Console.WriteLine("체력 + 50");
            //Console.WriteLine("골드 -500");
            //Viewer로
        }
    }

    //장비 관련 스크립트
    partial class Player
    {
//SceneManager의 currentViewer 프로퍼티로 설정 부탁

        //인벤토리에 해당 아이템이 있으면
        public bool CheckBag(int code)
        {
            //해당 코드의 아이템이 bag에 있는지
            bool hasItem = bag.Contains(code);
            return hasItem;
        }

        //인벤토리에 아이템이 들어오는 경우
        public void InputBag()
        {

            //임시로 선언&초기화
            ItemCode = DataManager.Instance.ItemDB.List[()]
            int code = ItemCode;
            int prise = 0;

            //상점에서 아이템 구매
            if(currentViewer == VIEW_TYPE.SHOP)
            {
                if(Character.gold >= (int)DataManager.Instance.ItemDB.List[])
                {
                    Character.gold -= prise;

                    //해당 아이템의 코드를 bag에 저장
                    //code = 해당 아이템 코드;
                    //이렇게 거쳐서 저장하는게 괜찮나?
                    bag.Add(code);
                }

                else
                {
                    //Console.WriteLine("소지금이 부족합니다!");
                    //Viewer로
                }
            }

            if(currentViewer == VIEW_TYPE.DUNGEONCLEAR)
            {
                Random random = new Random();
                int ItemDrop = random.Next(0, 101);
                //n% 확률로
                if(ItemDrop >= 90 || ItemDrop <= 10)
                {
                    //랜덤 아이템 드롭
                    Random dropItemCode = new Random();
                    code = dropItemCode.Next(0, 15);
                    bag.Add(code);
                }
                //근데 여기서 처리하는게 맞나?
            }
        }

        //인벤토리에 아이템이 나가는 경우
        public void RemoveBag()
        {
            //임시로 선언&초기화
            int code = 0;
            int prise = 0;

            //상점에서 아이템 판매
            if (CheckBag(code) == true && currentViewer == VIEW_TYPE.SALE)
            {
                //장착중이 아니라면
                if (CheckEquip(code) == false)
                {
                    Character.gold += (int)(prise * 0.85f);
                    bag.Remove(code);
                }

                //장착중이라면
                else
                {
                    //Console.WriteLine("장착중인 아이템은 판매할 수 없습니다.");
                    //Viewer로
                }
            }

            //버리기
            if(CheckBag(code) == true && currentViewer == VIEW_TYPE.INVENTORY)
            {
                //장착중이 아니라면
                if(CheckEquip(code) == false)
                {
                    bag.Remove(code);
                }

                //장착중이라면
                else
                {
                    //Console.WriteLine("장착중인 아이템은 버릴 수 없습니다.");
                    //Viewer로
                }
            }

        }

        //인벤토리에 해당 아이템이 있으면
        public bool CheckEquip(int code)
        {
            //해당 코드의 아이템을 장착하고 있는지
            bool equipItem = equip.Contains(code);
            return equipItem;
        }

        //장비 착용
        public void EquipItem()
        {
            //임시로 선언&초기화
            int code = 0;
            int equiped = 0;

            //아이템 소지 && 미장착
            if(CheckBag(code) == true && CheckEquip(code) == false)
            {
                //같은 타입 아이템 미장착
//Item의 ITEM_TYPE public 선언 부탁
                if()
                {
                    equip.Add(code);
                }

                //같은 타입 아이템 장착중
                else
                {
                    //장착중이던 아이템 해제
                    equip.Remove(equiped);

                    //장착하려는 아이템 착용
                    equip.Add(code);
                }
            }
        }

        //장비 해제
        public void UnEquipItem()
        {
            //임시로 선언&초기화
            int code = 0;

            //장착중 이라면
            if(CheckEquip(code) == true)
            {
                //equip List에서 삭제
                equip.Remove(code);
            }

            else
            {
                //Console.WriteLine("장착중인 아이템이 아닙니다!");
                //Viewer로
            }
        }
    }
}
