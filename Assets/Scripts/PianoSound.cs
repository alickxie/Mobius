using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoSound : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // lightingManager = GameObject.Find("LightManager").GetComponent<LightingManager>();
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
    }
}
