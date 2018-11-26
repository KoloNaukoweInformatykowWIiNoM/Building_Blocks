using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor;

// ####################################################################################
//	#FFFFFFC8	-	colorNormal
//	#006FBDFF	-	colorSelect
// ####################################################################################
public class UIScript : MonoBehaviour {

	public	GameObject	uiCursor;
	public	GameObject	uiInv;
	public	GameObject	uiMenu;
	public	GameObject	uiInfo;
	public	bool		mouseLock		=	true;

	public	Button		MenuResume;
	public	Button		MenuSave;
	public	Button		MenuMenu;
	public	Button 		MenuExit;

	public	Button		ItemBlockStone;
	public	Button		ItemBlockGrass;
	public	Button		ItemBlockDirt;
	public	Button		ItemBlockCobbleStone;
	public	Button		ItemBlockSand;
	public	Button		ItemBlockGravel;
	public	Button		ItemBlockSnow;
	public	Button		ItemBlockWood;
	public	Button		ItemBlockLeaves;
	public	Button		ItemBlockWoodPlank;
	public	Button		ItemBlockStoneBrick;
	public	Button		ItemBlockLamp;
	public	Button		ItemBlockGlass;

	public	Button		inventory1;
	public	Button		inventory2;
	public	Button		inventory3;
	public	Button		inventory4;
	public	Button		inventory5;

	public	GameObject	player;
	public	GameObject	map_relative;
	public	GameObject	inventoryMoverVisible;
	public	RawImage	inventoryMover;

	public	int[]		items;

	private	int			itemSelect		=	0;
	private	int			itemMove		=	-1;
	private	string		itemTexture		=	"";

	// --------------------------------------------------------------------------------
	void Start () {
		items = new int[5];
		items[0] = 2;
		items[1] = 3;
		items[2] = 4;
		items[3] = 6;
		items[4] = 11;

		setupButtonClick();
		ShowEscapeMenu( false );
		ShowInventoryMenu( false );

		mouseLock =	true;
		UpdateMouseLock();
		handInventorySelectReset();
		handIntventorySelect( 1 );
	}

	// --------------------------------------------------------------------------------
	void Update () {
		UseKeys();
		handInventorySelectItem();
		moveInventoryMover();
	}

	// --------------------------------------------------------------------------------
	void handInventorySelectItem() {
		if (!mouseLock) { return; }

		if ( Input.GetKeyUp(KeyCode.Alpha1) ) {
			handInventorySelectReset();
			handIntventorySelect( 0 );
		}
		if ( Input.GetKeyUp(KeyCode.Alpha2) ) {
			handInventorySelectReset();
			handIntventorySelect( 1 );
		}
		if ( Input.GetKeyUp(KeyCode.Alpha3) ) {
			handInventorySelectReset();
			handIntventorySelect( 2 );
		}
		if ( Input.GetKeyUp(KeyCode.Alpha4) ) {
			handInventorySelectReset();
			handIntventorySelect( 3 );
		}
		if ( Input.GetKeyUp(KeyCode.Alpha5) ) {
			handInventorySelectReset();
			handIntventorySelect( 4 );
		}
	}

	// --------------------------------------------------------------------------------
	void handInventorySelectReset() {
		for ( int i=1; i<=5; i++ ) {
			string	objectName		=	"FrameItem" + i.ToString();
			Color	objectColor		=	new Color( 0, 0, 0, 255 );
			GameObject.Find(objectName).GetComponent<Image>().color = objectColor;
		}
	}

	// --------------------------------------------------------------------------------
	void handIntventorySelect( int inv ) {
		string	objectName		=	"FrameItem" + (inv+1).ToString();
		Color	objectColor		=	new Color( 255, 255, 255, 255 ); 
		player.GetComponent<BlockCreate>().ActiveItem = items[inv];
		itemSelect				=	inv;
		GameObject.Find(objectName).GetComponent<Image>().color = objectColor;
	}

