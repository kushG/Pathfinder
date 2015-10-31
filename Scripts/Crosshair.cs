using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	Ray ray;
	public Camera fpsCam;
	public GameObject FPS;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		var directionRay = transform.forward;
		ray = fpsCam.ScreenPointToRay(transform.position);
		Debug.Log (ray.origin);
		Debug.Log (ray.direction);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity)){

			if(Input.GetMouseButton(0)){
				hit.transform.parent = FPS.transform;
			}
			Debug.Log (hit.transform.gameObject.name);
		}
	
	}
}
