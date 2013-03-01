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
using System;

/**
 * Holds the application id and secret for use with the Jibe SDK
 */
public class UnityJibeProperties
{
	/**
	 * This is the app id from Jibe for your specific application.
	 **/
	private static string appId = "184175d08e3b4bcaa367fe9629af2a18";
	public static string APP_ID
	{
		get 
		{
			return appId;
		}
	}
	/**
	 * This is your specific app's secret for use in setting up a Jibe connection.
	 **/
	private static string appSecret =  "b5232517a8784b5ea0a6975475b80270";
	public static string APP_SECRET
	{
		get
		{
			return appSecret;
		}
	}
	
	/**
	 * Call this method before any Jibe classes are accessed to set your application id and secret
	 **/
	public static void SetAppCredentials(string id, string secret)
	{
		appId = id;
		appSecret = secret;
	}
}

