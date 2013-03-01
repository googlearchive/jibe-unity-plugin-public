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

/**
 * JibeApiBridge is a main class that connects UnityJibeBridge on the Java side with
 * Unity's environment; All Connection classes derive from this class
 */
public class JibeApiBridge {	
	/**
	 * This is a reference to the main java class that allows for Jibe objects to be 
	 * created and methods to be called during the setup of a connection.
	 */
#if UNITY_ANDROID
	protected AndroidJavaObject bridge;
#endif

	public JibeApiBridge()
	{
#if UNITY_EDITOR
		// Don't do anything
#elif UNITY_ANDROID
		AndroidJNI.AttachCurrentThread();
		
		using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("jibe.sdk.client.unity.UnityJibeBridge"))
		{	
			bridge = cls_UnityPlayer.CallStatic<AndroidJavaObject>("getInstance");
			
			//Set APP_ID and APP_SECRET
			bridge.Call("setApptoAppIdentifier", UnityJibeProperties.APP_ID, UnityJibeProperties.APP_SECRET);
		}
#endif		
	}
	
	/**
	 * For debug purposes only and merely serves to display an message to the user.
	 **/
	public void showMessage(string message){
#if UNITY_EDITOR
		// Don't do anything
#elif UNITY_ANDROID
		bridge.Call("showMessage", message);
#endif
	}
	
	// TODO: Remove reference to AndroidJavaObject
	public AndroidJavaObject getContext()
	{
#if UNITY_EDITOR
		return new AndroidJavaObject(IntPtr.Zero);
#elif UNITY_ANDROID
		return bridge.Call<AndroidJavaObject>("getContext");
#endif
	}
	
	/**
	 *  Get the last intent from Android
	 */
	// TODO: Remove reference to AndroidJavaObject	
	public AndroidJavaObject peekLastIntent(){
#if UNITY_EDITOR
		return new AndroidJavaObject(IntPtr.Zero);
#elif UNITY_ANDROID
		return bridge.Call<AndroidJavaObject>("peekLastIntent");
#endif
	}
	
	// TODO: Remove reference to AndroidJavaObject	
	public AndroidJavaObject popLastIntent(){
#if UNITY_EDITOR
		return new AndroidJavaObject(IntPtr.Zero);
#elif UNITY_ANDROID
		return bridge.Call<AndroidJavaObject>("popLastIntent");
#endif
	}
	
}
