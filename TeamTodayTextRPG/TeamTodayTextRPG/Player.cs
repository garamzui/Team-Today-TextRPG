using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using static TeamTodayTextRPG.Characterclass;

namespace TeamTodayTextRPG
{
    class Player
    {
        public Character character { get; set; }
        public DataManager dataManager { get; set; }
        public GameManager gameManager { get; set; }
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

        public int CharacterCode { get; set; }
        public int ItemCode { get; set; }


        public void SetCharacter(int classNum, string name)
        {
            //데이터매니저에서 직업별 스탯을 '파싱'해서 가져온다
            //ㄴ 이제 데이터매니저에서 잘 되어있어서 이렇게 안해도 됨
            string[][] settingCharacter = dataManager.CharacterDB.
                       Parsing(dataManager.CharacterDB.Data);

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
            // enum이 public이 아니라...
            switch ()
            {
                case 0: character = DataManager.Instance.
                        CharacterDB.List[(int)CHAR_TYPE.WARRIOR];
                    SetStat();
                    break;
                case 1: character = DataManager.Instance.
                        CharacterDB.List[(int)CHAR_TYPE.MAGICIAN];
                    SetStat();
                    break;
                case 2: character = DataManager.Instance.
                        CharacterDB.List[(int)CHAR_TYPE.ASSASSIN];
                    SetStat();
                    break;
                    //case 3:
                    //    break;
            }

            void SetStat()
            {
                Attack = character.Attack;
                Defense = character.Def;
                CurHP = character.Hp;
                MaxHP = character.MaxHp;
                CurMP = character.Mp;
                MaxMP = character.MaxMp;
                Dodge = character.Dodge;
                Gold = character.gold;
            }

            //초기 소지 장비를 bag과 equip 리스트에 저장한다
            //직업별 초기 장비가 다르다면 수정
            //이것도 마찬가지로 쉽게 접근 가능
            bag.Add(int.Parse((dataManager.ItemDB.Parsing(dataManager.ItemDB.Data)[0][0])));
            bag.Add(int.Parse((dataManager.ItemDB.Parsing(dataManager.ItemDB.Data)[3][0])));

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
                character.Attack += 1;
                character.Def += 2;

                //Console.WriteLine("축하합니다! 레벨이 올랐습니다.");
                //Console.WriteLine("공격력 +1, 방어력 +2");
                //Viewer로
            }

        }

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
            //ShopViewer 인스턴스 생성
            GameManager.Instance.Viewer shopViewer = new ShopViewer();

            //임시로 선언&초기화
            int code = 0;
            int gold = 0;
            int prise = 0;

            //상점에서 아이템 구매
            if(shopViewer)
            {
                if(gold >= prise)
                {
                    gold -= prise;

                    //해당 아이템의 코드를 bag에 저장
                    //code = 해당 아이템 코드;
                    //이렇게 거쳐서 저장하는게 괜찮나?
                    bag.Add(code);
                }

                else
                {
                    Console.WriteLine("소지금이 부족합니다!");
                }
            }

            if(전투가 끝났을 때)
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
            int gold = 0;
            int prise = 0;

            //상점에서 아이템 판매
            if (CheckBag(code) == true && 상점에서 판매할 때)
            {
                //장착중이라면
                if (CheckEquip(code) == true)
                {
                    Console.WriteLine("장착중인 아이템은 판매할 수 없습니다.");
                }

                //장착중이 아니라면
                else
                {
                    gold += (int)(prise * 0.85f);
                    bag.Remove(code);
                }
            }

            //버리기
            if(CheckBag(code) == true && 인벤토리에서 버릴 때)
            {
                //장착중이라면
                if(CheckEquip(code) == true)
                {
                    Console.WriteLine("장착중인 아이템은 버릴 수 없습니다.");
                }

                //장착중이 아니라면
                else
                {
                    bag.Remove(code);
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

        public void EquipItem()
        {
            //임시로 선언&초기화
            int code = 0;
            int equiped = 0;

            //아이템 소지 && 미장착
            if(CheckBag(code) == true && CheckEquip(code) == false)
            {
                //같은 타입 아이템 미장착
                if(해당 ITEM_TYPE을 장착하지 않았을 때)
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
                Console.WriteLine("장착중인 아이템이 아닙니다!");
            }
        }

        //휴식 기능
        public void Rest()
        {
            character.Hp += 50;
            character.gold -= 500;
            Console.WriteLine("휴식을 취했다!");
            Console.WriteLine("체력 + 50");
            Console.WriteLine("골드 -500");
        }
    }
}
