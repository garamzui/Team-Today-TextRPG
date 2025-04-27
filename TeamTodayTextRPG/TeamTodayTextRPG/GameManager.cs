namespace TeamTodayTextRPG
{
    class GameManager
    {
        /* 『효빈』
            Lazy<T> 형식의 싱글톤 방법입니다.
            인스턴스의 생성 시기를 직접 정할 수 있기 때문에 리소스 낭비를 줄일 수 있고, 스레드 안정성 문제도 해결 가능합니다. 
        */
        private static readonly Lazy<GameManager> lazyInstance = new Lazy<GameManager>(() => new GameManager());
        public static GameManager Instance => lazyInstance.Value;


        // 『효빈』GameManager 생성자 입니다.
        private GameManager()
        {
            Player = new Player();
            Rand = new Random();
            Animation = new Animation();
            //NPC 인스턴스 (퀘스트 완료했을 때 값 등을 계속 저장 해두어야 하기 때문에 필요하다 생각했어요.)
            OldMan = new OldMan();
            YoungGirl = new YoungGirl();
            BlackSmith = new BlackSmith();
            Stranger = new Stranger();
            NonePlayableCharacter = OldMan;
        }


        // 『효빈』GameMananger 클래스의 프로퍼티 입니다.
        public Player Player { get; set; }
        public Random Rand { get; set; }
        public Dungeon Dungeon { get; set; }
        //NPC 프로퍼티
        public NonePlayableCharacter NonePlayableCharacter { get; set; }
        public OldMan OldMan { get; private set; }
        public YoungGirl YoungGirl { get; private set; }
        public BlackSmith BlackSmith { get; private set; }
        public Stranger Stranger { get; private set; }

        


      
        public Animation Animation { get; set; }



        public Monster BattleEnemy { get; set; }

        public SceneManager SceneManager => SceneManager.Instance;

        // 팩토리 메서드 위치 
        public Dungeon DungeonFactroy(int code)
        {
            switch (code)
            {
                case (int)DUNGEON_DIFF.Easy:
                    return new Dungeon_Easy();
                case (int)DUNGEON_DIFF.Normal:
                    return new Dungeon_Normal();
                case (int)DUNGEON_DIFF.Hard:
                    return new Dungeon_Hard();
                case (int)DUNGEON_DIFF.Hell:
                    return new Dungeon_Hell();
                default:
                    throw new ArgumentException("잘못된 던전 코드입니다.");
            }
        }
        public Monster MonsterFactory(int code)
        {
            switch (code)
            {

                case (int)MONSTER_CODE.Slime:
                    return new Slime();
                case (int)MONSTER_CODE.Goblin:
                    return new Goblin();
                case (int)MONSTER_CODE.Wolf:
                    return new Wolf();
                case (int)MONSTER_CODE.Ork:
                    return new Ork();
                case (int)MONSTER_CODE.Zakum:
                    return new Zakum();
                default:
                    throw new ArgumentException("잘못된 몬스터 코드입니다.");
            }
        }
    }
}