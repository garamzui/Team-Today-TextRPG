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
    
    }
    public abstract class NonePlayerableChatacter
    {

        public List<string> NPC { get; private set; }

        public NonePlayerableChatacter()
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

        public abstract void Quest();

        public void Init(string[] data) 
        {
            //0.NPC코드/1.퀘스트코드/2.NPC이름
            Code = (NPC_TYPE)int.Parse(data[0]);
            QCode = (QUEST_TYPE)int.Parse(data[1]);
            Name = data[2];
        
        }




        public class OldMan : NonePlayerableChatacter
        {
            public override void Quest()
            {
                Console.WriteLine("고블린 사냥");
            }

        }

        public class YoungGirl : NonePlayerableChatacter
        {
            public override void Quest()
            {
                Console.WriteLine("슬라임 사냥");
            }

        }

        public class BlackSmith : NonePlayerableChatacter
        {
            public override void Quest()
            {
                Console.WriteLine("글라디우스 사오기");
            }

        }

        public class Stranger : NonePlayerableChatacter
        {
            public override void Quest()
            {
                Console.WriteLine("자쿰 사냥");
            }

        }
    }
}
