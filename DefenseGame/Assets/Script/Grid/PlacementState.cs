using UnityEngine;

public class PlacementState : IBuildingState
{


    int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    GridObjectDatabaseSO databaseSO;
    GridData FloorData;
    GridData WeponData;
    PlacementObjectVisual placementObjectVisual;
    ObjectPlacer objectPlacer;

    public PlacementState(int iD,
                          Grid grid,
                          GridObjectDatabaseSO databaseSO,
                          GridData floorData,
                          GridData weponData,
                          PlacementObjectVisual placementObjectVisual,
                          ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.databaseSO = databaseSO;
        FloorData = floorData;
        WeponData = weponData;
        this.placementObjectVisual = placementObjectVisual;
        this.objectPlacer = objectPlacer;


        selectedObjectIndex = databaseSO.objectData.FindIndex(data => data.ID == ID);

        if (selectedObjectIndex > -1)
        {
            placementObjectVisual.ShowPreview(databaseSO.objectData[selectedObjectIndex].prefab, databaseSO.objectData[selectedObjectIndex].Size);
        }
        else
        {
            throw new System.Exception($"NO ID found {iD}");
        }

    }

    public void EndState()
    {
        placementObjectVisual.HidePreview();
    }
    public void OnAction(Vector3Int cellPosition)
    {
        bool canPlace = CheckPlacementValidity(cellPosition, selectedObjectIndex);
        if (!canPlace)
            return;
        Vector3 cellCenter = grid.GetCellCenterWorld(cellPosition);
        cellCenter.y = grid.transform.position.y; // or 0f if flat on ground
        int index = objectPlacer.objectPlacer(databaseSO.objectData[selectedObjectIndex].prefab, cellCenter);
        GridData selectedData = databaseSO.objectData[selectedObjectIndex].ID == 0 ? FloorData : WeponData;
        selectedData.AddObjectAt(cellPosition, databaseSO.objectData[selectedObjectIndex].Size, databaseSO.objectData[selectedObjectIndex].ID, index);

        placementObjectVisual.UpdatePosition(cellCenter, false);
    }
    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = databaseSO.objectData[selectedObjectIndex].ID == 0 ? FloorData : WeponData;
        return selectedData.CanPlaceObjectAt(gridPosition, databaseSO.objectData[selectedObjectIndex].Size);
    }

    public void UpdateState(Vector3Int cellPosition)
    {

        bool canPlace = CheckPlacementValidity(cellPosition, selectedObjectIndex);
        Vector3 cellCenter = grid.GetCellCenterWorld(cellPosition);
        cellCenter.y = grid.transform.position.y; // or 0f if flat on ground

        placementObjectVisual.UpdatePosition(cellCenter, canPlace);
    }
}
