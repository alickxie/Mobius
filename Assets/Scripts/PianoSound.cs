using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoSound : MonoBehaviour
{
    public static float counter = 0f;
    private float temper = 2.7f;

    // Start is called before the first frame update
    void Start()
    {
        // RenderSettings.skybox.SetFloat("_Atmosphere Thickness", Mathf.Sin(Time.time * Mathf.Deg2Rad * 100) + 1);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(this.gameObject.name);
        }

        RenderSettings.skybox.SetFloat("_AtmosphereThickness", counter + 0.15f);


        if (counter <= temper)
        {
            counter += 0.1f;
        }
        else
        {
            counter = 0;
        }
    }
}
