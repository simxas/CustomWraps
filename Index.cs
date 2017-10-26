using System.Collections;
using UnityEngine;
using SocketIO;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

public class Index : MonoBehaviour
{
	private SocketIOComponent socket;
	private GameObject go;

	public GameObject carDoorsL;
	public GameObject carDoorsR;
	public GameObject front;
	public GameObject back;
	public GameObject car;
	public GameObject doubleCab_roof;

	public GameObject sc_carDoorsL;
	public GameObject sc_carDoorsR;
	public GameObject sc_front;
	public GameObject sc_back;
	public GameObject sc_roof;
	public GameObject carSpaceCab;

	public GameObject mux_carDoorsL;
	public GameObject mux_carDoorsR;
	public GameObject mux_front;
	public GameObject mux_back;
	public GameObject mux_roof;
	public GameObject carMux;

	public GameObject chassisSC_carDoorsL;
	public GameObject chassisSC_carDoorsR;
	public GameObject chassisSC_front;
	public GameObject chassisSC_roof;
	public GameObject carChassisSC;

	public GameObject chassisSpaceC_carDoorsL;
	public GameObject chassisSpaceC_carDoorsR;
	public GameObject chassisSpaceC_front;
	public GameObject chassisSpaceC_roof;
	public GameObject carChassisSpaceC;

	public UnityEngine.Camera camera;
	private string nameOfImage;
	public string filePathRight;
	public string filePathLeft;
	public string filePathBack;
	public string filePathFront;
	public string filePathRoof;
	public string folderName;
	public string textureFile;

	//pasirinkto automobilio identifikatorius
	public string chosenCar;
	//pasirinktas tiling arba ne
	public string tiling;
	//kokio lygio tilinimas
	int tileX;
	int tileY;

	public List<string> names;
	enum GameState {Intro, LastListMember, Rendering, RenderingSC, RenderingDC, RenderingMux, RenderingChassisSC, RenderingChassisSpaceC, Saving};
	GameState currentState;
	float lastStateChange = 0.0f;
	public int frameRate = 25;
	private string exportFolder = "../export/";

	//nauju texturu dalis
	public Texture2D texR;
	public Texture2D texL;
	public Texture2D texF;
	public Texture2D texB;
	public Texture2D texRoof;

	//papildomos klases instancijuojamos
	Tiling Tiling = new Tiling();
	NoTiling NoTiling = new NoTiling();
	
	//AudioSource audio;
	//Kintamieji optimizavimui=====
	public int angle;
	public int rotation;
	public int index;

	public string dc_extension;
	public string sc_extension;
	public string mux_extension;
	public string chassisSC_extension;
	public string chassisSpaceC_extension;

	public string tileXs;
	public string tileYs;
	// int resWidth = 960;
	// int resHeight = 536;
	public int resWidth;
	public int resHeight;
	//		int resWidth = 3840;
	//		int resHeight = 2144;

	public RenderTexture rt;
	public Texture2D screenShot;
	public string pathToFile;
	public byte[] bytes;
	
