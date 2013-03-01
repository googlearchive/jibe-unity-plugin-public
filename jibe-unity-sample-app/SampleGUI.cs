/**
* Copyright 2012 Jibe Mobile Inc.
*
*  Permission is hereby granted, free of charge, to any person obtaining a copy of
*  this software and associated documentation files (the "Software"), to deal in
*  the Software without restriction, including without limitation the rights to
*  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
*  the Software, and to permit persons to whom the Software is furnished to do so,
*  subject to the following conditions:
*
*  The above copyright notice and this permission notice shall be included in all
*  copies or substantial portions of the Software.
*
*  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
*  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
*  FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
*  COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
*  IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
*  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*
* The foregoing permission shall not be deemed to grant either directly or by implication, estoppel, 
* or otherwise, any other right, interest or license to or under any intellectual property rights of Jibe Mobile, Inc.
*/

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

//
// This class contains only demo material. But the funtions used are intended to be used in a similar manner 
// for setting up the connection with Jibe and for managing the data transfers.
// 
	
public class SampleGUI : MonoBehaviour {
	
	private const string TAG = "TestGUI";
	
	String phoneNumber = "4151234578";
	String message = "Message to send...";
	
	private bool isArenaInstalled;
	SampleArenaHelper arenaHelper;
	SampleMyProfileHelper myProfileHelper;
	SampleAudioCall audioCall;
	SampleDatagramSocketConnection datagramSocketConnection;
	
	Coroutine dsgReceiveCoroutine;
		
	/// Use this for initialization
	void Awake () {
		// NOTE: Only one SimpleConnectionListener Component per GameObject is recommended, since any other
		// SimpleConnectionListeners would also get any callbacks meant for a single component
		UnityJibeProperties.SetAppCredentials("184175d08e3b4bcaa367fe9629af2a18", "b5232517a8784b5ea0a6975475b80270");
		
		isArenaInstalled = ArenaHelper.isCompatibleRealtimeEngineInstalled();
		if (isArenaInstalled)
		{
			arenaHelper = (new GameObject("JibeSampleArenaHelper", typeof(SampleArenaHelper))).GetComponent<SampleArenaHelper>();		
			myProfileHelper = (new GameObject("JibeSampleMyProfileHelper", typeof(SampleMyProfileHelper))).GetComponent<SampleMyProfileHelper>();
			audioCall = (new GameObject("JibeSampleAudio", typeof(SampleAudioCall))).GetComponent<SampleAudioCall>();
			datagramSocketConnection = (new GameObject("JibeSampleDatagramSocketConnection", 
				typeof(SampleDatagramSocketConnection))).GetComponent<SampleDatagramSocketConnection>();
		}
	}
	
	void Update()
	{
		if (datagramSocketConnection && datagramSocketConnection.connectionEstablished && !datagramSocketConnection.incomingIntent 
			&& dsgReceiveCoroutine == null)
			dsgReceiveCoroutine = StartCoroutine(ReceiveData());
	}
	
	IEnumerator ReceiveData()
	{
		Debug.Log("Starting ReceiveData");
		Coroutine<byte[]> internalReceive = StartCoroutine<byte[]>(datagramSocketConnection.receiveData());
		yield return internalReceive.coroutine;
		string message = System.Text.Encoding.ASCII.GetString(internalReceive.Value);
		if (!string.IsNullOrEmpty(message))
		{
			Debug.Log(message.Length + " " + message);
			datagramSocketConnection.showMessage(message);
		}
		dsgReceiveCoroutine = null;
	}
	
	/// OnGUI is called once per frame
	void OnGUI () 
	{		
//		Debug.Log(TAG+" Start OnGUI");
		float verticalSpacing = 30f;
		float fieldWidth = 120f;
		float fieldHeight = 40f;
		GUILayoutOption[] buttonOptions = new GUILayoutOption[] { GUILayout.Height(fieldHeight) };
		
		if (!isArenaInstalled)
		{
			GUILayout.Label("========= Arena must be installed first =========");
			if (GUILayout.Button("Install Arena", buttonOptions))
			{
				ArenaHelper.openGoolePlayPage();
				Application.Quit();
			}
			return;
		}
		
		///This GUI is just a temporary test of the features of the plugin. Each funtion here is a 
		///function that should be used somewhere in your game for connecting through Jibe
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		
		GUILayout.Label("========= Jibe SDK Tests =========");		
		GUILayout.Space(verticalSpacing);		
		
		GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.Width(fieldWidth), GUILayout.Height(fieldHeight) };
		if(myProfileHelper == null || !myProfileHelper.Helper.isInitialized() || !myProfileHelper.Helper.isOnline())
		{
			GUILayout.Label("User is not logged in");
		}
		else
		{
			GUILayout.Label("Logged in as: " + myProfileHelper.Helper.getDisplayName());
		}
		GUILayout.BeginHorizontal();
		GUILayout.Label("Connect with UserId:");
		if(datagramSocketConnection.connectionEstablished && datagramSocketConnection.PhoneNumber != null)
		{
			phoneNumber = datagramSocketConnection.PhoneNumber;
		}
		phoneNumber = GUILayout.TextField(phoneNumber, GUILayout.Width(fieldWidth));
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.Space(verticalSpacing);
		
