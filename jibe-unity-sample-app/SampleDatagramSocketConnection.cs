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
using System;
//using System.Linq;

public class SampleDatagramSocketConnection : MonoBehaviour, SimpleConnectionStateListener {
	
	private const string TAG = "DatagramSocketConnection";
	
	
	/**
	 * This tells us if there is an Intent coming from Jibe that we should respond to.
	 * For demo purposes this is used to enable the Accepting and Rejecting of incoming connections.
	 **/
	public bool incomingIntent = false;
	/**
	 * This is a simple variable to determine if we currently have a connection open.
	 * It is used internally and should only be read.
	 **/
	public bool connectionRequest = false;
	/**
	 * This is a simple variable to determine if we currently have an accepted connection.
	 * It is used internally and should only be read.
	 **/
	public bool connectionEstablished = false;
	
	public bool isHost = false;
	
	
	private string phoneNumeber = null;
	
	DatagramSocketConnection dsgInstance = null;	
	AndroidJavaObject processedIntent;

	/// Use this for initialization
	void Start () {
		dsgInstance = new DatagramSocketConnection(gameObject);
		onNewIntent();
		Debug.Log(TAG+" call onNewIntent by Start");
	}

	void onNewIntent()
	{
		Debug.Log(TAG+" call onNewIntent");
		AndroidJavaObject intent = dsgInstance.peekLastIntent();
		
		AndroidJavaClass jibeIntents = new AndroidJavaClass("jibe.sdk.client.JibeIntents");
		bool isSender = intent.Call<string>("getAction").StartsWith(jibeIntents.GetStatic<string>("ACTION_ARENA_CHALLENGE"));
		
		phoneNumeber = intent.Call<string>("getStringExtra", jibeIntents.GetStatic<string>("EXTRA_USERID"));
			
		if (isSender) 
		{
			//display waiting for receiver dialog
			openConnection(phoneNumeber);
			dsgInstance.popLastIntent();
			return;
		} 

		
		if (intent.GetRawObject() != IntPtr.Zero && dsgInstance.canProcessIntent(intent))
			StartCoroutine(processIntent(dsgInstance.popLastIntent()));
	}	
	
	public void onInitializationFailed(string message)
	{
		showMessage(message);
	}
	
	public void onInitialized(string message)
	{
		showMessage(message);
	}
	
	public void onStarted(string message)
	{
		showMessage(message);
		connectionEstablished = true;
	}
	
	public void onStartFailed(string info)
	{
		connectionRequest = false;
		connectionEstablished = false;
		incomingIntent = false;
		
		switch (info) 
		{
			case JibeSessionEvent.INFO_SESSION_CANCELLED:
				showMessage("onStartFailed : The sender has canceled the connection");
				break;
			case JibeSessionEvent.INFO_SESSION_REJECTED:
				showMessage("onStartFailed : The receiver has rejected the connection/is busy");
				break;
			case JibeSessionEvent.INFO_SESSION_TIMEOUT:
				showMessage("onStartFailed : The receiver did not accept the connection");
				break;
			case JibeSessionEvent.INFO_USER_NOT_ONLINE:
				showMessage("onStartFailed : Receiver is not online");
				break;
			case JibeSessionEvent.INFO_USER_UNKNOWN:
				showMessage("onStartFailed : This phone number is not known");
				break;
			default:
				showMessage("onStartFailed : Connection start failed." + info);
				break;
		}
		resetConnection();
	}
	
	public void onTerminated(string info)
	{
		connectionRequest = false;
		connectionEstablished = false;
		incomingIntent = false;
		Debug.Log("INSIDE DATAGRAM");
		switch (info) 
		{
			case JibeSessionEvent.INFO_GENERIC_EVENT:
				showMessage("You terminated the connection");
				break;
			case JibeSessionEvent.INFO_SESSION_TERMINATED_BY_REMOTE:
				showMessage("The remote party terminated the connection");
				break;
			default:
				showMessage("Connection terminated.");
				break;
		}
		
		resetConnection();
	}
	
	public IEnumerator processIntent(AndroidJavaObject intent)
	{	
		Debug.Log(TAG+" Start to check the JibeInstance");
		while(dsgInstance == null || !dsgInstance.isInitialized())
		{
			yield return null;
		}
		
		Debug.Log(intent.Call<string>("toString"));
		if(dsgInstance.canProcessIntent(intent))
		{
			Debug.Log(TAG+" I'm going to store the intent!");
			this.processedIntent = intent;
			Debug.Log(TAG+" The intent is perfect and I alredy store it!");
		}
		
		handleIncomingSessionIntent();		
	}
	
	public bool handleIncomingSessionIntent()
	{
		if(!dsgInstance.canProcessIntent(processedIntent))
			return false;
		
		AndroidJavaClass jibeIntents = new AndroidJavaClass("jibe.sdk.client.JibeIntents");
		Debug.Log("Incoming data session from: "+ processedIntent.Call<string>("getStringExtra", jibeIntents.GetStatic<string>("EXTRA_USERID")));
		
		try{
			Debug.Log(TAG+": Try to monitor the connection");
			dsgInstance.monitor(processedIntent);
			Debug.Log(TAG+": Connection monitored!");
		} catch (Exception e){
			Debug.LogError(e);
			return false;
		}
		
		connectionRequest = false;
		connectionEstablished = false;
		incomingIntent = true;
		return true;
	}
	
	public void sendData(byte[] data)
	{
		StartCoroutine(dsgInstance.send(data));
	}
	
	public void showMessage(string message){
		dsgInstance.showMessage(message);
	}
	
	public IEnumerator receiveData()
	{		
		Coroutine<byte[]> receive = StartCoroutine<byte[]>(dsgInstance.receive(1024));
		yield return receive.coroutine;
		yield return receive.Value;
	}
	
	public void acceptIncomingConnection()
	{
		dsgInstance.start(processedIntent);
		connectionRequest = true;
		connectionEstablished = true;
		incomingIntent = false;
	}
	
	public void rejectIncomingConnection()
	{
		dsgInstance.reject(processedIntent);
		connectionRequest = false;
		connectionEstablished = false;
		incomingIntent = false;
	}
	
	public void openConnection(string otherUserId){
		try {
			if(!dsgInstance.getState().Equals(SimpleConnectionState.STARTING))
			{
				dsgInstance.start(otherUserId);
				connectionRequest = true;
				isHost = true;
				incomingIntent = false;
				//this is for debug purpose only
				showMessage("Connection should be open");
			} 
		} catch (Exception e) {
			Debug.LogError(e.Message);
			resetConnection();
		}
	}
	
	public void closeConnession()
	{
		resetConnection();
	}
	
	public void resetConnection() {
		try {
			dsgInstance.stop();
			isHost = false;
			connectionRequest = false;
			connectionEstablished = false;
			incomingIntent = false;
		} catch (Exception e) {
			Debug.LogError("Failed to stop connection."+ e);
		}
	}
	
	public string PhoneNumber
	{
		get
		{
			return phoneNumeber;
		}
	}
}
