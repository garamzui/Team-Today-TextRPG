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

    
/*
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

        public void ShowInventory(GameManager gameManager, VIEW_TYPE vIEW_TYPE)
        {

            var player = gameManager.Player;

            if (player.Inventory != null)
            {
                foreach (var code in player.Inventory)
                {
                    var item = itemList.FirstOrDefault(i => i.Code == code);
                    if (item == null) continue;

                    Console.Write("- ");
                    if (player.CheckEquip(item.Code))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[E]");
                        Console.ResetColor();
                    }

                    Console.Write($"{item.Name}\t| ");
                    if (item.Atk != 0) Console.Write($"공격력 {(item.Atk > 0 ? "+" : "")}{item.Atk}\t| ");
                    if (item.Def != 0) Console.Write($"방어력 {(item.Def > 0 ? "+" : "")}{item.Def}\t| ");
                    Console.WriteLine($"{item.Text}");
                }
            }
        }

        public void ShowShop(GameManager gameManager, VIEW_TYPE vIEW_TYPE)
        {
            var player = gameManager.Player;

            foreach (var item in itemList)
            {
                if (player.CheckBag(item.Code)) Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write($"- {item.Name}\t| ");
                if (item.Atk != 0) Console.Write($"공격력 {(item.Atk > 0 ? "+" : "")}{item.Atk}\t| ");
                if (item.Def != 0) Console.Write($"방어력 {(item.Def > 0 ? "+" : "")}{item.Def}\t| ");
                Console.Write($"{item.Text}\t| ");

                if (player.CheckBag(item.Code))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("구매 완료");
                }

                else
                {
                    Console.WriteLine($"{item.Value} G");
                }

                Console.ResetColor();
            }
        }

        public void ShowsShopSale(GameManager gameManager)
        {
            var player = gameManager.Player;

            if (player.Inventory != null)
            {
                Console.WriteLine("[판매 목록]");

                foreach (var code in player.Inventory)
                {
                    var item = itemList.FirstOrDefault(i => i.Code == code);
                    if (item == null) continue;

                    Console.Write("- ");
                    if (player.CheckEquip(item.Code))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[E]");
                        Console.ResetColor();
                    }

                    Console.Write($"{item.Name}\t| ");
                    if (item.Atk != 0) Console.Write($"공격력 {(item.Atk > 0 ? "+" : "")}{item.Atk}\t| ");
                    if (item.Def != 0) Console.Write($"방어력 {(item.Def > 0 ? "+" : "")}{item.Def}\t| ");
                    Console.Write($"{item.Text}\t| ");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"판매가 {item.Value / 2} G");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
            }
        }
            
    }*/