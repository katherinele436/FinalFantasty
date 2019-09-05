using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class timer : MonoBehaviour {

	public double timeRemaining = 5;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;
	}


	void OnGUI(){
		if (timeRemaining > 0) {
			GUI.Label (new Rect (100,100,100,30), "Time Remaining: " + (int)timeRemaining);

		} else {
			GUI.Label (new Rect (50,25,100,30), "Time's up");
		}
	}
	/*
	IEnumerator ShowMessage(string text, float delay){
		time.text = text;
		prepText.enabled = true;
		yield return new WaitForSeconds (delay);
		prepText.enabled = false;
	}
	*/
}
