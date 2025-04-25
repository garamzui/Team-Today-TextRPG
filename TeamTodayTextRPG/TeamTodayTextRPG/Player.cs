using System;
using System.Numerics;
using System.Threading;
using System.Xml.Linq;
using TeamTodayTextRPG;

namespace TeamTodayTextRPG
{
    //프로퍼티 관련 스크립트
    public partial class Player
    {
        public Character Character { get; set; }
        public List<int> Bag { get; set; }
        public List<int> Equip { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }

        public int Gold { get; set; }
        public string Name { get; set; }
    }

    //스탯 관련 스크립트
    public partial class Player
    {

        public Player()
        {
            Bag = new List<int>();
            Equip = new List<int>();
            //SetCharacter()
        }

        //SceneManager에서 사용할 메서드
        public void SetCharacter(int classCode, string name)
        {
            //classCode별로 직업별 스탯 설정
            switch (classCode - 1)
            {
                case 0:
                    Character = new Warrior();
                    SetStat();
                    break;
                case 1:
                    Character = new Magician();
                    SetStat();
                    break;
                case 2:
                    Character = new Assassin();
                    SetStat();
                    break;
            }

            void SetStat()
            {
                Level = 1;
                Gold = 150000;
                Name = name;
            }

            //천 옷과 목검 Bag 리스트에 저장한다
            //직업별 초기 장비가 다르다면 수정
            Bag.Add(int.Parse(DataManager.Instance.ItemDB.List[0][0])); // 천 옷 기본제공
            Bag.Add(int.Parse(DataManager.Instance.ItemDB.List[4][0])); // 목검 기본제공

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
    public partial class Player
    {
        public bool CheckBag(int inputItemCode)
        {
            //해당 코드의 아이템이 bag에 있는지
            return Bag.Contains(inputItemCode);
        }

        //인벤토리에 아이템이 들어오는 경우 1 - 상점 구매
        public void InputBag(int inputItemCode, VIEW_TYPE type)
        {
            // 7번은 가격!
            int prise = int.Parse(DataManager.Instance.ItemDB.List[inputItemCode][7]);

            //상점에서 아이템 구매
            if (type == VIEW_TYPE.PURCHASE)
            {
                if (Gold >= prise)
                {
                    Gold -= prise;

                    //해당 아이템의 코드를 bag에 저장
                    Bag.Add(inputItemCode);
                }
            }

            //인벤토리에 아이템이 들어오는 경우 2 - 던전 클리어
            //던전 클리어 시 랜덤(20%) 확률로 아이템 드롭
            if(type == VIEW_TYPE.DUNGEON_CLEAR)
            {
                int ItemDrop = GameManager.Instance.Rand.Next(0, 101);
                //20% 확률로
                if(ItemDrop >= 90 || ItemDrop <= 10)
                {
                    //랜덤 아이템 드롭
                    int dropItemCode = GameManager.Instance.Rand.Next(0, DataManager.Instance.ItemDB.List.Count);
                    Bag.Add(dropItemCode);
                }
            }
        }

        //인벤토리에 아이템이 나가는 경우 1 - 상점 판매
        public void RemoveBag(int inputItemCode, VIEW_TYPE type)
        {
            int prise = int.Parse(DataManager.Instance.ItemDB.List[inputItemCode][7]);

            //상점에서 아이템 판매와 버리기
            //Bag에 있고 장착중이 아니라면
            if (CheckBag(inputItemCode) == true && CheckEquip(inputItemCode) == false)
            {
                //판매하기 화면이라면
                if (type == VIEW_TYPE.SALE)
                {
                    Gold += (int)(prise * 0.85f);
                }
                Bag.Remove(inputItemCode);
            }
        }

        //해당 아이템을 장착중이면
        public bool CheckEquip(int equipItemCode)
        {
            //해당 코드의 아이템을 장착하고 있는지
            return Equip.Contains(equipItemCode);
        }

        //장비 착용
        public void EquipItem(Type? equipedItemType, int equipItemCode)
        {
            // 가방에 아이템이 있고
            if (CheckBag(equipItemCode)) 
            {
                if (equipedItemType != null)
                {
                    // 동일 타입 장비일 경우 해체
                    foreach (var code in GameManager.Instance.Player.Equip)
                    {
                        // 기존조건 :
                        if ( DataManager.Instance.ItemDB.List[code][8] == DataManager.Instance.ItemDB.List[GameManager.Instance.Player.Bag[equipItemCode - 1]][8])
                        {
                            UnEquipItem(code);

                            //해제하는 아이템의 보유 스탯만큼 PlusAtk 과 PlusDef 감소
                            StatChange(code);

                            break;
                        }
                        Equip.Add(equipItemCode);

                        //장착한 아이템의 보유 스탯만큼 PlusAtk 과 PlusDef 상승
                        StatChange(equipItemCode - 1);

                        Console.Write($">> {DataManager.Instance.ItemDB.List[Bag[equipItemCode - 1]][1]}");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("을(를) 장착 하였습니다.\n");
                        Console.ResetColor();

                    }
                }

                else if(equipedItemType == null)
                {
                    //UnEquipItem(equipItemCode - 1);
                    //Console.Write($">> {DataManager.Instance.ItemDB.List[Bag[equipItemCode - 1]][1]}");
                    //Console.ForegroundColor = ConsoleColor.DarkCyan;
                    //Console.WriteLine("을(를) 장착 해제하였습니다.\n");
                    //Console.ResetColor();
                }

                else if ()
                {
                    Console.WriteLine("해당 아이템을 이미 착용중입니다.\n");
                }
            }

        }

        //장비 해제
        public void UnEquipItem(int equipItemCode)
        {
            //장착중 이라면
            if(CheckEquip(equipItemCode) == true)
            {
                //Equip List에서 삭제
                Equip.Remove(equipItemCode);
            }
        }

        //아이템 장착 및 해제에 따른 스탯 변화
        public void StatChange(int code)
        {
            var item = DataManager.Instance.ItemDB.List[code]; // 아이템 코드 그대로 사용!
            int atk = int.Parse(item[2]);
            int def = int.Parse(item[3]);

            if (CheckEquip(code))
            {
                Character.PlusAtk += atk;
                Character.PlusDef += def;
            }
            else
            {
                Character.PlusAtk -= atk;
                Character.PlusDef -= def;
            }

            // 토탈 반영
            Character.Attack = Character.TotalAtk;
            Character.Defence = Character.TotalDef;
        }

    }
}
