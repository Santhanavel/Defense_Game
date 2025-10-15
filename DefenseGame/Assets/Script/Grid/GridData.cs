using System;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new Dictionary<Vector3Int, PlacementData>();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int iD, int placedObjectIndex)
    {
        List<Vector3Int> occupyPositions = GetOccupyPositions(gridPosition, objectSize);

        PlacementData placementData = new PlacementData(occupyPositions, iD, placedObjectIndex);
        foreach (var pos in occupyPositions)
        {
            if (placedObjects.ContainsKey(pos))
                throw new Exception($"Position {pos} is already occupied by object ID {placedObjects[pos].ID}");
            placedObjects[pos] = placementData;
        }
    }

    private List<Vector3Int> GetOccupyPositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnValue = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                Vector3Int position = new Vector3Int(gridPosition.x + x, gridPosition.y, gridPosition.z + y);
                returnValue.Add(position);
            }
        }
        return returnValue;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> occupyPositions = GetOccupyPositions(gridPosition, objectSize);
        foreach (var pos in occupyPositions)
        {
            if (placedObjects.ContainsKey(pos))
                return false;
        }
        return true;
    }
}
public class PlacementData
{
    public List<Vector3Int> occupiedPositions = new List<Vector3Int>();
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlacedObjectIndex = placedObjectIndex;
    }
}