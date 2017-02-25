using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedColor : MonoBehaviour {
    public Shader shader;
    Material mat;
    public bool onOff = false;

    // Use this for initialization
    void Start () {
        mat = new Material(shader);
        mat.name = "InvertedColor";
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        if (shader && mat && onOff) {
            Graphics.Blit(src, dst, mat);
        } else {
            Graphics.Blit(src, dst);
        }
    }
}
