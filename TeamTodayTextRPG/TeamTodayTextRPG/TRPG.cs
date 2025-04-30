using System.Collections.Generic;

namespace TeamTodayTextRPG
{
    class TRPG
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //GameManager.Instance.Animation.TitleAnimation();
            var sm = SceneManager.Instance;
            var dm = DataManager.Instance;

            /*
            foreach (var ch in DataManager.Instance.ItemDB.List)
            {
                Console.WriteLine($"{ch.Code} / {ch.Type} / {ch.Name} / {ch.Text}, Value {ch.Value}");
            }*/


            
            sm.Intro();




            //『효빈』스테이트 머신
            while (true)
            {
                sm.SwitchScene(sm.CurrentViewType);
            }
        }
    }
}