using System.Collections.Generic;
using DefaultNamespace.RoomGenerationTypes;
using DungeonObjects;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class RoomDecoration
    {
        public List<ElementTag> tags = new List<ElementTag>();
        public static readonly List<RoomDecoration> roomDecorationTypes = new() {new PillarsInCorners(), new PillarsInMiddle()};
        
        public abstract void Create(DungeonLayout layout, Transform parent, float floorHeight,
            float ceilingHeight, RoomGenerator roomGeneratorSO);
    }
}