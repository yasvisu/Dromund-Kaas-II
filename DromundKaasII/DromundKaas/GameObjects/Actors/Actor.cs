using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.GameObjects.Enums;

namespace DromundKaasII.GameObjects.Actors
{
    public abstract class Actor : IActor
    {

        public int Health { get; set; }

        public Dictionary<StatusEffects, TimeSpan> StatusEffects;

        public virtual void Act()
        {
            throw new NotImplementedException("Not implemented.");
        }
    }
}
