using System.Collections.Generic;
using System.Diagnostics.Tracing;
using DungeonObjects;
using UnityEngine;

namespace DefaultNamespace.RoomGenerationTypes
{
    public class PillarsInMiddle : RoomDecoration
    {
        public PillarsInMiddle()
        {
            tags.Add(ElementTag.TakesUpMiddle);
        }
        
        public override void Create(DungeonLayout layout, Transform parent, float floorHeight, float ceilingHeight,
            RoomGenerator roomGeneratorSO)
        {
            List<RoomElement> pillars = roomGeneratorSO.GetElementsWithTag(ElementTag.Pillar);
            Pillar pillar = (Pillar) pillars[Random.Range(0, pillars.Count)];
            
            Vector3 pillarPos = parent.position + new Vector3(0, floorHeight, 0);
            Vector3 topPos = pillarPos + new Vector3(0, ceilingHeight - floorHeight, 0);
                
            Pillar obj = GameObject.Instantiate(pillar, pillarPos, Quaternion.identity, parent);
            obj.Anchor(pillarPos, topPos);
        }
    }
}