using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Interfaces;

namespace DromundKaasII.GameObjects.Actors.Players
{
    class Player : Actor, IPlayer
    {
        public int Score
        {
            get { throw new NotImplementedException(); }
        }
    }
}
