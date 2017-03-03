using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _Main : MonoBehaviour {

    public static string ipAdress;
    private InputField input;
	// Use this for initialization
	void Start () {
        input = GameObject.Find("InputField").GetComponent<InputField>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void connect() {
        //UDPSend sendObj = new UDPSend();
       // sendObj.init(ipAdress);
        SceneManager.LoadScene("playing");
    }

    public void sendIp(){
        UDPSend sendObj = new UDPSend();
        sendObj.init();
        sendObj.sendString("MyIP" + " " + Network.player.ipAddress);
        GameObject.Destroy(sendObj);
    }

    public void getInput(string inputText) {
        ipAdress = inputText;
    }
}
