using System;

using DromundKaasII.Engine.Exceptions;
using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Engine.GameObjects.Actors.Players;
using DromundKaasII.Interfaces;

namespace DromundKaasII.Engine
{
    /// <summary>
    /// Factory for easy creation of Actors on a GameState.
    /// </summary>
    public class ActorFactory
    {
        GameState GameState;

        /// <summary>
        /// Initialize new ActorFactory.
        /// </summary>
        /// <param name="G">The GameState for the factory to produce in.</param>
        public ActorFactory(GameState G)
        {
            this.GameState = G;
        }

        /// <summary>
        /// Create a new player.
        /// </summary>
        /// <param name="Player">The player to add to the GameState.</param>
        public void CreatePlayer(Player Player)
        {
            if (this.GameState.Player != null)
            {
                throw new PlayerAlreadyExistsException("Cannot create player: player already exists.");
            }
            if (this.GameState.Map[(int)Player.MapPosition.X, (int)Player.MapPosition.Y].Occupant != null)
            {
                throw new SpawnOccupiedException("Cannot spawn player: spawn point occupied.");
            }
            this.GameState.Player = Player;

            this.PutOnGameState(Player);
        }

        /// <summary>
        /// Create a new Actor.
        /// </summary>
        /// <param name="a">The actor to add to the GameState.</param>
        public void CreateActor(Actor a)
        {
            if (this.GameState.Map[(int)a.MapPosition.X, (int)a.MapPosition.Y].Occupant != null)
            {
                throw new SpawnOccupiedException("Cannot spawn actor: spawn point occupied.");
            }

            this.PutOnGameState(a);
        }

        /// <summary>
        /// Remove the player from the GameState.
        /// </summary>
        public void RemovePlayer()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove the actor from the GameState.
        /// </summary>
        /// <param name="a">The actor to remove from the GameState.</param>
        public void RemoveActor(Actor a)
        {
            this.RemoveFromGameState(a);
        }

        /// <summary>
        /// Put an actor on the GameState.
        /// </summary>
        /// <param name="a">The actor to put on the GameState.</param>
        private void PutOnGameState(Actor a)
        {
            this.GameState.Actors.Add(a);
            this.GameState.Map[(int)a.MapPosition.Y, (int)a.MapPosition.X].Occupant = a;
            if (a is IIlluminator)
            {
                this.GameState.Illuminators.Add(a as IIlluminator);
            }
        }

        /// <summary>
        /// Remove an actor from the GameState.
        /// </summary>
        /// <param name="a">The actor to remove from the GameState.</param>
        private void RemoveFromGameState(Actor a)
        {
            GameState.Map[(int)a.MapPosition.Y, (int)a.MapPosition.X].Occupant = null;
            GameState.Actors.Remove(a);
            if (a is IIlluminator)
            {
                this.GameState.Illuminators.Remove(a as IIlluminator);
            }
        }
    }
}
