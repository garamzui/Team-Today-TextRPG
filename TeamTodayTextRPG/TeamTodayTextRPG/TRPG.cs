namespace TeamTodayTextRPG
{
    class TRPG
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            VIEW_TYPE currentView = VIEW_TYPE.MAIN;
            var gm = GameManager.Instance;
            var sm = SceneManager.Instance;
            var dm = DataManager.Instance;

            Console.ReadLine();
            gm.Animation.TitleAnimation();


            // 『효빈』스테이트 머신
            while (true)
            {
                sm.SwitchScene(currentView);
                //『효빈』선택지 입력 시 다음 화면으로의 전환
                currentView = sm.ChangeNextView();

                //currentView = sm.CurrentViewer.NextView(sm.InputAction(sm.CurrentViewer.StartIndex, sm.CurrentViewer.EndIndex, Console.CursorTop));
            }
        }
    }
}