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
        public ZombieFriend(Vector2 MapPosition, Dictionary<string,Skill> SkillChain)
            : base(MapPosition,SkillChain)
        {
            this.Stats = new Statblock(8);
        }

        /// <summary>
        /// Tells the zombie to think of a next move.
        /// </summary>
        /// <param name="G">The gamestate to process.</param>
        public override void Act(GameState G)
        {
            G.Pathfinder.Orient(this.MapPosition, G.Player.MapPosition);
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
            base.Act(G);
        }
    }
}
