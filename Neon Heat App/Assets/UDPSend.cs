
using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;

public class UDPSend : MonoBehaviour
{
    private static int localPort;
    public Text text;

    // prefs
    private string IP;  // define in init
    public int port;  // define in init

    // "connection" things
    IPEndPoint remoteEndPoint;
    UdpClient client;

    // gui
    string strMessage = "";


    // call it from shell (as program)
    private static void Main()
    {
        UDPSend sendObj = new UDPSend();
        sendObj.init();

        // testing via console
        // sendObj.inputFromConsole();

        // as server sending endless
        //sendObj.sendEndless(" endless infos \n");

    }
    // start from unity3d
    public void Start()
    {
        init();
        
    }

    public void Update()
    {
        string s = string.Format("{0:G}", Input.acceleration.x);
        sendString(s);
        
    }

    void onAccuracyChanged(){
    }

    // OnGUI
    void OnGUI()
    {

    }

    // init
    public void init()
    {
        
        print("UDPSend.init()");

        // define
        IP = _Main.ipAdress;
        //Debug.Log("This will be the IP Address " + ipAddress);
        port = 15000;
        

        // ----------------------------
        // Senden
        // ----------------------------
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();

        // status
        print("Sending to " + IP + " : " + port);
        print("Testing: nc -lu " + IP + " : " + port);

        Handheld.Vibrate();
        sendString("MyIP" + " " + Network.player.ipAddress);
    }

    // inputFromConsole
    private void inputFromConsole()
    {
        try
        {
            string text;
            do
            {
                text = Console.ReadLine();

                // Den Text zum Remote-Client senden.
                if (text != "")
                {

                    // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
                    byte[] data = Encoding.UTF8.GetBytes(text);

                    // Den Text zum Remote-Client senden.
                    client.Send(data, data.Length, remoteEndPoint);
                }
            } while (text != "");
        }
        catch (Exception err)
        {
            print(err.ToString());
        }

    }

    // sendData
    public void sendString(string message)
    {
        try
        {
            //if (message != "")
            //{

            // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
            byte[] data = Encoding.UTF8.GetBytes(message);

            // Den message zum Remote-Client senden.
            client.Send(data, data.Length, remoteEndPoint);
            //}
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }


    // endless test
    private void sendEndless()
    {
        do
        {
            string s = string.Format("{0:G}", Input.acceleration.x);
            sendString(s);


        }
        while (true);

    }

}
