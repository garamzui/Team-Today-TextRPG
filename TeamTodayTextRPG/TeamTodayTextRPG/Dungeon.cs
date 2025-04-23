using System;
using System.Numerics;
using System.Threading;
using System.Collections.Generic;

enum DUNGEON_DIFF
{
    Easy = 0,
    Norma = 1,
    Hard = 2,
    Hell = 3
}

class Dungeon
{
    private Random rand = new Random();
    private int code;
    private string name;
    private int reward;
    private int exp;
    private int defLevel;
    private DUNGEON_DIFF diff;

    public Dungeon(string[] info)
    {
        if (info.Length >= 6)
        {
            code = int.Parse(info[0]);
            name = info[1];
            reward = int.Parse(info[2]);
            exp = int.Parse(info[3]);
            defLevel = int.Parse(info[4]);
            diff = (DUNGEON_DIFF)Enum.Parse(typeof(DUNGEON_DIFF), info[5]);
        }
    }

    public void EnterDungeon(Player player, List<Monster> monsters)
    {
        Console.WriteLine($"\n[{name}] 던전에 입장했습니다!");

        int currentFloor = 1;
        int maxFloor = monsters.Count; // 층수는 넘겨준 몬스터 수에 따라 결정

        while (currentFloor <= maxFloor && player.Hp > 0)
        {
            Console.WriteLine($"\n현재 {currentFloor}층입니다.");

            Monster monster = monsters[currentFloor - 1]; // 0번 인덱스 = 1층 몬스터

            Console.WriteLine($"\n{monster.Name}이(가) 등장했습니다! {(monster.IsBoss ? "[보스 몬스터]" : "")}");

            int originalAttack = player.BaseAttack;
            int originalDefense = player.BaseDefense;

            if (monster.IsBoss)
            {
                Console.WriteLine("\n[보스 효과] 플레이어의 능력치가 10% 감소합니다!");
                player.BaseAttack = (int)(player.BaseAttack * 0.9);
                player.BaseDefense = (int)(player.BaseDefense * 0.9);
            }

            bool fled = false;
            while (monster.Hp > 0 && player.Hp > 0)
            {
                Console.WriteLine("\n1. 공격하기");
                Console.WriteLine("2. 도망가기");
                Console.Write("\n>> ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    monster.Hp -= player.TotalAttack;
                    Console.WriteLine($"{monster.Name}에게 {player.TotalAttack} 데미지를 입혔습니다.");

                    if (monster.Hp <= 0)
                    {
                        Console.WriteLine("\n몬스터를 처치했습니다!");
                        player.Gold += monster.Reward;
                        Console.WriteLine($"보상으로 {monster.Reward}G를 얻었습니다!");

                        if (monster.IsBoss && monster.Name == "Zakum")
                        {
                            player.Title = "Zakum Slayer";
                            Console.WriteLine("\n[칭호 획득] 'Zakum Slayer' 칭호를 얻었습니다!");
                        }

                        break;
                    }

                    player.Hp -= monster.Attack;
                    Console.WriteLine($"{monster.Name}의 반격! {monster.Attack} 데미지 입음. (HP: {player.Hp}/{player.MaxHp})");

                    if (player.Hp <= 0)
                    {
                        Console.WriteLine("\n플레이어가 쓰러졌습니다!");
                        break;
                    }
                }
                else if (input == "2")
                {
                    Console.WriteLine("\n도망쳤습니다!");
                    fled = true;
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

            if (monster.IsBoss)
            {
                player.BaseAttack = originalAttack;
                player.BaseDefense = originalDefense;
                Console.WriteLine("\n[보스 효과 종료] 플레이어 능력치가 복구되었습니다.");
            }

            if (fled || player.Hp <= 0)
                break;

            currentFloor++;
        }

        if (player.Hp > 0 && currentFloor > maxFloor)
        {
            Console.WriteLine("\n🎉 던전을 모두 클리어했습니다! 🎉");
        }
    }

    public bool CheckClear(int playerLevel)
    {
        return playerLevel >= defLevel;
    }

    public int CalcReward(int playerLevel)
    {
        int bonus = 0;
        if (playerLevel > defLevel + 2)
        {
            bonus = rand.Next(50, 101);
        }
        return reward + bonus;
    }

    public int CalcMinusHP(int playerDefense)
    {
        int baseDamage = 10;

        switch (diff)
        {
            case DUNGEON_DIFF.Easy:
                baseDamage = 10;
                break;
            case DUNGEON_DIFF.Normal:
                baseDamage = 20;
                break;
            case DUNGEON_DIFF.Hard:
                baseDamage = 30;
                break;
            case DUNGEON_DIFF.Hell:
                baseDamage = 50;
                break;
        }

        int finalDamage = baseDamage - playerDefense;
        if (finalDamage < 1) finalDamage = 1;

        return finalDamage;
    }

    public void PrintDungeonInfo()
    {
        Console.WriteLine($"[{code}] {name} - {diff} 난이도");
        Console.WriteLine($"추천 레벨: {defLevel} / 기본 보상: {reward}G / 경험치: {exp}Exp");
    }
}
