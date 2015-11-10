using UnityEngine;
using System.Collections;

public class DoorCollider : MonoBehaviour {
	Transform parentbeforeCollision;
	public Transform fpstransform;
	// Use this for initialization
	void Start () {
		parentbeforeCollision = transform;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if(col.collider.transform.parent != parentbeforeCollision){
			col.collider.transform.SetParent(parentbeforeCollision);
		}
	}

}
