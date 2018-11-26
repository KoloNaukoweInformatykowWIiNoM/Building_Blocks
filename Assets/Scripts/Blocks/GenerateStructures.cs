using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ####################################################################################
public class GenerateStructures : MonoBehaviour {

	// --------------------------------------------------------------------------------
	public void makeBuld( int type, int frequency ) {
		for ( int i=0; i<frequency; i++ ) {
			int x = Random.Range(10, GetComponent<Generator>().width - 10);
			int z = Random.Range(10, GetComponent<Generator>().depth - 10);
			int y = 0;

			for (int iy=0; iy<GetComponent<Generator>().height; iy++) {
				if ( GetComponent<Generator>().world[x,iy,z] == null ) { y = iy; break; }
			}

			switch (type) {
				case 1:
					makeTree01( x, y, z );
					break;
				case 2:
					makeBush01( x, y, z );
					break;
				case 3:
					makeHome01( x, y, z );
					break;
				default:
					break;
			}
		}
	}

	// --------------------------------------------------------------------------------
	// XXXXXXXXXXXXXXXXXX   XXXXXXXXXXXXXXX         XXXXXXXXXXXXXXXXXX      XXXXXXXXXXXXXXXXXX
	// XXXXXXXXXXXXXXXXXX   XXXXXXXXXXXXXXXXXX      XXXXXXXXXXXXXXX         XXXXXXXXXXXXXXX   
	//       XXXXXX         XXXXXX      XXXXXX      XXXXXX                  XXXXXX
	//       XXXXXX         XXXXXX      XXXXXX      XXXXXX                  XXXXXX
	//       XXXXXX         XXXXXXXXXXXXXXXXXX      XXXXXXXXXXXX            XXXXXXXXXXXX
	//       XXXXXX         XXXXXXXXXXXXXXX         XXXXXXXXXXXX            XXXXXXXXXXXX
	//       XXXXXX         XXXXXX      XXXXXX      XXXXXX                  XXXXXX
	//       XXXXXX         XXXXXX      XXXXXX      XXXXXX                  XXXXXX
	//       XXXXXX         XXXXXX      XXXXXX      XXXXXXXXXXXXXXX         XXXXXXXXXXXXXXX
	//       XXXXXX         XXXXXX      XXXXXX      XXXXXXXXXXXXXXXXXX      XXXXXXXXXXXXXXXXXX
	// --------------------------------------------------------------------------------
	void makeTree01( int x, int y, int z ) {
		
		determineBlock( x,		y,		z,		true, 09 );			// wood stage
		determineBlock( x,		y+1,	z,		true, 09 );			// wood stage
		determineBlock( x,		y+2,	z,		true, 09 );			// wood stage
		determineBlock( x,		y+3,	z,		true, 09 );			// wood stage
		determineBlock( x,		y+4,	z,		true, 09 );			// wood stage
		determineBlock( x,		y+5,	z,		true, 09 );			// wood stage
		determineBlock( x,		y+6,	z,		true, 09 );			// wood stage

		determineBlock( x-1,	y+4,	z,		true, 10 );			// stage I
		determineBlock( x+1,	y+4,	z,		true, 10 );			// stage I
		determineBlock( x,		y+4,	z-1,	true, 10 );			// stage I
		determineBlock( x,		y+4,	z+1,	true, 10 );			// stage I

		determineBlock( x-1,	y+5,	z,		true, 10 );			// stage II
		determineBlock( x+1,	y+5,	z,		true, 10 );			// stage II
		determineBlock( x,		y+5,	z-1,	true, 10 );			// stage II
		determineBlock( x,		y+5,	z+1,	true, 10 );			// stage II
		determineBlock( x-1,	y+5,	z-1,	true, 10 );			// stage II
		determineBlock( x-1,	y+5,	z+1,	true, 10 );			// stage II
		determineBlock( x+1,	y+5,	z-1,	true, 10 );			// stage II
		determineBlock( x+1,	y+5,	z+1,	true, 10 );			// stage II

		determineBlock( x-1,	y+6,	z,		true, 10 );			// stage III
		determineBlock( x+1,	y+6,	z,		true, 10 );			// stage III
		determineBlock( x,		y+6,	z-1,	true, 10 );			// stage III
		determineBlock( x,		y+6,	z+1,	true, 10 );			// stage III
		determineBlock( x-1,	y+6,	z-1,	true, 10 );			// stage III
		determineBlock( x-1,	y+6,	z+1,	true, 10 );			// stage III
		determineBlock( x+1,	y+6,	z-1,	true, 10 );			// stage III
		determineBlock( x+1,	y+6,	z+1,	true, 10 );			// stage III
		determineBlock( x-2,	y+6,	z,		true, 10 );			// stage III
		determineBlock( x+2,	y+6,	z,		true, 10 );			// stage III
		determineBlock( x,		y+6,	z-2,	true, 10 );			// stage III
		determineBlock( x,		y+6,	z+2,	true, 10 );			// stage III

		determineBlock( x,		y+7,	z,		true, 10 );			// stage IV
		determineBlock( x-1,	y+7,	z,		true, 10 );			// stage IV
		determineBlock( x+1,	y+7,	z,		true, 10 );			// stage IV
		determineBlock( x,		y+7,	z-1,	true, 10 );			// stage IV
		determineBlock( x,		y+7,	z+1,	true, 10 );			// stage IV
		determineBlock( x-1,	y+7,	z-1,	true, 10 );			// stage IV
		determineBlock( x-1,	y+7,	z+1,	true, 10 );			// stage IV
		determineBlock( x+1,	y+7,	z-1,	true, 10 );			// stage IV
		determineBlock( x+1,	y+7,	z+1,	true, 10 );			// stage IV
		determineBlock( x-2,	y+7,	z-1,	true, 10 );			// stage IV
		determineBlock( x+2,	y+7,	z-1,	true, 10 );			// stage IV
		determineBlock( x-2,	y+7,	z+1,	true, 10 );			// stage IV
		determineBlock( x+2,	y+7,	z+1,	true, 10 );			// stage IV
		determineBlock( x-1,	y+7,	z-2,	true, 10 );			// stage IV
		determineBlock( x+1,	y+7,	z-2,	true, 10 );			// stage IV
		determineBlock( x-1,	y+7,	z+2,	true, 10 );			// stage IV
		determineBlock( x+1,	y+7,	z+2,	true, 10 );			// stage IV

		determineBlock( x,		y+8,	z,		true, 10 );			// stage V
		determineBlock( x-1,	y+8,	z,		true, 10 );			// stage V
		determineBlock( x+1,	y+8,	z,		true, 10 );			// stage V
		determineBlock( x,		y+8,	z-1,	true, 10 );			// stage V
		determineBlock( x,		y+8,	z+1,	true, 10 );			// stage V
		determineBlock( x-1,	y+8,	z-1,	true, 10 );			// stage V
		determineBlock( x-1,	y+8,	z+1,	true, 10 );			// stage V
		determineBlock( x+1,	y+8,	z-1,	true, 10 );			// stage V
		determineBlock( x+1,	y+8,	z+1,	true, 10 );			// stage V
		determineBlock( x-2,	y+8,	z-1,	true, 10 );			// stage V
		determineBlock( x+2,	y+8,	z-1,	true, 10 );			// stage V
		determineBlock( x-2,	y+8,	z+1,	true, 10 );			// stage V
		determineBlock( x+2,	y+8,	z+1,	true, 10 );			// stage V
		determineBlock( x-1,	y+8,	z-2,	true, 10 );			// stage V
		determineBlock( x+1,	y+8,	z-2,	true, 10 );			// stage V
		determineBlock( x-1,	y+8,	z+2,	true, 10 );			// stage V
		determineBlock( x+1,	y+8,	z+2,	true, 10 );			// stage V

		determineBlock( x,		y+9,	z,		true, 10 );			// stage VI
		determineBlock( x-1,	y+9,	z,		true, 10 );			// stage VI
		determineBlock( x+1,	y+9,	z,		true, 10 );			// stage VI
		determineBlock( x,		y+9,	z-1,	true, 10 );			// stage VI
		determineBlock( x,		y+9,	z+1,	true, 10 );			// stage VI
		determineBlock( x-1,	y+9,	z-1,	true, 10 );			// stage VI
		determineBlock( x-1,	y+9,	z+1,	true, 10 );			// stage VI
		determineBlock( x+1,	y+9,	z-1,	true, 10 );			// stage VI
		determineBlock( x+1,	y+9,	z+1,	true, 10 );			// stage VI
		determineBlock( x-2,	y+9,	z,		true, 10 );			// stage VI
		determineBlock( x+2,	y+9,	z,		true, 10 );			// stage VI
		determineBlock( x,		y+9,	z-2,	true, 10 );			// stage VI
		determineBlock( x,		y+9,	z+2,	true, 10 );			// stage VI

		determineBlock( x,		y+10,	z,		true, 10 );			// stage VII
		determineBlock( x-1,	y+10,	z,		true, 10 );			// stage VII
		determineBlock( x+1,	y+10,	z,		true, 10 );			// stage VII
		determineBlock( x,		y+5,	z-1,	true, 10 );			// stage VII
		determineBlock( x,		y+5,	z+1,	true, 10 );			// stage VII
		determineBlock( x-1,	y+10,	z-1,	true, 10 );			// stage VII
		determineBlock( x-1,	y+10,	z+1,	true, 10 );			// stage VII
		determineBlock( x+1,	y+10,	z-1,	true, 10 );			// stage VII
		determineBlock( x+1,	y+10,	z+1,	true, 10 );			// stage VII

		determineBlock( x,		y+11,	z,		true, 10 );			// stage VIII
		determineBlock( x-1,	y+11,	z,		true, 10 );			// stage VIII
		determineBlock( x+1,	y+11,	z,		true, 10 );			// stage VIII
		determineBlock( x,		y+11,	z-1,	true, 10 );			// stage VIII
		determineBlock( x,		y+11,	z+1, 	true, 10 );			// stage VIII
	}

