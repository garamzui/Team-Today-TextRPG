using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static TeamTodayTextRPG.Characterclass;

namespace TeamTodayTextRPG
{
    //프로퍼티 관련 스크립트
    partial class Player
    {
        public Character Character { get; set; }
        public List<int> Bag { get; set; }
        public List<int> Equip { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }

        public int Gold { get; set; }
        public string Name { get; set; }

        public int CharacterCode { get; set; }
        public int ItemCode { get; set; }
    }

    //스탯 관련 스크립트
    partial class Player
    {
        public void SetCharacter(int classCode, string name)
        {
            //string[][] settingCharacter = DataManager.CharacterDB.
            //Parsing(DataManager.CharacterDB.Data);


            //classCode별로 직업별 스탯 설정
            switch (classCode)
            {
                case 0:
                    Character = new Characterclass.Warrior();
                    SetStat();
                    break;
                case 1:
                    Character = new Characterclass.Magician();
                    SetStat();
                    break;
                case 2:
                    Character = new Characterclass.Assassin();
                    SetStat();
                    break;
            }

            void SetStat()
            {
                Level = 1;
                Gold = 1500;
                Name = name;
            }


        //초기 소지 장비를 bag과 Equip 리스트에 저장한다
        //직업별 초기 장비가 다르다면 수정
        //이것도 마찬가지로 쉽게 접근 가능
        Bag.Add(int.Parse((DataManager.ItemDB.Parsing(DataManager.ItemDB.Data)[0][0])));
        Bag.Add(int.Parse((DataManager.ItemDB.Parsing(DataManager.ItemDB.Data)[3][0])));

        Bag.Add(DataManager.Instance.ItemDB.List[(int)])

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
                Character.Defence += 2;

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
            Character.Gold -= 500;
//Characterclass에서 gold -> Gold로

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
        
        public bool CheckBag(int inputItemNum)
        {
            //해당 코드의 아이템이 bag에 있는지
            ItemCode = inputItemNum - 1;
            bool hasItem = Bag.Contains(ItemCode);
            return hasItem;
        }

        //인벤토리에 아이템이 들어오는 경우 1 - 상점 구매
        public void ShopToBag(int inputItemNum)
        {
            ItemCode = inputItemNum - 1;

            //아이템도 리스트로 변경된다면 이 조건식이 될 가능성이 높다는 거겠죠?
            int prise = (int)DataManager.Instance.ItemDB.List[ItemCode][5];

            //상점에서 아이템 구매
            if (currentViewer == VIEW_TYPE.SHOP)
            {
                if (Character.Gold >= prise)
                {
                    Character.Gold -= prise;

                    //해당 아이템의 코드를 bag에 저장
                    Bag.Add(ItemCode);
                }

                else
                {
                    //Console.WriteLine("소지금이 부족합니다!");
                    //Viewer로
                }
            }
        }

        //인벤토리에 아이템이 들어오는 경우 2 - 던전 클리어
        public void DungeonToBag()
        { 
            //던전 클리어 시 랜덤(20%) 확률로 아이템 드롭
            if(currentViewer == VIEW_TYPE.DUNGEONCLEAR)
            {

                int ItemDrop = GameManager.Instance.rand.Next(0, 101);
                //n% 확률로
                if(ItemDrop >= 90 || ItemDrop <= 10)
                {
                    //랜덤 아이템 드롭
                    int dropItemCode = GameManager.Instance.rand.Next(0, 아이템리스트.Length);
                    ItemCode = dropItemCode;
                    Bag.Add(ItemCode);
                }
                //근데 여기서 처리하는게 맞나?
            }
        }

        //인벤토리에 아이템이 나가는 경우 1 - 상점 판매
        public void RemoveBag(int inputItemNum)
        {
            ItemCode = inputItemNum - 1;
            int prise = (int)(DataManager.Instance.ItemDB.List[ItemCode][5]);

            //상점에서 아이템 판매
            if (CheckBag(ItemCode) == true && currentViewer == VIEW_TYPE.SALE)
            {
                //장착중이 아니라면
                if (CheckEquip(ItemCode) == false)
                {
                    Character.gold += (int)(prise * 0.85f);
                    Bag.Remove(ItemCode);
                }

                //장착중이라면
                else
                {
                    //Console.WriteLine("장착중인 아이템은 판매할 수 없습니다.");
                    //Viewer로
                }
            }

            //버리기
            if(CheckBag(inputItemNum) == true && currentViewer == VIEW_TYPE.INVENTORY)
            {
                //장착중이 아니라면
                if(CheckEquip(ItemCode) == false)
                {
                    Bag.Remove(ItemCode);
                }

                //장착중이라면
                else
                {
                    //Console.WriteLine("장착중인 아이템은 버릴 수 없습니다.");
                    //Viewer로
                }
            }

        }

        //해당 아이템을 장착중이면
        public bool CheckEquip(int equipItemNum)
        {
            ItemCode = equipItemNum - 1;
            //해당 코드의 아이템을 장착하고 있는지
            bool equipItem = Equip.Contains(ItemCode);
            return equipItem;
        }

        //장비 착용
        public void EquipItem(Equip)
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
                    Equip.Add(code);
                }

                //같은 타입 아이템 장착중
                else
                {
                    //장착중이던 아이템 해제
                    Equip.Remove(equiped);

                    //장착하려는 아이템 착용
                    Equip.Add(code);
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
                //Equip List에서 삭제
                Equip.Remove(code);
            }

            else
            {
                //Console.WriteLine("장착중인 아이템이 아닙니다!");
                //Viewer로
            }
        }
    }
}
