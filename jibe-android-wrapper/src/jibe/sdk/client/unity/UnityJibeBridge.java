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

import java.io.IOException;
import java.util.ArrayList;
import java.util.Stack;

import jibe.sdk.client.JibeServiceListener.ConnectFailedReason;
import jibe.sdk.client.apptoapp.Config;
import jibe.sdk.client.simple.SimpleApi;
import jibe.sdk.client.simple.SimpleApiStateListener;
import jibe.sdk.client.simple.SimpleConnectionStateListener;
import jibe.sdk.client.simple.arena.ArenaHelper;
import jibe.sdk.client.simple.call.AudioCallConnection;
import jibe.sdk.client.simple.myprofile.MyProfileHelper;
import jibe.sdk.client.simple.myprofile.MyProfileHelper.OnlineStateListener;
import jibe.sdk.client.simple.session.DatagramSocketConnection;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.util.Log;
import android.widget.Toast;

import com.unity3d.player.UnityPlayer;

/**
 * UnityJibeBridge is a class that works in the background as a bridge between Unity and the Jibe SDK on the 
 * Android side. It takes care of all of the instantiation of Jibe objects that a Unity programmer may need
 * and signals Unity when callbacks occur.
 */

public class UnityJibeBridge  {
	public static volatile UnityJibeBridge instance = null;
	
	private static final String TAG = UnityJibeBridge.class.getName();
	private static volatile Stack<Intent> intentStack = new Stack<Intent>();
	private static volatile ArrayList<UnityConnection> connections = new ArrayList<UnityConnection>(); 
	private Activity activity;
	
	/**
	 * Obtains the singleton instance for Unity to communicate with Jibe
	 */
	public static UnityJibeBridge getInstance() {
		if (instance == null) {
			synchronized(UnityJibeBridge.class) {
				if (instance == null) 
					instance = new UnityJibeBridge();
			}
		}
		
		return instance;
	}
	
	/**
	 * The current UnityPlayerActivity is cached, so that a context is available when calls to Jibe are made. 
	 */
	private UnityJibeBridge() {
		System.out.println("Constructor is called!");
		activity = UnityPlayer.currentActivity;
	}
	
	public Context getContext() {
		return activity.getApplicationContext();
	}
	
	/**
	 *  Sets the application ID and secret, which will restrict the applications with which this application 
	 *  will communicate.
	 * 
	 * @param applicationId
	 * @param applicationSecret
	 */
	public void setApptoAppIdentifier(String applicationId, String applicationSecret) {
		Log.d(TAG, "app: " + applicationId + " secret: " + applicationSecret);
		Config.getInstance().setAppToAppIdentifier(applicationId, applicationSecret);
	}
	
	/**
	 * Create a new DatagramSocketConnection and return the instance
	 * 
	 * @return DatagramSocketConnection
	 */	
	public DatagramSocketConnection datagramSocketConnection(final String unityReceiver) {
		SimpleConnectionStateListener simpleConnectionStateListener = new SimpleConnectionStateListener() {
			@Override
			public void onInitializationFailed(SimpleApi arg0, ConnectFailedReason arg1) {
				//Calls the corresponding callback function in Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onInitializationFailed", TAG + " onInitializationFailed");
			}

			@Override
			public void onInitialized(SimpleApi arg0) {
				//Calls the corresponding callback function in Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onInitialized", TAG + " onInitialized");
			}

			@Override
			public void onStartFailed(SimpleApi source, int info) {
				//Calls the corresponding callback function in Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onStartFailed", Integer.toString(info));
			}

			@Override
			public void onStarted(SimpleApi arg0) {
				//Calls the corresponding callback function in Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onStarted", TAG + " onStarted");				
			}

			@Override
			public void onTerminated(SimpleApi source, int info) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onTerminated", Integer.toString(info));
			}
		};
		
