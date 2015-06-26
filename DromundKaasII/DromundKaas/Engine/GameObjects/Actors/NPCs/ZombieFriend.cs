using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Skills;
using DromundKaasII.Input;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.NPCs
{
    public class ZombieFriend : Npc
    {
        public ZombieFriend(Vector2 MapPosition, Dictionary<string,Skill> SkillChain)
            : base(MapPosition,SkillChain)
        {
            this.Stats = new Statblock(8);
        }

        public override void Act(GameState G)
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
            base.Act(G);
        }
    }
}
