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
        private Characterclass Characterclass;
        private List<int> bag;
        private List<int> equip;
        private int level;
        private int exp;

        public void SetCharacter()
        {
            //Characterclass 스크립트에서 스탯의 정보를 받아온다


        }

        public void LevelUp()
        {
            int requiredExp = 100;

            //경험치가 요구 경험치보다 크거나 같아진다.
            if (exp >= requiredExp)
            {
                //경험치에서 요구 경험치 만큼 빼고 초과량은 현재 경험치로 남는다.
                exp = exp - requiredExp;

                //요구 경험치가 늘어난다.
                requiredExp += 25;

                //Console.WriteLine("축하합니다! 레벨이 올랐습니다.");
                //스탯 증가량 화면에 표시
            }

        }

        //인벤토리를 열면 활성화
        public bool CheckBag = false;

        //인벤토리에 아이템이 들어오는 경우
        public void InputBag()
        {
            //상점에서 아이템 구매


            //몬스터 드랍

        }

        //인벤토리에 아이템이 나가는 경우
        public void RemoveBag()
        {
            //상점에서 아이템 판매


            //버리기


        }

        //장비창을 열면 활성화
        public bool CheckEquip = false;


        public void EquipItem()
        {

        }

        public void UnEquipItem()
        {

        }
    }
}
