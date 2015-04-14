using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

	public Texture btnTexture;

	void OnGUI() {
		if (!btnTexture) {
			Debug.LogError("Please assign a texture on the inspector");
			return;
		}
		if (GUI.Button (new Rect (500, 400, 200, 100), "Retry!")) {
			Application.LoadLevel ("MainScene");
			
			Globals.ResetVariables();
		}
	}
}
