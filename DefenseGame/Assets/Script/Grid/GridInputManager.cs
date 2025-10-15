using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInputManager : MonoBehaviour
{
    [field: SerializeField]
    Camera SceneCamera;
    Vector3 lastPosition;
    [field: SerializeField]
    LayerMask PlacementLayer;

    public Vector3 GetGridPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = SceneCamera.nearClipPlane;
        Ray ray = SceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, PlacementLayer))
        {
            lastPosition = hit.point;
        }
        return lastPosition;

    }
}
