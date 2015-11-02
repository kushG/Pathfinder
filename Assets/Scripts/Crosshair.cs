using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	Ray ray;
	public Camera fpsCam;
	public GameObject FPS;
	GameObject hitobject;
	Transform parentBeforeGrab;
	Vector3 beforeGrabPos;
	// Use this for initialization
	void Start () {
		hitobject = gameObject;
	    parentBeforeGrab = gameObject.transform.parent;
		beforeGrabPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		var directionRay = transform.forward;
		ray = fpsCam.ScreenPointToRay(transform.position);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
			if(Input.GetMouseButton(0) && hit.transform.gameObject.tag == "movable"){
				parentBeforeGrab = hit.transform.parent;
				hitobject = hit.transform.gameObject;
				beforeGrabPos = hit.transform.position;
				hit.transform.parent = FPS.transform;
				Debug.Log("On click");
			}
			else{
				hitobject.transform.position = beforeGrabPos;
				hitobject.transform.parent = parentBeforeGrab;
				Debug.Log(hitobject + " hit object");

			}
		}
	
	}
}
