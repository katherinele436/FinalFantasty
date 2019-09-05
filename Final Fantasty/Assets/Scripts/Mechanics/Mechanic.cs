using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * An abstract class outlining the basics of what mechanics can do.
 * For the time being, each type of method activation will be defined in
 * all of the mechanics but, in the actual implementation activation methods 
 * which do not apply will be nothing more than stubs.
 * 
 * Note: Mechanics are now explicitly attached to game objects but they also have 
 * 
 * Version 0.1 (Mostly a demonstration. I will rework this from a software archetecture point of view.
 * 
 * Author: Rocky Petkov
 */
public abstract class Mechanic : MonoBehaviour {


	private string name;			// The plain-text name of a mechanic. This must match the name of the class
	private bool activated;			// Tracks whether the mechanic has been activated yet.
	private string toolTip;			// A tooltip associated with the mechanic. May be useful for a hover over dialogue
	private Card parent;			// Allows a mechanic to access information pertaining to the card it belongs to!
	public bool inheritable;		// Tracks if the mechanic is able to be passed down to meals with this card


	public void init(string name, Card parentCard, string description, bool inheritable) {
		parent = parentCard;
		toolTip = description;
		activated = false;
		this.inheritable = inheritable;

	}

	/*
	 * Instantiates a mechanic of the same name as the string given to this 
	 * method. Note: This method MUST be updated whenever we add a new mechanic
	 * TODO: Find a better way to handle this!
	 */
	public static Mechanic instantiateByName(string mechanicName, Card parentCard) {
		switch(mechanicName) {
			case("AlDente"):
				AlDente newMechanic = parentCard.gameObject.AddComponent<AlDente>() as AlDente;
				newMechanic.init(parentCard);
				return (Mechanic) newMechanic;
				break;
			default:
			throw new MechanicNotFound(String.Format("The mechanic {0} could not be found", mechanicName));
				break;
		}
	}

	// This method will contain any effects that happen when a card is drawn into a player's hand
	public abstract void onDraw ();

	// Contains any effects that happen when a card enters play
	public abstract void onPlayEnter ();

	// Contains any effects that happens when ingredients containing this card are combined. 
	public abstract void onCombine ();

	// Contains any effects that are triggered when a card is "stacked" upon another
	public abstract void onStack (); 

	// Accessor for parent card object
	public Card getParent() {
		return parent;
	}


	public string getName() {
		return name;
	}

	// Accessor for tool tip
	public string getToolTip() {
		return toolTip;
	}

	// Accessor for the activated property
	public bool getActivated() {
		return activated;
	}

	// Flips the activated property from False to True
	public void activate() {
		if (! activated) {
			activated = true;		// Flip that!
		}
	}


}