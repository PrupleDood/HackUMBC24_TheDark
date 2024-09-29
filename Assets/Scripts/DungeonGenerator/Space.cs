using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Space
{

    public int size = 50;

    public List<Vector3> centers;

    public List<Space> doorsJoinedTo = new();

    public int id;

    public Space()
    {
        centers = new List<Vector3>();
        id = Random.Range(Int32.MinValue, Int32.MaxValue);
    }

    public Space(List<Vector3> centers)
    {
        this.centers = centers;
        id = Random.Range(Int32.MinValue, Int32.MaxValue);
    }

    public List<Vector3> ContactPoints(Space other)
    {
        if (other.size == size) throw new Exception("The space sizes do not match!");
        if (other.id == id) throw new Exception("The spaces are the same!");
        
        List<Vector3> cPoints = new List<Vector3>();

        foreach (Vector3 p1 in centers)
        {
            foreach (Vector3 p2 in other.centers)
            {
                Vector3 diff = p2 - p1;
                if ((diff).magnitude == size) cPoints.Add(diff * 0.5f + p1);
                
            }
        }

        return cPoints;
    }

}
