using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This is intended to be a rough sketch of
 * card implementation only
 */
public class Card : MonoBehaviour {

	public const int NUM_STATS = 6;
	public static GameObject CardPrefab;													// Drag this over in the editor

	private string name;
	private string type;
	private Sprite graphic;
	private List<Mechanic> mechanics;
	private byte[] stats;


	// Use this for initialization
	void Start () {
		
	}

	// Instantiates a new card prefab object and returns reference to its card script
	public static void instantiateCard(DatabaseEntry cardInfo) {
		GameObject newObj = Instantiate(CardPrefab);								// TODO Ask the location manager where to put the card!			
		Card newCard = newObj.GetComponent<Card>();
		newCard.Init(cardInfo);	
	}

	// Initialises a card from a database entry
	// TODO Make compataible with DatabaseEntry accessors
	void Init(DatabaseEntry cardInfo) {
		name = cardInfo.name;
		type = cardInfo.type;
		stats = cardInfo.stats;
		mechanics = instantiateMechanics(cardInfo.mechanics);
		
		if (type == "Meal") {
			string spriteLocation = ImageProcessing.createMealCard(cardInfo);		//  Creating meal card and getting its location on disk
			graphic = IMG2Sprite.instance.LoadNewSprite(spriteLocation);			
			// TODO Place card appropriately
		}
		else {
			graphic = IMG2Sprite.instance.LoadNewSprite(cardInfo.spriteLocation);	// Creating a sprite from said meal card
			// TODO Position card correctly
		}
	return;
	}

	/*
	 * Reads through a supplied list of mechanics and adding instantiated
	 * copies of each mechanic to a list of mechanics that is then returned
	 * to the calling environment.
	 */
	private List<Mechanic> instantiateMechanics(List<string> mechanicStrings) {
		List<Mechanic> mechanicList = new List<Mechanic>(mechanicStrings.Count);
		foreach(string mechanic in mechanicStrings) {
			try {
				Mechanic.instantiateByName(mechanic, this);							// Instantiates the mechanic by name
			}
			catch (System.Exception e) {
				Debug.Log("Mechanic is not implemented. Double check database");	// If the mechanic doesn't exist print to debug log and move on
			}
		}
		return mechanicList;
	}

	// Update is called once per frame
	void Update () {
		
	}

	// Fetch method for stats
	public byte[] getStats() {return stats;}

	// Sets stats to be the supplied array
	// If supplied array is not of correct size, it will simply preserve the current set of stats
	public void setStats(byte[] newStats) {
		if (newStats.Length == NUM_STATS) {
			stats = newStats;
		}
		return;
	}

	// Returns a list of the card's mechanics!
	public List<Mechanic> getMechanics() {
		return mechanics;
	}
}
