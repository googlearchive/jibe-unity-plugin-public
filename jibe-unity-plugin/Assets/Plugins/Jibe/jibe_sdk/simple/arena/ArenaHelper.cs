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
 * Assists with checking for the Jibe Realtime Engine and for launching the friend picker
 */
public class ArenaHelper : JibeApiBridge 
{	
	AndroidJavaObject arenaHelper;
	
	/**
	 * Checks if a compatible Jibe Realtime Engine is installed on the device and whether the minimum 
	 * version requirement is met.	
	 *
	 * @return boolean isInstalled
	 */
	static public bool isCompatibleRealtimeEngineInstalled()
	{
#if UNITY_EDITOR
		return true;
#elif UNITY_ANDROID
		JibeApiBridge bridge = new JibeApiBridge();
		AndroidJavaClass jc = new AndroidJavaClass("jibe.sdk.client.simple.arena.ArenaHelper");
		return jc.CallStatic<bool>("isCompatibleRealtimeEngineInstalled", bridge.getContext());
#else
		return true;
#endif		
	}
	
	/**
	 * Opens the Google Play application to the download page for the Jibe Realtime Engine
	 * 
	 */
	static public void openGoolePlayPage()
	{
#if UNITY_EDITOR
		return;
#elif UNITY_ANDROID		
		JibeApiBridge bridge = new JibeApiBridge();
		AndroidJavaClass jc = new AndroidJavaClass("jibe.sdk.client.simple.arena.ArenaHelper");
		jc.CallStatic("openGooglePlayPage", bridge.getContext());
#else
		return true;
#endif
	}
	
	public ArenaHelper(GameObject obj) : base()
	{
#if UNITY_EDITOR
		// Just initialize object
#elif UNITY_ANDROID
		arenaHelper = bridge.Call<AndroidJavaObject>("arenaHelper", obj.name);	
#endif
	}
	
	/**
	 * Releases all resources allocated for this object. Needs to be invoked before the object can go out of scope.
	 */
	public void dipose()
	{
#if UNITY_EDITOR
		// Don't do anything
#elif UNITY_ANDROID
		arenaHelper.Call("dispose");
#endif
	}
	
	/**
	 * Checks whether the connection has been disposed and can no longer be used.
	 * 
	 * @return true, if disposed.
	 */
	public bool isDisposed()
	{
#if UNITY_EDITOR
		// Don't do anything
		return false;
#elif UNITY_ANDROID
		return arenaHelper.Call<bool>("isDisposed");
#endif
	}
	
	/**
	 * Checks whether the SimpleApi class is ready to be used (i.e. a connection to the Jibe Realtime Engine has been established successfully in the background)
	 * 
	 * @return true, if the connection is ready.
	 */
	public bool isInitialized()
	{
#if UNITY_EDITOR
		// Don't do anything
		return true;
#elif UNITY_ANDROID
		return arenaHelper.Call<bool>("isInitialized");
#endif
	}
		
	/**
	 * Launches Arena friend list for in-app friend selection
	 */
	public void startFriendPicker()
	{
#if UNITY_EDITOR
		// Don't do anything
#elif UNITY_ANDROID
		arenaHelper.Call("startFriendPicker");
#endif
	}
}
