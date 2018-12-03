using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSceneController : MonoBehaviour {
    public Light spotlight;
    public Light directionalLight;
    public float turnOnLight;
    public Renderer lightRend;
    public GameObject canvas;
    private Material mat;
    private Color litColor;
    private float lightIntensity;

    private void Start() {
        mat = lightRend.material;
        litColor = new Color(1, 0.7764434f, 0.1839623f);
        mat.SetColor("_EmissionColor", Color.black);

        lightIntensity = directionalLight.intensity;
        turnOnLight += Time.time;
    }
    // Update is called once per frame
    void Update () {
        if(turnOnLight < Time.time) {
            spotlight.intensity = 45f;
            directionalLight.intensity = lightIntensity; 
            mat.SetColor("_EmissionColor", litColor);

            if (!canvas.activeSelf) {
                canvas.SetActive(true);
            }

        }
        if (spotlight.intensity > 0f && Random.Range(0f,1f) > 0.99f) {
            spotlight.intensity = 0f;
            mat.SetColor("_EmissionColor", Color.black);
            turnOnLight = Time.time + Random.Range(0.1f, 0.3f);
        }
	}
}