	// --------------------------------------------------------------------------------
	// XXXXXXXXXXXXXXX         XXXXXX      XXXXXX            XXXXXXXXX         XXXXXX      XXXXXX
	// XXXXXXXXXXXXXXXXXX      XXXXXX      XXXXXX         XXXXXXXXXXXXXXX      XXXXXX      XXXXXX
	// XXXXXX      XXXXXX      XXXXXX      XXXXXX      XXXXXXXXX               XXXXXX      XXXXXX
	// XXXXXX      XXXXXX      XXXXXX      XXXXXX      XXXXXX                  XXXXXX      XXXXXX
	// XXXXXXXXXXXXXXX         XXXXXX      XXXXXX         XXXXXXXXXXXX         XXXXXXXXXXXXXXXXXX
	// XXXXXXXXXXXXXXXXXX      XXXXXX      XXXXXX            XXXXXXXXXXXX      XXXXXXXXXXXXXXXXXX
	// XXXXXX      XXXXXX      XXXXXX      XXXXXX                  XXXXXX      XXXXXX      XXXXXX
	// XXXXXX      XXXXXX       XXXXX      XXXXX                XXXXXXXXX      XXXXXX      XXXXXX
	// XXXXXXXXXXXXXXXXXX        XXXXXXXXXXXXXX        XXXXXXXXXXXXXXX         XXXXXX      XXXXXX
	// XXXXXXXXXXXXXXX            XXXXXXXXXXXX            XXXXXXXXX            XXXXXX      XXXXXX
	// --------------------------------------------------------------------------------
	void makeBush01( int x, int y, int z ) {
		
		determineBlock( x,		y,		z,		true, 09 );			// wood stage I

		determineBlock( x,		y+1,	z,		true, 09 );			// wood stage II
		determineBlock( x+1,	y+1,	z,		true, 09 );			// wood stage II
		determineBlock( x-1,	y+1,	z,		true, 09 );			// wood stage II
		determineBlock( x,		y+1,	z+1,	true, 09 );			// wood stage II
		determineBlock( x,		y+1,	z-1,	true, 09 );			// wood stage II

		determineBlock( x+1,	y+2,	z,		true, 09 );			// wood stage III
		determineBlock( x+2,	y+2,	z,		true, 09 );			// wood stage III
		determineBlock( x-1,	y+2,	z,		true, 09 );			// wood stage III
		determineBlock( x-2,	y+2,	z,		true, 09 );			// wood stage III
		determineBlock( x,		y+2,	z+1,	true, 09 );			// wood stage III
		determineBlock( x,		y+2,	z+2,	true, 09 );			// wood stage III
		determineBlock( x,		y+2,	z-1,	true, 09 );			// wood stage III
		determineBlock( x,		y+2,	z-2,	true, 09 );			// wood stage III

		determineBlock( x+3,	y+3,	z,		true, 10 );			// stage I
		determineBlock( x-3,	y+3,	z,		true, 10 );			// stage I
		determineBlock( x,		y+3,	z+3,	true, 10 );			// stage I
		determineBlock( x,		y+3,	z-3,	true, 10 );			// stage I
		determineBlock( x+1,	y+3,	z+1,	true, 10 );			// stage I
		determineBlock( x+1,	y+3,	z+2,	true, 10 );			// stage I
		determineBlock( x+1,	y+3,	z-1,	true, 10 );			// stage I
		determineBlock( x+1,	y+3,	z-2,	true, 10 );			// stage I
		determineBlock( x-1,	y+3,	z+1,	true, 10 );			// stage I
		determineBlock( x-1,	y+3,	z+2,	true, 10 );			// stage I
		determineBlock( x-1,	y+3,	z-1,	true, 10 );			// stage I
		determineBlock( x-1,	y+3,	z-2,	true, 10 );			// stage I
		determineBlock( x+2,	y+3,	z+1,	true, 10 );			// stage I
		determineBlock( x+2,	y+3,	z-1,	true, 10 );			// stage I
		determineBlock( x-2,	y+3,	z+1,	true, 10 );			// stage I
		determineBlock( x-2,	y+3,	z-1,	true, 10 );			// stage I

		determineBlock( x+2,	y+4,	z,		true, 10 );			// stage II
		determineBlock( x-2,	y+4,	z,		true, 10 );			// stage II
		determineBlock( x,		y+4,	z+2,	true, 10 );			// stage II
		determineBlock( x,		y+4,	z-2,	true, 10 );			// stage II
		determineBlock( x+3,	y+4,	z,		true, 10 );			// stage II
		determineBlock( x-3,	y+4,	z,		true, 10 );			// stage II
		determineBlock( x,		y+4,	z+3,	true, 10 );			// stage II
		determineBlock( x,		y+4,	z-3,	true, 10 );			// stage II
		determineBlock( x+1,	y+4,	z+1,	true, 10 );			// stage II
		determineBlock( x+1,	y+4,	z+2,	true, 10 );			// stage II
		determineBlock( x+1,	y+4,	z-1,	true, 10 );			// stage II
		determineBlock( x+1,	y+4,	z-2,	true, 10 );			// stage II
		determineBlock( x-1,	y+4,	z+1,	true, 10 );			// stage II
		determineBlock( x-1,	y+4,	z+2,	true, 10 );			// stage II
		determineBlock( x-1,	y+4,	z-1,	true, 10 );			// stage II
		determineBlock( x-1,	y+4,	z-2,	true, 10 );			// stage II
		determineBlock( x+2,	y+4,	z+1,	true, 10 );			// stage II
		determineBlock( x+2,	y+4,	z-1,	true, 10 );			// stage II
		determineBlock( x-2,	y+4,	z+1,	true, 10 );			// stage II
		determineBlock( x-2,	y+4,	z-1,	true, 10 );			// stage II

		determineBlock( x+2,	y+5,	z,		true, 10 );			// stage III
		determineBlock( x-2,	y+5,	z,		true, 10 );			// stage III
		determineBlock( x,		y+5,	z+2,	true, 10 );			// stage III
		determineBlock( x,		y+5,	z-2,	true, 10 );			// stage III
		//determineBlock( x+1,	y+5,	z+1,	true, 10 );			// stage III
		//determineBlock( x+1,	y+5,	z-1,	true, 10 );			// stage III
		//determineBlock( x-1,	y+5,	z+1,	true, 10 );			// stage III
		//determineBlock( x-1,	y+5,	z-1,	true, 10 );			// stage III
	}

