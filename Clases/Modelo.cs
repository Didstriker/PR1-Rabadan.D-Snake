using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Model
{

    
   public enum Direction
    {
        Up, Down, Left, Right
    }

    public class Point
    {
     public int X;
     public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
   
    public static class Logic
    {
        


        public static Random alea = new Random();  //Random generator for the food generator.
        static char[,] map = new char[20,40];      //Default Size of the array
        public static int wallType = 1; //to choose logic of walls
        public static int x;  //Vars for head's position, we initialize them on the middle of the map
        public static int y;
        public static int Score;
        public static readonly char snakeHead = 'e';
        public static readonly char snakeBody = '#';
        static char food = '*';
        public static ConsoleKeyInfo cki;
        static ConsoleKey lastCki = ConsoleKey.DownArrow; //le damos por defecto flecha abajo, porque se usa una vez antes de ser asignada y además solventamos el problema de empezar hacia donde esta el cuerpo.
        public static Direction direction = Direction.Right;
        public static List<Point> body = new List<Point>(); //A List of points called body where we're gonna save the position of each body block
        static bool AsignedDirection = false;
        static int Yfood =  Yfood = alea.Next(1, Logic.Map.GetLength(0) - 1); 
        static int Xfood =  Xfood = alea.Next(1, Logic.Map.GetLength(1) - 1);
        public static char[,] Map { get { return map; } set { map = value; } }


        public static bool quit = false;
        public static bool denegated = false; //we'll use this bool in "Setdirection"
        public static bool crashed = false;






         public static void PrepareMap()
        {
            for (int i = 0; i <= Map.GetLength(0) -1; i++)
            {
                for (int j = 0; j <= Map.GetLength(1) -1; j++)
                {
                    if (i == 0 || i == Map.GetLength(0) - 1)
                        Map[i, j] = '-';
                    else if (j == 0 || j == Map.GetLength(1) - 1)
                        Map[i, j] = '|';
                    else if (i == Yfood && j == Xfood)
                        Map[i, j] = food;
                    else Map[i, j] = ' ';
                }
            }
            
            
        } // Writes the logical values onto the array
         public static void IterateSnake()  //writes the Snake's body onto the array
           {
            foreach (Point point in body)               //iterar la lista de posiciones de body en mapa
                Map[point.Y, point.X] = snakeBody;
                        

           }  

         public static void WriteMapOnConsole(char[,] array)
         {
             for (int fila = 0; fila < array.GetLength(0); fila++) 
             {
                 for (int col = 0; col < array.GetLength(1); col++)
                     Console.Write(array[fila, col]);
                 Console.WriteLine();
             }
         }  //Writes the array at the console, "MostrarMatriz() de to la life"


         private static void GenerateFood() //private cause its used on "Eaten()".
         {
             do // to avoid generating food under the snake  !!!!Doesn't works, dont know why.
             {

                 Yfood = alea.Next(1, Map.GetLength(0) - 1);
                 Xfood = alea.Next(1, Map.GetLength(1) - 1);
             } while (Map[Yfood, Xfood] == snakeBody);

             Map[Yfood, Xfood] = food;       
         }


         public static void CheckCrashed() //doesn't need to explain. 
         {
             if (Map[y, x] == snakeBody) 
             {
                 crashed = true;
             }

         }


         public static void SetDirection() //reads the keyboard and set the direction
         {
             if (Console.KeyAvailable)
             {
                 if(!AsignedDirection) //this one for make sure we only make 1 move each time (got problems with that)
                 {

                 
                 cki = Console.ReadKey(true);

                 switch (cki.Key)
                 {
                     case ConsoleKey.UpArrow:
                         if (lastCki == ConsoleKey.DownArrow) //if we saved the last direction, we can avoid that the snake rotates 180 degrees.
                         { denegated = true; break; }
                         direction = Direction.Up;
                         denegated = false;
                         AsignedDirection = true;
                         break;
                     case ConsoleKey.LeftArrow:
                         if (lastCki == ConsoleKey.RightArrow)
                         { denegated = true; break; }
                         direction = Direction.Left;
                         denegated = false;
                         AsignedDirection = true;
                         break;
                     case ConsoleKey.RightArrow:
                         if (lastCki == ConsoleKey.LeftArrow)
                         { denegated = true; break; }
                         denegated = false;
                         direction = Direction.Right;
                         AsignedDirection = true;
                         break;
                     case ConsoleKey.DownArrow:
                         if (lastCki == ConsoleKey.UpArrow)
                         { denegated = true; break; }
                         denegated = false;
                         direction = Direction.Down;
                         AsignedDirection = true;
                         break;
                     case ConsoleKey.Escape:
                         quit = true;
                         break;
                 }

                 if (!denegated)
                     lastCki = cki.Key; //guardamos la ultima direccion tomada para evitar que la serpiente gire 180 grados
                 denegated = false;
               }
             }
             AsignedDirection = false;
         }

         public static void MoveSnake()
         {
             body.RemoveAt(body.Count - 1);        //now we erase the last char of the snake's body
             body.Insert(0, new Point(x, y));      //now we add the char on head's position to move the snake

             if (direction == Direction.Left)
             {
                 x--;
             }

             if (direction == Direction.Down)
             {
                 y++;
             }
             if (direction == Direction.Right)
             {
                 x++;
             }
             if (direction == Direction.Up)
             {
                 y--;
             }
         }  //logical movement of snake

         public static void CrossWall(int Wall)  //(if the type of game is "1"): ckecks if snake is on a wall, then moves it to the opposite end of map (if the type is "2"): checks if snake is on a wall then it crashed and game over.
         {
            if(Wall==1)
             if (Logic.Map[y, x] == '|' || Logic.Map[y, x] == '-') 
             {
                 if (direction == Direction.Left)
                     x = Logic.Map.GetLength(1) - 2;
                 if (direction == Direction.Down)
                     y = 1;
                 if (direction == Direction.Right)
                     x = 1;
                 if (direction == Direction.Up)
                     y = Logic.Map.GetLength(0) - 2;
             }
            if (Wall == 2)
                if (Map[y, x] == '|' || Map[y, x] == '-')
                    crashed = true;
         }

         public static void Eaten()  //Checks if snakes ate food, then generates a new one
         {
             if (Logic.Map[Logic.y, Logic.x] == food)  //if snakes ate food also we add one block of body
             {
                 body.Insert(0, new Point(Logic.x, Logic.y));
                 GenerateFood();
                 Score += 1000;
             }
         }

        public static void SetHeadOnMap() //this could be on "IterateSnake" but is possible to do more things if its separated. (simply sets the head at the array)
         {
             Map[y, x] = snakeHead;
         }

    }
       
   

}
