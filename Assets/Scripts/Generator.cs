using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ####################################################################################
//	  [ 00 ]	-	BlockCloud
//	  [ 01 ]	-	BlockMapBottom
//    [ 02 ]	-	BlockStone
//	  [ 03 ]	-	BlockGrass
//    [ 04 ]	-   BlockDirt
//    [ 05 ]	-   BlockCobbleStone
//    [ 06 ]	-   BlockSand
//    [ 07 ]	-   BlockGravel
//    [ 08 ]	-   BlockSnow
//    [ 09 ]	-   BlockWood
//    [ 10 ]	-   BlockLeaves
//    [ 11 ]	-	BlockWoodPlank
//    [ 12 ]	-	BlockStoneBrick
//    [ 13 ] 	-	BlockLight
//    [ 14 ] 	-	BlockWater
//    [ 15 ] 	-	BlockGlass

// ####################################################################################
public class Block {
	public	int			type;
	public	bool		visible;
	public	GameObject	block;

	public Block( int t, bool v, GameObject b ) {
		this.type		=	t;
		this.visible	=	v;
		this.block		=	b;
	}
}

// ####################################################################################
public class Generator : MonoBehaviour {

	public	int			seed				=	65565;
	public	int			width				=	128;
	public	int			depth				=	128;
	public	int			height				=	128;
	public	int			heightOffset		=	64;
	public	int			heightScale			=	20;
	public	float		detailScale			=	25.0f;

	public	int			cloudsAmount		=	20;
	public	int			cloudsSize			=	100;

	public	int			cavesFrequency		=	5;
	public	int			cavesDeep			=	500;
	public	int			cavesSize			=	2;

	public	GameObject	ContainerBlockCobbleStone;
	public	GameObject	ContainerBlockDirt;
	public	GameObject	ContainerBlockGlass;
	public	GameObject	ContainerBlockGrass;
	public	GameObject	ContainerBlockGravel;
	public	GameObject	ContainerBlockLeaves;
	public	GameObject	ContainerBlockLight;
	public	GameObject	ContainerBlockMapBottom;
	public	GameObject	ContainerBlockSand;
	public	GameObject	ContainerBlockSnow;
	public	GameObject	ContainerBlockStone;
	public	GameObject	ContainerBlockStoneBrick;
	public	GameObject	ContainerBlockWater;
	public	GameObject	ContainerBlockWood;
	public	GameObject	ContainerBlockWoodPlank;

	public	GameObject	BlockCobbleStone;
	public	GameObject	BlockDirt;
	public	GameObject	BlockGlass;
	public	GameObject	BlockGrass;
	public	GameObject	BlockGravel;
	public	GameObject	BlockLeaves;
	public	GameObject	BlockLight;
	public	GameObject	BlockMapBottom;
	public	GameObject	BlockSand;
	public	GameObject	BlockSnow;
	public	GameObject	BlockStone;
	public	GameObject	BlockStoneBrick;
	public	GameObject	BlockWater;
	public	GameObject	BlockWood;
	public	GameObject	BlockWoodPlank;

	public	GameObject	ContainerBlockCloud;
	public	GameObject	BlockCloud;

	public	GameObject	Player;

	private	int			shiftZ				=	0;
	private	int			shiftX				=	0;
	private	int			masterTypeBlock		=	-1;

	public	Block[,,]	world				=	new Block[ 0, 0, 0 ];

