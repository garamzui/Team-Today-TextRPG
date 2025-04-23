using System;
using TeamTodayTextRPG;

namespace TeamTodayTextRPG
{
    //**객체 받고/ 속성 받기** To.재우
    // SceneManager 클래스: 씬 전환을 관리
    // 『효빈』 씬 매니저는 전역적으로 사용하게 될 매니저 클래스기에 싱글톤으로 생성자를 만들어주겠습니다 :)
    public class SceneManager
    {
        private Viewer currentViewer;
       
        /* 『효빈』이미 GameManager 에서 싱글톤으로 객체를 만들었기 때문에 여기서 다시 만들 필요가 없습니다!.
                   GameMansger.Instance로 접근하시면 돼요!
        private GameManager gameManager;  // GameManager 객체를 멤버로 추가
        */

        /* 『효빈』기존의 생성자에요!
        public SceneManager(GameManager gameManager) //*GameManager 객체를 생성자에서 받아야함
        {
            this.gameManager = gameManager;  // 생성자에서 gameManager 객체 받기

        }*/

        //『효빈』 여기부터가 싱글톤으로 새로 만든 생성자입니다
        private static readonly Lazy<SceneManager> lazyInstance = new Lazy<SceneManager>(() => new SceneManager());
        public static SceneManager Instance => lazyInstance.Value;

        private SceneManager() 
        {
            //『효빈』 초기 생성시 메인뷰어로 들어가도록 하겠습니다.
            //         추후에 가람님의 '그것'을 초기화면으로 설정하도록 바꿀예정입니다. 예를들어...IntroViewer 처럼요
            currentViewer = new MainViewer();
        }

        public void SwitchScene(VIEW_TYPE viewType)
        {
            // 새로운 뷰어 할당
            switch (viewType)
            {
                case VIEW_TYPE.MAIN:
                    currentViewer = new MainViewer();
                    break;
                case VIEW_TYPE.STATUS:
                    currentViewer = new StatusViewer();
                    break;
                case VIEW_TYPE.INVENTORY:
                    currentViewer = new InventoryViewer();
                    break;
                case VIEW_TYPE.EQUIP:
                    currentViewer = new EquipViewer();
                    break;
                case VIEW_TYPE.SHOP:
                    currentViewer = new ShopViewer();
                    break;
                case VIEW_TYPE.PURCHASE:
                    currentViewer = new PurchaseViewer();
                    break;
                case VIEW_TYPE.SALE:
                    currentViewer = new SaleViewer();
                    break;
                case VIEW_TYPE.DUNGEON:
                    currentViewer = new DungeonViewer();
                    break;
                case VIEW_TYPE.DUNGEONCLEAR:
                    currentViewer = new DungeonClearViewer();
                    break;
                case VIEW_TYPE.REST:
                    currentViewer = new RestViewer();
                    break;
                case VIEW_TYPE.BATTLE:
                    currentViewer = new BattleViewer();
                    break;
                case VIEW_TYPE.MONSTER:
                    // GameManager에서 직접적으로 몬스터 객체를 가져오는 방식으로 수정
                    /* 『효빈』GameManager에서 Dungeon을 관리하게 하고 
                               1) 던전에 입장시에 몬스터들을 관리하는 List<Monster>를 생성  << 데이터 방식은 던전 설계에 따라 바뀔수도 있을 것 같아요
                               2) 랜덤 값을 이용하여 랜덤하게 몬스터 리스트를 초기화
                               3) 해당 정보는 GameManager.Instance.Dungeon.MonsterList  << 이런식으로 정보를 받아와서 사용하면 될것 같습니다.
                     */
                    // 『효빈』하단의 코드처럼 매개변수로 굳이 받아오지 않아도 된다는 뜻입니다! :)
                    // Monster currentMonster = gameManager.CurrentMonster;//*GameManager에서 속성을 추가 후 받아야함
                    //currentViewer = new MonsterViewer(currentMonster);

                    currentViewer = new MonsterViewer();
                    break;
            }

            // 새로운 뷰어의 화면 출력
            ShowCurrentView();
        }

        public void ShowCurrentView()
        {
            if (currentViewer != null)
            {
                currentViewer.ViewAction(gameManager);  // gameManager 객체를 넘기기
            }
        }
    }
}
