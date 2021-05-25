using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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
    [Tooltip("Prefab with the visual reticle")]
    private GameObject reticlePrefab;

    [SerializeField]
    [Tooltip("Reference to the AR Camera Transform")]
    private Transform cameraTransform;
    //Reference to the instantiated reticle
    private GameObject reticle;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public FurnitureConfig selectedFurniture;

    #endregion

    #region Unity Functions
    public void Awake()
    {
        reticle = Instantiate(reticlePrefab);
        planeManager.planesChanged += PlanesChanged;
    }
    void Update()
    {
        Vector2 screenCenter = ScreenUtils.GetScreenCenter();
        if (rayCastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            //Repositions the reticle
            RepositionReticle();
        }
    }
    private void PlanesChanged(ARPlanesChangedEventArgs arg)
    {
        
    }
#endregion


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

    public void EnableFurniturePreview()
    {
        TrackableId planeId = hits[0].trackableId;
        ARPlane plane = planeManager.trackables[planeId];
        PlaceFurnitureOnPlance(plane);
    }
}
