using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System.IO;

// This class helps update the jar when it is rebuilt by Eclipse
[InitializeOnLoad]
public class AndroidLibCopyHelper 
{
	static float checkFrequency = 5f;
	static double nextCheckTime;
	
	static AndroidLibCopyHelper()
	{
		// Only allow this callback to get added once
		if (EditorApplication.update == null
			|| !EditorApplication.update.GetInvocationList().Contains(Update as EditorApplication.CallbackFunction))
			EditorApplication.update += Update;
	}
	
	static void Update()
	{
		if (EditorApplication.timeSinceStartup > nextCheckTime)
		{
			string sourceFileName = "../jibe-android-wrapper/bin/jibeunityplugin.jar";
			string destFileName = "Assets/Plugins/Android/libs/jibeunityplugin.jar";
			if (File.GetLastWriteTime(sourceFileName) > File.GetLastWriteTime(destFileName))
			{
				Debug.Log("Updated Jibe Library @ " + System.DateTime.Now);
				File.Copy(sourceFileName, destFileName, true);
			}
			nextCheckTime = EditorApplication.timeSinceStartup + checkFrequency;
		}
	}
}