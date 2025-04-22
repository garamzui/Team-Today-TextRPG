using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;

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

        private int Code { get; set; }
        private string Name { get; set; }
        private int Atk { get; set; }
        private int Def { get ; set; }
        private string Text { get; set; }
        private int Value { get ;set; }
        private ITEMTYPE Type { get ;set; }
    }



        public class ItemDatabase
    {   //Item형식의 값을 저장할 공간
        private List<Item> itemList = new List<Item>();

        public List<Item> ItemList { get; set; }

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
            "5/스파르타의 창/7/0/스파르타의 전사들이 사용했다는 전설의 창입니다./2800/1";
            }
        }

        public void ShowName(string name)
        {

        }

        public void ShowAtk(int atk) 
        { 

        }

        public void ShowDef(int def) 
        { 

        }

        public void ShowInventory(GameManager gameManager , VIEW_TYPE vIEW_TYPE)
        {

        }

        public void ShowShop(GameManager gameManager, VIEW_TYPE vIEW_TYPE)
        {

        }

        public void ShowsShopSale(GameManager gameManager)
        {

        }
            
    }
}

