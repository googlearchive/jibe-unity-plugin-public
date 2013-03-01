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

#if !(UNITY_IPHONE || UNITY_ANDROID)
#error Set your build target to iOS or Android
#endif

/**
 * High-level API class, used for the convenient set-up of low-latency, app-to-app data connections. 
 * The connection follows a datagram model (i.e. there is no guaranteed delivery), as it is based upon UDP. 
 * See jibe.sdk.client.simple for code examples.
 */
public class DatagramSocketConnection : JibeApiBridge, SimpleConnection {
	private AndroidJavaObject datagramSocketConnection;
	
	public DatagramSocketConnection(GameObject obj) : base()
	{
#if UNITY_EDITOR
		datagramSocketConnection = new AndroidJavaObject(IntPtr.Zero);
#elif UNITY_ANDROID
		datagramSocketConnection = bridge.Call<AndroidJavaObject>("datagramSocketConnection", obj.name);
#endif
	}
	
	/**
	 * @see SimpleConnection.canProcessIntent(Intent)
	 * 
	 * @return true, if the intent matches this type of connection.
	 */
	public bool canProcessIntent(AndroidJavaObject intent)
	{
#if UNITY_ANDROID
		return datagramSocketConnection.Call<bool>("canProcessIntent", intent);
#endif
	}
	
	/**
	 * @see SimpleApi.dispose()
	 */
	public void dispose()
	{
#if UNITY_ANDROID
		datagramSocketConnection.Call("dispose");
#endif		
	}
	
	/**
	 * Gets the invitation message that is associated with this connection.
	 * 
	 * @return The invitation message that is associated with this connection.
	 */
	public string getInviteMessage() 
	{
#if UNITY_ANDROID		
		return datagramSocketConnection.Call<string>("getInviteMessage");
#endif		
	}
	
	/**
	 * @see SimpleConnection.getRemoteUserDisplayName()
	 * 
	 * @return The display name of the remote user.
	 */
	public string getRemoteUserDisplayName() 
	{
#if UNITY_ANDROID		
		return datagramSocketConnection.Call<string>("getRemoteUserDisplayName");
#endif
	}
	
	/**
	 * @see SimpleConnection.getRemoteUserId()
	 * 
	 * @return The userId.
	 */
	public string getRemoteUserId() 
	{
#if UNITY_ANDROID
		return datagramSocketConnection.Call<string>("getRemoteUserId");
#endif
	}
	
	/**
	 * @see SimpleConnection.getSessionId()
	 * 
	 * @return The current sessionId.
	 */
	public long getSessionId() 
	{
#if UNITY_ANDROID		
		return datagramSocketConnection.Call<long>("getSessionId");
#endif
	}
	
	/**
	 * @see SimpleConnection.getState()
	 */
	public string getState()
	{
		AndroidJavaObject state = datagramSocketConnection.Call<AndroidJavaObject>("getState");
		if (state.GetRawObject() != System.IntPtr.Zero)
			return state.Call<string>("toString");
		else
			return null;
	}
	
	/**
	 * @see SimpleApi.isDisposed()
	 * 
	 * @return true, if disposed.
	 */
	public bool isDisposed()
	{
#if UNITY_ANDROID		
		return datagramSocketConnection.Call<bool>("isDisposed");
#endif
	}
	
	/**
	 * @see SimpleApi.isInitialized()
	 * 
	 * @return true, if the connection is ready.
	 */
	public bool isInitialized()
	{
#if UNITY_ANDROID
		return datagramSocketConnection.Call<bool>("isInitialized");
#endif
	}
	
	/**
	 * @see SimpleConnection.isStarted()
	 * 
	 * @return true, if connected.
	 */
	public bool isStarted()
	{
#if UNITY_ANDROID		
		return datagramSocketConnection.Call<bool>("isStarted");
#endif
	}
	
	/**
	 * @see SimpleConnection.monitor(Intent)
	 * 
	 * @param intent - Intent which was thrown to notify the client about the incoming connection request.
	 */
	public void monitor(AndroidJavaObject intent)
	{
#if UNITY_ANDROID		
		datagramSocketConnection.Call("monitor", intent);
#endif
	}
	
