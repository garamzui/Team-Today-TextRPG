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
    BATTLE,         // 전투 화면
}

public abstract class Viewer
{
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
                return VIEW_TYPE.STATUS;
            case 2:
                return VIEW_TYPE.INVENTORY;
            case 3:
                return VIEW_TYPE.EQUIP;
            case 4:
                return VIEW_TYPE.SHOP;
            case 5:
                return VIEW_TYPE.DUNGEON;
            case 6:
                ExitGame();  // 게임 종료 처리
                return VIEW_TYPE.MAIN; // 이 라인은 실제로 실행되지 않음
            default:
                return VIEW_TYPE.MAIN;  // 잘못된 입력 처리
        }
    }

    // 게임 종료 처리
    private void ExitGame()
    {
        Console.WriteLine("게임 종료 중...");
        Environment.Exit(0); // 게임 종료
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
        switch (input)
        {
            case 1:
                return VIEW_TYPE.MAIN;
            default:
                return VIEW_TYPE.STATUS;
        }
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
                return VIEW_TYPE.INVENTORY;
            case 2:
                return VIEW_TYPE.INVENTORY;
            case 3:
                return VIEW_TYPE.MAIN;
            default:
                return VIEW_TYPE.INVENTORY;
        }
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
                return VIEW_TYPE.EQUIP;
            case 2:
                return VIEW_TYPE.EQUIP;
            case 3:
                return VIEW_TYPE.MAIN;
            default:
                return VIEW_TYPE.EQUIP;
        }
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
                return VIEW_TYPE.PURCHASE;
            case 2:
                return VIEW_TYPE.SALE;
            case 3:
                return VIEW_TYPE.MAIN;
            default:
                return VIEW_TYPE.SHOP;
        }
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
                return VIEW_TYPE.PURCHASE;
            case 2:
                return VIEW_TYPE.SHOP;
            default:
                return VIEW_TYPE.PURCHASE;
        }
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
                return VIEW_TYPE.SALE;
            case 2:
                return VIEW_TYPE.SHOP;
            default:
                return VIEW_TYPE.SALE;
        }
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
                return VIEW_TYPE.DUNGEON;
            case 2:
                return VIEW_TYPE.MAIN;
            default:
                return VIEW_TYPE.DUNGEON;
        }
    }
}

public class BattleViewer : Viewer
{
    public override void ViewAction(GameManager gameManager)
    {
        Console.Clear();
        Console.WriteLine("==== 전투 화면 ====");

        // 플레이어 정보 출력
        Console.WriteLine($"플레이어: {gameManager.Player.Name} (레벨 {gameManager.Player.Level})");
        Console.WriteLine($"체력: {gameManager.Player.Hp}/{gameManager.Player.MaxHp}");
        Console.WriteLine($"마나: {gameManager.Player.Mp}/{gameManager.Player.MaxMp}");
        Console.WriteLine($"경험치: {gameManager.Player.Exp}/{gameManager.Player.ExpToNextLevel}");

        // 몬스터 정보 출력
        var monster = gameManager.CurrentMonster;  // 게임 매니저에서 현재 몬스터를 가져옴
        Console.WriteLine($"\n몬스터: {monster.Name} (레벨 {monster.Level})");
        Console.WriteLine($"체력: {monster.Hp}/{monster.MaxHp}");

        // 전투 선택지 출력
        Console.WriteLine("\n1. 공격");
        Console.WriteLine("2. 방어");
        Console.WriteLine("3. 도망");
        Console.WriteLine("4. 메인 화면으로 돌아가기");
    }

    public override VIEW_TYPE NextView(GameManager gameManager, int input)
    {
        switch (input)
        {
            case 1:
                return VIEW_TYPE.BATTLE;  // 공격 선택
            case 2:
                return VIEW_TYPE.BATTLE;  // 방어 선택
            case 3:
                return VIEW_TYPE.BATTLE;  // 도망 선택
            case 4:
                return VIEW_TYPE.MAIN;    // 메인 화면으로 돌아가기
            default:
                return VIEW_TYPE.BATTLE;   // 잘못된 입력 처리
        }
    }
}
