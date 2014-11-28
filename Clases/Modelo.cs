using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public static class Map
    {
        string[,] map = new string[15, 15];

        public string[,] Map { get { return map; } private set { map = value; } }
    }

    public class Player
    {

    }


}
