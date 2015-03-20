using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	private GameObject objectToFollow;

	void Start() {
		objectToFollow = GameObject.Find ("Player").gameObject;
	}

	void Update () {
		var newCamPos = objectToFollow.transform.position;
		newCamPos.z = transform.position.z;
		
		transform.position = newCamPos;
	}
}
