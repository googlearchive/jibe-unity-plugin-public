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

using System.Collections.Generic;

/// @cond
/// Internal note: C# doesn't have the equivalent of Java enumerations, however, there is a close equivalent
/// offered on StackOverflow @ http://stackoverflow.com/questions/469287/c-sharp-vs-java-enum-for-those-new-to-c
/// 
/// The solution offered on StackOverflow is verbose enough that it does not offer much benefit over simple strings.
/// So, it was decided for simple enumerations, such as SimpleConnection.State and AudioEncodingOption to stick
/// with strings. The above solution is reserved for more complex enumerations, such as LegacyCameraRotation, which
/// has additional methods along with the enumeration values.
/// @endcond

/**
 * The current state of a connection 
 */
public class SimpleConnectionState 
{
	/* The connection has been disposed */
	public const string DISPOSED = "DISPOSE";
	
	/* The connection is in the processed of being disposed */
	public const string DISPOSING = "DISPOSING";
	
	/* The connection is idle */
	public const string IDLE = "IDLE";
	
	/* The connection is being monitored */
	public const string MONITORING = "MONITORING";
	
	/* The connection has started */
	public const string STARTED = "STARTED";
	
	/* The connection is in the process of starting */
	public const string STARTING = "STARTING";
	
	/* The connection is in the process of stopping */
	public const string STOPPING = "STOPPING";
}