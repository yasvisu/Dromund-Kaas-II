using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Input;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Players
{
    using DromundKaasII.Engine.GameObjects.Skills;

    public abstract class Player : Actor, IPlayer
    {
        protected GameInputs playerInputOptions;

        public Player(Vector2 MapPosition)
            : base(MapPosition)
        { }

        public Player(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : base(MapPosition, SkillChain)
        {

        }

        public int Score
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool LevelUp
        {
            get;
            set;
        }

        public override void Act(GameState G)
        {
            base.Act(G);
        }
    }
}
