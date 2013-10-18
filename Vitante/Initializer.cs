using System;

namespace Vitante
{
#if WINDOWS || XBOX
    static class Initializer
    {
        static void Main(string[] args)
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
#endif
}