	// --------------------------------------------------------------------------------
	void setupButtonClick() {
		ItemBlockStone.onClick.AddListener( ItemBlockStoneClick );
		ItemBlockGrass.onClick.AddListener( ItemBlockGrassClick );
		ItemBlockDirt.onClick.AddListener( ItemBlockDirtClick );
		ItemBlockCobbleStone.onClick.AddListener( ItemBlockCobbleStoneClick );
		ItemBlockSand.onClick.AddListener( ItemBlockSandClick );
		ItemBlockGravel.onClick.AddListener( ItemBlockGravelClick );
		ItemBlockSnow.onClick.AddListener( ItemBlockSnowClick );
		ItemBlockWood.onClick.AddListener( ItemBlockWoodClick );
		ItemBlockLeaves.onClick.AddListener( ItemBlockLeavesClick );
		ItemBlockWoodPlank.onClick.AddListener( ItemBlockWoodPlankClick );
		ItemBlockStoneBrick.onClick.AddListener( ItemBlockStoneBrickClick );
		ItemBlockLamp.onClick.AddListener( ItemBlockLampClick );
		ItemBlockGlass.onClick.AddListener( ItemBlockGlassClick );

		inventory1.onClick.AddListener( ItemBlock1Click );
		inventory2.onClick.AddListener( ItemBlock2Click );
		inventory3.onClick.AddListener( ItemBlock3Click );
		inventory4.onClick.AddListener( ItemBlock4Click );
		inventory5.onClick.AddListener( ItemBlock5Click );

		MenuResume.onClick.AddListener( MenuResumeClick );
		MenuSave.onClick.AddListener( MenuSaveClick );
		MenuMenu.onClick.AddListener( MenuMenuClick );
		MenuExit.onClick.AddListener( MenuExitClick );
	}
		
	// --------------------------------------------------------------------------------
	void ItemBlockStoneClick() { itemMove = 02; itemTexture = "tstone"; showInventoryMover(); }
	void ItemBlockGrassClick() { itemMove = 03; itemTexture = "tgrass"; showInventoryMover(); }
	void ItemBlockDirtClick() { itemMove = 04; itemTexture = "tdirt"; showInventoryMover(); }
	void ItemBlockCobbleStoneClick() { itemMove = 05; itemTexture = "tcobble"; showInventoryMover(); }
	void ItemBlockSandClick() { itemMove = 06; itemTexture = "tsand"; showInventoryMover(); }
	void ItemBlockGravelClick() { itemMove = 07; itemTexture = "tgravel"; showInventoryMover(); }
	void ItemBlockSnowClick() { itemMove = 08; itemTexture = "tsnow"; showInventoryMover(); }
	void ItemBlockWoodClick() { itemMove = 09; itemTexture = "twood"; showInventoryMover(); }
	void ItemBlockLeavesClick() { itemMove = 10; itemTexture = "tleaves"; showInventoryMover(); }
	void ItemBlockWoodPlankClick() { itemMove = 11; itemTexture = "twoodplank"; showInventoryMover(); }
	void ItemBlockStoneBrickClick() { itemMove = 12; itemTexture = "tstonebrick"; showInventoryMover(); }
	void ItemBlockLampClick() { itemMove = 13; itemTexture = "tlamp"; showInventoryMover(); }
	void ItemBlockGlassClick() { itemMove = 15; itemTexture = "tglass"; showInventoryMover(); }

	// --------------------------------------------------------------------------------
	void ItemBlock1Click() {
		if (itemMove >= 0) {
			Texture2D newTexture = (Texture2D)Resources.Load(itemTexture, typeof(Texture2D));
			GameObject.Find ("FrameItem1").GetComponentInChildren<RawImage>().texture = newTexture;
			items[0] = itemMove;
			ItemBlockFinish();
		}
	}

