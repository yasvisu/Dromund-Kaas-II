using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players
{
    class Player : Actor, IPlayer
    {
        public Player(Vector2 MapPosition)
            : base(MapPosition)
        {

        }

        public int Score
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