		UnityDatagramSocketConnection connection = new UnityDatagramSocketConnection(activity.getApplicationContext(), simpleConnectionStateListener, unityReceiver);
		synchronized (UnityJibeBridge.class) {
			connections.add(connection);
		}
		return connection;
	}
	
	/**
	 * Create a new ArenaHelper and return the instance
	 * 
	 * @return ArenaHelper
	 */	
	public ArenaHelper arenaHelper(final String unityReceiver) {
		SimpleApiStateListener simpleApiStateListener = new SimpleApiStateListener() {
			@Override
			public void onInitializationFailed(SimpleApi arg0, ConnectFailedReason arg1) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onInitializationFailed", TAG + " onInitializationFailed");
			}

			@Override
			public void onInitialized(SimpleApi arg0) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onInitialized", TAG + " onInitialized");
				
			}
		};
		
		return new ArenaHelper(activity.getApplicationContext(), simpleApiStateListener);	
	}
	
	/**
	 * Create a new MyProfileHelper and return the instance
	 * 
	 * @return MyProfileHelper
	 */	
	public MyProfileHelper myProfileHelper(final String unityReceiver) {
		SimpleApiStateListener simpleApiStateListener = new SimpleApiStateListener() {
			@Override
			public void onInitializationFailed(SimpleApi arg0, ConnectFailedReason arg1) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onInitializationFailed", TAG + " onInitializationFailed");
			}

			@Override
			public void onInitialized(SimpleApi arg0) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onInitialized", TAG + " onInitialized");
			}
		};
		
		OnlineStateListener onlineStateListener = new OnlineStateListener() {
			@Override
			public void onOnlineStateChanged(boolean isOnline) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onOnlineStateChanged", Boolean.toString(isOnline));
			}
		};
		
		return new MyProfileHelper(activity.getApplicationContext(), simpleApiStateListener, onlineStateListener);	
	}
	
	/**
	 * Create a new AudioCallConnection and return the instance
	 * 
	 * @return AudioCallConnection
	 */	
	public AudioCallConnection audioCallConnection(final String unityReceiver)
	{
		SimpleConnectionStateListener simpleConnectionStateListener = new SimpleConnectionStateListener() {
			@Override
			public void onInitializationFailed(SimpleApi arg0, ConnectFailedReason arg1) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onInitializationFailed", TAG + " onInitializationFailed");
				
			}

			@Override
			public void onInitialized(SimpleApi arg0) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onInitialized", TAG + " onInitialized");
				
			}

			@Override
			public void onStartFailed(SimpleApi source, int info) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onStartFailed", Integer.toString(info));
			}

			@Override
			public void onStarted(SimpleApi arg0) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onStarted", TAG + " onStarted");
				
			}

			@Override
			public void onTerminated(SimpleApi source, int info) {
				//Calls the corresponding callback function on Unity
				UnityPlayer.UnitySendMessage(unityReceiver, "onTerminated", Integer.toString(info));
			}
			
		};
		
		UnityAudioCallConnection connection = new UnityAudioCallConnection(activity.getApplicationContext(), simpleConnectionStateListener, unityReceiver);
		synchronized (UnityJibeBridge.class) {
			connections.add(connection);
		}
		return connection;
	}
	
	/**
	 * Send data through a DatagramSocketConnection instance
	 */
	public Thread sendData(final DatagramSocketConnection connection, final byte[] buffer)
	{
		return sendData(connection, buffer, 0, buffer.length);
	}
	
	/**
	 * Send a subset of data through a DatagramSocketConnection instance
	 */
	public Thread sendData(final DatagramSocketConnection connection, final byte[] buffer, final int offset, final int length)
	{
		Thread thread = new Thread(new Runnable() {
			@Override
			public void run() {
				try {
//					Log.d(TAG, "Length: " + buffer.length);
//					for (byte b : buffer)
//						Log.d(TAG, "Byte: " + b);
					connection.send(buffer, offset, length);
				} catch (IOException e) {
					// fail
				}
			}
		});
		thread.start();
		return thread;
	}
	
	/**
	 * Receive data through a DatagramSocketConnection instance
	 */
	public Thread receiveData(final DatagramSocketConnection connection, final int bufferSize) {
		Log.d(TAG, "Buffer size: " + bufferSize);		
		final Payload dataReceived = new Payload(new byte[bufferSize]);
		JibeReceiveThread thread = new JibeReceiveThread(dataReceived, new Runnable() {		
			@Override
			public void run() {
				try {					
					dataReceived.setLength(connection.receive(dataReceived.getBuffer()));					
//					Log.d(TAG, "Length: " + bytesReceived);
//					for (int i = 0; i < bytesReceived; i++)
//						Log.d(TAG, "Byte: " + dataReceived[i]);
				} catch (IOException e) {
					// fail
					Log.d(TAG, "EXCEPTION: " + e.getMessage());
				}
			}
		});
		thread.start();
		return thread;
	}
	
	/**
	 * Show a message using Android Toast
	 * (useful for debug purposes where you want to display information to the screen)
	 **/
	public void showMessage(final String message){
		System.out.println("Message From Unity: "+message);
		
		activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
            	Toast.makeText(activity.getApplicationContext(), message, Toast.LENGTH_SHORT).show();
            }
        });
	}
	
	/**
	 * Return the last intent received by the BroadcastReceiver. 
	 * 
	 * @return Last Intent
	 */
	public Intent peekLastIntent()
	{
		synchronized(UnityJibeBridge.class) {
			if (!intentStack.empty())
				return intentStack.peek();
			else
				return null;
		}
	}
	
	/**
	 * Return the last intent received by the BroadcastReceiver and remove it from the stack. 
	 * 
	 * @return Last Intent
	 */
	public Intent popLastIntent()
	{
		synchronized(UnityJibeBridge.class) {
			if (!intentStack.empty())
				return intentStack.pop();
			else
				return null;
		}
	}
	
	/**
	 * Store a new Intent that comes from broadcast receiver. 
	 */
	public static void addIntent(Intent broadcastReceiverIntent)
	{ 
		synchronized (UnityJibeBridge.class) {
			intentStack.push(broadcastReceiverIntent);
		
			System.out.println("Connection count: " + connections.size());
			for (UnityConnection c : connections)
			{
				boolean result = c.canProcessIntent(broadcastReceiverIntent);
				System.out.println("canProcess " + result);
				if (result)
					UnityPlayer.UnitySendMessage(c.getUnityReceiver(), "onNewIntent", "");
			}
		}		
	}
}
