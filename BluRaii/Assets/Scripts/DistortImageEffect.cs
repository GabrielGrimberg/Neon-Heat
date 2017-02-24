using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortImageEffect : MonoBehaviour {
    Material mat;
    public Texture noiseTex;
    public Shader shader;
    public float offsetColor = 0.001f;

    // Use this for initialization
    void Start () {
        mat = new Material(shader);
        mat.SetTexture("_SecondaryTex", noiseTex);
        mat.name = "ImageEffectMaterial";
	}
	
	// Update is called once per frame
	void Update () {
        offsetColor = Mathf.Lerp(offsetColor, 0.001f, Time.deltaTime / 0.1f);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        mat.SetFloat("_OffsetNoiseX", Random.Range(0f, 0.6f));
        float offsetNoise = mat.GetFloat("_OffsetNoiseY");
        mat.SetFloat("_OffsetNoiseY", offsetNoise + Random.Range(-0.03f, 0.03f));

        // Vertical shift
        float offsetPosY = mat.GetFloat("_OffsetPosY");
        if (offsetPosY > 0.0f) {
            mat.SetFloat("_OffsetPosY", offsetPosY - Random.Range(0f, offsetPosY));
        } else if (offsetPosY < 0.0f) {
            mat.SetFloat("_OffsetPosY", offsetPosY + Random.Range(0f, -offsetPosY));
        } else if (Random.Range(0, 150) == 1) {
            mat.SetFloat("_OffsetPosY", Random.Range(-0.5f, 0.5f));
        }

        mat.SetFloat("_OffsetColor", offsetColor);

        // Channel color shift
        /*
        float offsetColor = mat.GetFloat("_OffsetColor");
        if (offsetColor > 0.001f) {
            mat.SetFloat("_OffsetColor", offsetColor - 0.001f);
        } else if (Random.Range(0, 400) == 1) {
            mat.SetFloat("_OffsetColor", Random.Range(0.003f, 0.1f));
        }
        */
        

        // Distortion
        /*
        if (Random.Range(0, 15) == 1) {
            mat.SetFloat("_OffsetDistortion", Random.Range(1f, 480f));
        } else {
            mat.SetFloat("_OffsetDistortion", 480f);
        }
        */

        if (shader && mat) {
            Graphics.Blit(src, dst, mat);
        } else {
            Graphics.Blit(src, dst);
        }
    }

	public void Quake() {
		offsetColor =  Random.Range(0.1f, 0.3f);

		mat.SetFloat("_OffsetDirectionX", Random.Range(-1, 1.0f));
		mat.SetFloat("_OffsetDirectionY", Random.Range(-1, 1.0f));
	}
}
