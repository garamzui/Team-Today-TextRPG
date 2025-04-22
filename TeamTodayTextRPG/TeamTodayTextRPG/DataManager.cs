namespace TeamTodayTextRPG
{
    enum DATA_TYPE
    {
        CHARACTER,
        MONSTER,
        ITEM,
        DUNGEON
    }
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
        }

        public CharacterDatabase CharacterDB { get; set; }
        public MonsterDatabase MonsterDB { get; set; }
        public ItemDatabase ItemDB { get; set; }
        public DungeonDatabase DungeonDB { get; set; }

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
        // 0.코드 / 1.이름 / 2.공격력 / 3.방어력 / 4.체력 / 5.소지금 / 6.텍스트
        public string Data { get; private set; } =
            "0/전사/10/5/100/15000/전사입니다.#" +
            "1/마법사/20/2/50/15000/마법사입니다.#" +
            "2/도적/15/3/70/15000/도적입니다.#" +
            "3/사제/5/5/60/15000/사제입니다.";

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
        // 0.몬스터코드 / 1.이름 / 2.레벨 / 3.공격력 / 4.방어력 / 5.체력 / 6.보상골드 / 7.보상경험치 / 8.텍스트
        public string Data { get; private set; } =
            "0/1/매우약한 몬스터/3/0/8/50/10/매우 약한 적입니다.#" +
            "1/2/약한 몬스터/5/1/15/100/25/적당히 약한 적입니다.#" +
            "2/3/평범한 몬스터/8/3/20/300/40/평범한 적입니다.#" +
            "2/4/강한 몬스터/10/5/40/600/60/강력한 적입니다.#" +
            "3/5/매우강한 몬스터/12/10/100/1000/80/매우 강력한 적입니다.";

        public MonsterDatabase()
        {
            Init(Data);
        }
        protected override void SetData(string[] parameter)
        {
            List.Add(parameter);
        }
    }


    class ItemDatabase : Database<Item>
    {
        public List<Item> ItemList { get; set; } = new List<Item>();

        // 0.코드 / 1.이름 / 2.ATK / 3.DEF / 4.설명 / 5.가격 / 6.타입
        public string Data { get; private set; } =
            "0/수련자 갑옷/0/5/수련에 도움을 주는 갑옷입니다./1000/1#" +
            "1/무쇠 갑옷/0/9/무쇠로 만들어져 튼튼한 갑옷입니다./2000/1#" +
            "2/스파르타의 갑옷/0/15/스파르타의 전사들이 사용했다는 전설의 갑옷입니다./3500/1#" +
            "3/낡은 검/2/0/쉽게 볼 수 있는 낡은검 입니다./600/0#" +
            "4/청동 도끼/5/0/어디선가 사용됐던거 같은 도끼입니다./1500/0#" +
            "5/스파르타의 창/7/0/스파르타의 전사들이 사용했다는 전설의 창입니다./2800/0";

        public ItemDatabase()
        {
            Init(Data);
        }
        protected override void SetData(string[] parameter)
        {
            List.Add(new Item(parameter));
        }
        public ITEM_TYPE GetTypetoCode(int code) { return ItemList[code].Type; }

    }


    class DungeonDatabase : Database<Dungeon>
    {
        // 0.던전 코드 / 1.던전 이름 / 2.골드 보상 / 3.경험치 보상 / 4.몬스터 하한 레벨 / 5.몬스터 상한 레벨 / 6.난이도
        public string Data { get; private set; } =
            "0/쉬움 던전/1000/500/1/2/0#" +
            "1/일반 던전/1700/800/2/4/1#" +
            "2/어려움 던전/2500/1500/4/5/2";

        public DungeonDatabase()
        {
            Init(Data);
        }
        protected override void SetData(string[] parameter)
        {
            List.Add(new Dungeon(parameter));
        }
    }
}