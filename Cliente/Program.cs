using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Model;

namespace Snake.Cliente
{
    class Program
    {
        static int refresh = 50; //this means the delay on the snake to move (can be changed "in-game")
        static void Main()
        {            
            
            Inizialization();             
                GameLoop();
        }

        private static void GameLoop()
        {
            FirstMove();
            while (!Logic.quit && !Logic.crashed) //Main Game Loop
            {
                DateTime nextcheck = DateTime.Now.AddMilliseconds(refresh); // Now we create a datetime object, it will be our "homemade" timer, every "refresh" miliseconds he would take a direction
                while (nextcheck > DateTime.Now)                            //and continue with its work (we can also set the speed of the snake by changing the refresh value)
                
                {
                    Logic.SetDirection();
                }
                Logic.MoveSnake();
                Logic.CheckCrashed();
                Logic.CrossWall(Logic.wallType);
                if (!Logic.quit && !Logic.crashed)
                {
                    Logic.PrepareMap();
                    Logic.IterateSnake();
                    Logic.Eaten();
                    Logic.SetHeadOnMap();
                    Console.Clear();
                    Header();
                    Console.SetCursorPosition(0, 10);
                    Logic.WriteMapOnConsole(Logic.Map);
                    Logic.Score++;
                }
            }
            Logic.quit = false;
            MenuFinal();
        }

        private static void FirstMove()
        {
            Logic.cki = Console.ReadKey(true);

            switch (Logic.cki.Key)
            {
                case ConsoleKey.LeftArrow:    //no hay case uparrow porque si se empieza hacia arriba se crashea ya que el cuerpo de la serpiente empieza arriba
                    Logic.direction = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    Logic.direction = Direction.Right;
                    break;
                case ConsoleKey.DownArrow:
                    Logic.direction = Direction.Down;
                    break;
                case ConsoleKey.Escape:
                    Logic.quit = true;
                    break;
            }
        }

