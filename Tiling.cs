using UnityEngine;
using System.Collections;
using SocketIO;

public class Tiling : MonoBehaviour {
	private SocketIOComponent socket;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void renderTilingDoubleCab(int tileX, int tileY, Texture2D texR, Texture2D texL, Texture2D texF, Texture2D texB, Texture2D texRoof, string filePathLeft, GameObject carDoorsR, GameObject carDoorsL, GameObject front, GameObject back, GameObject doubleCab_roof) {
		//============
		//RIGHT=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesR = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendR = carDoorsR.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			//var texR = new Texture2D(1, 1);
			texR = new Texture2D(1, 1);
			texR.LoadImage(bytesR);
			//memory leak possible fix
			Destroy(rendR.material.mainTexture);
			rendR.material.mainTexture = texR;
			rendR.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//LEFT========
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesL = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendL = carDoorsL.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texL = new Texture2D(1, 1);
			texL.LoadImage(bytesL);
			//memory leak possible fix
			Destroy(rendL.material.mainTexture);
			rendL.material.mainTexture = texL;
			rendL.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//Back========
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesB = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendB = back.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texB = new Texture2D(1, 1);
			texB.LoadImage(bytesB);
			//memory leak possible fix
			Destroy(rendB.material.mainTexture);
			rendB.material.mainTexture = texB;
			rendB.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//FRONT=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesF = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendF = front.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texF = new Texture2D(1, 1);
			texF.LoadImage(bytesF);
			//memory leak possible fix
			Destroy(rendF.material.mainTexture);
			rendF.material.mainTexture = texF;
			rendF.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//ROOF=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesRoof = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendRoof = doubleCab_roof.GetComponent<Renderer>();
			texRoof = new Texture2D(1, 1);
			texRoof.LoadImage(bytesRoof);
			//memory leak possible fix
			Destroy(rendRoof.material.mainTexture);
			rendRoof.material.mainTexture = texRoof;
			rendRoof.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		
	}//end of renderDoubleCab
	//===========================================================================================

	public void renderTilingSpaceCab(int tileX, int tileY, Texture2D texR, Texture2D texL, Texture2D texF, Texture2D texB, Texture2D texRoof, string filePathLeft, GameObject sc_carDoorsR, GameObject sc_carDoorsL, GameObject sc_front, GameObject sc_back, GameObject sc_roof) {
		//============
		//RIGHT=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesR = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendR = sc_carDoorsR.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			//var texR = new Texture2D(1, 1);
			texR = new Texture2D(1, 1);
			texR.LoadImage(bytesR);
			//memory leak possible fix
			Destroy(rendR.material.mainTexture);
			rendR.material.mainTexture = texR;
			rendR.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//LEFT========
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesL = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendL = sc_carDoorsL.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texL = new Texture2D(1, 1);
			texL.LoadImage(bytesL);
			//memory leak possible fix
			Destroy(rendL.material.mainTexture);
			rendL.material.mainTexture = texL;
			rendL.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//Back========
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesB = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendB = sc_back.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texB = new Texture2D(1, 1);
			texB.LoadImage(bytesB);
			//memory leak possible fix
			Destroy(rendB.material.mainTexture);
			rendB.material.mainTexture = texB;
			rendB.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//FRONT=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesF = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendF = sc_front.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texF = new Texture2D(1, 1);
			texF.LoadImage(bytesF);
			//memory leak possible fix
			Destroy(rendF.material.mainTexture);
			rendF.material.mainTexture = texF;
			rendF.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//ROOF=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesRoof = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendRoof = sc_roof.GetComponent<Renderer>();
			texRoof = new Texture2D(1, 1);
			texRoof.LoadImage(bytesRoof);
			//memory leak possible fix
			Destroy(rendRoof.material.mainTexture);
			rendRoof.material.mainTexture = texRoof;
			rendRoof.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		
	}//end of renderSpaceCab
	//===========================================================================================

