using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

// ####################################################################################
public class LoadSave : MonoBehaviour {

	public		string			saveFilePath	=	"";
	public		GameObject		DayTime;

	// --------------------------------------------------------------------------------
	void Start () {
		MakeDirectory();
	}
	
	// --------------------------------------------------------------------------------
	void Update () {
		
	}

	// --------------------------------------------------------------------------------
	void MakeDirectory() {
		saveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

		if (!Directory.Exists( saveFilePath + "\\" + "KamilKarpinski" )) { Directory.CreateDirectory( saveFilePath + "\\" + "KamilKarpinski" ); }
		if (!Directory.Exists( saveFilePath + "\\" + "KamilKarpinski" + "\\" + "BuildingBlocks" )) { Directory.CreateDirectory( saveFilePath + "\\" + "KamilKarpinski" + "\\" + "BuildingBlocks" ); }

		saveFilePath = saveFilePath + "\\" + "KamilKarpinski" + "\\" + "BuildingBlocks";
	}

	// --------------------------------------------------------------------------------
	public void LoadFile() {
		MakeDirectory();

		if (!File.Exists( saveFilePath + "\\" + "gameSave" )) {
			// FILE NOT EXISTS
			Debug.Log("BackToMenu");
			SceneManager.LoadScene("MainMenu");
		}

		var openFile = File.OpenText( saveFilePath + "\\" + "gameSave" );
		var line = openFile.ReadLine();

		// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		int INTgetX = 8;
		int INTgetY = 100;
		int INTgetZ = 8;

		while (line != null) {

			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			if (line.Substring(0,5) == "World") {
					string getWidth = "";
					string getHeight = "";
					string getDepth = "";
					string getSeed = "";

					bool activeWidth = true;
					bool activeHeight = false;
					bool activeDepth = false;
					bool activeSeed = false;

				for ( int z=6; z<line.Length; z++ ) {

					if ( line.Substring(z, 1) == "," ) {
						if (activeWidth) { activeWidth = false; activeHeight = true; }
						else if (activeHeight) { activeHeight = false; activeDepth = true; }
						else if (activeDepth) { activeDepth = false; activeSeed = true; }
						continue;
					}

					if ( line.Substring(z, 1) == ";" ) {
						break;
					}

					if (activeWidth) { getWidth += line.Substring(z, 1); }
					if (activeHeight) { getHeight += line.Substring(z, 1); }
					if (activeDepth) { getDepth += line.Substring(z, 1); }
					if (activeSeed) { getSeed += line.Substring(z, 1); }
				}

				//Debug.Log( "WorldX: " + getWidth );
				//Debug.Log( "WorldY: " + getHeight );
				//Debug.Log( "WorldZ: " + getDepth );
				//Debug.Log( "WorldS: " + getSeed );

				int INTgetWidth = int.Parse( getWidth );
				int INTgetHeight = int.Parse( getHeight );
				int INTgetDepth = int.Parse( getDepth );
				int INTgetSeed = int.Parse( getSeed );

				this.GetComponent<Generator>().width = INTgetWidth;
				this.GetComponent<Generator>().depth = INTgetDepth;
				this.GetComponent<Generator>().seed = INTgetSeed;
				this.GetComponent<Generator>().world = new Block[ INTgetWidth, INTgetHeight, INTgetDepth ];
			}

			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			if (line.Substring(0,3) == "Day") {
				string DTday = "";
				string DTx = "";
				string DTy = "";
				string DTz = "";

				bool activeDTday = true;
				bool activeDTx = false;
				bool activeDTy = false;
				bool activeDTz = false;

				for ( int z=4; z<line.Length; z++ ) {

					if ( line.Substring(z, 1) == "," ) {
						if (activeDTday) { activeDTday = false; activeDTx = true; }
						else if (activeDTx) { activeDTx = false; activeDTy = true; }
						else if (activeDTy) { activeDTy = false; activeDTz = true; }
						continue;
					}

					if ( line.Substring(z, 1) == ";" ) {
						break;
					}

					if (activeDTday) { DTday += line.Substring(z, 1); }
					if (activeDTx) { DTx += line.Substring(z, 1); }
					if (activeDTy) { DTy += line.Substring(z, 1); }
					if (activeDTz) { DTz += line.Substring(z, 1); }
				}

				string INTDTdata = DTday;
				float INTDTx = float.Parse( DTx );
				float INTDTy = float.Parse( DTy );
				float INTDTz = float.Parse( DTz );

				DayTime.GetComponent<DayNightCycle>().dayData = INTDTdata;
				DayTime.transform.Rotate( INTDTx, INTDTy, INTDTz );
			}

			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			if (line.Substring(0,6) == "Player") {
					string getX = "";
					string getY = "";
					string getZ = "";

					bool activeX = true;
					bool activeY = false;
					bool activeZ = false;

				for ( int z=7; z<line.Length; z++ ) {

					if ( line.Substring(z, 1) == "," ) {
						if (activeX) { activeX = false; activeY = true; }
						else if (activeY) { activeY = false; activeZ = true; }
						continue;
					}

					if ( line.Substring(z, 1) == ";" ) {
						break;
					}

					if (activeX) { getX += line.Substring(z, 1); }
					if (activeY) { getY += line.Substring(z, 1); }
					if (activeZ) { getZ += line.Substring(z, 1); }
				}

				//Debug.Log( "PlayerX: " + getX );
				//Debug.Log( "PlayerY: " + getY );
				//Debug.Log( "PlayerZ: " + getZ );

				INTgetX = int.Parse( getX );
				INTgetY = int.Parse( getY );
				INTgetZ = int.Parse( getZ );
			}

			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			if (line.Substring(0,6) == "Object") {
					string getPosX = "";
					string getPosY = "";
					string getPosZ = "";
					string getType = "";
					string getVisible = "";

					bool activePosX = true;
					bool activePosY = false;
					bool activePosZ = false;	
					bool activeType = false;
					bool activeVisible = false;

				for ( int z=7; z<line.Length; z++ ) {

					if ( line.Substring(z, 1) == "," ) {
						if (activePosX) { activePosX = false; activePosY = true; }
						else if (activePosY) { activePosY = false; activePosZ = true; }
						else if (activePosZ) { activePosZ = false; activeType = true; }
						else if (activeType) { activeType = false; activeVisible = true; }
						continue;
					}

					if ( line.Substring(z, 1) == ";" ) {
						break;
					}

					if (activePosX) { getPosX += line.Substring(z, 1); }
					if (activePosY) { getPosY += line.Substring(z, 1); }
					if (activePosZ) { getPosZ += line.Substring(z, 1); }
					if (activeType) { getType += line.Substring(z, 1); }
					if (activeVisible) { getVisible += line.Substring(z, 1); }
				}

				//Debug.Log( "ObjectX: " + getPosX );
				//Debug.Log( "ObjectY: " + getPosY );
				//Debug.Log( "ObjectZ: " + getPosZ );
				//Debug.Log( "ObjectT: " + getType );
				//Debug.Log( "ObjectV: " + getVisible );

				int INTgetPosX = int.Parse( getPosX );
				int INTgetPosY = int.Parse( getPosY );
				int INTgetPosZ = int.Parse( getPosZ );
				int INTgetType = int.Parse( getType );
				int INTgetVisible = int.Parse( getVisible );

				bool BOOLgetVisible = false;
				if (INTgetVisible == 1) { BOOLgetVisible = true; } 

				Vector3 blockPos = new Vector3(INTgetPosX, INTgetPosY, INTgetPosZ);
				GameObject newBlock = GetComponent<Generator>().createBlock( INTgetType, blockPos ); 
				GetComponent<Generator>().world[ INTgetPosX, INTgetPosY, INTgetPosZ ] = new Block( INTgetType, BOOLgetVisible, newBlock );
			}

			// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			line = openFile.ReadLine();
		}

		this.GetComponent<Generator>().Player.transform.position = new Vector3(INTgetX, INTgetY+2, INTgetZ);
		// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

		openFile.Close();
	}

