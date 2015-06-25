﻿using System;
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

        public bool LevelUp { get; set; }

        // Illumination
        public Color IlluminationColor { get; set; }
        public float IlluminationRange
        {
            get
            {
                return this.Stats.IlluminationRange;
            }
        }
        public bool HasIlluminated
        {
            get
            {
                return false;
            }
        }

        public override void Act(GameState G)
        {
            this.Stats.Health--;
            this.Stats.Mana++;
            this.Stats.Focus++;
            if (this.Stats.Experience > (this.Stats.Level + 1) * 100)
            {
                this.LevelUp = true;
            }
            base.Act(G);
        }


    }
}