	// --------------------------------------------------------------------------------
	void Start () {
		string startCommand = PlayerPrefs.GetString( "startCommand" );

		if (startCommand == "NEW") {
			width = PlayerPrefs.GetInt( "width" );
			depth = PlayerPrefs.GetInt( "deepth" );
			seed  = PlayerPrefs.GetInt( "seed" );
			PlayerPrefs.DeleteAll();
			generateWorld();
		}

		else if (startCommand == "LOAD") {
			PlayerPrefs.DeleteAll();
			this.GetComponent<LoadSave>().LoadFile();
		}
	}
	// --------------------------------------------------------------------------------
	void generateWorld() {
		world		=	new Block[ width, height, depth ];

		shiftZ		=	depth / 2;
		shiftX		=	width / 2;

		for (int z = 0; z < depth; z++) {
			for (int x = 0; x < width; x++) {

				int y = (int) (Mathf.PerlinNoise( (x+seed)/detailScale, (z+seed)/detailScale ) * heightScale) + heightOffset;
				int masterY = 0;
				Vector3 blockPos = new Vector3 (x, y, z);
				generateBlock( y, blockPos, true, masterY );

				if ( (z)==shiftZ && (x)==shiftX ) {
					Player.transform.position = new Vector3(shiftX, y+2, shiftZ);
				}

				while (y > 0) {
					y--;
					masterY++;
					blockPos = new Vector3 (x, y, z);
					generateBlock( y, blockPos, false, masterY );
				}
			}
		}

		cloudsAmount = (int)(((width + height) / 2) * 0.156250);
		cloudsSize = (int)(((width + height) / 2) * 0.390625);
		cavesFrequency = (int)(((width + height) / 2) * 0.390625);
		cavesDeep = (int)(((width + height) / 2) * 0.390625);
		cavesSize = 1;

		int dirtFreq = (int)(((width + height) / 2) * 0.078125);
		int dirtDeep = (int)(((width + height) / 2) * 0.078125);
		int dirtSize = Mathf.CeilToInt( (float)(((width * height) / 2) * 0.015625) );

		int gravelFreq = (int)(((width + height) / 2) * 0.078125);
		int gravelDeep = (int)(((width + height) / 2) * 0.078125);
		int gravelSize = Mathf.CeilToInt( (float)(((width + height) / 2) * 0.015625) );

		Debug.Log( "Map Width: " + width.ToString() );
		Debug.Log( "Map Height: " + height.ToString() );
		Debug.Log( "Map Depth: " + depth.ToString() );

		Debug.Log( "Cloud Amount: " + cloudsAmount.ToString() );
		Debug.Log( "Cloud Size: " + cloudsSize.ToString() );

		Debug.Log( "Caves Frequency: " + cavesFrequency.ToString() );
		Debug.Log( "Caves Deep: " + cavesDeep.ToString() );
		Debug.Log( "Caves Size: " + cavesSize.ToString() );

		Debug.Log( "Dirt Frequency: " + dirtFreq.ToString() );
		Debug.Log( "Dirt Deep: " + dirtDeep.ToString() );
		Debug.Log( "Dirt Size: " + dirtSize.ToString() );

		Debug.Log( "Gravel Frequency: " + gravelFreq.ToString() );
		Debug.Log( "Gravel Deep: " + gravelDeep.ToString() );
		Debug.Log( "Gravel Size: " + gravelSize.ToString() );

		generateClouds( cloudsAmount, cloudsSize );
		generateCaves( cavesFrequency, cavesDeep, cavesSize );

		//generateOres( 10, 64, dirtFreq, dirtDeep, dirtSize, 4 );
		//generateOres( 10, 64, gravelFreq, gravelDeep, gravelSize, 7 );

		int structFreq = Mathf.CeilToInt ((float)(((width + height) / 2) * 0.125));
		GetComponent<GenerateStructures>().makeBuld( 1, structFreq );
		GetComponent<GenerateStructures>().makeBuld( 2, structFreq );
		GetComponent<GenerateStructures>().makeBuld( 3, 1 );
	}

