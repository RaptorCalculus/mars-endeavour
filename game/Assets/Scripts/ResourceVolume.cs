using UnityEngine;
using System.Collections;

public class ResourceVolume : MonoBehaviour {

	public float volume = 1;
	public float respawnTimeSec = 180;
	float volumeStart;

	void Start () {
		//DepleteOre ();
		volumeStart = volume;
	}
	
	void Update () {
		if (volume <= 0) {
			volume = volumeStart;
			DepleteOre();
		}
	}

	public void DepleteOre(){

		if (transform.childCount > 0) {
			transform.GetChild (0).renderer.enabled = false;
			transform.GetChild (0).collider.enabled = false;
		} else {
			renderer.enabled = false;
			collider.enabled = false;
		}
		Invoke ("respawn", respawnTimeSec);

	}

	public void respawn(){
		volume = 1;
		if (transform.childCount > 0) {
			transform.GetChild (0).renderer.enabled = true;
			transform.GetChild (0).collider.enabled = true;
		} else {
			renderer.enabled = true;
			collider.enabled = true;
		}
	}
}
