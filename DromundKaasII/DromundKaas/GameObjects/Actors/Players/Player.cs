using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Input;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players
{
    public abstract class Player : Actor, IPlayer
    {
        protected GameInputs playerInputOptions;

        public Player(Vector2 MapPosition)
            : base(MapPosition)
        {

        }

        public int Score
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public GameInputs PlayerInputOptions
        {
            get
            {
                return this.playerInputOptions;
            }
            set
            {
                if (value != GameInputs.None)
                {
                    this.playerInputOptions = value;
                }
            }
        }
    }
}
