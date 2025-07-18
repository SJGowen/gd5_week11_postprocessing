using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class WeatherSystem : MonoBehaviour
{
    [Header("Global")]
    public Material globalMaterial;
    public Light sunLight;
    public Material skyboxMaterial;
    public TMP_Text weatherText;
    public GameObject[] flowers;
    public GameObject[] grasses;

    [Header("Winter Assets")]
    public ParticleSystem winterParticleSystem;
    public Volume winterVolume;

    [Header("Rain Assets")]
    public ParticleSystem rainParticleSystem;
    public Volume rainVolume;

    [Header("Autumn Assets")]
    public ParticleSystem autumnParticleSystem;
    public Volume autumnVolume;

    [Header("Summer Assets")]
    public ParticleSystem summerParticleSystem;
    public Volume summerVolume;

    private WeatherType currentWeatherType;

    private void Start()
    {
        Summer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Winter();
        if (Input.GetKeyDown(KeyCode.Alpha2)) Autumn();
        if (Input.GetKeyDown(KeyCode.Alpha3)) Rain();
        if (Input.GetKeyDown(KeyCode.Alpha4)) Summer();
    }

    public void Winter()
    {
        if (currentWeatherType == WeatherType.Winter) return;
        TurnAllActiveWeatherEffectsOff();

        UpdateWeatherText("Winter");

        if (winterParticleSystem != null) winterParticleSystem.Play();
        if (winterVolume != null) winterVolume.gameObject.SetActive(true);

        globalMaterial.SetFloat("_SnowFade", 1.0f);
        globalMaterial.SetFloat("_AutumnFade", 0.4f);
        sunLight.intensity = 0.5f;
        SetVisibilityOfFlowers(false);
        SetVisibilityOfGrasses(false);
        skyboxMaterial.SetFloat("_AtmosphereThickness", 1f);

        skyboxMaterial.SetFloat("_SunSize", 0.07f);
        skyboxMaterial.SetFloat("_SunSizeConvergence", 4f);
        skyboxMaterial.SetFloat("_AtmosphereThickness", 1.2f);
        skyboxMaterial.SetColor("_SkyTint", new Color(0.6f, 0.7f, 0.8f));
        skyboxMaterial.SetColor("_GroundColor", new Color(0.8f, 0.8f, 0.9f));
        skyboxMaterial.SetFloat("_Exposure", 0.6f);

        currentWeatherType = WeatherType.Winter;
    }

    public void Rain()
    {
        if (currentWeatherType == WeatherType.Rain) return;
        TurnAllActiveWeatherEffectsOff();

        UpdateWeatherText("Rain");

        globalMaterial.SetFloat("_SnowFade", 0.0f);
        globalMaterial.SetFloat("_AutumnFade", 0.0f);
        sunLight.intensity = 0.5f;
        SetVisibilityOfFlowers(true);
        SetVisibilityOfGrasses(true);

        skyboxMaterial.SetFloat("_SunSize", 0.08f);
        skyboxMaterial.SetFloat("_SunSizeConvergence", 2.5f);
        skyboxMaterial.SetFloat("_AtmosphereThickness", 1.2f);
        skyboxMaterial.SetColor("_SkyTint", new Color(0.9f, 0.6f, 0.3f));
        skyboxMaterial.SetColor("_GroundColor", new Color(0.6f, 0.8f, 0.5f));
        skyboxMaterial.SetFloat("_Exposure", 1.1f);

        currentWeatherType = WeatherType.Rain;
    }

    public void Autumn()
    {
        if (currentWeatherType == WeatherType.Autumn) return;
        TurnAllActiveWeatherEffectsOff();

        UpdateWeatherText("Autumn");

        globalMaterial.SetFloat("_SnowFade", 0.0f);
        globalMaterial.SetFloat("_AutumnFade", 0.4f);
        sunLight.intensity = 0.8f;
        SetVisibilityOfFlowers(false);
        SetVisibilityOfGrasses(true);

        skyboxMaterial.SetFloat("_SunSize", 0.08f);
        skyboxMaterial.SetFloat("_SunSizeConvergence", 3f);
        skyboxMaterial.SetFloat("_AtmosphereThickness", 1.2f);
        skyboxMaterial.SetColor("_SkyTint", new Color(0.9f, 0.6f, 0.3f));
        skyboxMaterial.SetColor("_GroundColor", new Color(0.4f, 0.3f, 0.2f));
        skyboxMaterial.SetFloat("_Exposure", 0.8f);

        currentWeatherType = WeatherType.Autumn;
    }

    public void Summer()
    {
        if (currentWeatherType == WeatherType.Summer) return;
        TurnAllActiveWeatherEffectsOff();

        UpdateWeatherText("Summer");

        globalMaterial.SetFloat("_SnowFade", 0.0f);
        globalMaterial.SetFloat("_AutumnFade", 0.0f);
        sunLight.intensity = 1.0f;
        SetVisibilityOfFlowers(true);
        SetVisibilityOfGrasses(true);

        skyboxMaterial.SetFloat("_SunSize", 0.09f);
        skyboxMaterial.SetFloat("_SunSizeConvergence", 2f);
        skyboxMaterial.SetFloat("_AtmosphereThickness", 0.5f);
        skyboxMaterial.SetColor("_SkyTint", new Color(0.7f, 0.9f, 1.0f));
        skyboxMaterial.SetColor("_GroundColor", new Color(0.6f, 0.8f, 0.5f));
        skyboxMaterial.SetFloat("_Exposure", 1.3f);

        currentWeatherType = WeatherType.Summer;
    }

    private void UpdateWeatherText(string weather)
    {
        if (weatherText != null)
        {
            weatherText.text = weather;
        }
    }

    private void SetVisibilityOfFlowers(bool isVisible)
    {
        foreach (GameObject flower in flowers)
        {
            if (flower != null)
            {
                flower.SetActive(isVisible);
            }
        }
    }

    private void SetVisibilityOfGrasses(bool isVisible)
    {
        foreach (GameObject grass in grasses)
        {
            if (grass != null)
            {
                grass.SetActive(isVisible);
            }
        }
    }

    private void TurnAllActiveWeatherEffectsOff()
    {
        if (winterParticleSystem != null && winterParticleSystem.isPlaying)
        {
            winterParticleSystem.Stop();
        }
        if (winterVolume != null && winterVolume.isActiveAndEnabled)
        {
            winterVolume.gameObject.SetActive(false);
        }

        if (rainParticleSystem != null && rainParticleSystem.isPlaying)
        {
            rainParticleSystem.Stop();
        }
        if (rainVolume != null && rainVolume.isActiveAndEnabled)
        {
            rainVolume.gameObject.SetActive(false);
        }

        if (autumnParticleSystem != null && autumnParticleSystem.isPlaying)
        {
            autumnParticleSystem.Stop();
        }
        if (autumnVolume != null && autumnVolume.isActiveAndEnabled)
        {
            autumnVolume.gameObject.SetActive(false);
        }

        if (summerParticleSystem != null && summerParticleSystem.isPlaying)
        {
            summerParticleSystem.Stop();
        }
        if (summerVolume != null && summerVolume.isActiveAndEnabled)
        {
            summerVolume.gameObject.SetActive(false);
        }
    }
}

public enum WeatherType
{
    Winter,
    Rain,
    Autumn,
    Summer
}