	/**
	 * Receives data into a specified buffer on the transfer thread.
	 * 
	 * @param maxBufferSize - The max buffer size to allocate to receive data.
	 */
	public IEnumerator receive(int maxBufferSize)
	{
#if UNITY_ANDROID
		AndroidJavaObject thread = bridge.Call<AndroidJavaObject>("receiveData", datagramSocketConnection, maxBufferSize);
		while (thread.Call<bool>("isAlive"))
			yield return null;
		
		byte[] buffer = thread.Call<byte[]>("getDataReceived");
		int length = thread.Call<int>("getDataLength");
		System.Array.Resize(ref buffer, length);
		yield return buffer;
#else 
		yield return new byte[] {};
#endif		
	}
		
	/**
	 * @see SimpleConnection.reject(Intent)
	 * 
	 * 
	 * @param intent - Intent which was thrown to notify the client about the incoming connection request.
	 */
	public void reject(AndroidJavaObject intent)
	{
#if UNITY_ANDROID
		datagramSocketConnection.Call("reject", intent);
#endif
	}
	
	/**
	 * Sends the given buffer.
	 * 
	 * @param buffer - The buffer to send. This MUST not be null.
	 */
	public IEnumerator send(byte[] buffer)
	{
#if UNITY_ANDROID		
		AndroidJavaObject thread = bridge.Call<AndroidJavaObject>("sendData", datagramSocketConnection, buffer);
		while (thread.Call<bool>("isAlive"))
			yield return null;
#else
		yield return null;
#endif		
	}
	
	/**
	 * Sends the given buffer with control.
	 * 
	 * @param buffer - The buffer to send. This MUST not be null.
	 * @param offset - The offset, within the buffer, from which to start sending data.
	 * @param length - The number of bytes to send.
	 */
	public IEnumerator send(byte[] buffer, int offset, int length)
	{
#if UNITY_ANDROID		
		AndroidJavaObject thread = bridge.Call<AndroidJavaObject>("sendData", datagramSocketConnection, buffer, offset, length);
		while (thread.Call<bool>("isAlive"))
			yield return null;		
#else
		yield return null;
#endif		
	}
	
	/**
	 * @see SimpleConnection.setAutoAccept(boolean)
	 * 
	 * @param autoAccept - Set to true, in order to automatically accept incoming connections.
	 */
	public void setAutoAccept(bool autoAccept)
	{
#if UNITY_ANDROID		
		datagramSocketConnection.Call("setAutoAccept", autoAccept);
#endif
	}
	
	/**
	 * @see SimpleConnection.start(Intent)
	 * 
	 * @param intent - The phone number/SIP URI of user you are trying to connect (e.g. +491235678 or (408) 345-6789)
	 */
	public void start(AndroidJavaObject intent)
	{
#if UNITY_ANDROID
		datagramSocketConnection.Call("start", intent);
#endif
	}
	
	/**
	 * @see SimpleConnection.start(String)
	 * 
	 * @param receiverUserId - The phone number/SIP URI of user you are trying to connect (e.g. +491235678 or (408) 345-6789)
	 */
	public void start(string receiverUserId)
	{
#if UNITY_ANDROID		
		datagramSocketConnection.Call("start", receiverUserId);
#endif		
	}
	
	/**
	 * Starts a datagram socket to a remote user. Returns immediately. A SimpleConnectionStateListener has to be used,
	 * in order to detect when the connection has been started, or if it fails.
	 * 
	 * @param receiverUserId - The phone number/SIP URI of user you are trying to connect (e.g. +491235678 or (408) 345-6789)
	 * @param inviteMessage - A message that can be sent with the request to establish a data connection, for example, 
	 * "Fancy a game?". This message can be displayed on the remote user's device.
	 */
	public void start(string receiverUserId, string inviteMessage)
	{
#if UNITY_ANDROID
		datagramSocketConnection.Call("start", receiverUserId, inviteMessage);
#endif		
	}
	
	/**
	 * @see SimpleConnection.stop()
	 */
	public void stop()
	{
#if UNITY_ANDROID		
		datagramSocketConnection.Call("stop");
#endif		
	}
}
