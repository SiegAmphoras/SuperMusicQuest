using UnityEngine;
using System.Collections;

public class KeyCount : MonoBehaviour {
	public GUIStyle customGuiStyle;
	public Transform target;
	private playerController pC;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		pC = player.GetComponent<playerController> ();
	}

	void OnGUI(){
		Vector3 point = Camera.main.WorldToScreenPoint (target.position);
		GUI.contentColor = Color.black;;
		GUI.Label (new Rect(point.x,Screen.height-point.y,250,250),pC.keys.ToString(),customGuiStyle);
	}
}
