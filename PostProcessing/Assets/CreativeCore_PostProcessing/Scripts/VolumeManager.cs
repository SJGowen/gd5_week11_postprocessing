using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
    public Volume globalVolume;
    ColorAdjustments colorAdjustments;
    float postExposure;
    DepthOfField depthOfField;
    float aperture;
    Bloom bloom;
    float intensity;
    float threshold;

    void Start()
    {
        if (globalVolume.profile.TryGet(out colorAdjustments))
        {
            postExposure = colorAdjustments.postExposure.value;
        }

        if (globalVolume.profile.TryGet(out depthOfField))
        {
            aperture = depthOfField.aperture.value;
        }

        if (globalVolume.profile.TryGet(out bloom))
        {
            intensity = bloom.intensity.value;
            threshold = bloom.threshold.value;
        }
    }

    void Update()
    {
        // globalVolume.priority = 5;
        // globalVolume.weight = 0;

        bool isSpacePressed = Input.GetKey(KeyCode.Space);

        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = isSpacePressed ? 0.5f : postExposure;
        }

        if (depthOfField != null)
        {
            depthOfField.aperture.value = isSpacePressed ? 10f : aperture;
        }

        if (bloom != null)
        {
            //bloom!.active = isSpacePressed ? false : true;
            bloom.threshold.value = isSpacePressed ? 5f : threshold;

            if (Input.GetKeyDown(KeyCode.R))
            {
                bloom.intensity.value = 0f;
                Debug.Log($"Setting Bloom intensity value to {bloom.intensity.value}.");
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                bloom.intensity.value = intensity;
                Debug.Log($"Setting Bloom intensity value to {bloom.intensity.value}.");

            }
        }
    }
}