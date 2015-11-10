using UnityEngine;
using System.Collections;

public class ColliderTriggers : MonoBehaviour {
	bool snap = false;
	GameObject door;
	public Mesh cube;
	public bool doorOpenClose = false;
	public int doorOpenDirection = 1;
	public bool isSnapVertical = false;
	public float snapOffset = 0.2f;
	public bool snapX = false;
	public bool snapY = false;
	public bool snapZ = true;
	public bool rotateY = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(snap)
		{
			//Snapping
			if(!doorOpenClose){
				//Reset Parent/child
				if(door.transform.parent != null){
					if(door.transform.parent.gameObject.name == "Door hinge"){
						Transform hingedoor = door.transform.parent;
						door.transform.parent = hingedoor.parent;
						hingedoor.parent = door.transform;
					}
				}

				if(snapX){
					float tempX = transform.position.x - snapOffset * doorOpenDirection;
					door.transform.position = Vector3.MoveTowards(door.transform.position, transform.position, 2.05f * Time.deltaTime);
					door.transform.position = new Vector3(tempX, door.transform.position.y, door.transform.position.z);
				}
				else if(snapY){
					float tempY = transform.position.y - snapOffset * doorOpenDirection;
					door.transform.position = Vector3.MoveTowards(door.transform.position, transform.position, 2.05f * Time.deltaTime);
					door.transform.position = new Vector3(door.transform.position.x, tempY, door.transform.position.z);
				}
				else if(snapZ && !rotateY){
					float tempZ = transform.position.z - snapOffset * doorOpenDirection;
					door.transform.position = Vector3.MoveTowards(door.transform.position, transform.position, 2.05f * Time.deltaTime);
					door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y, tempZ);
				}

				if(isSnapVertical){
					door.transform.rotation = Quaternion.identity;				
					door.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));
					GetComponent<MeshFilter>().mesh.Clear ();
				}
				else if(rotateY && snapZ){
					door.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
					float tempZ = transform.position.z - snapOffset * doorOpenDirection;
					door.transform.position = Vector3.MoveTowards(door.transform.position, transform.position, 2.05f * Time.deltaTime);
					door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y, tempZ);
				}
				else{
					door.transform.rotation = Quaternion.identity;				
				}
			    
			}

			if(door.gameObject.tag == "Glass"){
				if(Input.GetKeyDown (KeyCode.G)){
					Destroy (door);
				}
			}


			// Door Open/Close
			if(Input.GetKeyDown (KeyCode.E)){
				doorOpenClose = !doorOpenClose;
				if(doorOpenClose && door!= null){
					// MESH
					GetComponent<MeshFilter>().mesh.Clear ();
					//Door open
					if(door.transform.FindChild("Door hinge") !=null){
						Transform hingedoor = door.transform.FindChild("Door hinge").transform;
						door.transform.FindChild("Door hinge").transform.parent = door.transform.parent;
						door.transform.parent = hingedoor;
						hingedoor.transform.Rotate (0, -80f * doorOpenDirection, 0);
					}
				}
				else{
					if(door!=null){
						Transform hingedoor = door.transform.parent;
						hingedoor.transform.Rotate (0, 80f * doorOpenDirection, 0);		
					}
				}
			}
		}

	}

	void OnTriggerEnter(Collider col){
		Debug.Log (col.gameObject.tag);
		// Snap Door to Collider gameobject's position
		if(col.gameObject.tag == "Door" || col.gameObject.tag == "Glass"){
			//TODO: Change door to some generic name
			door = col.gameObject;
			snap = true;					                                
		}

	}

	void OnTriggerExit(Collider col){
		Debug.Log (col.gameObject.tag + "exit");
		// Snap Door to Collider gameobject's position
		if(col.gameObject.tag == "Door"|| col.gameObject.tag == "Glass"){
			door = col.gameObject;
			snap = false;	
			GetComponent<MeshFilter>().mesh = cube;
			if(rotateY){
				door.transform.rotation = Quaternion.identity;
			}
		}
		
	}
}
