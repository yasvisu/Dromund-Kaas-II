using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Input;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.NPCs
{
    public class ZombieFriend : Npc
    {
        public ZombieFriend(Vector2 MapPosition)
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

            this.Stats.Health = this.Stats.MaxHealth;
            this.Stats.Mana = this.Stats.MaxMana;
            this.Stats.Focus = this.Stats.MaxFocus;

        }

        public override void Act(Engine.GameState G)
        {
            this.DesiredAction = GameInputs.Up;

            if (this.MapPosition.X == 0)
            {
                this.DesiredAction = GameInputs.Down;
            }
            if(this.MapPosition.Y==G.MapHeight-1)
            {
                this.DesiredAction = GameInputs.Right;
            }
            if (this.MapPosition.X == G.MapWidth - 1)
            {
                this.DesiredAction = GameInputs.Up;
            }
            if (this.MapPosition.Y == 0 && this.MapPosition.X!=0)
            {
                this.DesiredAction = GameInputs.Left;
            }

        }
    }
}