	// --------------------------------------------------------------------------------
	void generateBlock( int y, Vector3 blockPos, bool visible, int masterY ) {
		GameObject	newBlock	=	null;
		int			typeBlock	=	-1;

		// GENERATE TERRAIN FIRST LAYER BLOCKS
		if ( masterY == 0 ) {
			if (y > 80)			{ typeBlock = 08; } 
			else if (y > 64)	{ typeBlock = 03; }
			else if (y > 0)		{ typeBlock = 06; }
			masterTypeBlock = typeBlock;
		}

		// GENERATE TERRAIN UNDER LAYER BLOCKS
		if ( masterY > 0 ) {
			if (masterY < 5) {
				if (masterTypeBlock == 08) { typeBlock = 08; }
				if (masterTypeBlock == 03) { typeBlock = 04; }
				if (masterTypeBlock == 06) { typeBlock = 06; }
			} 

			else if (masterY < 8) {
				if (masterTypeBlock == 08) { typeBlock = 04; }
				if (masterTypeBlock == 03) { typeBlock = 02; }
				if (masterTypeBlock == 06) { typeBlock = 04; }
			}

			else if (masterY >= 8) {
				if (masterTypeBlock == 08) { typeBlock = 02; }
				if (masterTypeBlock == 03) { typeBlock = 02; }
				if (masterTypeBlock == 06) { typeBlock = 02; }
			}
		}

		// GENERATE TERRAIN BOTTOM MAP BLOCKS
		if ( y == 0 ) { typeBlock = 01; }

		// GENERATE BLOCK
		if (visible) { newBlock = createBlock( typeBlock, blockPos ); }
		world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ] = new Block( typeBlock, visible, newBlock );

	}

	// --------------------------------------------------------------------------------
	public GameObject createBlock( int type, Vector3 blockPos ) {
		switch (type) {
			case 0:
				var newBlock0 = Instantiate (BlockCloud, blockPos, Quaternion.identity);
				newBlock0.transform.parent = ContainerBlockCloud.transform;
				return newBlock0;
			case 1:
				var newBlock1 = Instantiate (BlockMapBottom, blockPos, Quaternion.identity);
				newBlock1.transform.parent = ContainerBlockMapBottom.transform;
				return newBlock1;
			case 2:
				var newBlock2 = Instantiate (BlockStone, blockPos, Quaternion.identity);
				newBlock2.transform.parent = ContainerBlockStone.transform;
				break;
			case 3:
				var newBlock3 = Instantiate (BlockGrass, blockPos, Quaternion.identity);
				newBlock3.transform.parent = ContainerBlockGrass.transform;
				return newBlock3;
			case 4:
				var newBlock4 = Instantiate (BlockDirt, blockPos, Quaternion.identity);
				newBlock4.transform.parent = ContainerBlockDirt.transform;
				break;
			case 5:
				var newBlock5 = Instantiate (BlockCobbleStone, blockPos, Quaternion.identity);
				newBlock5.transform.parent = ContainerBlockCobbleStone.transform;
				break;
			case 6:
				var newBlock6 = Instantiate (BlockSand, blockPos, Quaternion.identity);
				newBlock6.transform.parent = ContainerBlockSand.transform;
				return newBlock6;
			case 7:
				var newBlock7 = Instantiate (BlockGravel, blockPos, Quaternion.identity);
				newBlock7.transform.parent = ContainerBlockGravel.transform;
				break;
			case 8:
				var newBlock8 = Instantiate (BlockSnow, blockPos, Quaternion.identity);
				newBlock8.transform.parent = ContainerBlockSnow.transform;
				return newBlock8;
			case 9:
				var newBlock9 = Instantiate (BlockWood, blockPos, Quaternion.identity);
				newBlock9.transform.parent = ContainerBlockWood.transform;
				break;
			case 10:
				var newBlock10 = Instantiate (BlockLeaves, blockPos, Quaternion.identity);
				newBlock10.transform.parent = ContainerBlockLeaves.transform;
				break;
			case 11:
				var newBlock11 = Instantiate (BlockWoodPlank, blockPos, Quaternion.identity);
				newBlock11.transform.parent = ContainerBlockWoodPlank.transform;
				break;
			case 12:
				var newBlock12 = Instantiate (BlockStoneBrick, blockPos, Quaternion.identity);
				newBlock12.transform.parent = ContainerBlockStoneBrick.transform;
				break;
			case 13:
				var newBlock13 = Instantiate (BlockLight, blockPos, Quaternion.identity);
				newBlock13.transform.parent = ContainerBlockLight.transform;
				break;
			case 14:
				var newBlock14 = Instantiate (BlockWater, blockPos, Quaternion.identity);
				newBlock14.transform.parent = ContainerBlockWater.transform;
				break;
			case 15:
				var newBlock15 = Instantiate (BlockGlass, blockPos, Quaternion.identity);
				newBlock15.transform.parent = ContainerBlockGlass.transform;
			break;
			default:
				break;
		}
		return null;
	}

	// --------------------------------------------------------------------------------
	void generateClouds( int amount, int size ) {

		for ( int i=0; i<amount; i++ ) {
			int x = Random.Range(0,width-1);
			int z = Random.Range(0,depth-1);

			for ( int j=0; j<size; j++ ) {
				Vector3 cloudPos = new Vector3( x, height-1, z );
				var block = createBlock( 0, cloudPos );
				world[ (int)cloudPos.x, (int)cloudPos.y, (int)cloudPos.z ] = new Block(0, true, block);
				x += Random.Range(-1,2);
				z += Random.Range(-1,2);
				if ( x<0 || x>=width ) { x = width/2; }
				if ( z<0 || z>=depth ) { z = depth/2; }
			}
		}

	}

	// --------------------------------------------------------------------------------
	void generateCaves( int frequency, int deep, int size ) {

		for ( int i=0; i<frequency; i++ ) {
			int x = Random.Range( 10, width-10 );	
			int y = Random.Range( 10, height-10 );	
			int z = Random.Range( 10, depth-10 );

			for ( int j=0; j<deep; j++ ) {

				for ( int ix=(-size); ix<size; ix++ ) {
					for ( int iy=(-size); iy<size; iy++ ) {
						for ( int iz=(-size); iz<size; iz++ ) {

							if ( !(ix==0 && iy==0 && iz==0) ) {
								Vector3 blockPos = new Vector3( x+ix, y+iy, z+iz );
								if ( world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ] != null ) {
									if ( world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ].block != null ) {
										Destroy( world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ].block );
									}
									world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ] = null;
								}
							}

				}}}
					
				x += Random.Range( -1, 2 );
				y += Random.Range( -1, 2 );
				z += Random.Range( -1, 2 );
				if ( x<size || x >= width - size) { x = width/2; } 
				if ( y<size || y >= height - size) { y = height/2; } 
				if ( z<size || z >= depth - size) { z = depth/2; } 
			}
		}

		for ( int z=1; z<depth-1; z++ ) {
			for ( int x=1; x<width-1; x++ ) { 
				for ( int y=1; y<height-1; y++ ) {

					if ( world[x,y,z] == null) {

						for ( int ix=(-1); ix<=1; ix++ ) {
							for ( int iy=(-1); iy<=1; iy++ ) {
								for ( int iz=(-1); iz<=1; iz++ ) {
								
									if ( !( ix==0 && iy==0 && iz==0 ) ) {
										Vector3 blockPos = new Vector3( x+ix, y+iy, z+iz );

										if (world [(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] != null) {
											if (!world [(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].visible) {
												int type = world [(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].type;

												var block = createBlock (type, blockPos);
												world [(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].visible = true;
												world [(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].block = block;
											}
										}
									}

						}}}
					}
		}}}
	}

	// --------------------------------------------------------------------------------
	void generateOres( int minHeight, int maxHeight, int frequency, int deep, int size, int type ) {
		GameObject	newBlock	=	null;

		int newMinHeight = minHeight;
		int newMaxHeight = maxHeight;

		if ( minHeight < 0+size ) { newMinHeight = 1+size; } 
		if ( maxHeight > (height-1)-size ) { newMaxHeight = (height-1)-size; }

		for ( int i=0; i<frequency; i++ ) {
			int x = Random.Range( 10, width-10 );	
			int y = Random.Range( newMinHeight, newMaxHeight );	
			int z = Random.Range( 10, depth-10 );

			for ( int j=0; j<deep; j++ ) {

				for ( int ix=(-size); ix<size; ix++ ) {
					for ( int iy=(-size); iy<size; iy++ ) {
						for ( int iz=(-size); iz<size; iz++ ) {

							if ( !(ix==0 && iy==0 && iz==0) ) {
								if ( x+ix <= 10 || x+ix >= width-10 ) { continue; }
								if ( y+iy <= 10 || y+iy >= height-10 ) { continue; }
								if ( z+iz <= 10 || z+iz >= depth-10 ) { continue; }

								Vector3 blockPos = new Vector3( x+ix, y+iy, z+iz );
								if ( world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ] != null ) {
									if ( world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ].block != null ) {
										Destroy( world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ].block );

									}
									newBlock = createBlock (type, blockPos);
									world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ].type = type;
									world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ].block = newBlock; 
									world[ (int)blockPos.x, (int)blockPos.y, (int)blockPos.z ].visible = true; 
								}
							}

						}}}

				x += Random.Range( -1, 2 );
				y += Random.Range( -1, 2 );
				z += Random.Range( -1, 2 );
				if ( x<size || x >= width - size) { x = width/2; } 
				if ( y<size || y >= height - size) { y = height/2; } 
				if ( z<size || z >= depth - size) { z = depth/2; } 
			}
		}
	}

	// --------------------------------------------------------------------------------
	void Update () {
		
	}

}
// ####################################################################################