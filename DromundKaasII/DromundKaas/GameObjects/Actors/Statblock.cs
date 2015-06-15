using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.GameObjects.Actors
{
    public class Statblock
    {
        public int Health { get; set; }
        public int Mana { get; set; }

        public int Focus { get; set; }

        public float Strength { get; set; }
        public float Constitution { get; set; }
        public float Intelligence { get; set; }
        public float Dexterity { get; set; }
        // ...

    }
}
