using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor;

// ####################################################################################
public class MainMenu : MonoBehaviour {

	public		Button 		NewGameMainButton;
	public		Button		LoadGameMainButton;
	public		Button		ExitGameMainButton;
	public		Button		GenerateWorldButton;
	public		Button		LoadWorldButton;
	public		Button		LastSaveButton;
	public		Button		InformationsButton;

	public		Text		TextWidth;
	public		Text		TextDeepth;
	public		Text		TextSeed;

	public		GameObject	PanelNewGame;
	public		GameObject	PanelLoadGame;
	public		GameObject	PanelInformation;

	// --------------------------------------------------------------------------------
	void Start () {
		hideUIs();
		setupButtons();
	}
	
	// --------------------------------------------------------------------------------
	void Update () {
		
	}

	// --------------------------------------------------------------------------------
	void setupButtons() {
		NewGameMainButton.onClick.AddListener( NewGameMainButtonClick );
		LoadGameMainButton.onClick.AddListener( LoadGameMainButtonClick );
		ExitGameMainButton.onClick.AddListener( ExitGameMainButtonClick );
		GenerateWorldButton.onClick.AddListener( GenerateWorldButtonClick );
		LoadWorldButton.onClick.AddListener( LoadWorldButtonClick );
		LastSaveButton.onClick.AddListener( LastSaveButtonClick );
		InformationsButton.onClick.AddListener( InformationsButtonClick );
	}

	void NewGameMainButtonClick() { hideUIs(); PanelNewGame.SetActive(true); }
	void LoadGameMainButtonClick() { hideUIs(); PanelLoadGame.SetActive(true); }
	void ExitGameMainButtonClick() { Application.Quit(); }
	void GenerateWorldButtonClick() { 
		int width = 128;
		int deepth = 128;
		int seed = Random.Range(10000,65565);

		try {
			width = int.Parse(TextWidth.text);
			if ( width < 16 || width > 128 ) {
				width = 64;
				//EditorUtility.DisplayDialog("Width out of bounds","Width is set to 64","OK");
			}
			deepth = int.Parse(TextDeepth.text);
			if ( deepth < 16 || deepth > 128 ) {
				deepth = 64;
				//EditorUtility.DisplayDialog("Deepth out of bounds","Depth is set to 64","OK");
			}
		} catch( System.Exception ) {
			//EditorUtility.DisplayDialog("Incorrect data","There is invalid width or depth value","OK");
			return;
		}

		try {
			seed = int.Parse(TextSeed.text);
			if ( seed < 0 || seed > 65565 ) {
				seed = Random.Range(10000,65565);
				//EditorUtility.DisplayDialog("Seed out of bounds","Seed is set randomly to" + seed.ToString(),"OK");
			}
		} catch( System.Exception ) {
			//EditorUtility.DisplayDialog("Seed incorrect","Seed is set randomly to" + seed.ToString(),"OK");
		}

		passDataNew( width, deepth, seed );
	}
	void LoadWorldButtonClick() { passDataLoad(); }
	void LastSaveButtonClick() { passDataLoad(); }
	void InformationsButtonClick() { hideUIs(); PanelInformation.SetActive(true); }

	// --------------------------------------------------------------------------------
	void hideUIs() {
		PanelNewGame.SetActive(false);
		PanelLoadGame.SetActive(false);
		PanelInformation.SetActive(false);
	}

	// --------------------------------------------------------------------------------
	void passDataNew( int width, int deepth, int seed ) {
		PlayerPrefs.SetString( "startCommand", "NEW" );
		PlayerPrefs.SetInt( "width", width );
		PlayerPrefs.SetInt( "deepth", deepth );
		PlayerPrefs.SetInt( "seed", seed );
		Debug.Log("StartNewGame");
		SceneManager.LoadScene("Game");
		//Application.LoadLevel( "Game" );
	}

	// --------------------------------------------------------------------------------
	void passDataLoad() {
		PlayerPrefs.SetString( "startCommand", "LOAD" );
		Debug.Log("LoagGame");
		SceneManager.LoadScene("Game");
		//Application.LoadLevel( "Game" );
	}

	// --------------------------------------------------------------------------------
}
// ####################################################################################