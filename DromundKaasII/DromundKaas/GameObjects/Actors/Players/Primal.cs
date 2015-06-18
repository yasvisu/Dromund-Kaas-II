﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players
{
    public class Primal : Player
    {

        public Primal(Vector2 MapPosition)
            : base(MapPosition)
        {
            this.Stats = new Statblock()
            {
                Strength = 8,
                Dexterity = 8,
                Constitution = 8,
                Intelligence = 8,

                Wisdom = 8,
                Charisma = 8,
                Psychic = 8,
                
                TraversalPower = 100,
            };
        }


    }
}
