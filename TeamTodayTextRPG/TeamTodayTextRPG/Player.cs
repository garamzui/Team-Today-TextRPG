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
        //모호성 오류 뜨는게 Dungeon에서 class Player를 선언하셨더라고요
        //그리고 아이템에서도 Name을 똑같이 선언해서 그렇습니다
        //이름 조율 필요
        //생각해보니 회의중에 바꿨던 내용을 메인에 안올려주셔서
        //충돌이 있을지 모릅니다
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
        //SceneManager에서 사용할 메서드
        public void SetCharacter(int classCode, string name)
        {
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

            //천 옷과 목검 Bag 리스트에 저장한다
            //직업별 초기 장비가 다르다면 수정
            Bag.Add(DataManager.Instance.ItemDB.List[0].Code);
            Bag.Add(DataManager.Instance.ItemDB.List[4].Code);

            //초기 장비를 가지고 있되 장착은 되어있지 않은 상태로 시작해서
            //인벤토리를 처음 열면 장착&해제 튜토리얼 구현해 보는 것 괜찮을지도
        }


        public void LevelUp()
        {
            int requiredExp = 100;

    //던전 클리어시 처치한 몬스터에 따라 경험치를 얻는 구조 필요
            //경험치가 요구 경험치보다 크거나 같아진다.
            if (Exp >= requiredExp)
            {
                //경험치에서 요구 경험치 만큼 빼고 초과량은 현재 경험치로 남는다.
                Exp -= requiredExp;

                //레벨 및 요구 경험치 스탯이 늘어난다.
                Level++;
                requiredExp += 25;
                Character.Attack += 1;
                Character.Defence += 2;
                Character.PassiveSkill();
                //스탯 증가량 화면에 표시
                //Console.WriteLine("축하합니다! 레벨이 올랐습니다.");
                //Console.WriteLine("공격력 +1, 방어력 +2");
                //Viewer로
            }
        }

        //휴식 기능
        public void Rest()
        {
            Character.Hp += 50;
            Gold -= 500;
        }
    }

    //장비 관련 스크립트
    partial class Player
    {
        
        public bool CheckBag(int inputItemNum)
        {
            //해당 코드의 아이템이 bag에 있는지
            ItemCode = inputItemNum - 1;
            bool hasItem = Bag.Contains(ItemCode);
            return hasItem;
        }

        //인벤토리에 아이템이 들어오는 경우 1 - 상점 구매
        //다시 합쳐
        public void InputBag(int inputItemNum, VIEW_TYPE type)
        {
            ItemCode = inputItemNum;
            int prise = DataManager.Instance.ItemDB.List[inputItemNum].Value;

            //상점에서 아이템 구매
            if (type == VIEW_TYPE.PURCHASE)
            {
                if (Gold >= prise)
                {
                    Gold -= prise;

                    //해당 아이템의 코드를 bag에 저장
                    Bag.Add(ItemCode);
                }
            }

            //인벤토리에 아이템이 들어오는 경우 2 - 던전 클리어
            //던전 클리어 시 랜덤(20%) 확률로 아이템 드롭
            if(type == VIEW_TYPE.DUNGEONCLEAR)
            {
                int ItemDrop = GameManager.Instance.rand.Next(0, 101);
                //20% 확률로
                if(ItemDrop >= 90 || ItemDrop <= 10)
                {
                    //랜덤 아이템 드롭
                    int dropItemCode = GameManager.Instance.rand.Next(0, 아이템리스트.Length);
                    ItemCode = dropItemCode;
                    Bag.Add(ItemCode);
                }
            }
        }

        //인벤토리에 아이템이 나가는 경우 1 - 상점 판매
        public void RemoveBag(int inputItemNum, VIEW_TYPE type)
        {
            ItemCode = inputItemNum;
            int prise = DataManager.Instance.ItemDB.List[inputItemNum].Value;

            //상점에서 아이템 판매와 버리기
            //Bag에 있고 장착중이 아니라면
            if (CheckBag(ItemCode) == true && CheckEquip(ItemCode) == false)
            {
                //판매하기 화면이라면
                if (type == VIEW_TYPE.SALE)
                {
                    Gold += (int)(prise * 0.85f);
                }
                Bag.Remove(ItemCode);
            }
        }

        //해당 아이템을 장착중이면
        public bool CheckEquip(int equipItemNum)
        {
            ItemCode = equipItemNum;
            //해당 코드의 아이템을 장착하고 있는지
            bool equipItem = Equip.Contains(ItemCode);
            return equipItem;
        }

        //장비 착용
        public void EquipItem(int equipItemNum, ITEM_TYPE type)
        {
            ItemCode = equipItemNum;
            //private라 보호수준 오류
            int equiped = -1;
                
            //아이템 소지 && 미장착
            if (CheckBag(ItemCode) == true && CheckEquip(ItemCode) == false)
            {
                //같은 타입 아이템 미장착
                //이미 장착한 아이템의 타입 != 장착하려는 아이템의 타입
                //equiped(장착한 아이템 타입 저장) !=
                if(equiped != )
                {
                    Equip.Add(ItemCode);
                    equiped = DataManager.Instance.ItemDB.List[equipItemNum].Type;
                }

                //같은 타입 아이템 장착중
                else
                {
                    //장착중이던 아이템 해제
                    Equip.Remove(equiped);

                    //장착하려는 아이템 착용
                    Equip.Add(ItemCode);
                }
            }
        }

        //장비 해제
        public void UnEquipItem(int equipItemNum)
        {
            ItemCode = equipItemNum;

            //장착중 이라면
            if(CheckEquip(ItemCode) == true)
            {
                //Equip List에서 삭제
                Equip.Remove(ItemCode);
            }
        }
    }
}
