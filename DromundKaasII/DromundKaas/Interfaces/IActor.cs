using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Engine;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Interfaces
{
    public interface IActor
    {
        Vector2 MapPosition { get; set; }
    }
}
