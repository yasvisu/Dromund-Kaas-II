using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players
{
    class Primal : Actor
    {
     
        public Primal(Vector2 MapPosition, int baseHealth, int baseMana, int baseFocus):base(MapPosition)
        {
            this.Stats = new Statblock();
        }


    }
}