		if (GUILayout.Button("Friends List", options)) {			
			if(arenaHelper != null && arenaHelper.Areana.isInitialized())
			{
				//This opens the Jibe Arena Friends list so that the user can select which friend to challenge and play against.
				//if(arenaHelper != null && arenaHelper)
				arenaHelper.openJibeFriendsList();
			}
		}
	
		GUILayout.Space(verticalSpacing);
		GUILayout.Label("Audio Call Test");		
		GUILayout.BeginHorizontal();
		GUI.enabled = !audioCall.connectionOpen && !audioCall.incomingIntent;
		
		
		if(GUILayout.Button("Open", buttonOptions)) 
		{		
			///This opens a connection to the provided phone number using the JibeBundleTransferConnection
			///This function is also called upon return from the friends list so as to start the connection that the user would expect
			audioCall.openConnection(phoneNumber);
		}
	
		GUI.enabled = !audioCall.connectionOpen && audioCall.incomingIntent;
		if(GUILayout.Button("Accept", buttonOptions)) 
		{	
			///This function is used to accept an incoming session
			audioCall.acceptIncomingConnection();
		}
	
		if (GUILayout.Button("Reject", buttonOptions)) {
			///This rejects an incoming session
			audioCall.rejectIncomingConnection();			
		}
		
		GUI.enabled = audioCall.connectionOpen;
		if(GUILayout.Button("Close", buttonOptions)){
			
			///This resets the connection, getting rid of any past miscellaneous information and is use to close 
			///connection between the parties. After being called, the connection can be started again to another user
			audioCall.resetConnection();			
		}
		GUILayout.EndHorizontal();
		
		GUILayout.Space(verticalSpacing);
		GUI.enabled = true;
		GUILayout.Label("DatagramSocketConnection Test");		
		GUILayout.BeginHorizontal();
		GUI.enabled = !datagramSocketConnection.connectionEstablished && !datagramSocketConnection.incomingIntent;
		if(GUILayout.Button("Open", buttonOptions)) 
		{		
			///This opens a connection to the provided phone number using the JibeBundleTransferConnection
			///This function is also called upon return from the friends list so as to start the connection that the user would expect.
			datagramSocketConnection.openConnection(phoneNumber);
		}
	
		GUI.enabled = !datagramSocketConnection.connectionEstablished && datagramSocketConnection.incomingIntent;
		if(GUILayout.Button("Accept", buttonOptions)) 
		{	
			///This function is used to accept an incoming session
			datagramSocketConnection.acceptIncomingConnection();
		}
	
		if (GUILayout.Button("Reject", buttonOptions)) {
			///This rejects an incoming session
			datagramSocketConnection.rejectIncomingConnection();			
		}
		
		GUI.enabled = datagramSocketConnection.connectionEstablished;
		if(GUILayout.Button("Close", buttonOptions))
		{
			///This resets the connection, getting rid of any past miscellaneous information and is use to close 
			///connection between the parties. After being called, the connection can be started again to another user
			datagramSocketConnection.resetConnection();			
		}
		
		
		GUILayout.EndHorizontal();
		message = GUILayout.TextField(message, GUILayout.ExpandWidth(true), GUILayout.Height(100f));
		GUI.enabled = !string.IsNullOrEmpty(message) && datagramSocketConnection.connectionEstablished && !datagramSocketConnection.incomingIntent;
		if (GUILayout.Button("Send", buttonOptions))
		{
			Debug.Log(message.Length);
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes(message);
			datagramSocketConnection.sendData(bytes);
		}
		
		GUILayout.EndArea();

	}	
	
	
	
}
