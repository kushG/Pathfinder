using UnityEngine;
using System.Collections;

public class SeeMovable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.E)){
			GameObject[] movables = GameObject.FindGameObjectsWithTag("movable");
			foreach(GameObject movable in movables){
				movable.GetComponent<Renderer>().material.color = Color.yellow;
			}
		}
	}
}
