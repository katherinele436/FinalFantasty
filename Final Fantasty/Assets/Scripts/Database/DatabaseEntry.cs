using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

// A tiny class for holding the database entry of a card!
// <3 Rocky
// TODO Write get and set methods, so we can have proper encapsulation
public class DatabaseEntry {
	public string name;
	public string description;
	public string type;
	public string artLocation;						// For central card art
	public string spriteLocation;					// For a completed sprite. Null if a meal card!
	public string ingredientTag;					// Null if not an ingredient
	public string tag;
	public List<string> mechanics;				
	public bool multiItems;							// We may want to have this kind of check at the GUI level
	public byte[] stats;						    // Take note of the fact I'm storing this in a byte!

	/*
	 * Constructor takes an XML node and loops through sub-nodes assogning
	 * data to correct attributes!
	*/
	public DatabaseEntry(XmlNode cardInfo) {
		string[] python_List_Delimiters = {"[", ",", "]", "(", ")"};
		int i;
		XmlNodeList itemContent = cardInfo.ChildNodes;
		foreach(XmlNode data in itemContent){
			switch(data.Name){
				case "Type":
					type = data.InnerText;
					if (type != "Tool") {
						multiItems = true;			
					}
				break;
				case "Name":
					name = data.InnerText;
				break;
				case "Description":
					description = data.InnerText;
				break;
				case "Stats":
					stats = new byte[6];
					string[] statString = data.InnerText.Split(python_List_Delimiters, 
											StringSplitOptions.RemoveEmptyEntries);		// Parsing into array of strings!
					for (i = 0; i < 6; i++) {
						stats[i] = Byte.Parse(statString[i]);							// Writing to stats array!
					}
				break;
				case "Mechanics":
					string[] mechanicsTemp = data.InnerText.Split(python_List_Delimiters, StringSplitOptions.RemoveEmptyEntries);
					mechanics = new List<string>(mechanicsTemp);
				break;
				case "Art":
					artLocation = data.InnerText;
				break;
				case "Picture Location":
					spriteLocation = data.InnerText;
				break;
				case "Ingredient Tag":
					ingredientTag = data.InnerText;
				break;
				case "Tag":
					tag = data.InnerText;
				break;
			}
		}
	}

	/* 
	 * This constructor should only be called in the act of procedrually generating cards. Everything that is
	 * not required by this special case will be passed into the "Clone" constructor as a NULL value.
	 * As a result, it is best to use with caution
	 */
	public DatabaseEntry(string name, string type, string artLocation, List<string> mechanics, byte[] stats) {

		// TODO Find a bunch of quotes about exploration!
		string mistakeDescription = "We shall not cease from exploration, and the end of all our exploring "  
									+ "will be to arrive where we started and know the place for the first time. - T.S. Elliot";

		// Seriously doe, fuck it. I'd rather have code repition then twist to C#'s evil clutches.
		if (stats.Length != 6) {
			throw new Exception("Improper stat array given!");
		}
		this.name 			= name;
		this.description	= mistakeDescription;
		this.type			= type;
		this.artLocation	= artLocation;			// For central card art
		this.spriteLocation = null;					// For a completed sprite. Null if a meal card!
		this.ingredientTag	= null;					// Null if not an ingredient
		this.mechanics	    = mechanics;
		this.tag 			= name;
		this.multiItems		= true;
		this.stats			= stats;							// Take note of the fact I'm storing this in a byte!\
	
	}

	// Returns a deep copy of this database entry
	public DatabaseEntry clone() {
		return new DatabaseEntry(name, description, type, artLocation, 
			spriteLocation, ingredientTag, mechanics, multiItems, stats);	// Return a brand new DB entry with same type of info
	}

	// This constructor takes each and every bit of information individually. It is only called by the clone method and the 
	public DatabaseEntry(string name, string description, string type, string artLocation, string spriteLocation,
		string ingredientTag, List<string> mechanics, bool multiItems, byte[] stats) {

		// Check if the stats array is of the correct size
		if (stats.Length != 6) {
			throw new Exception("Improper stat array given!");
		}
		this.name 			= name;
		this.description	= description;
		this.type			= type;
		this.artLocation	= artLocation;						// For central card art
		this.spriteLocation = spriteLocation;					// For a completed sprite. Null if a meal card!
		this.ingredientTag	= ingredientTag;					// Null if not an ingredient
		this.mechanics	    = mechanics;			
		this.multiItems		= multiItems;
		this.stats			= stats;							// Take note of the fact I'm storing this in a byte!\
	}

}