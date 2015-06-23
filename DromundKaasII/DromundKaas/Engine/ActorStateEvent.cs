using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.GameObjects.Actors;

namespace DromundKaasII.Engine
{
    public class ActorStateEvent
    {
        public ActorStateEvent(ActorEvents EventType, Actor Actor)
        {
            this.EventType = EventType;
            this.Actor = Actor;
        }

        public ActorEvents EventType { get; set; }
        public Actor Actor { get; set; }
    }
}