        private static void Inizialization()
        {
            if (!Logic.quit)
            {
                Console.Title = "Snake - By Didac";
                Console.WindowHeight = 40;
                Console.WindowWidth = 100;
                Console.CursorVisible = false;
                Logic.x = Logic.Map.GetLength(1) / 2;
                Logic.y = Logic.Map.GetLength(0) / 2;
                Logic.Score = 0;
                Logic.crashed = false;
                Menu1();
                Logic.PrepareMap();
                InitialSnakeBody();
                Logic.IterateSnake();
                Logic.SetHeadOnMap();
                Header();
                Console.SetCursorPosition(0, 10);
                Logic.WriteMapOnConsole(Logic.Map);
                while (!Console.KeyAvailable) //Waiting for first key down
                {

                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        private static void Menu1()
        {
            Header();
            Console.WriteLine("");
            Console.WriteLine("Welcome to Snake by Didac");
            Console.WriteLine("Check the options and start the game!");
            Console.SetCursorPosition(5, 12);
            Console.Write("1. Start");
            Console.SetCursorPosition(5, 13);
            Console.Write("2. Options");
            Console.SetCursorPosition(5, 14);
            Console.Write("3. Exit");

            Logic.cki = Console.ReadKey(true);
            switch(Logic.cki.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();                    
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Menu2();
                    break;
                case ConsoleKey.D3:
                    Environment.Exit(0);
                    Logic.quit = true;
                    break;
                default :
                    Console.Clear();
                    Menu1();
                 //   Inizialization();
                    break;

            }



        }
        private static void Menu2()
        {
            Header();
            Console.SetCursorPosition(5, 12);
            Console.Write("1. Play with Walls or Not ({0})", Logic.wallType==2 ? "Yes" : "No, this is the best option :D");
            Console.SetCursorPosition(5, 13);
            Console.Write("2. Refresh time   *{0}",refresh);
            Console.SetCursorPosition(5, 14);
            Console.Write("3. Return");

            Logic.cki = Console.ReadKey(true);
            switch (Logic.cki.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    if (Logic.wallType==2)
                        Logic.wallType=1;
                    else Logic.wallType=2;
                    Menu2();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Menu3();
                    Menu2();
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    Menu1();
                    break;
                default:
                    Console.Clear();
                    Menu2();
                    break;

            }        
        }

        private static void Menu3()
        {
                        Header();
            Console.SetCursorPosition(5, 12);
            Console.Write("1. Snake Speed = Hell (every 25 ms)");
            Console.SetCursorPosition(5, 13);
            Console.Write("2. Snake Speed = Hard (every 50 ms)");
            Console.SetCursorPosition(5, 14);
            Console.Write("3. Snake Speed = Medium (every 75 ms)");
            Console.SetCursorPosition(5,15);
            Console.Write("4. Snake Speed = Easy for pussies (every 100 ms)");

            Logic.cki = Console.ReadKey(true);
            switch (Logic.cki.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    refresh = 25;
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    refresh = 50;
                    break;
                case ConsoleKey.D3:
                    Console.Clear();
                    refresh = 75;
                    break;
                case ConsoleKey.D4:
                    Console.Clear();
                    refresh = 100;
                    break;
                default:
                    Console.Clear();
                    Menu3();
                    break;
            }


        }

        private static void MenuFinal()   //supermega-GameOverAnimation proudly made by Didac :D
        {
            do        
            {
                if(!Console.KeyAvailable)
                {

                
                Console.Clear();
                Header();
                Console.SetCursorPosition(0, 20);              
                Console.WriteLine("You've scored {0}", Logic.Score);
                Console.WriteLine();
                Console.WriteLine("1. Main menu");
                Console.WriteLine("2. Exit");
                Console.SetCursorPosition(0, 10);
                GameOverAnimation1();
                }

                if (!Console.KeyAvailable)
                {
                    Console.Clear();
                    Header();
                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine("You've scored {0}", Logic.Score);
                    Console.WriteLine();
                    Console.WriteLine("1. Main menu");
                    Console.WriteLine("2. Exit");
                    Console.SetCursorPosition(0, 10);
                    GameOverAnimation2();
                }
                if (!Console.KeyAvailable)
                {
                    Console.Clear();
                    Header();
                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine("You've scored {0}", Logic.Score);
                    Console.WriteLine();
                    Console.WriteLine("1. Main menu");
                    Console.WriteLine("2. Exit");
                    Console.SetCursorPosition(0, 10);
                    GameOverAnimation3();
                }
                if (!Console.KeyAvailable)
                {
                    Console.Clear();
                    Header();
                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine("You've scored {0}", Logic.Score);
                    Console.WriteLine();
                    Console.WriteLine("1. Main menu");
                    Console.WriteLine("2. Exit");
                    Console.SetCursorPosition(0, 10);
                    GameOverAnimation4();
                }
                if (!Console.KeyAvailable)
                {
                    Console.Clear();
                    Header();
                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine("You've scored {0}", Logic.Score);
                    Console.WriteLine();
                    Console.WriteLine("1. Main menu");
                    Console.WriteLine("2. Exit");
                    Console.SetCursorPosition(0, 10);
                    GameOverAnimation3();
                }
                if (!Console.KeyAvailable)
                {
                    Console.Clear();
                    Header();
                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine("You've scored {0}", Logic.Score);
                    Console.WriteLine();
                    Console.WriteLine("1. Main menu");
                    Console.WriteLine("2. Exit");
                    Console.SetCursorPosition(0, 10);
                    GameOverAnimation2();
                }
            } while (!Console.KeyAvailable);
            Logic.cki = Console.ReadKey(true);
            switch (Logic.cki.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Main();
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Logic.quit = true;
                    break;
                default:
                    Console.Clear();
                    MenuFinal();
                    break;
            }



        }

        private static void InitialSnakeBody()
        {
            Logic.body.Clear();
            for (int i = 0; i < 5; i++)                
                Logic.body.Add(new Point(Logic.x, Logic.y - i));
        }//Create the points for initial snake's body

 

        private static void Header()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Red;            
            Console.WriteLine(@"  ________  _____  ___        __       __   ___  _______  ");
            Console.WriteLine(@" /        )(\    \|   \      /  \     |/ | /   )/       | ");
            Console.WriteLine(@"(:   \___/ |.\\   \    |    /    \    (: |/   /(: ______) ");
            Console.WriteLine(@" \___  \   |: \.   \\  |   /' /\  \   |    __/  \/    |   ");
            Console.WriteLine(@"  __/  \\  |.  \    \. |  //  __'  \  (// _  \  // ___)_  ");
            Console.WriteLine(@" /  \   :) |    \    \ | /   /  \\  \ |: | \  \(:       | ");
            Console.WriteLine(@"(_______/   \___|\____\)(___/    \___)(__|  \__)\_______) ");
            Console.ResetColor();
        }        


        private static void GameOverAnimation1()
        {

            Console.WriteLine(@"  /$$$$$$   /$$$$$$  /$$      /$$ /$$$$$$$$        /$$$$$$  /$$    /$$ /$$$$$$$$ /$$$$$$$ ");
            Console.WriteLine(@" /$$__  $$ /$$__  $$| $$$    /$$$| $$_____/       /$$__  $$| $$   | $$| $$_____/| $$__  $$");
            Console.WriteLine(@"| $$  \__/| $$  \ $$| $$$$  /$$$$| $$            | $$  \ $$| $$   | $$| $$      | $$  \ $$");
            Console.WriteLine(@"| $$ /$$$$| $$$$$$$$| $$ $$/$$ $$| $$$$$         | $$  | $$|  $$ / $$/| $$$$$   | $$$$$$$/");
            Console.WriteLine(@"| $$|_  $$| $$__  $$| $$  $$$| $$| $$__/         | $$  | $$ \  $$ $$/ | $$__/   | $$__  $$");
            Console.WriteLine(@"| $$  \ $$| $$  | $$| $$\  $ | $$| $$            | $$  | $$  \  $$$/  | $$      | $$  \ $$");
            Console.WriteLine(@"|  $$$$$$/| $$  | $$| $$ \/  | $$| $$$$$$$$      |  $$$$$$/   \  $/   | $$$$$$$$| $$  | $$");
            Console.WriteLine(@" \______/ |__/  |__/|__/     |__/|________/       \______/     \_/    |________/|__/  |__/");

            System.Threading.Thread.Sleep(200);
        }

        private static void GameOverAnimation4()
        {
            Console.WriteLine(@"  ______    ______   __       __  ________         ______   __     __  ________  _______  ");
            Console.WriteLine(@" /      \  /      \ /  \     /  |/        |       /      \ /  |   /  |/        |/       \ ");
            Console.WriteLine(@"/$$$$$$  |/$$$$$$  |$$  \   /$$ |$$$$$$$$/       /$$$$$$  |$$ |   $$ |$$$$$$$$/ $$$$$$$  |");
            Console.WriteLine(@"$$ | _$$/ $$ |__$$ |$$$  \ /$$$ |$$ |__          $$ |  $$ |$$ |   $$ |$$ |__    $$ |__$$ |");
            Console.WriteLine(@"$$ |/    |$$    $$ |$$$$  /$$$$ |$$    |         $$ |  $$ |$$  \ /$$/ $$    |   $$    $$< ");
            Console.WriteLine(@"$$ |$$$$ |$$$$$$$$ |$$ $$ $$/$$ |$$$$$/          $$ |  $$ | $$  /$$/  $$$$$/    $$$$$$$  |");
            Console.WriteLine(@"$$ \__$$ |$$ |  $$ |$$ |$$$/ $$ |$$ |_____       $$ \__$$ |  $$ $$/   $$ |_____ $$ |  $$ |");
            Console.WriteLine(@"$$    $$/ $$ |  $$ |$$ | $/  $$ |$$       |      $$    $$/    $$$/    $$       |$$ |  $$ |");
            Console.WriteLine(@" $$$$$$/  $$/   $$/ $$/      $$/ $$$$$$$$/        $$$$$$/      $/     $$$$$$$$/ $$/   $$/ ");
            System.Threading.Thread.Sleep(200);
        }

        private static void GameOverAnimation3()
        {
            Console.WriteLine(@"  ______    ______   __       __  ________         ______   __     __  ________  _______  ");
            Console.WriteLine(@" /      \  /      \ |  \     /  \|        \       /      \ |  \   |  \|        \|       \");
            Console.WriteLine(@"|  $$$$$$\|  $$$$$$\| $$\   /  $$| $$$$$$$$      |  $$$$$$\| $$   | $$| $$$$$$$$| $$$$$$$\");
            Console.WriteLine(@"| $$ __\$$| $$__| $$| $$$\ /  $$$| $$__          | $$  | $$| $$   | $$| $$__    | $$__| $$");
            Console.WriteLine(@"| $$|    \| $$    $$| $$$$\  $$$$| $$  \         | $$  | $$ \$$\ /  $$| $$  \   | $$    $$");
            Console.WriteLine(@"| $$ \$$$$| $$$$$$$$| $$\$$ $$ $$| $$$$$         | $$  | $$  \$$\  $$ | $$$$$   | $$$$$$$\");
            Console.WriteLine(@"| $$__| $$| $$  | $$| $$ \$$$| $$| $$_____       | $$__/ $$   \$$ $$  | $$_____ | $$  | $$");
            Console.WriteLine(@" \$$    $$| $$  | $$| $$  \$ | $$| $$     \       \$$    $$    \$$$   | $$     \| $$  | $$");
            Console.WriteLine(@"  \$$$$$$  \$$   \$$ \$$      \$$ \$$$$$$$$        \$$$$$$      \$     \$$$$$$$$ \$$   \$$");
            System.Threading.Thread.Sleep(200);
        }

        private static void GameOverAnimation2()
        {
            Console.WriteLine(@" $$$$$$\   $$$$$$\  $$\      $$\ $$$$$$$$\        $$$$$$\  $$\    $$\ $$$$$$$$\ $$$$$$$\  ");
            Console.WriteLine(@"$$  __$$\ $$  __$$\ $$$\    $$$ |$$  _____|      $$  __$$\ $$ |   $$ |$$  _____|$$  __$$\ ");
            Console.WriteLine(@"$$ /  \__|$$ /  $$ |$$$$\  $$$$ |$$ |            $$ /  $$ |$$ |   $$ |$$ |      $$ |  $$ |");
            Console.WriteLine(@"$$ |$$$$\ $$$$$$$$ |$$\$$\$$ $$ |$$$$$\          $$ |  $$ |\$$\  $$  |$$$$$\    $$$$$$$  |");
            Console.WriteLine(@"$$ |\_$$ |$$  __$$ |$$ \$$$  $$ |$$  __|         $$ |  $$ | \$$\$$  / $$  __|   $$  __$$< ");
            Console.WriteLine(@"$$ |  $$ |$$ |  $$ |$$ |\$  /$$ |$$ |            $$ |  $$ |  \$$$  /  $$ |      $$ |  $$ |");
            Console.WriteLine(@"\$$$$$$  |$$ |  $$ |$$ | \_/ $$ |$$$$$$$$\        $$$$$$  |   \$  /   $$$$$$$$\ $$ |  $$ |");
            Console.WriteLine(@" \______/ \__|  \__|\__|     \__|\________|       \______/     \_/    \________|\__|  \__|");

            System.Threading.Thread.Sleep(200);
        }
    }
}