	public void Start() 
	{
		QualitySettings.antiAliasing = 8;
		//audio = GetComponent<AudioSource>();
		this.go = GameObject.Find("SocketIO");
		this.socket = this.go.GetComponent<SocketIOComponent>();
		
		carDoorsL = GameObject.Find("doubleCab_side_left");
		carDoorsR = GameObject.Find("doubleCab_side_right");
		front = GameObject.Find("doubleCab_front");
		back = GameObject.Find("doubleCab_back");
		doubleCab_roof = GameObject.Find("doubleCab_roof");
		car = GameObject.Find("ParentDoubleCab");

		sc_carDoorsL = GameObject.Find("sc_side_left");
		sc_carDoorsR = GameObject.Find("sc_side_right");
		sc_front = GameObject.Find("sc_front");
		sc_back = GameObject.Find("sc_back");
		sc_roof = GameObject.Find ("sc_roof");
		carSpaceCab = GameObject.Find("ParentSpaceCab");

		mux_carDoorsL = GameObject.Find("mux_side_left");
		mux_carDoorsR = GameObject.Find("mux_side_right");
		mux_front = GameObject.Find("mux_front");
		mux_back = GameObject.Find("mux_back");
		mux_roof = GameObject.Find ("mux_roof");
		carMux = GameObject.Find("ParentMux");

		chassisSC_carDoorsL = GameObject.Find("chassisSingleCab_left");
		chassisSC_carDoorsR = GameObject.Find("chassisSingleCab_right");
		chassisSC_front = GameObject.Find("chassisSingleCab_front");
		chassisSC_roof = GameObject.Find("chassisSingleCab_roof");
		carChassisSC = GameObject.Find("ParentChassisSingleCab");

		chassisSpaceC_carDoorsL = GameObject.Find("chassisSpaceCab_left");
		chassisSpaceC_carDoorsR = GameObject.Find("chassisSpaceCab_right");
		chassisSpaceC_front = GameObject.Find("chassisSpaceCab_front");
		chassisSpaceC_roof = GameObject.Find("chassisSpaceCab_roof");
		carChassisSpaceC = GameObject.Find("ParentSpaceCabChassis");

		//setting objects to hide
		car.active = false;
		carSpaceCab.active = false;
		carMux.active = false;
		carChassisSC.active = false;
		carChassisSpaceC.active = false;
		
		this.camera = GetComponent<Camera>();
		
		SetCurrentState(GameState.Intro);
		
		//audio.mute = true;
	}
	
	void SetCurrentState(GameState state){
		currentState = state;
		lastStateChange = Time.time;
	}
	
	float GetStateElapsed() {
		return Time.time - lastStateChange;
	}
	
	public string getAssetsPath() {
		return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CustomWraps/");
	}
	
	
	public string getAssetsPathR() {
		return Path.Combine(this.getAssetsPath(), "right/");
	}
	public string getAssetsPathL() {
		return Path.Combine(this.getAssetsPath(), "left/");
	}
	public string getAssetsPathB() {
		return Path.Combine(this.getAssetsPath(), "back/");
	}
	public string getAssetsPathF() {
		return Path.Combine(this.getAssetsPath(), "front/");
	}
	public string getAssetsPathRoof() {
		return Path.Combine(this.getAssetsPath(), "roof/");
	}

	public string getExportPath() {
		return Path.Combine(this.getAssetsPath(), "export/");
	}

