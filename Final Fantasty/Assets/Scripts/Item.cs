using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public string itemName;
	public int itemID;
	public string itemDescription;
	public Texture2D itemIcon;
	public int itemSweet;
	public int itemSour;
	public int itemSalty;
	public int itemSpicy;
	public bool multiItems;
	public int itemCounter;
	public ItemType itemType;

	private static int autoIndex = 0;

	public enum ItemType {
		ingredients,
		equipments,
		condiments,
		meal,	
	}

	public Item(){
		
	}

	public Item(ItemType){
		itemType = type;
	}

	public Item(Dictionary<string, string> dict){
		itemName = dict["itemName"];
		itemID = autoIndex++;
		itemDescription = dict ["itemDescription"];
		itemIcon = Resources.Load<Texture2D>("Item Icon/" + dict["itemName"]);
		itemSweet = int.Parse (dict ["itemSweet"]);
		itemSour = int.Parse (dict ["itemSour"]);
		itemSalty = int.Parse (dict ["itemSalty"]);
		itemSpicy = int.Parse (dict ["itemSpicy"]);
		itemType = System.Enum.Parse (typeof(Item.ItemType), dict ["itemType"].ToString);
		multiItems = bool.Parse (dict["multiItems"]);
		itemCounter = multiItems ? 1 : 0;
		
	}

	public Item(string name, string desc, int sweet, int sour, int salty, int spicy, itemType iType, bool mItems, int counter){
		itemName = name;
		itemID = autoIndex++;
		itemDescription = desc;
		itemIcon = Resources.Load<Texture2D>("Item Icon/" + name);
		itemSweet = sweet;
		itemSour = sour;
		itemSalty = salty;
		itemSpicy = spicy;
		itemType = iType;
		multiItems = mItems;
		itemCounter = counter;
	}

	public Item(string name, string desc, int sweet, int sour, int salty, int spicy, itemType iType, bool mItems, int counter){
		itemName = name;
		itemID = autoIndex++;
		itemDescription = desc;
		itemIcon = Resources.Load<Texture2D>("Item Icon/" + name);
		itemSweet = sweet;
		itemSour = sour;
		itemSalty = salty;
		itemSpicy = spicy;
		itemType = iType;
		multiItems = mItems;
		itemCounter = counter;
	}
}
