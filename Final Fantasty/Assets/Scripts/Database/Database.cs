using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class Database : MonoBehaviour {


	public TextAsset InventoryAsset;

	private static Database _instance;											// Private copy of instance		
	private Dictionary<string, DatabaseEntry> itemsDict;

	public static Database instance												// Tracks present instance of database. Now we can use it from anywhere!
	{
		get    
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.

			if(_instance == null)
				_instance = GameObject.FindObjectOfType<Database>();
			return _instance;
		}
	}

	// TODO We need to ensure this persists across scenes, otherwise there will be a signifigant overhead on game start
	void Start (){
		ReadItems ();		// We shouldn't need to do anything more. The database is a behind the scenes thing
	}

	/*
	 * Parses an XML file to create a hashtable mapping each unique card tag
	 * to its information associated with the card (also stored in the XML file)
	 */
	private void ReadItems() {
		itemsDict = new Dictionary<string, DatabaseEntry> ();
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (InventoryAsset.text);					 				// Loading the XML document
		XmlNodeList itemList = xmlDoc.GetElementsByTagName("Card"); 			// look for the card tag in the xml file
		DatabaseEntry newEntry;

		foreach(XmlNode itemInfo in itemList) {
			newEntry = new DatabaseEntry (itemInfo);
			tag = newEntry.tag;
			itemsDict.Add(tag, newEntry);	// Associate the tag with a database entry read from the XML's node
		}
	}

	// Returns a clone of a given card's database entry
	public DatabaseEntry searchByTag(string tag) {
		if (itemsDict.ContainsKey(tag)) {
			return itemsDict[tag].clone();
		}
		else {
			throw new ItemNotFound("The card you are trying to dind can not be found");
		}
	}
}