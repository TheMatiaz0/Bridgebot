using System;

namespace GameJamProject
{
#if WINDOWS || LINUX
 
    public static class Program
    {

        [STAThread]
        static void Main()
        {
            using (TrualityEngine.TheGame game = new TrualityEngine.TheGame(new MyGameHeart()))
                game.Run();
        }
    }
#endif
}
