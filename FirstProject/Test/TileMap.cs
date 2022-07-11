using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class TileMap
    {
        bool isStarTile;
        int tileNum;

        public TileMap(bool isStarTile, int tileNum)
        {
            this.isStarTile = isStarTile;
            this.tileNum = tileNum;
        }

        public bool IsStarTile() { return isStarTile; }
        public int GetTileNum()  { return tileNum; }
    }
}
