﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players.Tier1
{
    class Builder : Tier1
    {
        public Builder(Vector2 MapPosition)
            : base(MapPosition)
        {

        }

        public Builder(Primal candidate)
            : base(candidate)
        {

        }
    }
}
