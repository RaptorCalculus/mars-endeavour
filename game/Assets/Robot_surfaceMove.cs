using UnityEngine;
using System.Collections;

public class Robot_surfaceMove : MonoBehaviour {
	public float moveSpeed = 2.0f; // Units per second
	public float speed = 5;
	public bool moving = false;
	//public Transform dest;
	public Vector3 target;
	public float jitter = .1f;
	public bool selected = false;
	public bool inTube = false;
	public int repairVal = 1;
	public float repairWait = 1;
	float nextRepair = 0;
	string tubeName;
	int tube;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void LateUpdate(){
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), Time.deltaTime * 6);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target - transform.position), Time.deltaTime * 6);
	}

	void Update () {

		//if (moving == true && transform.position != dest.position) {
			
		if (moving == true && transform.position != target) {
			
			//transform.LookAt(dest);
			//transform.LookAt(target);

		 	
		

			float step = speed * Time.deltaTime;
			Debug.Log ("TARGET   " + target);
			//transform.position = Vector3.MoveTowards (transform.position, dest.position, step);
			transform.position = Vector3.MoveTowards (transform.position, target, step);


			//Vector3 targetDir = target - transform.position;
			//float stepR = speed * Time.deltaTime;
			//Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepR, 0.0F);
			//Debug.DrawRay(transform.position, newDir, Color.red);
			//transform.rotation = Quaternion.LookRotation(newDir);

								//	jitter = jitter * -1;
								//	Vector3 temp = new Vector3(0,jitter,0);
								//	Debug.Log ("Jitter:" + jitter);
								//	transform.position += temp; 
		}
	}

	void OnCollisionEnter(Collision other) {
		Debug.Log ("Collision with  " + other.gameObject.tag);
	}

	void OnTriggerEnter(Collider other) {
		
		Debug.Log ("Trigger with  " + other.tag);
		
		if (other.tag == "TubeEntrance") {
				inTube = true;
				gameObject.transform.GetChild (1).gameObject.SetActive (true);
				moving = false;
				if (other.name == "LavaTube1") {
						tube = 0;
				}
				if (other.name == "LavaTube2") {
						tube = 1;
				}
				if (other.name == "LavaTube3") {
						tube = 2;
				}
				GameObject.Find ("GameController").GetComponent<GameController> ().EnterTube (tube, gameObject);
		}

		if (other.tag == "TubeExit") {
			inTube = false;
			gameObject.transform.GetChild (1).gameObject.SetActive (false);
			moving = false;

			if (other.name == "Exit1") {
				tubeName = "LavaTube1";
			}
			if (other.name == "Exit2") {
				tubeName = "LavaTube2";
			}
			if (other.name == "Exit3") {
				tubeName = "LavaTube3";
			}
			GameObject.Find ("GameController").GetComponent<GameController> ().ExitTube (tubeName, gameObject);
		}

	}

	void OnTriggerStay(Collider other){
		Debug.Log ("Rover Colliding with   " + other.name);
		if (other.tag == "Building") {
			BuildingManager bm = other.collider.GetComponent<BuildingManager>();
			if (Time.time > nextRepair){
				nextRepair = Time.time + repairWait;
				if (bm.HP != bm.maxHP){
					bm.repair(repairVal);
					
				}		
			}
		}
	}

}