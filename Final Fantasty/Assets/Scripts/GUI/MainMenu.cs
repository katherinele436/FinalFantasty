using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;

	public float guiPlacementY;
	void OnGUI(){
		//display background textture
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), backgroundTexture);

		//display buttons 

		if(GUI.Button(new Rect(Screen.width *.25f, Screen.height *.5f, Screen.width *.5f, Screen.height *.1f), "Play Game")){
			
		}

		if(GUI.Button(new Rect(Screen.width *.25f, Screen.height *.5f, Screen.width * .7f, Screen.height *.1f), "Option")){

		}
	}
}
