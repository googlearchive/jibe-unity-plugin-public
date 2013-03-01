/*
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
package jibe.sdk.client.unity;

/**
 * JibeReceiveThread is a wrapper class which will be passed back to Unity.
 * Having a specific object with methods was necessary, so that Unity could get data back from the thread.
 * 
 */

public class JibeReceiveThread extends Thread
{
	Payload dataReceived;
	
	public JibeReceiveThread(Payload data, Runnable runnable) {
		super(runnable);
		dataReceived = data;
	}
	
	public byte[] getDataReceived() {
		return dataReceived.getBuffer();
	}
	
	public int getDataLength() {
		return dataReceived.getLength();
	}
}