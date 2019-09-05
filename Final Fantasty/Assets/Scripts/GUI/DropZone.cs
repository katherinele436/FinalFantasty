using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public Text textBox;
	public string message;
	public IEnumerator messageRoutine;

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
		//Debug.Log("OnPointerExit");
		if(eventData.pointerDrag == null)
			return;

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}


	}

	public void OnDrop(PointerEventData eventData) {
		//Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
		if(d != null) {
			
			d.parentToReturnTo = this.transform;


		}

		if (textBox == null)
			return;
		
		if (messageRoutine != null) {
			StopCoroutine (messageRoutine);
		}
		messageRoutine = ShowMessage (message, 4);
		StartCoroutine (messageRoutine);
	}

	IEnumerator ShowMessage(string text, float delay){
		textBox.text = text;
		textBox.enabled = true;
		yield return new WaitForSeconds (delay);
		textBox.enabled = false;
	}
}
