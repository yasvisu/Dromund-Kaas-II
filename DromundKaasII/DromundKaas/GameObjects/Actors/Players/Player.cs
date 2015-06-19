using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Input;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players
{
    using DromundKaasII.GameObjects.Skills;

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

        public Skill[] PlayerSkills { get; protected set; }

        public override void Act(Engine.GameState G)
        {
            
        }
    }
}
