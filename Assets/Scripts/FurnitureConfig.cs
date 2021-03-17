using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum FurniturePlacementType
{
    Floor,
    Ceiling, 
    Wall
}
[CreateAssetMenu(fileName = "NewFurniture.asset", menuName = "Configs/Furniture")]
public class FurnitureConfig : ScriptableObject
{
    public FurniturePlacementType type;
    public string displayName;
    public int id;
    public int price;
    public GameObject furniturePrefab;
    public Vector2 minPlaneSize;

    public bool canBePlaced(ARPlane plane)

    {
            bool canPlace = false;
            switch (plane.alignment)
            {
                case PlaneAlignment.HorizontalUp:
                    canPlace = type == FurniturePlacementType.Ceiling;
                    break;
                case PlaneAlignment.HorizontalDown:
                    canPlace = type == FurniturePlacementType.Ceiling;
                    break;
                case PlaneAlignment.Vertical:
                    canPlace = type == FurniturePlacementType.Wall;
                    break;
            }

            return canPlace;
    }

    public bool fitsOnPlance (ARPlane plane)
    {
        if (minPlaneSize.x <= plane.size.x && minPlaneSize.y <= plane.size.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}