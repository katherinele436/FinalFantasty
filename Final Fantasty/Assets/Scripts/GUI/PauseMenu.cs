using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseUI;

	private bool paused = false;

	void Start(){
		pauseUI.SetActive(false);
	}

	void Update(){
		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;
		}

		if (paused) {
			pauseUI.SetActive (true);
			Time.timeScale = 0;
		}

		if (!paused) {
			pauseUI.SetActive (false);
			Time.timeScale = 1;
		}
	}

	public void Resume(){
		paused = false;
	}
		
	public void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void MainMenu(){
		Application.LoadLevel (0);
	}

	public void Quit(){
		Application.Quit();
	}
}
