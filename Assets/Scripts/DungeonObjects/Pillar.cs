using System;
using UnityEngine;

namespace DungeonObjects
{
    public class Pillar : DetailElement
    {
        public override void Anchor(Vector3 a, Vector3 b)
        {
            float ySize = 0;
            foreach (MeshRenderer r in GetComponentsInChildren<MeshRenderer>())
            {
                ySize += r.bounds.size.y;
            }

            transform.position = a;
            transform.localScale = new Vector3(0.02f, (a - b).y * (-0.02f / ySize), 0.02f);
        }
    }
}