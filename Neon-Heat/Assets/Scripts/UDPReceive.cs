using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour {

	// receiving Thread
	Thread receiveThread;

	public GameObject sendRef;
	UDPSend udpSendRef;

	// udpclient object
	UdpClient client;

	// public
	public string IP = "127.0.0.1";
	public int port; // define > init

	// infos
	public static string lastReceivedUDPPacket="";
	public string allReceivedUDPPackets=""; // clean up this from time to time!

	public bool phoneIP = false;
	private string phoneIpAddress;


	// start from shell
	private static void Main()
	{
		UDPReceive receiveObj=new UDPReceive();
		receiveObj.init();

		string text="";
		do
		{
			text = Console.ReadLine();
		}
		while(!text.Equals("exit"));
	}
	// start from unity3d
	public void Start()
	{
		udpSendRef = sendRef.GetComponent<UDPSend> ();
		init();
	}
		
	void Update()
	{
		if (phoneIP) {
			string[] splitString = phoneIpAddress.Split(new string[] { " " }, StringSplitOptions.None);
			udpSendRef.init(splitString[1]);
			Debug.Log ("Assigned IP");
			phoneIP = false;
		}
	}
	// init
	private void init()
	{
		// Endpunkt definieren, von dem die Nachrichten gesendet werden.
		print("UDPSend.init()");

		// define port
		port = 15000;

		// status
		print("Sending to 127.0.0.1 : "+port);
		print("Test-Sending to this Port: nc -u 127.0.0.1  "+port+"");


		// ----------------------------
		// Abhören
		// ----------------------------
		// Lokalen Endpunkt definieren (wo Nachrichten empfangen werden).
		// Einen neuen Thread für den Empfang eingehender Nachrichten erstellen.
		receiveThread = new Thread(
			new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();

	}

	// receive thread
	private  void ReceiveData()
	{

		client = new UdpClient(port);
		while (true)
		{

			try
			{
				// Bytes empfangen.
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
				byte[] data = client.Receive(ref anyIP);

				// Bytes mit der UTF8-Kodierung in das Textformat kodieren.
				string text = Encoding.UTF8.GetString(data);

				// Den abgerufenen Text anzeigen.
				//print(">> " + text);

				if(text.Contains("MyIP")){
					phoneIpAddress = text;
					phoneIP = true;
				}

				lastReceivedUDPPacket=text;
				//Debug.Log(text);
				// ....
				allReceivedUDPPackets=allReceivedUDPPackets+text;

			}
			catch (Exception err)
			{
				print(err.ToString());
			}

			Thread.Sleep (1);
		}
	}

	void OnApplicationQuit() {
		receiveThread.Abort();
		client.Close();
	}

	// getLatestUDPPacket
	// cleans up the rest
	public string getLatestUDPPacket()
	{
		allReceivedUDPPackets="";
		return lastReceivedUDPPacket;
	}
}