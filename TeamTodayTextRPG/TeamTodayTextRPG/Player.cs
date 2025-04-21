using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTodayTextRPG
{
    class Player
    {
        private Character character;
        private List<int> bag;
        private List<int> equip;
        private int level;
        private int exp;

        public void SetCharacter(int stat, string name)
        {
            //Character 스크립트에서 스탯의 정보를 받아온다

        }

        public void LevelUp(int requiredExp)
        {
            if (exp >= requiredExp)
            {
                
            }

        }

        public bool CheckBag(int hasBag)
        {
            
        }

        public void InputBag(int inBag)
        {

        }

        public void RemoveBag(int outBag)
        {

        }

        public bool CheckEquip(int hasEquip)
        {

        }

        public void EquipItem(int equip)
        {

        }

        public void UnEquipItem(int unEquip)
        {

        }
    }
}
