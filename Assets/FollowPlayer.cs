using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject objectToFollow;


	void Update () {
		var newCamPos = objectToFollow.transform.position;
		newCamPos.z = transform.position.z;
		
		transform.position = newCamPos;
	}
}
