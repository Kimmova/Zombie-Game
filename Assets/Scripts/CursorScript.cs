using UnityEngine;
using System.Collections;



public class CursorScript : MonoBehaviour {

	public Texture2D crosshair;
	int cursorSizeX = 64;
	int cursorSizeY = 64;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	public void OnGUI(){

		GUI.DrawTexture(new Rect(Event.current.mousePosition.x - cursorSizeX/2, Event.current.mousePosition.y - cursorSizeY/2, cursorSizeX, cursorSizeY), crosshair);

	}
}
