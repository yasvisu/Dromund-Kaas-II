using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players
{
    class Primal : Actor
    {
        private const int BaseHealth = 100;
        private const int BaseMana = 100;
        private const int BaseFocus = 100;
        
        public Primal(Vector2 MapPosition, int baseHealth, int baseMana, int baseFocus):base(MapPosition)
        {
            this.Stats = new Statblock();
        }


    }
}
