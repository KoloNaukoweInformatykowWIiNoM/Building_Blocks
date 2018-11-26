using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ####################################################################################
public class BlockCreate : MonoBehaviour {

	public		Canvas			m_ui;
	public		GameObject 		Map;

	public		int				ActiveItem		=	02;

	// --------------------------------------------------------------------------------
	void Start () {
		
	}

	// --------------------------------------------------------------------------------
	void putBlock( Ray selection ) {
		RaycastHit block;

		if (Physics.Raycast (selection, out block, 6.5f)) {
			Vector3 bP = block.transform.position;
			Vector3	hP = bP - block.point;

			hP.x = Mathf.Abs(hP.x);
			hP.y = Mathf.Abs(hP.y);
			hP.z = Mathf.Abs(hP.z);

			if (hP.x > hP.z && hP.x > hP.y) { bP.x -= (int) Mathf.RoundToInt(selection.direction.x); }
			else if (hP.y > hP.x && hP.y > hP.z) { bP.y -= (int) Mathf.RoundToInt(selection.direction.y); }
			else { bP.z -= (int) Mathf.RoundToInt(selection.direction.z); }

			if ( Map.GetComponent<Generator>().world [(int)bP.x, (int)bP.y, (int)bP.z] != null ) { return; } 
			var newBlock = Map.GetComponent<Generator>().createBlock( ActiveItem, bP );
			Map.GetComponent<Generator>().world [(int)bP.x, (int)bP.y, (int)bP.z] = new Block (ActiveItem, true, newBlock);
		}

	}

	// --------------------------------------------------------------------------------
	void Update () {
		if (!m_ui.GetComponent<UIScript>().mouseLock) { return; }

		if (Input.GetMouseButtonDown (1)) {
			Ray selection = Camera.main.ScreenPointToRay( new Vector3(Screen.width/2.0f, Screen.height/2.0f, 0 ) );
			putBlock( selection );				
		}
	}

}
// ####################################################################################