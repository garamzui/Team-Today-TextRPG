using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using TeamTodayTextRPG;

namespace TeamTodayTextRPG
{
    enum DATA_TYPE
    {
        CHARACTER,
        MONSTER,
        ITEM,
        DUNGEON
    }
    /*
     ============> JSON FILE IO
    
        public void SaveData(T data, string filePath)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
            File.WriteAllText(filePath, json);
        }
    ============> 기존의 데이터 파싱
      public string[][] Parsing(string data)
        {
            string[] parsingTemp = data.Split('#');
            string[][] returnStr = new string[parsingTemp.Length][];

            for (int i = 0; i < parsingTemp.Length; i++)
            {
                returnStr[i] = parsingTemp[i].Split('/');
            }

            return returnStr;
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
        }

        public CharacterDatabase CharacterDB { get; private set; }
        public MonsterDatabase MonsterDB { get; private set; }
        public ItemDatabase ItemDB { get; private set; }
        public DungeonDatabase DungeonDB { get; private set; }
    }


    abstract class Database<T>
    {
        public List<T> List { get; protected set; } = new List<T>();
        public string SavePath { get; protected set; } = "";

        public void Init(string data)
        {
            List = LoadData(data);
        }

        public List<T> LoadData(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var dataType = JsonConvert.DeserializeObject<List<T>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            return dataType;
        }

    }


    class CharacterDatabase : Database<Character>
    {
        public CharacterDatabase()
        {
            SavePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database\\Character_Database.JSON");
            Init(SavePath);
        }
    }

    class MonsterDatabase : Database<Monster>
    {
        public MonsterDatabase()
        {
            SavePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database\\Monster_Database.JSON");
            Init(SavePath);
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
            //Init(Data);
        }
        //protected override void SetData(string[] parameter)
        //{
        //    List.Add(parameter);
        //}
        /*
        public ITEM_TYPE GetTypetoCode(int code) {
                //return List[code][8]; 
        }*/
    }


    class DungeonDatabase : Database<Dungeon>
    {
        public DungeonDatabase()
        {
            SavePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database\\Dungeon_Database.JSON");
            Init(SavePath);
        }
    }
}

