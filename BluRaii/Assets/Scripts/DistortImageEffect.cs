using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortImageEffect : MonoBehaviour {
    Material mat;
    public Shader shader;
    
    // Use this for initialization
    void Start () {
        mat = new Material(shader);
        mat.name = "ImageEffectMaterial";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        if (shader && mat) {
            Graphics.Blit(src, dst, mat);
        } else {
            Graphics.Blit(src, dst);
        }
    }
}
