using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTodayTextRPG
{
    public enum NPC_TYPE
    { 
    OLDMAN,
    YOUNGGIRL,
    BLACKSMITH,
    STRANGER
    
    }

    public enum QUEST_TYPE
    {
        OLDMAN,
        YOUNGGIRL,
        BLACKSMITH,
        STRANGER

    }
    public abstract class NonePlayableCharacter
    {

        public List<string> NPC { get; private set; }

        public NonePlayableCharacter()
        {
            NPC = new List<string>();
            NPC.Add("노인");
            NPC.Add("소녀");
            NPC.Add("대장장이");
            NPC.Add("수상한사람");

        }
       
        public NPC_TYPE Code { get; set; }
        public QUEST_TYPE QCode { get; set; }
        public string Name { get; set; }
        public int QuestNum { get; set; }
        public bool IsQuest { get; set; } = false;
        public bool QuestGoals { get; set; } = false ;
        public bool CompletQuest { get; set; } = false;

        public string QuestDialog {  get; set; }
        public string NopeQuest { get; set; }

        public string QuestFinished { get; set; }
        public string QuestUnFinished { get; set; }
        public abstract void Quest();

        public void Init(string[] data) 
        {
            //0.NPC코드/1.퀘스트코드/2.NPC이름/3.퀘스트멘트/4.퀘스트 거절 시 멘트/5.퀘스트 완료문구/6.퀘스트 미완 문구
            Code = (NPC_TYPE)int.Parse(data[0]);
            QCode = (QUEST_TYPE)int.Parse(data[1]);
            Name = data[2];
            QuestDialog = data[3];
            NopeQuest = data[4];
            QuestFinished = data[5];
            QuestUnFinished = data[6];

        }




        public class OldMan : NonePlayableCharacter
        {
            public OldMan() 
            {
                Init(DataManager.Instance.NpcDB.List[(int)NPC_TYPE.OLDMAN]);
            }
            public override void Quest()
            {
                Console.WriteLine("고블린 사냥");
            }

        }

        public class YoungGirl : NonePlayableCharacter
        {
            public YoungGirl()
            {
                Init(DataManager.Instance.NpcDB.List[(int)NPC_TYPE.YOUNGGIRL]);
            }
            public override void Quest()
            {
                Console.WriteLine("슬라임 사냥");
            }

        }

        public class BlackSmith : NonePlayableCharacter
        {
            public BlackSmith()
            {
                Init(DataManager.Instance.NpcDB.List[(int)NPC_TYPE.BLACKSMITH]);
            }
            public override void Quest()
            {
                Console.WriteLine("글라디우스 사오기");
            }

        }

        public class Stranger : NonePlayableCharacter
        {
            public Stranger()
            {
                Init(DataManager.Instance.NpcDB.List[(int)NPC_TYPE.STRANGER]);
            }
            public override void Quest()
            {
                Console.WriteLine("자쿰 사냥");
            }

        }
    }
}
