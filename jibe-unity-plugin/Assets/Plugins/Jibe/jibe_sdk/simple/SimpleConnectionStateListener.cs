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

/**
 * A callback interface, supported by all connection classes in jibe.sdk.client.simple.*, which allows 
 * applications to monitor the state of an individual connection. See jibe.sdk.client.simple.contacts for a code example.
 */

public interface SimpleConnectionStateListener : SimpleApiStateListener {
	/**
	 * The connection has been started. Media will start to flow.
	 */
	void onStarted(string message);
	
	/**
	 * The connection could not be started.
	 * 
	 * The info parameters contain the reason, if the applications wish to evaluate this.
	 * 
	 * Typical reasons are that connection request was cancelled by the sender, before the connection could actually be established, 
	 * or because the incoming session was rejected by the receiver.
	 * 
	 * @see
	 * 
	 * JibeSessionEvent.INFO_USER_UNKNOWN Phone number of receiver not known to the network
	 * 
	 * JibeSessionEvent.INFO_USER_NOT_ONLINE Receiver known but not online
	 * 
	 * JibeSessionEvent.INFO_SESSION_CANCELLED Sender has cancelled the connection.
	 * 
	 * JibeSessionEvent.INFO_SESSION_REJECTED Receiver has rejected the connection (user busy)
	 * 
	 * JibeSessionEvent.INFO_SESSION_TIMEOUT Receiver did not accept the connection (no answer)
	 * 
	 * JibeSessionEvent.INFO_RESOURCE_UNAVAILABLE Problem with phone resources (e.g. acquisition of video, audio, etc)
	 * 
	 * @param info - A JibeSessionEvent info value.
	 */
	void onStartFailed(string info);
	
	/**
	 * The connection has been terminated.
	 * 
	 * The Info parameter contains the reason for evaluation, which may (or may not) be relevant to the application. 
	 * Typical reasons are that the connection was terminated by the sender or by the receiver.
	 * 
	 * @see
	 * 
	 * JibeSessionEvent.INFO_SESSION_TERMINATED_BY_REMOTE.
	 * 
	 * For local termination, the default info from JibeEvent is used. See: JibeEvent.INFO_GENERIC_EVENT
	 *
	 * @param info - A JibeSessionEvent info value.
	 */
	void onTerminated(string info);
}
