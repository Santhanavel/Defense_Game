using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementObjectVisual : MonoBehaviour
{
    [SerializeField]
    float previewYOffset = 0.05f;

    private GameObject ObjectPreview;
    [SerializeField]
    private GameObject cellIndicator;

    [SerializeField]
    Material previewMaterial;
   public Material previewMaterialInstance;
    Renderer CellIndicatorRendrer; 
    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterial);
        cellIndicator.SetActive(false);
        CellIndicatorRendrer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void ShowPreview(GameObject prefab, Vector2Int size)
    {
        if (ObjectPreview != null)
        {
            Destroy(ObjectPreview);
        }
        ObjectPreview = Instantiate(prefab);
        PreparePreavie(ObjectPreview);
        PrepareCursor(size);


        cellIndicator.SetActive(true);

    }

    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
          //  CellIndicatorRendrer.material.mainTextureScale = size;
        }

    }

    private void PreparePreavie(GameObject objectPreview)
    {
        Renderer[] renderers = objectPreview.GetComponentsInChildren<Renderer>();
        foreach (Renderer item in renderers)
        {
            Material[] materials = item.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            item.materials = materials;
        }
    }

    public void HidePreview()
    {

        cellIndicator.SetActive(false);
        if (ObjectPreview != null)
        {
            Destroy(ObjectPreview);
        }
    }

    public void UpdatePosition(Vector3 position,bool validity)
    {

        MovePreview(position);
        MoveCurser(position);
        ApplyFeedback(validity);


    }

    private void ApplyFeedback(bool validity)
    {
        Color color = validity ? Color.white : Color.red;
        CellIndicatorRendrer.material.color = color;
        color.a = 0.5f;
        previewMaterialInstance.color = color;
    }

    private void MoveCurser(Vector3 position)
    {
       cellIndicator.transform.position = position; 

    }

    private void MovePreview(Vector3 position)
    {
        ObjectPreview.transform.position = new Vector3(position.x, position.y + previewYOffset, position.z);
    }
}
