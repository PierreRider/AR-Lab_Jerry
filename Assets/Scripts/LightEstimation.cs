using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
    [SerializeField]
    private Light light;

    [SerializeField]
    private ARCameraManager cameraManager;

    [SerializeField]
    private float brightnessMultiplier = 2;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void OnEnable()
    {
        cameraManager.frameReceived += OnFrameReceived;
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= OnFrameReceived;
    }

    private void OnFrameReceived(ARCameraFrameEventArgs frameData)
    {

        ARLightEstimationData lightData = frameData.lightEstimation;

        if (frameData.lightEstimation.averageBrightness.HasValue)
        {
            light.intensity = lightData.averageBrightness.Value * brightnessMultiplier;
        }

        if (lightData.averageColorTemperature.HasValue)
        {
            light.colorTemperature = lightData.averageColorTemperature.Value;
            light.useColorTemperature = true;
        }

        if (lightData.mainLightColor.HasValue)
        {
            light.color = lightData.mainLightColor.Value;
        }

        if (lightData.mainLightDirection.HasValue)
        {
            light.transform.rotation = Quaternion.LookRotation(lightData.mainLightDirection.Value);
        }

        if (lightData.mainLightIntensityLumens.HasValue)
        {
            light.intensity = lightData.mainLightIntensityLumens.Value;
        }

        if (lightData.ambientSphericalHarmonics.HasValue)
        {
            RenderSettings.ambientMode = AmbientMode.Skybox;
            RenderSettings.ambientProbe = lightData.ambientSphericalHarmonics.Value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
