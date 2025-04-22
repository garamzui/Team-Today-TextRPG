using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Numerics;

namespace TeamTodayTextRPG
{
    enum ITEMTYPE
    {
        WEAPON = 0,
        ARMOR = 1
    }

    public class Item
    {
        private int code;
        private string name;
        private int atk;
        private int def;
        private string text;
        private int value;
        private ITEMTYPE type;

        public Item(string[] str) //Parse로 데이터 변환
        {
            code = int.Parse(str[0]);
            name = str[1];
            atk = int.Parse(str[2]);
            def = int.Parse(str[3]);
            text = str[4];
            value = int.Parse(str[5]);
            if (int.Parse(str[6]) == (int)ITEMTYPE.WEAPON)
            {
                type = ITEMTYPE.WEAPON;
            }
            else if (int.Parse(str[6]) == (int)ITEMTYPE.ARMOR)
            {
                type = ITEMTYPE.ARMOR;
            }
        }

        public int Code { get; private set; }
        public string Name { get; private set; }
        public int Atk { get; private set; }
        public int Def { get ; private set; }
        public string Text { get; private set; }
        public int Value { get ; private set; }
        private ITEMTYPE Type { get ;set; }
    }



        public class ItemDatabase
    {   //Item형식의 값을 저장할 공간
        private List<Item> itemList = new List<Item>();
        public List<Item> ItemList => itemList;
        private int Atk;
        private int Def;

        public ItemDatabase() 
        {
            InitItem();
        }

        public void InitItem() 
        {
            itemList = new List<Item>();
            {
                string data =
               "0/수련자 갑옷/0/5/수련에 도움을 주는 갑옷입니다./1000/0" +
               "1/무쇠 갑옷/0/9/무쇠로 만들어져 튼튼한 갑옷입니다./2000/0" +
               "2/스파르타의 갑옷/0/15/스파르타의 전사들이 사용했다는 전설의 갑옷입니다./3500/0" +
               "3/낡은 검/2/0/쉽게 볼 수 있는 낡은검 입니다./600/1" +
               "4/청동 도끼/5/0/어디선가 사용됐던거 같은 도끼입니다./1500/1" +
               "5/스파르타의 창/7/0/스파르타의 전사들이 사용했다는 전설의 창입니다./2500/1";
                string[] lines = data.Split('\n');

                foreach (string line in lines)
                {
                    string[] parts = line.Trim().Split('/');
                    if (parts.Length == 7)
                    {
                        Item item = new Item(parts);
                        itemList.Add(item);
                    }
                }
            }
        }

        public void ShowName(string name)
        {
            foreach (var item in itemList)
            {
                Console.WriteLine($"{item.Code}: {item.Name} (공격력: {item.Atk}, 방어력: {item.Def}) - {item.Text} [가격: {item.Value} G]");
            }
        }

        public void ShowAtk(int atk) 
        {
            if (Atk != 0) Console.Write($"Atk {(Atk >= 0 ? " + " : "")}{Atk}");
        }

        public void ShowDef(int def) 
        {
            if (Def != 0) Console.Write($"Def {(Def >= 0 ? " + " : "")}{Def}");
        }

        public void ShowInventory(GameManager gameManager , VIEW_TYPE vIEW_TYPE)
        {

            if (Player.Inventory != null)
            {
                foreach (var item in Player.Inventory)
                {
                    Console.Write("- ");
                    if (Player.CheckEquip(item))
                    {
                        Console.Write("[E]");
                    }
                }
            }
        }

        public void ShowShop(GameManager gameManager, VIEW_TYPE vIEW_TYPE)
        {

        }

        public void ShowsShopSale(GameManager gameManager)
        {

        }
            
    }
}

