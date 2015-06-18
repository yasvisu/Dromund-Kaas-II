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

        public override void Act(Engine.GameState G)
        {
            switch (this.PlayerInputOptions)
            {
                case GameInputs.None:
                    this.DesiredAction = ActionTypeOptions.None;
                    break;

                // Movement

                case GameInputs.Up:
                    this.DesiredAction = ActionTypeOptions.Move;
                    this.GroundTarget = new Vector2(this.MapPosition.X, this.MapPosition.Y - 1);
                    break;
                case GameInputs.Down:
                    this.DesiredAction = ActionTypeOptions.Move;
                    this.GroundTarget = new Vector2(this.MapPosition.X, this.MapPosition.Y + 1);
                    break;
                case GameInputs.Left:
                    this.DesiredAction = ActionTypeOptions.Move;
                    this.GroundTarget = new Vector2(this.MapPosition.X - 1, this.MapPosition.Y);
                    break;
                case GameInputs.Right:
                    this.DesiredAction = ActionTypeOptions.Move;
                    this.GroundTarget = new Vector2(this.MapPosition.X + 1, this.MapPosition.Y);
                    break;

                // Actions
/*
                case GameInputs.A1:

                case GameInputs.A2:
                case GameInputs.A3:
                case GameInputs.A4:
                case GameInputs.A5:

                case GameInputs.Interact:*/
                default:
                    this.DesiredAction = ActionTypeOptions.None;
                    break;
            }
        }
    }
}
