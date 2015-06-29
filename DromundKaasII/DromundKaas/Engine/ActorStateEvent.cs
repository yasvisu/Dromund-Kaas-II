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
        /// <summary>
        /// Initialize a new ActorStateEvent.
        /// </summary>
        /// <param name="EventType">The type of event.</param>
        /// <param name="Actor">The actor the event is in relation to.</param>
        public ActorStateEvent(ActorEvents EventType, IActor Actor)
            : this(EventType, Actor, Actor.MapPosition, null)
        { }

        /// <summary>
        /// Initialize a new ActorStateEvent.
        /// </summary>
        /// <param name="EventType">The type of event.</param>
        /// <param name="Actor">The actor the event is in relation to.</param>
        /// <param name="Location">The location of the event.</param>
        /// <param name="Skill">The skill that is related to the event.</param>
        public ActorStateEvent(ActorEvents EventType, IActor Actor, Vector2 Location, ISkill Skill)
        {
            this.EventType = EventType;
            this.Actor = Actor;
            this.Location = Location;
            this.Skill = Skill;
        }

        /// <summary>
        /// The type of event.
        /// </summary>
        public ActorEvents EventType { get; set; }

        /// <summary>
        /// The actor the event is in relation to.
        /// </summary>
        public IActor Actor { get; set; }

        /// <summary>
        /// The location of the event.
        /// </summary>
        public Vector2 Location { get; set; }

        /// <summary>
        /// The skill that is related to the event.
        /// </summary>
        public ISkill Skill { get; set; }
    }
}
