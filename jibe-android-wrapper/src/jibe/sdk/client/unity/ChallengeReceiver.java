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

import java.util.List;

import jibe.sdk.client.JibeIntents;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

/**
 * SessionReceiver is a BroadcastReceiver that catches Jibe-related intents sent from Arena. 
 * Subsequently, it will start the main activity.
 * 
 */

public class ChallengeReceiver extends BroadcastReceiver {
	
	private static final String LOG_TAG = ChallengeReceiver.class.getName();
	
	
	@Override
	public void onReceive(final Context context, Intent intent) {		
		// Need to check for action==null since as of Android 4.x applications need to be activated to before they can receive
		// broadcasts which are not explicitly directed at a class. This is done via a "dummy intent".
		if (intent.getAction() == null) 
		{
    		return;
    	}
	
		try 
		{	
			Log.d(LOG_TAG, "Broadcast receiver is sending intent. Action = " + intent.getAction());
			long currentSessionId = intent.getLongExtra(JibeIntents.EXTRA_SESSION_ID, -1);				
			Log.d(LOG_TAG, "Broadcast receiver is sending intent. Session = " + currentSessionId);
			
			UnityJibeBridge.addIntent(intent);
			
			if (UnityPlayer.currentActivity == null || !UnityPlayer.currentActivity.hasWindowFocus()){
				Intent i = new Intent(context, Class.forName(getLauncherClassName(context)));
				i.setAction(Intent.ACTION_MAIN);
				i.addCategory(Intent.CATEGORY_LAUNCHER);
				i.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_SINGLE_TOP);
				context.startActivity(i);
			}
		} 
		catch (ClassNotFoundException e) 
		{
			e.printStackTrace();
		}
	}
	
	/**
	 * This method research what is the name of the first activity
	 * that user has set on the manifest as fist activity will be
	 * lunched. 
	 * 
	 * @return The name of the Main activity
	 */
	private String getLauncherClassName(Context context)
	{
		Intent intent = new Intent(Intent.ACTION_MAIN);
        intent.addCategory(Intent.CATEGORY_LAUNCHER);
		PackageManager pm =  context.getPackageManager();
		
		List<ResolveInfo> infos = pm.queryIntentActivities(intent, PackageManager.GET_RESOLVED_FILTER);
		
		for(ResolveInfo inf : infos)
		{
			if(inf.activityInfo.packageName.equalsIgnoreCase(context.getPackageName()))
				return inf.activityInfo.name;
		}
		
		return null;
	}
	
}
