using UnityEngine;
using System.Collections;

public class LimitMap : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Bullet") {
			Destroy (col.gameObject);
		}
	}
}
