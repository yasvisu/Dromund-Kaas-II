using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects
{
    public class Illuminator : IIlluminator
    {
        public Illuminator(Color IlluminationColor, float IlluminationRange, Vector2 MapPosition)
        {
            this.IlluminationColor = IlluminationColor;
            this.IlluminationRange = IlluminationRange;
            this.MapPosition = MapPosition;
        }

        public Illuminator(IIlluminator I)
            : this(I.IlluminationColor, I.IlluminationRange, I.MapPosition)
        { }

        public Color IlluminationColor { get; set; }

        public float IlluminationRange { get; set; }

        public bool HasIlluminated { get; set; }

        public Vector2 MapPosition { get; set; }
    }
}