	//---------------------------------------------------
	//UPDATE
	//---------------------------------------------------
	public void Update() {

		
		if (Input.GetKeyDown("space")) {
			car.transform.Rotate(new Vector3(0, 0, -10));
		}
		
		switch (currentState) {
		case GameState.Intro:
			
			if (names.Count > 0) {						
				SetCurrentState(GameState.LastListMember);
			}				
			break;
			
		case GameState.LastListMember:
			textureFile = names[names.Count - 1];
			names.Remove(textureFile);

			//chosenCar = "sc";
			switch (chosenCar)
			{
			case "lsspacecab":
				carSpaceCab.active = true;
				SetCurrentState(GameState.RenderingSC);
				break;
			case "doublecab":
				car.active = true;
				SetCurrentState(GameState.RenderingDC);
				break;
			case "mux":
				carMux.active = true;
				SetCurrentState(GameState.RenderingMux);
				break;
			case "lxcabchassis":
				carChassisSC.active = true;
				SetCurrentState(GameState.RenderingChassisSC);
				break;
			case "lxspacecab":
				carChassisSpaceC.active = true;
				SetCurrentState(GameState.RenderingChassisSpaceC);
				break;
			default:
				Debug.Log ("There are no cars with the selected name.");
				break;
			}

			//SetCurrentState(GameState.Rendering);				
			break;
			
		case GameState.RenderingDC:
			
			filePathRight = this.getAssetsPathR() + textureFile;
			filePathLeft = this.getAssetsPathL() + textureFile;
			filePathBack = this.getAssetsPathB() + textureFile;
			filePathFront = this.getAssetsPathF() + textureFile;
			//roof dalis
			filePathRoof = this.getAssetsPathRoof() + "roofTexture.png";

			//issaukiamas renderis
			if(tiling == "false"){
				NoTiling.renderDoubleCab(texR, texL, texF, texB, texRoof, filePathRight, filePathLeft, filePathBack, filePathFront, filePathRoof, carDoorsR, carDoorsL, front, back, doubleCab_roof);
			}else if(tiling == "true"){
				Tiling.renderTilingDoubleCab(tileX, tileY, texR, texL, texF, texB, texRoof, filePathLeft, carDoorsR, carDoorsL, front, back, doubleCab_roof);
			}
			//kol kas direktorijos sukurimui naudoju right failo pavadinima				
			dc_extension = System.IO.Path.GetExtension(filePathRight);
			this.folderName = textureFile.Substring(0, textureFile.Length - dc_extension.Length);
			
			System.IO.Directory.CreateDirectory(this.getExportPath() + this.folderName);
			
			SetCurrentState(GameState.Saving);				
			break;

		case GameState.RenderingSC:
			
			filePathRight = this.getAssetsPathR() + textureFile;
			filePathLeft = this.getAssetsPathL() + textureFile;
			filePathBack = this.getAssetsPathB() + textureFile;
			filePathFront = this.getAssetsPathF() + textureFile;
			filePathRoof = this.getAssetsPathRoof() + "roofTexture.png";

			//issaukiamas renderis
			if(tiling == "false"){
				NoTiling.renderSpaceCab(texR, texL, texF, texB, texRoof, filePathRight, filePathLeft, filePathBack, filePathFront, filePathRoof, sc_carDoorsR, sc_carDoorsL, sc_front, sc_back, sc_roof);
			}else if(tiling == "true"){
				Tiling.renderTilingSpaceCab(tileX, tileY, texR, texL, texF, texB, texRoof, filePathLeft, sc_carDoorsR, sc_carDoorsL, sc_front, sc_back, sc_roof);
			}
			sc_extension = System.IO.Path.GetExtension(filePathRight);
			this.folderName = textureFile.Substring(0, textureFile.Length - sc_extension.Length);
			
			System.IO.Directory.CreateDirectory(this.getExportPath() + this.folderName);
			
			SetCurrentState(GameState.Saving);				
			break;

		case GameState.RenderingMux:
			
			filePathRight = this.getAssetsPathR() + textureFile;
			filePathLeft = this.getAssetsPathL() + textureFile;
			filePathBack = this.getAssetsPathB() + textureFile;
			filePathFront = this.getAssetsPathF() + textureFile;
			filePathRoof = this.getAssetsPathRoof() + "roofTexture.png";

			//issaukiamas renderis
			if(tiling == "false"){
				NoTiling.renderMux(texR, texL, texF, texB, texRoof, filePathRight, filePathLeft, filePathBack, filePathFront, filePathRoof, mux_carDoorsR, mux_carDoorsL, mux_front, mux_back, mux_roof);
			}else if(tiling == "true"){
				Tiling.renderTilingMux(tileX, tileY, texR, texL, texF, texB, texRoof, filePathLeft, mux_carDoorsR, mux_carDoorsL, mux_front, mux_back, mux_roof);
			}
			mux_extension = System.IO.Path.GetExtension(filePathRight);
			this.folderName = textureFile.Substring(0, textureFile.Length - mux_extension.Length);
			
			System.IO.Directory.CreateDirectory(this.getExportPath() + this.folderName);
			
			SetCurrentState(GameState.Saving);				
			break;
		
		case GameState.RenderingChassisSC:
			
			filePathRight = this.getAssetsPathR() + textureFile;
			filePathLeft = this.getAssetsPathL() + textureFile;
			filePathBack = this.getAssetsPathB() + textureFile;
			filePathFront = this.getAssetsPathF() + textureFile;
			filePathRoof = this.getAssetsPathRoof() + "roofTexture.png";

			//issaukiamas renderis
			if(tiling == "false"){
				NoTiling.renderChassisSC(texR, texL, texF, texB, texRoof, filePathRight, filePathLeft, filePathBack, filePathFront, filePathRoof, chassisSC_carDoorsR, chassisSC_carDoorsL, chassisSC_front, chassisSC_roof);
			}else if(tiling == "true"){
				Tiling.renderTilingChassisSC(tileX, tileY, texR, texL, texF, texB, texRoof, filePathLeft, chassisSC_carDoorsR, chassisSC_carDoorsL, chassisSC_front, chassisSC_roof);
			}
			chassisSC_extension = System.IO.Path.GetExtension(filePathRight);
			this.folderName = textureFile.Substring(0, textureFile.Length - chassisSC_extension.Length);
			
			System.IO.Directory.CreateDirectory(this.getExportPath() + this.folderName);
			
			SetCurrentState(GameState.Saving);				
			break;

		case GameState.RenderingChassisSpaceC:
			
			filePathRight = this.getAssetsPathR() + textureFile;
			filePathLeft = this.getAssetsPathL() + textureFile;
			filePathBack = this.getAssetsPathB() + textureFile;
			filePathFront = this.getAssetsPathF() + textureFile;
			filePathRoof = this.getAssetsPathRoof() + "roofTexture.png";

			//issaukiamas renderis
			if(tiling == "false"){
				NoTiling.renderChassisSpaceC(texR, texL, texF, texB, texRoof, filePathRight, filePathLeft, filePathBack, filePathFront, filePathRoof, chassisSpaceC_carDoorsR, chassisSpaceC_carDoorsL, chassisSpaceC_front, chassisSpaceC_roof);
			}else if(tiling == "true"){
				Tiling.renderTilingChassisSpaceC(tileX, tileY, texR, texL, texF, texB, texRoof, filePathLeft, chassisSpaceC_carDoorsR, chassisSpaceC_carDoorsL, chassisSpaceC_front, chassisSpaceC_roof);
			}
			chassisSpaceC_extension = System.IO.Path.GetExtension(filePathRight);
			this.folderName = textureFile.Substring(0, textureFile.Length - chassisSpaceC_extension.Length);
			
			System.IO.Directory.CreateDirectory(this.getExportPath() + this.folderName);
			
			SetCurrentState(GameState.Saving);				
			break;
			
		case GameState.Saving:
			angle = 0;
			rotation = -10;			
			index = 0;
			switch (chosenCar)
			{
			case "lsspacecab":

				do {
					carSpaceCab.transform.rotation = Quaternion.Euler(0, angle, 0);
					saveImage(index.ToString("D2")+".png");
					
					angle += rotation; 
					if (angle == 360) angle = 0; 
					else if (angle == -10) angle = 350;
				} while (++index < 37);

				break;
			case "doublecab":
				do {
					car.transform.rotation = Quaternion.Euler(0, angle, 0);
					saveImage(index.ToString("D2")+".png");
					
					angle += rotation; 
					if (angle == 360) angle = 0; 
					else if (angle == -10) angle = 350;
				} while (++index < 37);
				break;
			case "mux":
				do {
					carMux.transform.rotation = Quaternion.Euler(0, angle, 0);
					saveImage(index.ToString("D2")+".png");
					
					angle += rotation; 
					if (angle == 360) angle = 0; 
					else if (angle == -10) angle = 350;
				} while (++index < 37);
				break;
			case "lxcabchassis":
				do {
					carChassisSC.transform.rotation = Quaternion.Euler(0, angle, 0);
					saveImage(index.ToString("D2")+".png");
					
					angle += rotation; 
					if (angle == 360) angle = 0; 
					else if (angle == -10) angle = 350;
				} while (++index < 37);
				break;
			case "lxspacecab":
				do {
					carChassisSpaceC.transform.rotation = Quaternion.Euler(0, angle, 0);
					saveImage(index.ToString("D2")+".png");
					
					angle += rotation; 
					if (angle == 360) angle = 0; 
					else if (angle == -10) angle = 350;
				} while (++index < 37);
				break;
			default:
				Debug.Log ("There are no cars with the selected name.");
				break;
			}
			
			Debug.Log("Done, have a good day!");
			
			Dictionary<string, string> createdFolder = new Dictionary<string, string>();
			createdFolder["folderName"] = this.folderName;
			this.socket.Emit("done", new JSONObject(createdFolder));

			car.active = false;
			carSpaceCab.active = false;
			carMux.active = false;
			carChassisSC.active = false;
			carChassisSpaceC.active = false;

			//si dalis turetu atlaisvinti/isvalyti memory kur nusede nenaudojamos texturos


			texR = null;
			texL = null;
			texF = null;
			texB = null;
			texRoof = null;
			chosenCar = null;
			filePathRight = null;
			filePathLeft = null;
			filePathBack = null;
			filePathFront = null;
			filePathRoof = null;
			textureFile = null;
			dc_extension = null;
			sc_extension = null;
			mux_extension = null;
			chassisSC_extension = null;
			chassisSpaceC_extension = null;
			folderName = null;
			angle = 0;
			rotation = 0;
			index = 0;
			tileXs = null;
			tileYs = null;
			tileX = 0;
			tileY = 0;
			nameOfImage = null;			
			chosenCar = null;			
//			tiling = null;
			resWidth = 0;
			resHeight = 0;
			//bandau releasinti textura, kuria naudoju saugojimui.
			rt.Release();
			rt = null;
			screenShot = null;
			pathToFile = null;
			bytes = null;

			Resources.UnloadUnusedAssets();

			SetCurrentState(GameState.Intro);
			//===================
			//quits application
			//===================
			if (names.Count == 0) {						
				Application.Quit();
			}

			break;
		}
		
		this.socket.On("render", Test);
		
	}

	
	public void saveImage(string fileName) {
		//resWidth = 960;
		//resHeight = 536;
		resWidth = 1920;
		resHeight = 1072;
//		resWidth = 3840;
//		resHeight = 2144;
		
		rt = new RenderTexture(resWidth, resHeight, 32);
		this.camera.targetTexture = rt;
		
		screenShot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
		this.camera.Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
		this.camera.targetTexture = null;
		RenderTexture.active = null;
		Destroy(rt);

		//screenShot = TextureFilter.Convolution (screenShot, TextureFilter.GaussianKernel ((float)Math.Sqrt (2), 3, false), 1);
		TextureScale.Bilinear (screenShot, 960, 536);
		bytes = screenShot.EncodeToPNG();

		pathToFile = this.getExportPath() + this.folderName + "/" + fileName;
		
		System.IO.File.WriteAllBytes(pathToFile, bytes);

	}
	
	public void Test(SocketIOEvent e)
	{
		nameOfImage = e.data["file"].ToString();		
		nameOfImage = nameOfImage.Replace("\"",string.Empty);

		chosenCar = e.data ["variant"].ToString ();
		chosenCar = chosenCar.Replace("\"",string.Empty);

		tiling = e.data["tiling"].ToString();
		tiling = tiling.Replace("\"",string.Empty);
		//kintamieji skirti paimti is json skaicius ir paskui juos convertinti i int
		tileXs = e.data["tileX"].ToString();
		tileXs = tileXs.Replace("\"",string.Empty);
		tileYs = e.data["tileY"].ToString();
		tileYs = tileYs.Replace("\"",string.Empty);

		tileX = int.Parse(tileXs);
		tileY = int.Parse(tileYs);
//		Debug.Log ("tileX yra: "+tileX+" ir tileY yra: "+tileY+", ir tiling yra: "+tiling);

//		Dictionary<string, string> failas = new Dictionary<string, string>();
//		failas["file_get"] = nameOfImage;
//		this.socket.Emit("file_get", new JSONObject(failas));

		if (!names.Contains(nameOfImage)){
			names.Add(nameOfImage);
		}		
	}
}