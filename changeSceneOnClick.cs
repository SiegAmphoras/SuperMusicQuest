using UnityEngine;
using System.Collections;

public class changeSceneOnClick : MonoBehaviour {
	
	public string levelToLoad = "";
	
	void Start(){
		Debug.Log(levelToLoad);
		GameControl.control.reset ();
	}
	
	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			Application.LoadLevel(levelToLoad);
		}
	}
}
