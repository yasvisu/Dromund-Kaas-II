using DromundKaasII.GameObjects.Actors;
using DromundKaasII.GameObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.GameObjects
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
