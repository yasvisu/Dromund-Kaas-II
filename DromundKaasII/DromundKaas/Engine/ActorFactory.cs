using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Exceptions;
using DromundKaasII.GameObjects.Actors.Players;
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

        public void CreatePlayer(Vector2 location, Player Player)
        {
            if(this.GameState.Player!=null)
            {
                throw new PlayerAlreadyExistsException();
            }

            this.GameState.Actors.Add(Player);
            this.GameState.Player = Player;
            this.GameState.Map[(int)Player.MapPosition.Y, (int)Player.MapPosition.X].Occupant = Player;
        }

        public void CreateNpc()
        {
            throw new NotImplementedException();
        }
    }
}
