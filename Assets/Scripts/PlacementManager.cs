using System;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject basicTower;
    private GameObject dummyPlacement;
    public Camera cam;
    private GameObject hoverTile;
    public LayerMask tileLayer;
    public LayerMask towerLayer;
    public bool isBuilding = false;

    private void Start()
    {
        StartBuilding();
    }

    public Vector2 GetMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void GetCurrentHoverTile()
    {
        Vector2 mousePosition = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0.1f, tileLayer, -100, 100);
        if (hit.collider != null)
        {
            GameObject hitter = hit.collider.gameObject;
            if (mapGenerator.mapTiles.Contains(hitter))
            {
                if (!mapGenerator.pathTiles.Contains(hitter))
                {
                    hoverTile = hit.collider.gameObject;
                }
            }
        }
    }

    public bool CheckForTower()
    {
        bool towerOnSpot = false;
        Vector2 mousePosition = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0.1f, towerLayer, -100, 100);
        if (hit.collider)
        {
            towerOnSpot = true;
        }

        return towerOnSpot;
    }

    public void PlaceBuilding()
    {
        if (hoverTile)
        {
            if (CheckForTower() == false)
            {
                GameObject newTower = Instantiate(basicTower);
                newTower.layer = towerLayer;
                newTower.transform.position = hoverTile.transform.position;

                EndBuilding();
            }
        }
    }

    public void StartBuilding()
    {
        isBuilding = true;
        dummyPlacement = Instantiate(basicTower);
        if (dummyPlacement.GetComponent<Tower>() != null)
        {
            Destroy(dummyPlacement.GetComponent<Tower>());
        }

        if (dummyPlacement.GetComponent<BarrelRotation>() != null)
        {
            Destroy(dummyPlacement.GetComponent<BarrelRotation>());
        }
    }

    public void EndBuilding()
    {
        isBuilding = false;
        if (dummyPlacement)
        {
            Destroy(dummyPlacement);
        }
    }

    private void Update()
    {
        if (isBuilding && dummyPlacement)
        {
            GetCurrentHoverTile();
            if (hoverTile)
            {
                dummyPlacement.transform.position = hoverTile.transform.position;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                PlaceBuilding();
            }
        }
    }
}