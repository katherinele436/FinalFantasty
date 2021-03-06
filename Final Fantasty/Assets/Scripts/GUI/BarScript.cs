﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	[SerializeField]
	private float fillAmount;

	[SerializeField]
	private Image Content;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();
	}


	private void HandleBar(){
		Content.fillAmount = Map(0,0,5,0,1);
	}

	private float Map(float value, float inMin, float inMax,float outMin, float outMax){
		return(value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
