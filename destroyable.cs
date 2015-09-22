using UnityEngine;
using System.Collections;

public class destroyable : MonoBehaviour {

	public int destroyID;
	
	public void destroy(int itemID) {
		if(itemID == destroyID) {
			Destroy(gameObject);
		}
	}
}
