using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonLayout
{
    public Dictionary<Vector3, Space> spaces;
    public Dictionary<int, Vector3> spaceOrder;
    public List<Vector3> doors;
    private List<Vector3> cardinalDirections = new() { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
    public List<Tuple<Space, Space>> linkedSpaces = new List<Tuple<Space, Space>>();

    public DungeonLayout()
    {
        spaces = new Dictionary<Vector3, Space>();
        spaceOrder = new Dictionary<int, Vector3>();
        doors = new List<Vector3>();
    }

    public void GenerateLayout()
    {
        Vector3 pointer = Vector3.zero;
        Space currentSpace = new Space();
        int spacesInRoom = 1;

        currentSpace.centers.Add(pointer);

        for (int spaceCount = Random.Range(10, 15); spaceCount > 0; spaceCount--)
        {
            List<Vector3> openPositions = new List<Vector3>();
            foreach (Vector3 cardinalDirection in cardinalDirections)
            {
                if (!spaces.ContainsKey(pointer + cardinalDirection * 50))
                    openPositions.Add(pointer + cardinalDirection * 50);
            }

            if (openPositions.Count != 0) pointer = openPositions[Random.Range(0, openPositions.Count - 1)];
            else
            {
                pointer = spaces.ElementAt(Random.Range(0, spaces.Keys.Count - 1)).Key;
                continue;
            }

            currentSpace.centers.Add(pointer);


            spacesInRoom--;
            if (spacesInRoom == 0)
            {
                spacesInRoom = 1; //Random.Range(1, 4);

                foreach (Vector3 point in currentSpace.centers)
                {
                    spaces.Add(point, currentSpace);
                }

                if (Random.Range(0, 2) == 0) pointer = spaces.ElementAt(Random.Range(0, spaces.Keys.Count - 1)).Key;

                currentSpace = new Space();
            }
        }
    }

    public void GenerateDoors()
    {
        int count = spaces.Keys.Count;
        while (count > 0)
        {
            foreach (Space space in spaces.Values)
            {
                if (count <= 0) break;
                List<Vector3> spaceContactPoints = ContactPoints(space);

                //Debug.Log(spaceContactPoints[0]);

                if (spaceContactPoints.Count == 0) continue;
                
                count--;
                Vector3 doorPos = spaceContactPoints[Random.Range(0, spaceContactPoints.Count - 1)];

                doorPos -= Vector3.zero;

                doors.Add(doorPos);
                linkedSpaces.Add(LinkedSpaces(doorPos));
            }
        }

    }

    public List<Vector3> ContactPoints(Space space)
    {
        List<Vector3> spaceContactPoints = new List<Vector3>();
        foreach (Vector3 point in space.centers)
        {
            foreach (Vector3 cardinalDirection in cardinalDirections)
            {
                if (spaces.ContainsKey(point + cardinalDirection * 50))
                {
                    Vector3 door = point + cardinalDirection * 25;
                    Tuple<Space, Space> forward = new(spaces[door + cardinalDirection * 25],
                        spaces[door - cardinalDirection * 25]);
                    Tuple<Space, Space> backward = new(forward.Item2, forward.Item1);
                    if (!linkedSpaces.Contains(forward) && !linkedSpaces.Contains(backward))
                    {
                        spaceContactPoints.Add(door);
                    }
                }
            }
        }

        return spaceContactPoints;
    }

    public void PrintLayout()
    {
        float xMin = 0;
        float yMin = 0;
        float zMin = 0;
        float xMax = 0;
        float yMax = 0;
        float zMax = 0;

        string top = "_____";

        foreach (Space space in spaces.Values)
        {
            foreach (Vector3 point in space.centers)
            {
                if (point.x < xMin) xMin = point.x;
                if (point.y < yMin) yMin = point.y;
                if (point.z < zMin) zMin = point.z;
                if (point.x > xMax) xMax = point.x;
                if (point.y > yMax) yMax = point.y;
                if (point.z > zMax) zMax = point.z;
            }
        }


        int i = 0;
        for (float y = yMin; y <= yMax; y += 50)
        {
            Debug.Log("Y level: " + y);
            for (float x = xMin; x <= xMax; x += 50)
            {
                for (int s = 0; s < (5 - (x + "").Length); s++) top += "_";
                top += x;
            }

            Debug.Log(top);
            for (float z = zMin; z <= zMax; z += 50)
            {
                string line = "";
                for (int s = 0; s < (5 - (z + "").Length); s++) line += "_";
                line += z + "_";


                for (float x = xMin; x <= xMax; x += 50)
                {
                    line += (!spaces.ContainsKey(new Vector3(x, y, z)) ? "_____" : "__R__");
                }

                Debug.Log(line);

                i++;
            }
        }
    }

    public Tuple<Space, Space> LinkedSpaces(Vector3 door)
    {
        float x = door.x % 50;
        float y = door.y % 50;
        float z = door.z % 50;

        Vector3 diff = new Vector3(x, y, z);

        return new Tuple<Space, Space>(spaces[door + diff], spaces[door - diff]);
    }
}