using UnityEngine;

public class LightingManager : MonoBehaviour
{
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    private GameObject DayBGM;
    private GameObject NightBGM; 
    public GameObject RainParticle;

    // public static float counter = 0f;
    // private float temper = 2.7f;

    private void Start()
    {
        DayBGM = GameObject.Find("DayBGM");
        DayBGM.SetActive(false);
        NightBGM = GameObject.Find("NightBGM");
        RainParticle = GameObject.Find("RainParticle");
    }

    private void Update()
    {
        if (Preset == null)
            return;

        // if (Application.isPlaying)
        // {
        //     //(Replace with a reference to the game time)
        //     TimeOfDay += Time.deltaTime;
        //     TimeOfDay %= 24; //Modulus to ensure always between 0-24
        //     UpdateLighting(TimeOfDay / 24f);
        // }
        // else
        // {
        //     UpdateLighting(TimeOfDay / 24f);
        // }
    }

    public void SunRotate()
    {
        //(Replace with a reference to the game time)
        TimeOfDay += Time.deltaTime;
        TimeOfDay %= 24; //Modulus to ensure always between 0-24
        UpdateLighting(TimeOfDay / 24f);

        // RenderSettings.skybox.SetFloat("_AtmosphereThickness", counter + 0.15f);

        // if (counter <= temper)
        // {
        //     counter += 0.1f;
        // }
        // else
        // {
        //     counter = 0;
        // }

        if (TimeOfDay > 6f && TimeOfDay < 18f)
        {
            DayBGM.SetActive(true);
            NightBGM.SetActive(false);
            RainParticle.SetActive(false);
        }
        else
        {
            DayBGM.SetActive(false);
            NightBGM.SetActive(true);
            RainParticle.SetActive(true);
        }
    }

    // public void SkyboxUpdate()
    // {
    //     RenderSettings.skybox.SetFloat("_AtmosphereThickness", counter + 0.15f);

    //     if (counter <= temper)
    //     {
    //         counter += 0.1f;
    //     }
    //     else
    //     {
    //         counter = 0;
    //     }
    // }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            // DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
            DirectionalLight.transform.RotateAround(Vector3.zero, new Vector3(20, 8, 0), (-15f*Time.deltaTime));
        }
    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
