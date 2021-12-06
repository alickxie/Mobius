using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> keys;
    private float offset = 15.22f;

    // Start is called before the first frame update
    void Start()
    {
        if (keys != null && keys.Count > 0)
        {
            keys = keys.OrderBy(k => -k.transform.position.x).ToList();
        }
    }

    public void MoveRoad()
    {
        GameObject movedRoad = keys[0];
        keys.Remove(movedRoad);
        float newX = keys[keys.Count - 1].transform.position.x - offset;
        movedRoad.transform.position = new Vector3(newX, 0, 0);
        keys.Add(movedRoad);
    }
}
