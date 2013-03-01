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

/**
 * The Jibe Event class for session events.
 */
public class JibeSessionEvent {
	public const string INFO_GENERIC_EVENT = "0";
	
	/**
	 * The user's status is unknown.
	 */
	public const string INFO_USER_UNKNOWN = "1";
	
	/**
	 * The user is not online.
	 */
	public const string INFO_USER_NOT_ONLINE = "2";
	
	/**
	 * The session has timed out.
	 */
	public const string INFO_SESSION_TIMEOUT = "3";
	
	/**
	 *  The session has been rejected.
	 */
	public const string INFO_SESSION_REJECTED = "4";
	
	/**
	 * The session has been cancelled.
	 */
	public const string INFO_SESSION_CANCELLED = "5";
	
	/**
	 * A necessary resource is not available.
	 */
	public const string INFO_RESOURCE_UNAVAILABLE = "6";
	
	/**
	 * The session has been stopped.
	 */
	public const string INFO_SESSION_STOPPED = "7";
	
	/**
	 * The session has been terminated, at the request of a remote party.
	 */
	public const string INFO_SESSION_TERMINATED_BY_REMOTE = "8";
	
	/**
	 * Invite response with 403 because app is blacklisted.	
	 */
	public const string INFO_FORBIDDEN_UNKNOWN_APPID = "1001";
	
	/**
	 * Invite response with 403 because of invalid signature.
	 */
	public const string INFO_FORBIDDEN_INVALID_SIGNATURE = "1002";
	
	/**
	 * Invite response with 403 because of restricted use.
	 */
	public const string INFO_FORBIDDEN_RESTRICTED_USE = "1003";
	
	/**
	 * Invite response with 403 because of unknown app id.
	 */
	public const string INFO_FORBIDDEN_BLACKLISTED_APPID = "1020";
	
	/**
	 * Invite response with 403 because originating user is blacklisted.
	 */
	public const string INFO_FORBIDDEN_BLACKLISTED_USER_A = "1021";
	
	/**
	 * Invite response with 403 because receiving user is blacklisted.
	 */
	public const string INFO_FORBIDDEN_BLACKLISTED_USER_B = "1022";
	
	/**
	 * Invite response with 403 because msisdn is blacklisted.
	 */
	public const string INFO_FORBIDDEN_BLACKLISTED_MSISDN = "1023";
	
	/**
	 * Invite response with 403 for some unknown reason.
	 */
	public const string INFO_FORBIDDEN_UNKNOWN = "2000";
}
