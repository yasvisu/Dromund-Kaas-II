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
        public ActorEvents EventType;
        public Actor Actor;
    }
}