	void ItemBlock2Click() {
		if (itemMove >= 0) {
			Texture2D newTexture = (Texture2D)Resources.Load(itemTexture, typeof(Texture2D));
			GameObject.Find ("FrameItem2").GetComponentInChildren<RawImage>().texture = newTexture;
			items[1] = itemMove;
			ItemBlockFinish ();
		}
	}

	void ItemBlock3Click() {
		if (itemMove >= 0) {
			Texture2D newTexture = (Texture2D)Resources.Load(itemTexture, typeof(Texture2D));
			GameObject.Find ("FrameItem3").GetComponentInChildren<RawImage>().texture = newTexture;
			items[2] = itemMove;
			ItemBlockFinish();
		}
	}

	void ItemBlock4Click() {
		if (itemMove >= 0) {
			Texture2D newTexture = (Texture2D)Resources.Load(itemTexture, typeof(Texture2D));
			GameObject.Find ("FrameItem4").GetComponentInChildren<RawImage>().texture = newTexture;
			items[3] = itemMove;
			ItemBlockFinish();
		}
	}
	void ItemBlock5Click() {
		if (itemMove >= 0) {
			Texture2D newTexture = (Texture2D)Resources.Load(itemTexture, typeof(Texture2D));
			GameObject.Find ("FrameItem5").GetComponentInChildren<RawImage>().texture = newTexture;
			items[4] = itemMove;
			ItemBlockFinish();
		}
	}

	// --------------------------------------------------------------------------------
	void MenuResumeClick() {
		mouseLock = !mouseLock;
		UpdateMouseLock();
		ShowEscapeMenu (!mouseLock);
	}

	void MenuSaveClick() {
		map_relative.GetComponent<LoadSave>().SaveFile();

		mouseLock = !mouseLock;
		UpdateMouseLock();
		ShowEscapeMenu (!mouseLock);
	}

	void MenuMenuClick() { Debug.Log("BackToMenu"); SceneManager.LoadScene("MainMenu"); }
	void MenuExitClick() { Application.Quit(); }

	// --------------------------------------------------------------------------------
	void ItemBlockFinish() {
		handIntventorySelect(itemSelect);
		itemMove = -1;
		itemTexture = "";
		hideInventoryMover();
	}

	// --------------------------------------------------------------------------------
	void showInventoryMover() {
		inventoryMoverVisible.SetActive( true );
		Texture2D newTexture = (Texture2D)Resources.Load(itemTexture, typeof(Texture2D));
		inventoryMover.texture = newTexture;
	}

	// --------------------------------------------------------------------------------
	void moveInventoryMover() {
		if (inventoryMoverVisible.activeSelf) {
			inventoryMoverVisible.transform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		}
	}
	// --------------------------------------------------------------------------------
	void hideInventoryMover() {
		inventoryMoverVisible.SetActive( false );
	}

	// --------------------------------------------------------------------------------
	void UseKeys() {
		if( Input.GetKeyUp(KeyCode.Escape) ) {
			mouseLock = !mouseLock;
			UpdateMouseLock();

			ShowEscapeMenu (!mouseLock);
			ShowInventoryMenu( false );
		}

		if( Input.GetKeyUp(KeyCode.E) ) {
			mouseLock = !mouseLock;
			UpdateMouseLock();

			ShowEscapeMenu( false );
			ShowInventoryMenu(!mouseLock);
		}

		if (!mouseLock) { UpdateMouseLock(); }
	}

	// --------------------------------------------------------------------------------
	void UpdateMouseLock() {
		if (mouseLock) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1;
		} else if (!mouseLock) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Time.timeScale = 0;
		}
	}

	// --------------------------------------------------------------------------------
	void ShowEscapeMenu( bool enable ) { uiMenu.SetActive( enable ); }
	void ShowInventoryMenu( bool enable ) { uiInv.SetActive ( enable ); ItemBlockFinish(); } 
	// --------------------------------------------------------------------------------
}
// ####################################################################################