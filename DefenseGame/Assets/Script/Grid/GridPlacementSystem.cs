using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementSystem : MonoBehaviour
{

    [field: SerializeField]
    GridInputManager inputManager;
    [field: SerializeField]
    Grid grid;
    [field: SerializeField]
    GridObjectDatabaseSO databaseSO;

    [SerializeField]
    private GameObject gridVisualization;

    private GridData FloorData, WeponData;

    [SerializeField]
    PlacementObjectVisual placementObjectVisual;

    Vector3Int lastDetectedPosition = Vector3Int.zero;
    [SerializeField]
    ObjectPlacer objectPlacer;

    IBuildingState buildingState;
    private void Start()
    {
        StopPlacement();
        FloorData = new();
        WeponData = new();

    }
    public void StatrtPlacement(int ID)
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID ,grid,databaseSO,FloorData,WeponData,placementObjectVisual,objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
;
    }

    private void PlaceStructure()
    {
        if(inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetGridPosition();
        Vector3Int cellPosition = grid.WorldToCell(mousePosition);
        buildingState.OnAction(cellPosition);
    }
    void StopPlacement()
    {
        if (buildingState == null)
            return;
        buildingState.EndState();
        gridVisualization.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    private void Update()
    {
        if (buildingState == null)
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetGridPosition();
        Vector3Int cellPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != cellPosition)
        {
            buildingState.UpdateState(cellPosition);
            lastDetectedPosition = cellPosition;
        }

    }
}
