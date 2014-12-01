using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace Cliente
{
    class Program
    {


        public enum Direction
        {
            Up,Down,Left,Right
        }
        static void Main()
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 40;
            Console.CursorVisible = false;

            int x, y;

            char food = '*';

            char snakehead = 'e';

            char snakebody = '#';

            int snakelenght;

            bool crashed = false;

            bool quit = false;

            int refresh = 75;

            Direction direction;

            ConsoleKeyInfo cki;
            
            x = Console.WindowWidth / 2 -2;
            y = Console.WindowHeight / 2;


            Console.SetCursorPosition(x,y);
            Console.Write(snakebody+snakebody+snakehead);

           




            while(!quit)
            {



                while(!Console.KeyAvailable)
                {

                    System.Threading.Thread.Sleep(10);
                }



                cki = Console.ReadKey(true);

                switch(cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        direction = Direction.Up;
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = Direction.Right;
                        break;
                    case ConsoleKey.DownArrow:
                        direction = Direction.Down;
                        break;
                    case ConsoleKey.Escape:
                        quit = true;
                        break;
                }
               DateTime nextcheck = DateTime.Now.AddMilliseconds(refresh);
                while(!quit && !crashed)
                {

                    while(nextcheck > DateTime.Now)
                    {
                        if(Console.KeyAvailable)
                        {
                            cki = Console.ReadKey(true);

                            switch (cki.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    direction = Direction.Up;
                                    break;
                                case ConsoleKey.LeftArrow:
                                    direction = Direction.Left;
                                    break;
                                case ConsoleKey.RightArrow:
                                    direction = Direction.Right;
                                    break;
                                case ConsoleKey.DownArrow:
                                    direction = Direction.Down;
                                    break;
                                case ConsoleKey.Escape:
                                    quit = true;
                                    break;
                            }
                        }
                    }


                    if(!quit)
                    {

                    }

                }


            }



        }


    }
}
