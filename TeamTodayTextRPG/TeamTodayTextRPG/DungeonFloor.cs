using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Threading;
namespace TeamTodayTextRPG
{
    class Dungeon2
    {
        static void Main(string[] args)
        {
            // 플레이어 생성
            Player player = new Player();
            player.Name = "Jaehun";
            player.Hp = 100;
            player.MaxHp = 100;
            player.Gold = 1000;

            // 던전 생성
            Dungeon dungeon = new Dungeon(new string[] { "1", "불의 던전", "1000", "500", "3", "Hell" });

            // 몬스터 리스트 생성 (층별 강화 포함)
            List<Monster> monsters = new List<Monster>();

            for (int floor = 1; floor <= 4; floor++)
            {
                //int baseHp = 30;
                //int baseAttack = 5;

                int baseReward = 100;

                double multiplier = 1.0 + (floor - 1) * 0.1; // 층수에 따라 10%씩 강화

                int scaledHp = (int)(baseHp * multiplier);
                int scaledAttack = (int)(baseAttack * multiplier);
                int scaledReward = (int)(baseReward * multiplier);

                //rand(1,6)

                switch (case){
                    case 0:
                    monsters.Add(new Slime($"몬스터 Lv.{floor}", scaledHp, scaledAttack, scaledReward));
                }
               
            }

            // 5층은 보스 Zakum
            monsters.Add(new Monster("Zakum", 500, 50, 2000, true));

            // 던전 입장
            dungeon.EnterDungeon(player, monsters);
        }
    }
}
