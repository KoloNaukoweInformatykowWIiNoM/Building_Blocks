using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ####################################################################################
public class BlockDestroy : MonoBehaviour {

	public		Canvas			m_ui;
	public		GameObject 		Map;

	// --------------------------------------------------------------------------------
	void Start () {
		
	}

	// --------------------------------------------------------------------------------
	void deleteBlock( Ray selection ) {
		RaycastHit block;

		if (Physics.Raycast (selection, out block, 6.5f)) {
			Vector3 bP = block.transform.position;

			if (Map.GetComponent<Generator>().world[ (int)bP.x, (int)bP.y, (int)bP.z ] == null) { return; }
			if (Map.GetComponent<Generator>().world[ (int)bP.x, (int)bP.y, (int)bP.z ].type == 1) { return; } 

			Map.GetComponent<Generator>().world[ (int)bP.x, (int)bP.y, (int)bP.z ] = null;
			Destroy( block.transform.gameObject );
			searchNeighbour( bP );
		}

	}

	// --------------------------------------------------------------------------------
	void searchNeighbour( Vector3 bP ) {

		for ( int x=(-1); x<=1; x++ ) {
			for ( int y=(-1); y<=1; y++ ) {
				for ( int z=(-1); z<=1; z++ ) {

					if ( !(x==0 && y==0 && z==0) ) {
						Vector3 block = new Vector3( bP.x+x, bP.y+y, bP.z+z );
						showBlock( block );
					}

				}
			}
		}

	}

	// --------------------------------------------------------------------------------
	void showBlock( Vector3 blockPos ) {
		if ( blockPos.x < 0 || blockPos.x >= Map.GetComponent<Generator>().width ) { return; }
		if ( blockPos.y < 0 || blockPos.y >= Map.GetComponent<Generator>().height ) { return; }
		if ( blockPos.z < 0 || blockPos.z >= Map.GetComponent<Generator>().depth ) { return; }

		if (Map.GetComponent<Generator>().world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ] == null) { return; }

		if (!Map.GetComponent<Generator>().world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ].visible){
			int type = Map.GetComponent<Generator>().world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ].type;

			var block = Map.GetComponent<Generator>().createBlock( type, blockPos );
			Map.GetComponent<Generator>().world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ].visible = true;
			Map.GetComponent<Generator> ().world [(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].block = block;
		}
	}

	// --------------------------------------------------------------------------------
	void Update () {
		if (!m_ui.GetComponent<UIScript>().mouseLock) { return; }

		if (Input.GetMouseButtonDown (0)) {
			Ray selection = Camera.main.ScreenPointToRay( new Vector3(Screen.width/2.0f, Screen.height/2.0f, 0 ) );
			deleteBlock( selection );				
		}
	}

}
// ####################################################################################