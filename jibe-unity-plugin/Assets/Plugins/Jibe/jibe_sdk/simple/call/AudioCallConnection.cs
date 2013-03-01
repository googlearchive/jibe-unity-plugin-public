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
 * High-level API class, offering convenient set-up of app-to-app VoIP audio calls. See jibe.sdk.client.simple for code examples.
 */
public class AudioCallConnection : JibeApiBridge, SimpleConnection {
	private AndroidJavaObject audioCallConnection;
	
	public AudioCallConnection(GameObject obj) : base()
	{
#if UNITY_EDITOR
		audioCallConnection = new AndroidJavaObject(IntPtr.Zero);
#elif UNITY_ANDROID
		audioCallConnection = bridge.Call<AndroidJavaObject>("audioCallConnection", obj.name);
		Debug.Log("audioCallConnection: "+audioCallConnection.GetRawObject());
		//showMessage("I call ShowMessage!");
#endif
	}
	
	public AndroidJavaObject getIntent()
	{
#if UNITY_ANDROID
		return audioCallConnection.Call<AndroidJavaObject>("getIntent");
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
		return audioCallConnection.Call<bool>("canProcessIntent", intent);
#endif
	}
	
	/**
	 * @see SimpleApi.dispose()
	 */
	public void dispose()
	{
#if UNITY_ANDROID
		audioCallConnection.Call("dispose");
#endif
	}
	
	/**
	 * Returns the enum value of the currently selected audio quality level. See AudioEncodingOption
	 * 
	 * @return The audio encoding option
	 */
	public string getAudioEncodingOption() 
	{
#if UNITY_ANDROID
		AndroidJavaObject audioEncoding = audioCallConnection.Call<AndroidJavaObject>("getAudioEncodingOption");
		if (audioEncoding.GetRawObject() != System.IntPtr.Zero)
			return audioEncoding.Call<string>("toString");
		else
			return null;
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
		return audioCallConnection.Call<string>("getRemoteUserDisplayName");
#endif
	}
	
	/**
	 * @see SimpleConnection.getRemoteUserId()
	 */
	public string getRemoteUserId() 
	{
#if UNITY_ANDROID
		return audioCallConnection.Call<string>("getRemoteUserId");
#endif
	}
	
	/**
	 * @see SimpleConnection.getSessionId()
	 */
	public long getSessionId() 
	{
#if UNITY_ANDROID
		return audioCallConnection.Call<long>("getSessionId");
#endif
	}
	
	/**
	 * @see SimpleConnection.getState()
	 */
	public string getState()
	{
#if UNITY_ANDROID
		AndroidJavaObject state = audioCallConnection.Call<AndroidJavaObject>("getState");
		if (state.GetRawObject() != System.IntPtr.Zero)
			return state.Call<string>("toString");
		else
			return null;
#endif
	}
	
	/**
	 * @see SimpleApi.isDisposed()
	 * 
	 * @return true, if disposed.
	 */
	public bool isDisposed()
	{
#if UNITY_ANDROID
		return audioCallConnection.Call<bool>("isDisposed");
#endif
	}
	
	/**
	 * Checks whether echo cancellation is fully supported on the user's handset in order to deliver an optimal handsfree calling experience. 
	 * If not, the application should recommend to the user to user a headset if a handsfree calling scenario is required.
	 * 
	 * @return boolean true if a headset is required, false otherwise.
	 */
	public bool isHeadsetRequiredForHandsfree()
	{
#if UNITY_ANDROID
		return audioCallConnection.Call<bool>("isHeadsetRequiredForHandsfree");
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
		return audioCallConnection.Call<bool>("isInitialized");
#endif
	}
	
	/**
	 * Returns mute/unmute state of microphone
	 * 
	 * @return boolean true if mute, false otherwise.
	 */
	public bool isMicrophoneMute()
	{
#if UNITY_ANDROID
		return audioCallConnection.Call<bool>("isMicrophoneMute");
#endif
	}
	
	/**
	 * Returns the on/off state of the speakerphone.
	 * 
	 * @return boolean true if the speakerphone is on, false otherwise.
	 */
	public bool isSpeakerphoneOn()
	{
#if UNITY_ANDROID
		return audioCallConnection.Call<bool>("isSpeakerponeOn");
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
		return audioCallConnection.Call<bool>("isStarted");
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
		audioCallConnection.Call("monitor", intent);
#endif
	}
	
	/**
	 * @see SimpleConnection.reject(Intent)
	 * 
	 * @return intent - Intent which was thrown to notify the client about the incoming connection request.
	 */
	public void reject(AndroidJavaObject intent)
	{
#if UNITY_ANDROID
		audioCallConnection.Call("reject", intent);
#endif
	}
	
//	public void setAudioEncodingOption(AudioEncodingOption option)
//	{
//		
//	}
	
	/**
	 * @see SimpleConnection.setAutoAccept(boolean)
	 * 
	 * @return autoAccept - Set to true, in order to automatically accept incoming connections.
	 */
	public void setAutoAccept(bool autoAccept)
	{
#if UNITY_ANDROID
		audioCallConnection.Call("setAutoAccept", autoAccept);
#endif
	}
	
	/**
	 * Mute/unmute the microphone during audio call Requires android.permission.MODIFY_AUDIO_SETTINGS to be declared in manifest.
	 * 
	 * @param on - Set to true, in order to mute the microphone. 
	 */
	public void setMicrophoneMute(bool on)
	{
#if UNITY_ANDROID
		audioCallConnection.Call("setMicrophoneMute", on);
#endif
	}
	
	/**
	 * Returns the on/off state of the speakerphone.
	 * 
	 * @return boolean true if the speakerphone is on, false otherwise.	
	 */
	public void setSpeakerphoneOn(bool on)
	{
#if UNITY_ANDROID
		audioCallConnection.Call("setSpeakerphoneOn", on);
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
		audioCallConnection.Call("start", intent);
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
		audioCallConnection.Call("start", receiverUserId);
#endif
	}
	
	/**
	 * @see SimpleConnection.stop()
	 */
	public void stop()
	{
#if UNITY_ANDROID
		audioCallConnection.Call("stop");
#endif
	}
}
