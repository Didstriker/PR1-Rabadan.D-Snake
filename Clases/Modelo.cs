using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public static class Mapping
    {
        static char[,] map = new char[40, 40];

        public static char[,] Map { get { return map; } private set { map = value; } }

        
    }

    public static class Player
    {
        static char snakehead = 'e';

        static char snakebody = '#';

        static int snakelenght;

    }


}
