using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.RoomGenerationTypes;
using UnityEngine;

namespace DefaultNamespace
{
    public class DungeonGenerator : MonoBehaviour
    {
        public DungeonLayout layout;

        public RoomGenerator RoomGeneratorSO;

        public GameObject roomObj;
        public GameObject doorObj;

        public Transform roomParent;
        public Transform doorParent;

        void Start()
        {
            layout = new DungeonLayout();
            layout.GenerateLayout();
            layout.GenerateDoors();
            //layout.PrintLayout();

            CreateRooms();
            //CreateDoors(); It don't work!
        }
        
        
        public void CreateRooms()
        {
            foreach (Vector3 pos in layout.spaces.Keys)
            {
                List<RoomDecoration> decorations = new List<RoomDecoration>();

                if (pos == layout.spaces.ElementAt(layout.spaces.Count - 1).Key) decorations.Add(new EndPillar());
                else if (pos != Vector3.zero) 
                {
                    int deco = Random.Range(0, 3);
                    if (deco != 0)
                    {
                        deco--;
                        decorations.Add(RoomDecoration.roomDecorationTypes[deco]);
                    }
                }

                GameObject parent = Instantiate(roomObj, pos, Quaternion.identity, roomParent);
                RoomGeneratorSO.GenerateRoom(layout, parent.transform, -10, Random.Range(15, 25), decorations);
            }
        }

        public void CreateDoors()
        {
            foreach (Vector3 doorPos in layout.doors)
            {
                float rotation = 0;
                float x = doorPos.x % 50;
                float z = doorPos.z % 50;
                if (x > 0)
                {
                    rotation = 90;
                }
                
                Instantiate(doorObj, doorPos, Quaternion.Euler(0, rotation, 0), doorParent);
            }
        }
    }
}