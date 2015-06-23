﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine.Exceptions;
using DromundKaasII.Engine.GameObjects.Actors.NPCs;
using DromundKaasII.Engine.GameObjects.Actors.Players;
using DromundKaasII.Engine.GameObjects.Skills;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine
{
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

            this.GameState.Actors.Add(Player);
            this.GameState.Player = Player;
            this.GameState.Map[(int)Player.MapPosition.Y, (int)Player.MapPosition.X].Occupant = Player;
        }

        public void CreateNpc(Npc Npc)
        {
            if (this.GameState.Map[(int)Npc.MapPosition.X, (int)Npc.MapPosition.Y].Occupant != null)
            {
                throw new SpawnOccupiedException();
            }

            this.GameState.Actors.Add(Npc);
            this.GameState.Map[(int)Npc.MapPosition.Y, (int)Npc.MapPosition.X].Occupant = Npc;
        }

        public void RemovePlayer()
        {
            throw new NotImplementedException();
        }

        public void RemoveNpc(Npc Npc)
        {
            GameState.Map[(int)Npc.MapPosition.Y, (int)Npc.MapPosition.X].Occupant = null;
            GameState.Actors.Remove(Npc);
        }
    }
}