	// --------------------------------------------------------------------------------
	// XXXXXX      XXXXXX         XXXXXXXXXXXX         XXXXXXXX        XXXXXXXX      XXXXXXXXXXXXXXXXXX
	// XXXXXX      XXXXXX        XXXXXXXXXXXXXX        XXXXXXXXX      XXXXXXXXX      XXXXXXXXXXXXXXX
	// XXXXXX      XXXXXX       XXXXX      XXXXX       XXXXXX XXX    XXX XXXXXX      XXXXXX
	// XXXXXX      XXXXXX      XXXXXX      XXXXXX      XXXXXX  XXX  XXX  XXXXXX      XXXXXX
	// XXXXXXXXXXXXXXXXXX      XXXXXX      XXXXXX      XXXXXX   XXXXXX   XXXXXX      XXXXXXXXXXXX
	// XXXXXXXXXXXXXXXXXX      XXXXXX      XXXXXX      XXXXXX    XXXX    XXXXXX      XXXXXXXXXXXX
	// XXXXXX      XXXXXX      XXXXXX      XXXXXX      XXXXXX            XXXXXX      XXXXXX
	// XXXXXX      XXXXXX       XXXXX      XXXXX       XXXXXX            XXXXXX      XXXXXX
	// XXXXXX      XXXXXX        XXXXXXXXXXXXXX        XXXXXX            XXXXXX      XXXXXXXXXXXXXXX
	// XXXXXX      XXXXXX         XXXXXXXXXXXX         XXXXXX            XXXXXX      XXXXXXXXXXXXXXXXXX
	// --------------------------------------------------------------------------------
	void makeHome01( int x, int y, int z ) {

		determineBlock( x-2,	y,		z-2,	true, 05 );			// stage I
		determineBlock( x-2,	y,		z-1,	true, 05 );			// stage I
		determineBlock( x-2,	y,		z,		true, 05 );			// stage I
		determineBlock( x-2,	y,		z+1,	true, 05 );			// stage I
		determineBlock( x-2,	y,		z+2,	true, 05 );			// stage I
		determineBlock( x+2,	y,		z-2,	true, 05 );			// stage I
		determineBlock( x+2,	y,		z-1,	true, 05 );			// stage I
		determineBlock( x+2,	y,		z,		true, 05 );			// stage I
		determineBlock( x+2,	y,		z+1,	true, 05 );			// stage I
		determineBlock( x+2,	y,		z+2,	true, 05 );			// stage I
		determineBlock( x-1,	y,		z-2,	true, 05 );			// stage I
		determineBlock( x,		y,		z-2,	true, 05 );			// stage I
		determineBlock( x+1,	y,		z-2,	true, 05 );			// stage I
		determineBlock( x-1,	y,		z+2,	true, 05 );			// stage I
		determineBlock( x,		y,		z+2,	true, 05 );			// stage I
		determineBlock( x+1,	y,		z+2,	true, 05 );			// stage I
		determineBlock( x,		y,		z,		true, 11 );			// stage I
		determineBlock( x,		y,		z+1,	true, 11 );			// stage I
		determineBlock( x,		y,		z-1,	true, 11 );			// stage I
		determineBlock( x-1,	y,		z+1,	true, 11 );			// stage I
		determineBlock( x-1,	y,		z,		true, 11 );			// stage I
		determineBlock( x-1,	y,		z-1,	true, 11 );			// stage I
		determineBlock( x+1,	y,		z+1,	true, 11 );			// stage I
		determineBlock( x+1,	y,		z,		true, 11 );			// stage I
		determineBlock( x+1,	y,		z-1,	true, 11 );			// stage I

		determineBlock( x-2,	y+1,	z-2,	true, 12 );			// stage II
		determineBlock( x-2,	y+1,	z-1,	true, 12 );			// stage II
		determineBlock( x-2,	y+1,	z,		true, 12 );			// stage II
		determineBlock( x-2,	y+1,	z+1,	true, 12 );			// stage II
		determineBlock( x-2,	y+1,	z+2,	true, 12 );			// stage II
		determineBlock( x+2,	y+1,	z-2,	true, 12 );			// stage II
		determineBlock( x+2,	y+1,	z-1,	true, 12 );			// stage II
		determineBlock( x+2,	y+1,	z,		true, 12 );			// stage II
		determineBlock( x+2,	y+1,	z+1,	true, 12 );			// stage II
		determineBlock( x+2,	y+1,	z+2,	true, 12 );			// stage II
		determineBlock( x-1,	y+1,	z-2,	true, 12 );			// stage II
		determineBlock( x,		y+1,	z-2,	true, 12 );			// stage II
		determineBlock( x+1,	y+1,	z-2,	true, 12 );			// stage II
		determineBlock( x-1,	y+1,	z+2,	true, 12 );			// stage II
		//determineBlock( x,		y+2,	z+2,	true, 12 );			// stage II ENTER
		determineBlock( x+1,	y+1,	z+2,	true, 12 );			// stage II

		determineBlock( x-2,	y+2,	z-2,	true, 12 );			// stage III
		determineBlock( x-2,	y+2,	z-1,	true, 12 );			// stage III
		determineBlock( x-2,	y+2,	z,		true, 12 );			// stage III
		determineBlock( x-2,	y+2,	z+1,	true, 12 );			// stage III
		determineBlock( x-2,	y+2,	z+2,	true, 12 );			// stage III
		determineBlock( x+2,	y+2,	z-2,	true, 12 );			// stage III
		determineBlock( x+2,	y+2,	z-1,	true, 12 );			// stage III
		determineBlock( x+2,	y+2,	z,		true, 15 );			// stage III
		determineBlock( x+2,	y+2,	z+1,	true, 12 );			// stage III
		determineBlock( x+2,	y+2,	z+2,	true, 12 );			// stage III
		determineBlock( x-1,	y+2,	z-2,	true, 12 );			// stage III
		determineBlock( x,		y+2,	z-2,	true, 15 );			// stage III
		determineBlock( x+1,	y+2,	z-2,	true, 12 );			// stage III
		determineBlock( x-1,	y+2,	z+2,	true, 12 );			// stage III
		//determineBlock( x,		y+2,	z+2,	true, 12 );			// stage III ENTER
		determineBlock( x+1,	y+2,	z+2,	true, 12 );			// stage III

		determineBlock( x,		y+3,	z,		true, 13 );			// stage IV
		determineBlock( x-2,	y+3,	z-2,	true, 12 );			// stage IV
		determineBlock( x-2,	y+3,	z-1,	true, 12 );			// stage IV
		determineBlock( x-2,	y+3,	z,		true, 12 );			// stage IV
		determineBlock( x-2,	y+3,	z+1,	true, 12 );			// stage IV
		determineBlock( x-2,	y+3,	z+2,	true, 12 );			// stage IV
		determineBlock( x+2,	y+3,	z-2,	true, 12 );			// stage IV
		determineBlock( x+2,	y+3,	z-1,	true, 12 );			// stage IV
		determineBlock( x+2,	y+3,	z,		true, 12 );			// stage IV
		determineBlock( x+2,	y+3,	z+1,	true, 12 );			// stage IV
		determineBlock( x+2,	y+3,	z+2,	true, 12 );			// stage IV
		determineBlock( x-1,	y+3,	z-2,	true, 12 );			// stage IV
		determineBlock( x,		y+3,	z-2,	true, 12 );			// stage IV
		determineBlock( x+1,	y+3,	z-2,	true, 12 );			// stage IV
		determineBlock( x-1,	y+3,	z+2,	true, 12 );			// stage IV
		determineBlock( x,		y+3,	z+2,	true, 12 );			// stage IV
		determineBlock( x+1,	y+3,	z+2,	true, 12 );			// stage IV

		determineBlock( x-2,	y+4,	z-2,	true, 1 );			// stage V
		determineBlock( x-2,	y+4,	z-1,	true, 11 );			// stage V
		determineBlock( x-2,	y+4,	z,		true, 11 );			// stage V
		determineBlock( x-2,	y+4,	z+1,	true, 11 );			// stage V
		determineBlock( x-2,	y+4,	z+2,	true, 11 );			// stage V
		determineBlock( x+2,	y+4,	z-2,	true, 11 );			// stage V
		determineBlock( x+2,	y+4,	z-1,	true, 11 );			// stage V
		determineBlock( x+2,	y+4,	z,		true, 11 );			// stage V
		determineBlock( x+2,	y+4,	z+1,	true, 11 );			// stage V
		determineBlock( x+2,	y+4,	z+2,	true, 11 );			// stage V
		determineBlock( x-1,	y+4,	z-2,	true, 11 );			// stage V
		determineBlock( x,		y+4,	z-2,	true, 11 );			// stage V
		determineBlock( x+1,	y+4,	z-2,	true, 11 );			// stage V
		determineBlock( x-1,	y+4,	z+2,	true, 11 );			// stage V
		determineBlock( x,		y+4,	z+2,	true, 11 );			// stage V
		determineBlock( x+1,	y+4,	z+2,	true, 11 );			// stage V
		determineBlock( x,		y+4,	z,		true, 11 );			// stage V
		determineBlock( x,		y+4,	z+1,	true, 11 );			// stage V
		determineBlock( x,		y+4,	z-1,	true, 11 );			// stage V
		determineBlock( x-1,	y+4,	z+1,	true, 11 );			// stage V
		determineBlock( x-1,	y+4,	z,		true, 11 );			// stage V
		determineBlock( x-1,	y+4,	z-1,	true, 11 );			// stage V
		determineBlock( x+1,	y+4,	z+1,	true, 11 );			// stage V
		determineBlock( x+1,	y+4,	z,		true, 11 );			// stage V
		determineBlock( x+1,	y+4,	z-1,	true, 11 );			// stage V

		determineBlock( x+3,	y+4,	z-2,	true, 11 );			// stage V
		determineBlock( x+3,	y+4,	z-1,	true, 11 );			// stage V
		determineBlock( x+3,	y+4,	z,		true, 11 );			// stage V
		determineBlock( x+3,	y+4,	z+1,	true, 11 );			// stage V
		determineBlock( x+3,	y+4,	z+2,	true, 11 );			// stage V
		determineBlock( x-3,	y+4,	z-2,	true, 11 );			// stage V
		determineBlock( x-3,	y+4,	z-1,	true, 11 );			// stage V
		determineBlock( x-3,	y+4,	z,		true, 11 );			// stage V
		determineBlock( x-3,	y+4,	z+1,	true, 11 );			// stage V
		determineBlock( x-3,	y+4,	z+2,	true, 11 );			// stage V
		determineBlock( x-2,	y+4,	z-3,	true, 11 );			// stage V
		determineBlock( x-1,	y+4,	z-3,	true, 11 );			// stage V
		determineBlock( x,		y+4,	z-3,	true, 11 );			// stage V
		determineBlock( x+1,	y+4,	z-3,	true, 11 );			// stage V
		determineBlock( x+2,	y+4,	z-3,	true, 11 );			// stage V
		determineBlock( x-2,	y+4,	z+3,	true, 11 );			// stage V
		determineBlock( x-1,	y+4,	z+3,	true, 11 );			// stage V
		determineBlock( x,		y+4,	z+3,	true, 11 );			// stage V
		determineBlock( x+1,	y+4,	z+3,	true, 11 );			// stage V
		determineBlock( x+2,	y+4,	z+3,	true, 11 );			// stage V

		determineBlock( x-2,	y+5,	z-2,	true, 1 );			// stage VI
		determineBlock( x-2,	y+5,	z-1,	true, 11 );			// stage VI
		determineBlock( x-2,	y+5,	z,		true, 11 );			// stage VI
		determineBlock( x-2,	y+5,	z+1,	true, 11 );			// stage VI
		determineBlock( x-2,	y+5,	z+2,	true, 11 );			// stage VI
		determineBlock( x+2,	y+5,	z-2,	true, 11 );			// stage VI
		determineBlock( x+2,	y+5,	z-1,	true, 11 );			// stage VI
		determineBlock( x+2,	y+5,	z,		true, 11 );			// stage VI
		determineBlock( x+2,	y+5,	z+1,	true, 11 );			// stage VI
		determineBlock( x+2,	y+5,	z+2,	true, 11 );			// stage VI
		determineBlock( x-1,	y+5,	z-2,	true, 11 );			// stage VI
		determineBlock( x,		y+5,	z-2,	true, 11 );			// stage VI
		determineBlock( x+1,	y+5,	z-2,	true, 11 );			// stage VI
		determineBlock( x-1,	y+5,	z+2,	true, 11 );			// stage VI
		determineBlock( x,		y+5,	z+2,	true, 11 );			// stage VI
		determineBlock( x+1,	y+5,	z+2,	true, 11 );			// stage VI
		determineBlock( x,		y+5,	z,		true, 11 );			// stage VI
		determineBlock( x,		y+5,	z+1,	true, 11 );			// stage VI
		determineBlock( x,		y+5,	z-1,	true, 11 );			// stage VI
		determineBlock( x-1,	y+5,	z+1,	true, 11 );			// stage VI
		determineBlock( x-1,	y+5,	z,		true, 11 );			// stage VI
		determineBlock( x-1,	y+5,	z-1,	true, 11 );			// stage VI
		determineBlock( x+1,	y+5,	z+1,	true, 11 );			// stage VI
		determineBlock( x+1,	y+5,	z,		true, 11 );			// stage VI
		determineBlock( x+1,	y+5,	z-1,	true, 11 );			// stage VI

		determineBlock( x,		y+6,	z,		true, 11 );			// stage VII
		determineBlock( x,		y+6,	z+1,	true, 11 );			// stage VII
		determineBlock( x,		y+6,	z-1,	true, 11 );			// stage VII
		determineBlock( x-1,	y+6,	z+1,	true, 11 );			// stage VII
		determineBlock( x-1,	y+6,	z,		true, 11 );			// stage VII
		determineBlock( x-1,	y+6,	z-1,	true, 11 );			// stage VII
		determineBlock( x+1,	y+6,	z+1,	true, 11 );			// stage VII
		determineBlock( x+1,	y+6,	z,		true, 11 );			// stage VII
		determineBlock( x+1,	y+6,	z-1,	true, 11 );			// stage VII

		determineBlock( x,		y+7,	z,		true, 11 );			// stage VIII
	}

	// --------------------------------------------------------------------------------
	void determineBlock( int x, int y, int z, bool visible, int type ) {
		if (GetComponent<Generator> ().world [x, y, z] == null) {
			Vector3 blockPos = new Vector3 (x, y, z);
			GameObject go = GetComponent<Generator> ().createBlock (type, blockPos);
			GetComponent<Generator> ().world [x, y, z] = new Block (type, true, go);
		} 
	}

	// --------------------------------------------------------------------------------
}
// ####################################################################################