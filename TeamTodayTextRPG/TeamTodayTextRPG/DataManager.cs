namespace TeamTodayTextRPG
{
    enum DATA_TYPE
    {
        CHARACTER,
        MONSTER,
        ITEM,
        DUNGEON
    }
    /*『효빈』
        데이터 매니저입니다.
        모든 데이터를 이곳에서 관리하고 접근합니다. (추후 set함수는 전부 private로 변경할 예정이에요~)

        ============================================================================================
        List<string[]>
        캐릭터 데이터베이스 접근       DataManager.Instance.CharacterDB.List[캐릭터 코드]

        List<string[]>
        몬스터 데이터베이스 접근       DataManager.Instance.MonsterDB.List[몬스터 코드]

        * 아이템과 던전 데이터 베이스도 추후 string[] 리스트로 변경 될 가능성이 높습니다..!*
        List<Item>
        아이템 데이터베이스 접근       DataManager.Instance.ItemDB.List[아이템 코드]

        List<Dungeon>
        던전   데이터베이스 접근       DataManager.Instance.DungeonDB.List[던전 코드]
        ============================================================================================

        아이템 코드는 각 데이터들의 enum으로 관리합니다.
        제가 담당한 Monster로 예를 들자면....

            enum MONSTER_CODE           <= enumerator 는 보통 전부 대문자로 씁니다..! 소문자도 상관은 없어요:)
            {
                트랄랄랄로_트랄랄라 = 0,
                퉁x9_사후르,
                리릴리_라릴라,
                카푸치노_어쌔시노,
                ...
            }
            
            Q1) 위의 enum을 활용하여 리릴리 라릴라의 데이터를 받아와 리릴리 라릴라의 데이터를 얻고 싶다면?
            A1) DataManager.Instance.MonsterDB.List[(int)MONSTER_CODE.리릴리_라릴라]           <- string[]형 데이터입니다.
                                        0.코드 / 1.레벨 / 2.이름 / 3.공격력 / 4.방어력 / 5.체력 / 6.보상골드 / 7.보상경험치 / 8.텍스트  => 이 값들이 들어있습니다

            Q2) 가져온 데이터에서 공격력 값만 얻고 싶다면?
            A2) DataManager.Instance.MonsterDB.List[(int)MONSTER_CODE.리릴리_라릴라][3]           <- 공격력은 3번 인덱스에 있습니다!
    */


    class DataManager
    {
        private static readonly Lazy<DataManager> lazyInstance = new Lazy<DataManager>(() => new DataManager());

        public static DataManager Instance => lazyInstance.Value;

        private DataManager()
        {
            CharacterDB = new CharacterDatabase();
            MonsterDB = new MonsterDatabase();
            ItemDB = new ItemDatabase();
            DungeonDB = new DungeonDatabase();
            NpcDB = new NonePlayerableCharacterDatabase(); 
        }

        public CharacterDatabase CharacterDB { get; private set; }
        public MonsterDatabase MonsterDB { get; private set; }
        public ItemDatabase ItemDB { get; private set; }
        public DungeonDatabase DungeonDB { get; private set; }

        public NonePlayerableCharacterDatabase NpcDB { get; private set; }
    }

    abstract class Database<T>
    {
        public List<T> List { get; set; } = new List<T>();

        public void Init(string data)
        {
            foreach (string[] str in Parsing(data))
            {
                SetData(str);
            }
        }

        public string[][] Parsing(string data)
        {
            string[] parsingTemp = data.Split('#');
            string[][] returnStr = new string[parsingTemp.Length][];

            for (int i = 0; i < parsingTemp.Length; i++)
            {
                returnStr[i] = parsingTemp[i].Split('/');
            }

            return returnStr;
        }
        protected abstract void SetData(string[] parameter);
    }


    class CharacterDatabase : Database<string[]>
    {

        //0.코드/1.직업이름/2.공격력/3.방어력/4.체력/5.마력/6.회피/7.액티브스킬이름/8.패시브스킬이름/9.직업설명
        public string Data { get; private set; } =
            "0/전사/12/5/100/40/1/겁나쎄게 올려치기/강철피부/높은 방어력, 기본 공격력, 체력#" +
            "1/마법사/5/3/50/100/3/썬더봁/마력증강/방어 무시, 높은 마나, 스킬의존성#" +
            "2/도적/7/1/75/75/10/연격/날쌘 움직임/높은 회피, 크리티컬 히트";



        public CharacterDatabase()
        {
            Init(Data);
        }

        protected override void SetData(string[] parameter)
        {
            List.Add(parameter);
        }
    }


    class MonsterDatabase : Database<string[]>
    {
        // 0.코드 / 1.이름 / 2.레벨 / 3.공격력 / 4.방어력 / 5.체력 / 6.보상골드 / 7.보상경험치 / 8.몬스터 등급 / 9.텍스트
        public string Data { get; private set; } =
            "0/슬라임/1/3/0/8/50/10/0/움직임이 느려 처치하기 쉬운 몬스터입니다 늪지와 같은 습한 곳에 주로 서식합니다#" +
            "1/고블린/2/5/1/15/100/25/0/힘은 약하지만 약간의 지능을 가지고 있습니다 어떤 모험가가 꾸준히 개체수를 줄이고 있습니다#" +
            "2/울프/3/8/3/20/300/40/0/빠른 움직임으로 먹잇감을 사냥합니다 무리 단위 생활을 하는 것이 일반적입니다#" +
            "3/보어/4/15/1/30/600/60/0/몸집이 크고, 강력한 돌진 공격을 사용합니다#" +
            "4/오크/4/10/5/40/600/60/0/지능은 매우 떨어지지만 무시무시한 힘을 가지고 있습니다 취이익 취익#" +
            "5/자쿰/5/12/10/100/1000/80/1/전설 속 지옥의 나무 알려진 정보가 극히 드물다 왠지 팔을 떨어뜨릴 수 있을 것 같다";

        public MonsterDatabase()
        {
            Init(Data);
        }
        protected override void SetData(string[] parameter)
        {
            List.Add(parameter);
        }
    }

    class ItemDatabase : Database<string[]>
    {
        //0.코드 /1.이름 /2. ATK /3.DEF /4.HP /5.MP /6.설명 /7.가격 /8.타입
        public string Data { get; private set; } =
            //기본 지급
            "0/목검/2/0/0/0/초보자용 검입니다/700/0#" +
            "1/천 옷/0/3/0/0/매우 저렴한 방어구 입니다/500/1#" +

            //무기
            "2/글라디우스/5/0/0/0/영웅이 썼을지도 모를 무기입니다/4000/0#" +
            "3/위저드 완드/3/0/0/5/견습 마법사의 무기입니다/4000/0#" +
            "4/필드 대거/4/0/0/5/좀도둑이 들고다닐만한 무기입니다/4000/0#" +
            "5/그레이트 소드/20/0/0/0/무거워서 양손으로 들어야합니다/20000/0#" +
            "6/미스릴 스태프/7/0/0/5/큼지막한 미스릴이 박혀있습니다/20000/0#" +
            "7/무명 쌍검/20/0/0/0/누구도 그 원래 이름을 알지 못합니다/20000/0#" +

            //방어구
            "8/체인 메일/0/3/0/0/무거운만큼 튼튼한 갑옷입니다/3000/1#" +
            "9/실크 로브/0/1/0/0/가볍지만 방어 마법이 걸려있습니다/3000/1#" +
            "10/가죽 조끼/0/2/0/0/굉장히 질긴 재질입니다/3000/1#" +
            "11/대지의 수호자/0/10/0/0/땅의 기운을 머금은 갑옷입니다/15000/1#" +
            "12/별빛의 로브/0/7/0/0/별의 힘이 깃든 신비한 로브입니다/15000/1#" +
            "13/흑월/0/8/0/0/달빛을 받으면 더욱 어두워집니다/15000/1";

        /*
            "8/HP 포션/0/0/0/HP 20을 채워줍니다./300/2#" +
            "9/MP 포션/0/0/0/MP 20을 채워줍니다./300/2";*/

        public ItemDatabase()
        {
            Init(Data);
        }
        protected override void SetData(string[] parameter)
        {
            List.Add(parameter);
        }
        /*
        public ITEM_TYPE GetTypetoCode(int code) {
                //return List[code][8]; 
        }*/
    }


    class DungeonDatabase : Database<string[]>
    {
        // 0.코드 / 1.던전 이름 / 2.골드 보상 / 3.경험치 보상 / 4.몬스터 하한 레벨 / 5.몬스터 상한 레벨 / 6.최대 몬스터 마리 수/7.난이도 / 8.던전간략설명
        public string Data { get; private set; } =
            "0/쉬움 던전/1000/500/1/2/2/0/하남자들이 가는 곳입니다.#" +
            "1/일반 던전/1700/800/2/4/3/1/평범한 난이도의 던전입니다.#" +
            "2/어려움 던전/2500/1500/4/5/4/2/좀 치는 사람들이 가는 곳입니다.#" +
            "3/헬 던전/10000/3000/5/5/1/3/오줌을 지릴 수 있습니다.";

        public DungeonDatabase()
        {
            Init(Data);
        }
        protected override void SetData(string[] parameter)
        {
            List.Add(parameter);
        }
    }
    class NonePlayerableCharacterDatabase : Database<string[]>
    {

        //0.NPC코드/1.퀘스트코드/2.NPC이름/3.퀘스트멘트/4.퀘스트 거절 시 멘트/5.퀘스트 완료문구/6.퀘스트 미완 문구
        public string Data { get; private set; } =
            "0/0/노인/블라블라 고블린을 10마리 어쩌구 /떼잉 쯧 배알도 없는 놈 같으니/나도 그정도는 해/아직 덜 됐다 이 덜떨어진 놈#" +
            "1/1/소녀/어젯밤 내 슬라임이 무너졌어 어쩌구 내 목숨을 가져가도 좋아 저쩌구/떼잉 쯧/곰마워/무너졌어흐흑 #" +
            "2/2/대장장이/상점으로 돌아가 글라디우스를 구매하여 가져 오십시오/어디가십니까/사오라고/이 칼은 이제 제 껍니다#"+
            "3/3/수상한사람/크크크ㅡ크크크크큭크크크큭크큭 크하하하하하하 크엑 컹 /후후후후...푸후후후..크흐흐..크하하하하핰핰캌/진짜로 했다고?ㄷㄷ/오호호호이히히히낄낄낄";



        public NonePlayerableCharacterDatabase()
        {
            Init(Data);
        }

        protected override void SetData(string[] parameter)
        {
            List.Add(parameter);
        }
    }
}