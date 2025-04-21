public enum VIEW_TYPE
{
    MAIN,           // 게임 시작 화면
    STATUS,         // 상태 보기 화면
    INVENTORY,      // 인벤토리 화면
    EQUIP,          // 장비 화면
    SHOP,           // 상점 화면
    PURCHASE,       // 아이템 구매 화면
    SALE,           // 아이템 판매 화면
    DUNGEON,        // 던전 화면
    DUNGEONCLEAR,   // 던전 클리어 화면
    REST,           // 휴식 화면
    Battle,         // 전투 화면
    BattlePlayer,   // 전투 플레이어 화면
    BattleEnemy,    // 전투 적 화면
}

public abstract class Viewer
{
    private int startIndex;
    private int endIndex;
    private int dungeonCode;
    public abstract void ViewAction(GameManager gameManager);
    public abstract VIEW_TYPE NextView(GameManager gameManager, int input);
}

public class MainViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("게임 시작 화면");
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 장비");
        Console.WriteLine("4. 상점");
        Console.WriteLine("5. 던전");
        Console.WriteLine("6. 종료");
    }

    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.STATUS;  // 상태 보기 화면
            case 2:
                return VIEW_TYPE.INVENTORY;  // 인벤토리 화면
            case 3:
                return VIEW_TYPE.EQUIP;  // 장비 화면
            case 4:
                return VIEW_TYPE.SHOP;  // 상점 화면
            case 5:
                return VIEW_TYPE.DUNGEON;  // 던전 화면
            case 6:
                Environment.Exit(0);  // 게임 종료
                break;
            default:
                break;
        }
        return VIEW_TYPE.MAIN;  // 기본적으로 메인 화면으로 돌아감
    }
}
public class StatusViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("상태 보기 화면");
        Console.WriteLine($"체력: {gameManager.Player.Hp}");
        Console.WriteLine($"마나: {gameManager.Player.Mp}");
        Console.WriteLine($"레벨: {gameManager.Player.Level}");
        Console.WriteLine($"경험치: {gameManager.Player.Exp}");
        Console.WriteLine("1. 메인 화면으로 돌아가기");
    }
    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        if (input == 1)
            return VIEW_TYPE.MAIN;  // 메인 화면으로 돌아감
        return VIEW_TYPE.STATUS;  // 상태 보기 화면 유지
    }
}
public class InventoryViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("인벤토리 화면");
        Console.WriteLine("1. 아이템 사용");
        Console.WriteLine("2. 아이템 버리기");
        Console.WriteLine("3. 메인 화면으로 돌아가기");
    }
    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.INVENTORY;  // 아이템 사용 화면
            case 2:
                return VIEW_TYPE.INVENTORY;  // 아이템 버리기 화면
            case 3:
                return VIEW_TYPE.MAIN;  // 메인 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.INVENTORY;  // 인벤토리 화면 유지
    }
}

public class EquipViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("장비 화면");
        Console.WriteLine("1. 장비 착용");
        Console.WriteLine("2. 장비 해제");
        Console.WriteLine("3. 메인 화면으로 돌아가기");
    }
    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.EQUIP;  // 장비 착용 화면
            case 2:
                return VIEW_TYPE.EQUIP;  // 장비 해제 화면
            case 3:
                return VIEW_TYPE.MAIN;  // 메인 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.EQUIP;  // 장비 화면 유지
    }
}

