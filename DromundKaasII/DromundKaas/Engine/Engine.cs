using DromundKaasII.GameObjects;
using DromundKaasII.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DromundKaasII.Engine
{
    public class Engine:IEngine
    {
        public GameState GameState { get; set; }

        public IEnumerable<ActorStateEvent> TranspiredEvents
        {
            get { throw new NotImplementedException(); }
        }


        public void Step()
        {
            throw new NotImplementedException();
        }



    }
}
