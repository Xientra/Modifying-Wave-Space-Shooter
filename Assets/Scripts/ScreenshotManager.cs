using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
	public int superSize = 1;
	private int index = 0;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Print))
		{
			MakeScreenshot();
		}
#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(1))
		{
			EditorApplication.isPaused = !EditorApplication.isPaused;
		}
#endif
	}

	public void MakeScreenshot()
	{
		string filename;
		do
		{
			filename = "Screenshot_" + index + ".png";
			index++;
		}
		while (File.Exists(filename));

		Debug.Log("Screenshot done!");
		ScreenCapture.CaptureScreenshot(filename, superSize);
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(ScreenshotManager))]
public class ScreenshotManagerEditor: Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		var sm = (ScreenshotManager)target;
		if (GUILayout.Button("Take Screenshot"))
		{
			sm.MakeScreenshot();
		}
	}
}
#endif