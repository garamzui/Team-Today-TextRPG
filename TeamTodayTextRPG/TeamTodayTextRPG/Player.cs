using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace TeamTodayTextRPG
{
    class Player
    {
        public Characterclass characterClass { get; set; }
        public DataManager dataManager { get; set; }
        public List<int> bag { get; set; }
        public List<int> equip { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        //public int AttackPower { get; set; }
        //public int DefensePower { get; set; }
        //public int HP { get; set; }
        //public int MP { get; set; }
        public int CharacterCode { get; set; }
        public int ItemCode { get; set; }



        public void SetCharacter()
        {
            //데이터매니저에서 직업별 스탯을 '파싱'해서 가져온다
            string[][] settingCharacter = dataManager.CharacterDB.Parsing(dataManager.CharacterDB.Data);

            //CharacterCode의 경우의 수에 따라 스탯을 설정한다
            //CharacterCode = 
            //switch (CharacterCode)
            //{
            //    case0:
            //break;
            //    case1:
            //break;

            //}
            //초기 소지 장비를 bag과 equip 리스트에 저장한다
            
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

                //Console.WriteLine("축하합니다! 레벨이 올랐습니다.");
                //스탯 증가량 화면에 표시
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
            //임시로 선언&초기화
            int code = 0;
            int gold = 0;
            int prise = 0;

            //상점에서 아이템 구매
            if(상점에서 아이템을 구매하려할 때)
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
                //n% 확률로 랜덤 아이템 드롭
                //근데 여기서 처리하는게 맞나?
                Random random = new Random();
                int ItemDrop = random.Next(0, 101);
                if(ItemDrop >= 85)
                {
                    Random dropItemCode = new Random();
                    code = dropItemCode.Next(0, 15);
                    bag.Add(code);
                }
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
                    Console.WriteLine("장착중인 아이템은 버릴 수 없습니다.")
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
    }
}