	// --------------------------------------------------------------------------------
	public void SaveFile() {

		if (File.Exists( saveFilePath + "\\" + "gameSave" )) {
			//OVERRIDE THE FILE
			File.Delete( saveFilePath + "\\" + "gameSave" );
		}

		var saveFile = File.CreateText( saveFilePath + "\\" + "gameSave" );

		int maxX = this.GetComponent<Generator>().width;
		int maxY = this.GetComponent<Generator>().height;
		int maxZ = this.GetComponent<Generator>().depth;
		int seed = this.GetComponent<Generator>().seed;

		int playerX = (int) this.GetComponent<Generator>().Player.transform.position.x;
		int playerY = (int) this.GetComponent<Generator>().Player.transform.position.y;
		int playerZ = (int) this.GetComponent<Generator>().Player.transform.position.z;

		saveFile.WriteLine( "World," + maxX.ToString() + "," + maxY.ToString() + "," + maxZ.ToString() + "," + seed.ToString() + ";" );
		string DTdata = DayTime.GetComponent<DayNightCycle>().dayData;
		string DTx = DayTime.transform.localRotation.x.ToString();
		string DTy = DayTime.transform.localRotation.y.ToString();
		string DTz = DayTime.transform.localRotation.z.ToString();
		saveFile.WriteLine( "Day," + DTdata + "," + DTx + "," + DTy + "," + DTz + ";" );
		saveFile.WriteLine( "Player," + playerX.ToString() + "," + playerY.ToString() + "," + playerZ.ToString() + ";" );

		for ( int ix=0; ix<maxX; ix++ ) {
			for ( int iy=0; iy<maxY; iy++ ) {
				for ( int iz=0; iz<maxZ; iz++ ) {
					if ( this.GetComponent<Generator>().world[ix,iy,iz] != null ) {
						int type = this.GetComponent<Generator>().world[ix,iy,iz].type;
						bool visible = this.GetComponent<Generator>().world[ix,iy,iz].visible;
						int vis = 0;
						if (visible) { vis = 1; }
						saveFile.WriteLine( "Object," + ix.ToString() + "," + iy.ToString() + "," + iz.ToString() + "," + type.ToString() + "," + vis.ToString() + ";" );
					}
				}
			}
		}

		saveFile.Close();
	}

}
// ####################################################################################