using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	Ray ray;
	public Camera fpsCam;
	public GameObject FPS;
	GameObject hitobject;
	Transform parentBeforeGrab;
	bool objectGrabbed = false;
	public ParticleSystem explosion;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		hitobject = gameObject;
	    parentBeforeGrab = gameObject.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		ray = fpsCam.ScreenPointToRay(transform.position);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1)){
			if(Input.GetMouseButtonDown(0)){
				if(hit.transform.gameObject.tag == "Door" ||
				   hit.transform.gameObject.tag == "Glass"||
				   hit.transform.gameObject.tag == "movable" ){
					parentBeforeGrab = hit.transform.parent;
					hitobject = hit.transform.gameObject;
					objectGrabbed = true;
					if(hit.transform.gameObject.GetComponent<Rigidbody>() != null)
					{
						hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
					}
					Debug.Log(hitobject + "Hit Object");

					hit.transform.parent = FPS.transform;				
				}

			}
			if(Input.GetMouseButtonUp(0) && objectGrabbed){
				hitobject.transform.parent = parentBeforeGrab;
				if(hitobject.transform.gameObject.GetComponent<Rigidbody>() != null)
				{
					hitobject.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
				}
			}
		}
	
	}
	
}
