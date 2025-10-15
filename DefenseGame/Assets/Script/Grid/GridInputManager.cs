using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GridInputManager : MonoBehaviour
{
    [field: SerializeField]
    Camera SceneCamera;
    Vector3 lastPosition;
    [field: SerializeField]
    LayerMask PlacementLayer;

    public event Action OnClicked,OnExit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
    
    public Vector3 GetGridPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = SceneCamera.nearClipPlane;
        Ray ray = SceneCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, PlacementLayer))
        {
            lastPosition = hit.point;
        }
        return lastPosition;

    }
}
