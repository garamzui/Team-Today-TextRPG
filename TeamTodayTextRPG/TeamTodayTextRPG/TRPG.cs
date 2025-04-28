namespace TeamTodayTextRPG
{
    class TRPG
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            GameManager.Instance.Animation.TitleAnimation();
            var sm = SceneManager.Instance;

            // 『효빈』스테이트 머신
            while (true)
            {
                sm.SwitchScene(sm.CurrentViewType);
            }
        }
    }
}