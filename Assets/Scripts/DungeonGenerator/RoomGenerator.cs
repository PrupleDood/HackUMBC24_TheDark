using System.Collections.Generic;
using DungeonObjects;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu]
    public class RoomGenerator : ScriptableObject
    {
        public static readonly List<Vector3> cardinalDirections = new() { Vector3.right, Vector3.forward, Vector3.left, Vector3.back };
        public List<RoomElement> elements;

        public void OnEnable()
        {
            elements = new List<RoomElement>();
            elements = new List<RoomElement>(Resources.LoadAll<RoomElement>(""));
        }
        
        public void GenerateRoom(DungeonLayout layout, Transform parent, float floorHeight, float ceilingHeight, List<RoomDecoration> decorations)
        {
            List<RoomElement> walls = GetElementsWithTag(ElementTag.Wall);
            List<RoomElement> doorWalls = GetElementsWithTag(ElementTag.DoorWall);
            List<RoomElement> ceilings = GetElementsWithTag(ElementTag.Ceiling);
            List<RoomElement> floors = GetElementsWithTag(ElementTag.Floor);
            
            StructuralElement wall = (StructuralElement) walls[Random.Range(0, walls.Count)];
            StructuralElement doorWall = (StructuralElement) doorWalls[Random.Range(0, doorWalls.Count)];
            StructuralElement ceiling = (StructuralElement) ceilings[Random.Range(0, ceilings.Count)];
            StructuralElement floor = (StructuralElement) floors[Random.Range(0, floors.Count)];
            
            int degrees = -90;
            foreach (Vector3 direction in cardinalDirections)
            {
                Vector3 newPos = parent.position + direction * 25;
                
                Instantiate(layout.doors.Contains(newPos) ? doorWall : wall, newPos, Quaternion.Euler(90, degrees, 0), parent);
                
                degrees -= 90;
                
                wall = (StructuralElement) walls[Random.Range(0, walls.Count)];
                doorWall = (StructuralElement) doorWalls[Random.Range(0, doorWalls.Count)];
                ceiling = (StructuralElement) ceilings[Random.Range(0, ceilings.Count)];
                floor = (StructuralElement) floors[Random.Range(0, floors.Count)];
            }
            
            Instantiate(ceiling, parent.position + new Vector3(0, ceilingHeight, 0), Quaternion.Euler(0, 0, 180), parent);
            Instantiate(floor, parent.position + new Vector3(0, floorHeight, 0), Quaternion.Euler(0, 0, 0), parent);
            
            foreach (RoomDecoration decorationAddition in decorations)
            {
                if (ceiling.tags.Contains(ElementTag.TakesUpMiddle) && decorationAddition.tags.Contains(ElementTag.TakesUpMiddle)) continue;
                decorationAddition.Create(layout, parent, floorHeight, ceilingHeight, this);
            }
            
        }

        public List<RoomElement> GetElementsWithTag(ElementTag tag)
        {
            List<RoomElement> withTag = new List<RoomElement>();
            foreach (RoomElement element in elements)
            {
                if (element.tags.Contains(tag)) withTag.Add(element);
            }

            return withTag;
        }
    }
}