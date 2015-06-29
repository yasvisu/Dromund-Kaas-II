using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine
{
    /// <summary>
    /// An event that occurred with relation to an Actor.
    /// </summary>
    public class ActorStateEvent
    {
        public ActorStateEvent(ActorEvents EventType, IActor Actor)
            : this(EventType, Actor, Actor.MapPosition, null)
        { }

        public ActorStateEvent(ActorEvents EventType, IActor Actor, Vector2 Location, ISkill Skill)
        {
            this.EventType = EventType;
            this.Actor = Actor;
            this.Location = Location;
            this.Skill = Skill;
        }

        public ActorEvents EventType { get; set; }
        public IActor Actor { get; set; }
        public Vector2 Location { get; set; }
        public ISkill Skill { get; set; }
    }
}
