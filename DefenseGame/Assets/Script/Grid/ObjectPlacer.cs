using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{

    List<GameObject> placedObject = new();

    public int objectPlacer(GameObject prefab , Vector3 position)
    {
        GameObject objectSpown = Instantiate(prefab);
        objectSpown.transform.position = position;
        placedObject.Add(objectSpown);

        return placedObject.Count - 1;
    }

  
}
