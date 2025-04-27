using System;
using System.Numerics;
using System.Threading;
using System.Xml.Linq;
using TeamTodayTextRPG;

namespace TeamTodayTextRPG
{
    //참조 관련 스크립트
    public partial class Player
    {
        public Character Character { get; set; }
        public List<int> Bag { get; set; }
        public List<int> WeaponEquip { get; set; }
        public List<int> ArmorEquip { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int Gold { get; set; }
        public int RequiredExp { get; set; }
        public string Name { get; set; }

        public int equipedWpCode = -1;
        public int equipedAmCode = -1;
    }

    //스탯 관련 스크립트
    public partial class Player
    {
        public Player()
        {
            Bag = new List<int>();
            WeaponEquip = new List<int>();
            ArmorEquip = new List<int>();
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
                RequiredExp = 100;
                Name = name;
            }

            //천 옷과 목검 Bag 리스트에 저장한다
            //직업별 초기 장비가 다르다면 수정
            Bag.Add(int.Parse(DataManager.Instance.ItemDB.List[0][0])); // 천 옷 기본제공
            Bag.Add(int.Parse(DataManager.Instance.ItemDB.List[4][0])); // 목검 기본제공  <- 인덱스 바뀔 예정

            //초기 장비를 가지고 있되 장착은 되어있지 않은 상태로 시작해서
            //인벤토리를 처음 열면 장착&해제 튜토리얼 구현해 보는 것 괜찮을지도
        }

        public bool LevelUp()
        {
            //던전 클리어시 처치한 몬스터에 따라 경험치를 얻는 구조 필요
            //경험치가 요구 경험치보다 크거나 같아진다.
            if (Exp >= RequiredExp)
            {
                //경험치에서 요구 경험치 만큼 빼고 초과량은 현재 경험치로 남는다.
                int count = 0;
                while(Exp >= RequiredExp)
                {
                    Exp -= RequiredExp;
                    RequiredExp += 25;
                    count++;
                }

                //레벨 및 요구 경험치 스탯이 늘어난다.
                Level+=count;
                Character.Attack += (1*count);
                Character.Defence += (2*count);
                //『효빈』 패시브 정확히 뭔지 모르겠네요
                //Character.PassiveSkill();
                return true;
            }
            else return false;
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
            if (type == VIEW_TYPE.DUNGEON_CLEAR)
            {
                int randNum = GameManager.Instance.Rand.Next(0, 101);
                //20% 확률로
                if (randNum >= 90 || randNum <= 10)
                {
                    //랜덤 아이템 드롭
                    int dropItemCode = GameManager.Instance.Rand.Next(0, DataManager.Instance.ItemDB.List.Count + 1);
                    Bag.Add(int.Parse(DataManager.Instance.ItemDB.List[dropItemCode][0]));
                }
            }
        }

        //인벤토리에 아이템이 나가는 경우 1 - 상점 판매
        public void RemoveBag(int inputItemCode, VIEW_TYPE Vtype)
        {
            int prise = int.Parse(DataManager.Instance.ItemDB.List[inputItemCode][7]);

            //상점에서 아이템 판매와 버리기
            //Bag에 있고 장착중이 아니라면
            if (CheckBag(inputItemCode) == true && CheckEquip(inputItemCode, ITEM_TYPE.WEAPON) == false ||
                CheckBag(inputItemCode) == true && CheckEquip(inputItemCode, ITEM_TYPE.ARMOR) == false)
            {
                //판매하기 화면이라면
                if (Vtype == VIEW_TYPE.SALE)
                {
                    Gold += (int)(prise * 0.85f);
                }
                Bag.Remove(inputItemCode);
            }
        }

        //새로운 접근법----
        //장비 아이템 슬룻을 만든다면?

        //버추얼 장비착용
        //무기 착용
        //방어구 착용

        //무기는 무기변수에
        //방어구는 방어구변수에
        //----

        //해당 무기 아이템을 장착중이면
        public bool CheckEquip(int equipItemCode, ITEM_TYPE Itype)
        {
            //해당 코드의 아이템을 장착하고 있는지
            switch (Itype)
            {
                case ITEM_TYPE.WEAPON:
                    return WeaponEquip.Contains(equipItemCode);

                case ITEM_TYPE.ARMOR:
                    return ArmorEquip.Contains(equipItemCode);

                default:
                    return false;
            }
        }

        //장비 착용
        public void EquipItem(int equipItemCode, ITEM_TYPE Itype)
        {
            //타입 비교
            switch (Itype)
            {
                case ITEM_TYPE.WEAPON:
                    WeaponEquip.Add(equipItemCode);
                    equipedWpCode = equipItemCode;
                    ChangeStat(equipItemCode);
                    break;

                case ITEM_TYPE.ARMOR:
                    ArmorEquip.Add(equipItemCode);
                    equipedAmCode = equipItemCode;
                    ChangeStat(equipItemCode);
                    break;
            }
        }

        //장비 교체
        public void ChangeItem(int equipItemCode, ITEM_TYPE Itype)
        {
            //장착중이 아니라면
            //if (equipedWpCode == -1 || equipedAmCode == -1) return;
            
            //if()

            //타입비교
            switch (Itype)
            {
                case ITEM_TYPE.WEAPON:
                    WeaponEquip.Clear();
                    WeaponEquip.Add(equipItemCode);
                    ChangeStat(equipItemCode);
                    ChangeStat(equipedWpCode);
                    equipedWpCode = equipItemCode;
                    break;

                case ITEM_TYPE.ARMOR:
                    ArmorEquip.Clear();
                    ArmorEquip.Add(equipItemCode);
                    ChangeStat(equipItemCode);
                    ChangeStat(equipedAmCode);
                    equipedAmCode = equipItemCode;
                    break;
            }
        }

        //장비 해제
        public void UnEquipItem(int equipItemCode)
        {
            //장착중 이라면
            if (WeaponEquip.Contains(equipItemCode) == true)
            {
                WeaponEquip.Clear();
                equipedWpCode = -1;
                ChangeStat(equipItemCode);
            }

            else
            {
                ArmorEquip.Clear();
                equipedAmCode = -1;
                ChangeStat(equipItemCode);
            }
        }

        //아이템 장착 및 해제에 따른 스탯 변화
        public void ChangeStat(int code)
        {
            var item = DataManager.Instance.ItemDB.List[code]; // 아이템 코드 그대로 사용!
            int atk = int.Parse(item[2]);
            int def = int.Parse(item[3]);
            

            if (CheckEquip(code, ITEM_TYPE.WEAPON) || CheckEquip(code, ITEM_TYPE.ARMOR))
            {
                Character.PlusAtk += atk;
                Character.PlusDef += def;
            }
            else
            {
                Character.PlusAtk -= atk;
                Character.PlusDef -= def;
            }
        }
    }
}

//// 기존 리스트 속 인덱스 비교 코드 
//// 동일 타입 장비일 경우 해체
//foreach (var code in GameManager.Instance.Player.Equip)
//{
//    if (DataManager.Instance.ItemDB.List[code][8] == DataManager.Instance.ItemDB.List[GameManager.Instance.Player.Bag[equipItemCode - 1]][8])
//    {
//        UnEquipItem(code);
//        break;
//    }
//    Equip.Add(equipItemCode);
//    equipedItemCode = equipItemCode;

//    Console.Write($">> {DataManager.Instance.ItemDB.List[Bag[equipItemCode - 1]][1]}");
//    Console.ForegroundColor = ConsoleColor.DarkCyan;
//    Console.WriteLine("을(를) 장착 하였습니다.\n");
//    Console.ResetColor();
//}
