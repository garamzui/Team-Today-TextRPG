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

        //Characterclass 스크립트에서 스탯의 정보를 받아온다
        public void SetCharacter()
        {


        }

        public void LevelUp()
        {
            int requiredExp = 100;

            //경험치가 요구 경험치보다 크거나 같아진다.
            if (exp >= requiredExp)
            {
                //경험치에서 요구 경험치 만큼 빼고 초과량은 현재 경험치로 남는다.
                exp = exp - requiredExp;

                //레벨 및 요구 경험치가 늘어난다.
                level++;
                requiredExp += 25;

                //Console.WriteLine("축하합니다! 레벨이 올랐습니다.");
                //스탯 증가량 화면에 표시
            }

        }
        //아이템에 코드값을 받아서 그 아이템이 내 인벤에 있는지 없는지 체크하는 불값
        //플레이어 bag이라는 리스트가 아이템의 코드번호를 가지고 저장
        //0~7 아이템 있는데 6번 찾고 싶으면 bag(6)을 입력하면 true 반환 하는 식으로 제작
        //equipItem의 경우는 같은 장비군은 중복장착하면 안되니까 세부내용 조금 다름

        //인벤토리에 해당 아이템이 있으면
        public bool CheckBag(int code)
        {
            //bag에 Item의 code를 인덱스로 저장한다
            

            
        }

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

        //인벤토리에 해당 아이템이 있으면
        public bool CheckEquip(int code)
        {

        }


        public void EquipItem()
        {

        }

        public void UnEquipItem()
        {

        }
    }
}
