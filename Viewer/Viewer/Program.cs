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
    REST            // 휴식 화면
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
