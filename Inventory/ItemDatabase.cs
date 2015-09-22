using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();
	
	void Start() {
		items.Add(new Item("Sword", 0, "A sharp sword", Item.ItemType.Weapon));
		items.Add(new Item("Apple", 1, "A tasty treat\n\nHeals 1 health\n\nRight-Click to use", Item.ItemType.Consumable));
		items.Add(new Item("Key", 2, "A secret key", Item.ItemType.Quest));
		items.Add(new Item("Melter", 3, "A device to melt ice", Item.ItemType.Destroyer));
	}
}
