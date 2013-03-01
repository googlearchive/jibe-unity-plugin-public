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

/**
 * Base interface, implemented by all connection classes in jibe.sdk.client.simple.*.
 */

public interface SimpleConnection : SimpleApi {
	/**
	 * Checks whether a given intent matches this type of simple connection. Can be used to 
	 * update the application UI upon valid incoming intents prior to calling start(Intent) or reject(Intent)
	 * 
	 * @param incomingIntent -
	 * @return true, if the intent matches this type of connection.
	 */
	bool canProcessIntent(AndroidJavaObject intent);
	
	/**
	 * Returns the display name (if available) of the remote user. 
	 * 
	 * @return The display name of the remote user.
	 */
	string getRemoteUserDisplayName();
	
	/**
	 * Returns the userId (phone number/SIP URI) of the remote user.
	 * 
	 * @return The userId.
	 */
	string getRemoteUserId();
	
	/**
	 * Returns the ID of the SIP session underpinning the connection, for use with the low-level APIs. 
	 * 
	 * @return The current sessionId .
	 */
	long getSessionId();
	
	/**
	 * Returns the state of the current connection as defined in SimpleConnectionState
	 */
	string getState(); 
	
	/**
	 * Checks whether the connection has been established successfully, end-to-end, in which case all signaling has been set up, and media data is ready to flow.
	 * 
	 * @return true, if connected.
	 */
	bool isStarted();
	
	/**
	 * Monitor an incoming connection. The listener will receive events for this connection without making changes to its state.
	 * 
	 * @param incomingIntent - Intent which was thrown to notify the client about the incoming connection request.
	 */
	void monitor(AndroidJavaObject intent);
	
	/**
	 * Rejects an incoming connection.
	 * 
	 * @params incomingIntent - Intent which was thrown to notify the client about the incoming connection request.
	 */
	void reject(AndroidJavaObject intent);
	
	/**
	 * Specify whether incoming connections should be automatically accepted. Note that incoming calls will only be auto-accepted if there isn't already another call in progress.
	 * 
	 * The application's ID needs to be set, via UnityJibeProperties.setAppToAppIdentifier(String, String) , before enabling the auto-accept feature.
	 * 
	 * @param autoAccept - Set to true, in order to automatically accept incoming connections.
	 */
	void setAutoAccept(bool autoAccept);
	
	/**
	 * Starts connection from an intent and returns immediately. Sender side intent (user selects "challenge" inside Jibe Arena): 
	 * jibe.sdk.client.JibeIntents.ACTION_ARENA_CHALLENGE Receiver side intent: depending on the type of SimpleConnection being 
	 * used such as: jibe.sdk.client.JibeIntents.ACTION_INCOMING_DATASESSION jibe.sdk.client.JibeIntents.ACTION_INCOMING_CALL 
	 * jibe.sdk.client.JibeIntents.ACTION_INCOMING_FILETRANSFER If you want to start a connection directly with a phone number 
	 * or SIP URI on the sender side, see start(String) SimpleConnectionStateListener has to be used, to detect when the connection 
	 * has been started, or if the connection request fails.
	 * 
	 * @param intent - Intent with which to start the connection
	 */
	void start(AndroidJavaObject intent);
	
	/**
	 * Opens a SIP-session-based connection to a remote user. Returns immediately. SimpleConnectionStateListener has to be used, to detect when 
	 * the connection has been started, or if the connection request fails.
	 * 
	 * @param receiverUserId - The phone number/SIP URI of user you are trying to connect (e.g. +491235678 or (408) 345-6789) 
	 */
	void start(string receiverUserId);
	
	/**
	 * Closes the connection and all of the resources that it uses.
	 */
	void stop();
}