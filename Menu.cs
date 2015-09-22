using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private bool showMenu;

	void Update() {
		if(Input.GetButtonDown("Menu")) {
			showMenu = !showMenu;
		}
	}
	
	void OnGUI() {
		if(showMenu) {
			GUI.Box (new Rect (10, 10, 100, 50), "Menu");
			if(GUI.Button (new Rect(20, 35, 80, 20), "Quit")) {
				Application.Quit();
			}
		}
	}
}
