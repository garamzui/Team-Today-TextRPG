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
    protected int startIndex;
    protected int endIndex;
    protected int dungeonCode;

    public abstract void ViewAction(GameManager);

   
    public abstract VIEW_TYPE NextView(GameManager, int);
}