public class ShopViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("상점 화면");
        Console.WriteLine("1. 아이템 구매");
        Console.WriteLine("2. 아이템 판매");
        Console.WriteLine("3. 메인 화면으로 돌아가기");
    }
    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.PURCHASE;  // 아이템 구매 화면
            case 2:
                return VIEW_TYPE.SALE;  // 아이템 판매 화면
            case 3:
                return VIEW_TYPE.MAIN;  // 메인 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.SHOP;  // 상점 화면 유지
    }
}
public class PurchaseViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("아이템 구매 화면");
        Console.WriteLine("1. 아이템 구매");
        Console.WriteLine("2. 메인 화면으로 돌아가기");
    }
    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.PURCHASE;  // 아이템 구매 화면
            case 2:
                return VIEW_TYPE.SHOP;  // 상점 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.PURCHASE;  // 아이템 구매 화면 유지
    }
}
public class SaleViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("아이템 판매 화면");
        Console.WriteLine("1. 아이템 판매");
        Console.WriteLine("2. 메인 화면으로 돌아가기");
    }
    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.SALE;  // 아이템 판매 화면
            case 2:
                return VIEW_TYPE.SHOP;  // 상점 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.SALE;  // 아이템 판매 화면 유지
    }
} 
public class DungeonViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("던전 화면");
        Console.WriteLine("1. 던전 탐험");
        Console.WriteLine("2. 메인 화면으로 돌아가기");
    }
    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.DUNGEON;  // 던전 탐험 화면
            case 2:
                return VIEW_TYPE.MAIN;  // 메인 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.DUNGEON;  // 던전 화면 유지
    }
}

public class DungeonClearViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("던전 클리어 화면");
        Console.WriteLine("1. 보상 받기");
        Console.WriteLine("2. 메인 화면으로 돌아가기");
    }
    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.DUNGEONCLEAR;  // 보상 받기 화면
            case 2:
                return VIEW_TYPE.MAIN;  // 메인 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.DUNGEONCLEAR;  // 던전 클리어 화면 유지
    }
}
public class RestViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("휴식 화면");
        Console.WriteLine("1. 체력 회복");
        Console.WriteLine("2. 마나 회복");
        Console.WriteLine("3. 메인 화면으로 돌아가기");
    }
    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.REST;  // 체력 회복 화면
            case 2:
                return VIEW_TYPE.REST;  // 마나 회복 화면
            case 3:
                return VIEW_TYPE.MAIN;  // 메인 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.REST;  // 휴식 화면 유지
    }
}

public class BattleViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("전투 화면");
        Console.WriteLine("1. 공격");
        Console.WriteLine("2. 방어");
        Console.WriteLine("3. 도망");
        Console.WriteLine("4. 메인 화면으로 돌아가기");
    }
    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.BATTLE;  // 공격 화면
            case 2:
                return VIEW_TYPE.BATTLE;  // 방어 화면
            case 3:
                return VIEW_TYPE.BATTLE;  // 도망 화면
            case 4:
                return VIEW_TYPE.MAIN;  // 메인 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.BATTLE;  // 전투 화면 유지
    }
}

public class BattlePlayerViewer : Viewer
{
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    public string Name { get; set; }

    public BattlePlayerViewer(string name)
    {
        Name = name;
        Hp = 100;
        Mp = 50;
        Level = 1;
        Exp = 0;
    }

    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("전투 플레이어 화면");
        Console.WriteLine("1. 공격");
        Console.WriteLine("2. 방어");
        Console.WriteLine("3. 도망");
        Console.WriteLine("4. 메인 화면으로 돌아가기");
    }

    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.BattlePlayer;  // 공격 화면
            case 2:
                return VIEW_TYPE.BattlePlayer;  // 방어 화면
            case 3:
                return VIEW_TYPE.BattlePlayer;  // 도망 화면
            case 4:
                return VIEW_TYPE.MAIN;  // 메인 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.BattlePlayer;  // 전투 플레이어 화면 유지
    }
}

public class BattleEnemyViewer : Viewer
{
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    public string Name { get; set; }

    public BattleEnemyViewer(string name)
    {
        Name = name;
        Hp = 100;
        Mp = 50;
        Level = 1;
        Exp = 0;
    }

    public override void ViewAction(GameManager gameManager)
    {
        Console.WriteLine("전투 적 화면");
        Console.WriteLine("1. 공격");
        Console.WriteLine("2. 방어");
        Console.WriteLine("3. 도망");
        Console.WriteLine("4. 메인 화면으로 돌아가기");
    }

    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.BattleEnemy;  // 공격 화면
            case 2:
                return VIEW_TYPE.BattleEnemy;  // 방어 화면
            case 3:
                return VIEW_TYPE.BattleEnemy;  // 도망 화면
            case 4:
                return VIEW_TYPE.MAIN;  // 메인 화면으로 돌아감
            default:
                break;
        }
        return VIEW_TYPE.BattleEnemy;  // 전투 적 화면 유지
    }
}
