using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ################################################################################
public class PlayerController : MonoBehaviour {

	public	float	speed	=	10;

	// ----------------------------------------------------------------------------
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
	}

	// ----------------------------------------------------------------------------
	void Update () {
		float translation = Input.GetAxis ("Vertical") * speed;
		float starffe = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;

		transform.Translate (starffe, 0, translation);

		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}
	}

	// ----------------------------------------------------------------------------
}
// ################################################################################