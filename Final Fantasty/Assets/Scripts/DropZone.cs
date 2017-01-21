﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler   {

	public void onPointerEnter(PointerEventData eventData){
		Debug.Log ("onPointerEnter");
	}
		
	// Use this for initialization
	public void OnPointerExit(PointerEventData eventData){
		Debug.Log ("onPointerExit");
	}

	public void OnDrop(PointerEventData eventData){
		Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);

		Draggable d =  eventData.pointerDrag.GetComponent<Draggable> ();
		if (d != null) {
			d.parentToReturnTo = this.transform;
		}
	}
}