	//MUX
	public void renderTilingMux(int tileX, int tileY, Texture2D texR, Texture2D texL, Texture2D texF, Texture2D texB, Texture2D texRoof, string filePathLeft, GameObject mux_carDoorsR, GameObject mux_carDoorsL, GameObject mux_front, GameObject mux_back, GameObject mux_roof) {
		//============
		//RIGHT=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesR = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendR = mux_carDoorsR.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			//var texR = new Texture2D(1, 1);
			texR = new Texture2D(1, 1);
			texR.LoadImage(bytesR);
			//memory leak possible fix
			Destroy(rendR.material.mainTexture);
			rendR.material.mainTexture = texR;
			rendR.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//LEFT========
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesL = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendL = mux_carDoorsL.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texL = new Texture2D(1, 1);
			texL.LoadImage(bytesL);
			//memory leak possible fix
			Destroy(rendL.material.mainTexture);
			rendL.material.mainTexture = texL;
			//sita eilute atsakinga uz tilinima
			rendL.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//Back========
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesB = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendB = mux_back.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texB = new Texture2D(1, 1);
			texB.LoadImage(bytesB);
			//memory leak possible fix
			Destroy(rendB.material.mainTexture);
			rendB.material.mainTexture = texB;
			rendB.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//FRONT=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesF = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendF = mux_front.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texF = new Texture2D(1, 1);
			texF.LoadImage(bytesF);
			//memory leak possible fix
			Destroy(rendF.material.mainTexture);
			rendF.material.mainTexture = texF;
			rendF.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//ROOF=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesRoof = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendRoof = mux_roof.GetComponent<Renderer>();
			texRoof = new Texture2D(1, 1);
			texRoof.LoadImage(bytesRoof);
			//memory leak possible fix
			Destroy(rendRoof.material.mainTexture);
			rendRoof.material.mainTexture = texRoof;
			rendRoof.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		
	}//end of renderMux
	//===========================================================================================

	//CHASSISSINGLECAB
	public void renderTilingChassisSC(int tileX, int tileY, Texture2D texR, Texture2D texL, Texture2D texF, Texture2D texB, Texture2D texRoof, string filePathLeft, GameObject chassisSC_carDoorsR, GameObject chassisSC_carDoorsL, GameObject chassisSC_front, GameObject chassisSC_roof) {
		//============
		//RIGHT=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesR = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendR = chassisSC_carDoorsR.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			//var texR = new Texture2D(1, 1);
			texR = new Texture2D(1, 1);
			texR.LoadImage(bytesR);
			//memory leak possible fix
			Destroy(rendR.material.mainTexture);
			rendR.material.mainTexture = texR;
			rendR.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//LEFT========
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesL = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendL = chassisSC_carDoorsL.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texL = new Texture2D(1, 1);
			texL.LoadImage(bytesL);
			//memory leak possible fix
			Destroy(rendL.material.mainTexture);
			rendL.material.mainTexture = texL;
			rendL.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//FRONT=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesF = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendF = chassisSC_front.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texF = new Texture2D(1, 1);
			texF.LoadImage(bytesF);
			//memory leak possible fix
			Destroy(rendF.material.mainTexture);
			rendF.material.mainTexture = texF;
			rendF.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//ROOF=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesRoof = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendRoof = chassisSC_roof.GetComponent<Renderer>();
			texRoof = new Texture2D(1, 1);
			texRoof.LoadImage(bytesRoof);
			//memory leak possible fix
			Destroy(rendRoof.material.mainTexture);
			rendRoof.material.mainTexture = texRoof;
			rendRoof.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		
	}//end of renderChassisSC
	//===========================================================================================

	//CHASSISSPACECAB
	public void renderTilingChassisSpaceC(int tileX, int tileY, Texture2D texR, Texture2D texL, Texture2D texF, Texture2D texB, Texture2D texRoof, string filePathLeft, GameObject chassisSpaceC_carDoorsR, GameObject chassisSpaceC_carDoorsL, GameObject chassisSpaceC_front, GameObject chassisSpaceC_roof) {
		//============
		//RIGHT=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesR = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendR = chassisSpaceC_carDoorsR.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			//var texR = new Texture2D(1, 1);
			texR = new Texture2D(1, 1);
			texR.LoadImage(bytesR);
			//memory leak possible fix
			Destroy(rendR.material.mainTexture);
			rendR.material.mainTexture = texR;
			rendR.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//LEFT========
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesL = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendL = chassisSpaceC_carDoorsL.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texL = new Texture2D(1, 1);
			texL.LoadImage(bytesL);
			//memory leak possible fix
			Destroy(rendL.material.mainTexture);
			rendL.material.mainTexture = texL;
			rendL.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//FRONT=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesF = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendF = chassisSpaceC_front.GetComponent<Renderer>();
			// Renderer front = front.GetComponent<Renderer>();
			texF = new Texture2D(1, 1);
			texF.LoadImage(bytesF);
			//memory leak possible fix
			Destroy(rendF.material.mainTexture);
			rendF.material.mainTexture = texF;
			rendF.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		//============
		//ROOF=======
		//============
		if (System.IO.File.Exists(filePathLeft)) {
			var bytesRoof = System.IO.File.ReadAllBytes(filePathLeft);
			Renderer rendRoof = chassisSpaceC_roof.GetComponent<Renderer>();
			texRoof = new Texture2D(1, 1);
			texRoof.LoadImage(bytesRoof);
			//memory leak possible fix
			Destroy(rendRoof.material.mainTexture);
			rendRoof.material.mainTexture = texRoof;
			rendRoof.material.mainTextureScale = new Vector2(tileX, tileY);
		} else {
			this.socket.Emit("Nerasta_textura");
		}
		
	}//end of renderChassisSpaceC
}
