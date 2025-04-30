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
        public DungeonDatabase DungeonDB { get; private set; }
        public ItemDatabase ItemDB { get; private set; }
    }


    abstract class Database<T>
    {
        public List<T> List { get; set; } = new List<T>();
        public string SavePath { get; set; } = "";

        public void Init(string data)
        {
            List = LoadData(data);
        }

        public List<T> LoadData(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Converters = new List<Newtonsoft.Json.JsonConverter>
                {
                    new StringEnumConverter()
                }
            };
            var dataType = JsonConvert.DeserializeObject<List<T>>(json, settings);
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

    class ItemDatabase : Database<Item>
    {
        public ItemDatabase()
        {
            SavePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database\\Item_Database.JSON");
            Init(SavePath);
        }
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