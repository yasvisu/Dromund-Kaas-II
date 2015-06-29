using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.Exceptions;
using DromundKaasII.Engine.GameObjects.Actors;
using DromundKaasII.Engine.GameObjects.Actors.Debris;
using DromundKaasII.Engine.GameObjects.Actors.NPCs;
using DromundKaasII.Engine.GameObjects.Actors.Players;
using DromundKaasII.Engine.GameObjects.Skills;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine
{
    /// <summary>
    /// Factory for easy creation of Actors on a GameState.
    /// </summary>
    public class ActorFactory
    {
        GameState GameState;

        public ActorFactory(GameState G)
        {
            this.GameState = G;
        }

        public void CreatePlayer(Player Player)
        {
            if (this.GameState.Player != null)
            {
                throw new PlayerAlreadyExistsException();
            }
            if (this.GameState.Map[(int)Player.MapPosition.X, (int)Player.MapPosition.Y].Occupant != null)
            {
                throw new SpawnOccupiedException();
            }
            this.GameState.Player = Player;

            this.PutOnGameState(Player);
        }

        public void CreateActor(Actor a)
        {
            if (this.GameState.Map[(int)a.MapPosition.X, (int)a.MapPosition.Y].Occupant != null)
            {
                throw new SpawnOccupiedException();
            }

            this.PutOnGameState(a);
        }

        public void RemovePlayer()
        {
            throw new NotImplementedException();
        }

        public void RemoveActor(Actor a)
        {
            RemoveFromGameState(a);
        }

        private void PutOnGameState(Actor a)
        {
            this.GameState.Actors.Add(a);
            this.GameState.Map[(int)a.MapPosition.Y, (int)a.MapPosition.X].Occupant = a;
            if (a is IIlluminator)
            {
                this.GameState.Illuminators.Add(a as IIlluminator);
            }
        }

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
