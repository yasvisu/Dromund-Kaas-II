using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.GameObjects.Actors;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Skills
{
    public class Skill
    {
        // add appropriate constructors and properties for the fields
        // ...

        public string Name { get; set; }
        public int ManaCost { get; set; }
        public int FocusCost { get; set; }
        public int Range { get; set; }


    }
}
