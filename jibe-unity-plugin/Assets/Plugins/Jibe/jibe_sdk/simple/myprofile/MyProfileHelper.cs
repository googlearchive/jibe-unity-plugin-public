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
 * High-level API class, used to retrieve the details of the user's profile See jibe.sdk.client.simple.myprofile for a code example.
 */
public class MyProfileHelper : JibeApiBridge {
	
	private AndroidJavaObject myProfileHelper;
	
	public MyProfileHelper(GameObject obj) : base()
	{
#if UNITY_EDITOR
		myProfileHelper = new AndroidJavaObject(IntPtr.Zero);
#elif UNITY_ANDROID
		myProfileHelper = bridge.Call<AndroidJavaObject>("myProfileHelper", obj.name);	
#endif
	}
	
	/**
	 * Releases all resources allocated for this object. Needs to be invoked before the object can go out of scope.
	 */
	public void dipose()
	{
#if UNITY_ANDROID
		myProfileHelper.Call("dispose");
#endif
	}
	
	/**
	 * Retrieves the Jibe Cloud/ARENA display name/nickname of the user.
	 * 
	 * @return String display name/nickname
	 */
	public string getDisplayName() 
	{
#if UNITY_ANDROID
//		Debug.Log("Call getDisplayName, row object: "+myProfileHelper.GetRawObject());
		string name = myProfileHelper.Call<string>("getDisplayName");
//		Debug.Log("Name: "+name);
		return name;
#endif
	}
	
	/**
	 * Retrieves eMail/SIP URI associated with the Jibe Cloud/ARENA user account.
	 * 
	 * @return String eMail/SIP URI
	 */
	public string getEmail() 
	{
#if UNITY_ANDROID
		return myProfileHelper.Call<string>("getEmail");
#endif
	}
	
	/**
	 * Retrieves the MSISDN (international phone number e.g. +1 (234) 5678 9012). In most places, this is the country code followed by the local phone number without leading zero.
	 * 
	 * @return The MSISDN associated with this profile.
	 */
	public string getMsisdn() 
	{
#if UNITY_ANDROID
		return myProfileHelper.Call<string>("getMsisdn");
#endif
	}
	
	/**
	 * Retrieves the thumbnail picture URL associated with the Jibe Cloud/ARENA user account.
	 * 
	 * @return String thumbnail picture URL
	 */
	public string getThumbnailUrl()
	{
#if UNITY_ANDROID
		return myProfileHelper.Call<string>("getThumbnailUrl");
#endif
	}
	
	/**
	 * Retrieves the Jibe Cloud/ARENA userId of the user.
	 * 
	 * @return long userId
	 */
	public long getUserId()
	{
#if UNITY_ANDROID
		return myProfileHelper.Call<long>("getUserId");
#endif
	}
	
	/**
	 * Checks whether the MyProfileHelper is closed. All resources have been released. A new instance has to be created for future use.
	 * 
	 * @return true, if disposed.
	 */
	public bool isDisposed()
	{
#if UNITY_ANDROID
		return myProfileHelper.Call<bool>("isDisposed");
#endif
	}
	
	/**
	 * Checks whether the MyProfileHelper is ready to be used (i.e. a connection to the Jibe Realtime Engine has been established successfully in the background)
	 * 
	 * @return true, if initialized.
	 */
	public bool isInitialized()
	{
#if UNITY_ANDROID
		return myProfileHelper.Call<bool>("isInitialized");
#endif
	}
	
	/**
	 * Checks if the connection to the Jibe Network and Jibe Cloud/ARENA is currently active.
	 * 
	 * @return boolean user online state
	 */
	public bool isOnline()
	{
#if UNITY_ANDROID
		return myProfileHelper.Call<bool>("isOnline");
#endif
	}
	
	/**
	 * Starts listening for updates to the online state of the connection. Updates are received via the jibe.sdk.client.simple.profile.MyProfileHelperListener
	 */
	public void startMonitoringOnlineState()
	{
#if UNITY_ANDROID
		myProfileHelper.Call("startMonitoringOnlineState");
#endif
	}
	
	/**
	 * Stops listening for updates to the online state of the connection. No more online state updates are received via the jibe.sdk.client.simple.profile.MyProfileHelperListener
	 */
	public void stopMonitoringOnlineState()
	{
#if UNITY_ANDROID
		myProfileHelper.Call("stopMonitoringOnlineState");
#endif
	}
}
