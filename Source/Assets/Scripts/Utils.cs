using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public struct Formation
    {
        public float Distance;
        public int StartingWidth;
        public int NextRowWidthBonus;
    }

    public struct Level
    {
        public int numOfWaves;
        public int [] sizesOfWaves;
        public int bossIndex;
        public int squadSize;
        public float spawnTimer;
    }
}
