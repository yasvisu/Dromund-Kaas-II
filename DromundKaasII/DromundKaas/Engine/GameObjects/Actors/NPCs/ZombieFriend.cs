using System;
using System.Collections.Generic;
using DromundKaasII.Engine.GameObjects.Skills;
using DromundKaasII.Input;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.NPCs
{
    /// <summary>
    /// A simple zombie.
    /// </summary>
    public class ZombieFriend : Npc
    {
        /// <summary>
        /// Initializes a new ZombieFriend.
        /// </summary>
        /// <param name="MapPosition">The ZombieFriend's position.</param>
        /// <param name="SkillChain">The skills the ZombieFriend can pick from.</param>
        public ZombieFriend(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : base(MapPosition, SkillChain)
        {
            this.Stats = new Statblock(8);

            this.Skills[0] = SkillChain["Hit"];
        }

        /// <summary>
        /// Tells the zombie to think of a next move.
        /// </summary>
        /// <param name="G">The gamestate to process.</param>
        public override void Act(GameState G)
        {
            this.Direction = G.Pathfinder.Orient(this.MapPosition, G.Player.MapPosition);
            this.DesiredAction = this.Direction.ToGameInput();

            for (int i = 0; i < this.Skills.Length; i++)
            {
                if (this.Skills[i] != null)
                {
                    if (Calculator.Distance(this.MapPosition, G.Player.MapPosition) <= this.Skills[i].Range)
                    {
                        this.DesiredAction = (GameInputs)Enum.Parse(typeof(GameInputs), "A" + (i + 1));
                    }
                }
            }

            base.Act(G);
        }
    }
}
