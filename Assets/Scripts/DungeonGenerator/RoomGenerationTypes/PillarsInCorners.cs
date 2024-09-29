using System.Collections.Generic;
using DungeonObjects;
using UnityEngine;

namespace DefaultNamespace.RoomGenerationTypes
{
    public class PillarsInCorners : RoomDecoration
    {
        public override void Create(DungeonLayout layout, Transform parent, float floorHeight, float ceilingHeight, RoomGenerator roomGeneratorSO)
        {
            List<RoomElement> pillars = roomGeneratorSO.GetElementsWithTag(ElementTag.Pillar);
            Pillar pillar = (Pillar) pillars[Random.Range(0, pillars.Count)];

            Vector3 pos = new Vector3(-15, floorHeight, -15);

            foreach (Vector3 dir in RoomGenerator.cardinalDirections)
            {
                Vector3 pillarPos = parent.position + pos;
                Vector3 topPos = pillarPos + new Vector3(0, ceilingHeight - floorHeight, 0);
                
                Pillar obj = GameObject.Instantiate(pillar, pillarPos, Quaternion.identity, parent);
                obj.Anchor(pillarPos, topPos);
                
                pos += dir * 30;
            }
        }
    }
}