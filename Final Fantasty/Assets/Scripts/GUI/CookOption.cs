using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class CookOption : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public Text textBox;
	public string message;
	public IEnumerator messageRoutine;
	public GameObject buttonBox;
	public int buttonInt;
	private bool noButton;
	private GameObject cookButton;
	private int dropzoneObjects;

	// Use this for initialization
	void Start () {
		noButton = false;
		buttonInt = 0;
		dropzoneObjects = 0;
	}

	public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("OnPointerEnter");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			d.placeholderParent = this.transform;
		}



	}

	public void OnPointerExit(PointerEventData eventData) {
		Debug.Log("OnPointerExit");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}


		if(buttonInt< 2 && noButton == true){
			Destroy(cookButton);
			noButton = false;
		}
	}

	public void OnDrop(PointerEventData eventData) {


		//Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);

		Draggable d = eventData.pointerDrag.GetComponent<Draggable> ();
		if (d != null){
			d.parentToReturnTo = this.transform;
		}

	
		buttonInt = buttonInt + 1;
		if (buttonInt >= 2 && noButton==false) {
			Instantiate (buttonBox,new Vector3(0,0,0),new Quaternion(0,0,0,0), this.transform);
			noButton = true;
		}

	}
}