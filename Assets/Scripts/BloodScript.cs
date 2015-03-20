using UnityEngine;
using System.Collections;

public class BloodScript : MonoBehaviour {

	public float bloodTimer;
	public float time ;

	void Start(){
		InvokeRepeating ("Deteriorate", 0, time);
	}

	void Deteriorate(){

		Color color = renderer.material.color;
		color.a -= 100 / bloodTimer / 100;
		renderer.material.color = color;
		if (color.a <= 0) {
			Destroy (gameObject);
		}
	}
}
