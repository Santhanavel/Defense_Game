using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementSystem : MonoBehaviour
{

    [field: SerializeField]
    GameObject mouseIndicator , cellIndicator;
    [field: SerializeField]
    GridInputManager inputManager;
    [field: SerializeField]
    Grid grid;


    private void Update()
    {

        Vector3 mousePosition = inputManager.GetGridPosition();
        Vector3Int cellPosition = grid.WorldToCell(mousePosition);

        mouseIndicator.transform.position = mousePosition;

        Vector3 cellCenter = grid.GetCellCenterWorld(cellPosition);
        cellCenter.y = grid.transform.position.y; // or 0f if flat on ground
        cellIndicator.transform.position = cellCenter;
    }
}
