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
    enum ITEM_TYPE
    {
        WEAPON = 0,
        ARMOR = 1
    }

    public abstract class Item
    {
        public void Init(string[] str) //Parse로 데이터 변환
        {
            Code = int.Parse(str[0]);
            Name = str[1];
            Atk = int.Parse(str[2]);
            Def = int.Parse(str[3]);
            Text = str[4];
            Value = int.Parse(str[5]);
            if (int.Parse(str[6]) == (int)ITEM_TYPE.WEAPON)
            {
                Type = ITEM_TYPE.WEAPON;
            }
            else if (int.Parse(str[6]) == (int)ITEM_TYPE.ARMOR)
            {
                Type = ITEM_TYPE.ARMOR;
            }
        }

        public int Code { get; private set; }
        public string Name { get; private set; }
        public int Atk { get; private set; }
        public int Def { get; private set; }
        public string Text { get; private set; }
        public int Value { get; private set; }

        private ITEM_TYPE Type { get; set; }
    }


    public class Potion : Item
    {
        public Potion()
        {
            Init(DataManager.Instance.ItemDB.List[])
        }

        public healHp(int upHp)
        {

        }

    }

}

   