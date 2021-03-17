using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlacementReticule : MonoBehaviour
{
    #region Variables
    [Header("Scene References")]
    [SerializeField]
    [Tooltip("Reference to the plane manager in the scene")]
    private ARPlaneManager planeManager;

    [SerializeField]
    [Tooltip("Reference to the ray cast manager in the scene")]
    private ARRaycastManager rayCastManager;

    [SerializeField]
    [Tooltip("Reference to the AR Camera Transform")]
    private Transform cameraTransform;

    private GameObject reticle;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    #endregion
    public void Awake()
    {
        planeManager.planesChanged += PlanesChanged;
    }
    private void PlanesChanged(ARPlanesChangedEventArgs arg)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
    //     Vector2 screenCenter = ScreenUtils.GetScreenCenter();
    //     if (rayCastManager.Raycast)
    //     {
            
    //     }
    // }
    public FurnitureConfig selectedFurniture;
    private void RepositionReticle()
    {
        Pose pose = hits[0].pose;

        Vector3 normal = pose.rotation * Vector3.up;

        Vector3 userVector = cameraTransform.position - pose.position;

        if(Vector3.Dot(userVector, normal) >= 0)
        {
            reticle.transform.SetPositionAndRotation(pose.position, pose.rotation);
        }
    }
    public void PlaceFurnitureOnPlance(ARPlane plane)

    {
        if ( selectedFurniture.canBePlaced(plane) && selectedFurniture.fitsOnPlance(plane))
        {
            Instantiate(selectedFurniture.furniturePrefab, reticle.transform);
        }
        else
        {
            Debug.Log("This piece of furniture doesn't fit the given plane");

        }
    }
}
