using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.GameObjects.Enums;

namespace DromundKaasII.Interfaces
{
    public interface IEngineOptions
    {
        GameSpeedOptions GameSpeed { get; set; }
        GameDifficultyOptions GameDifficulty { get; set; }
    }
}
