using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	public int rows, columns;
	public GUISkin skin;
	public Item equippedItem = null;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	private HealthScript health;
	private ItemDatabase database;
	private bool showInventory;
	private bool showTooltip;
	private string tooltip;
	
	private bool draggingItem;
	private Item draggedItem;
	private int prevIndex;
	
	// Use this for initialization
	void Start () {
		for(int i = 0; i < rows * columns; i++) {
			slots.Add(new Item());
			inventory.Add(new Item());
		}
		database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
		health = GameObject.FindGameObjectWithTag("HealthHolder").GetComponent<HealthScript>();
		AddItem(0);
		AddItem(1);
	}
	
	void Update() {
		if(Input.GetButtonDown("Inventory")) {
			showInventory = !showInventory;
			if(showInventory) {
				GameControl.control.pauseForScene();
				print("paused");
			} else {
				GameControl.control.unpauseGame();
				print("unpuased");
			}
		}
	}
	
	void OnGUI() {
		tooltip = "";
		GUI.skin = skin;
	
		if(showInventory) {
			DrawInventory();
			if(showTooltip && !draggingItem) {
				GUI.Box(new Rect(Event.current.mousePosition.x + 15f, Event.current.mousePosition.y, 200, 200), tooltip, skin.GetStyle("Tooltip"));
			}
		}
		if(draggingItem) {
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
		}
	}
	
	void DrawInventory() {
		Event e = Event.current;
		int i = 0;
		float startX = 0.5f * (Screen.width - (columns * 60 + 10));
		float startY = 0.5f * (Screen.height - (rows * 60 + 10));
		GUI.Box(new Rect(startX, startY, columns * 60 + 10, rows * 60 + 10),"", skin.GetStyle("Background")); 
		for(int row = 0; row < rows; row++) {
			for(int col = 0; col < columns; col++, i++) {
				Rect slotRect = new Rect(startX + 10 + col * 60, startY + 10 + row * 60, 50, 50);
				if(i == 0) {
					GUI.Box(slotRect, "", skin.GetStyle("Equipped"));
				} else {
					GUI.Box(slotRect, "", skin.GetStyle("Slot"));
				}
				slots[i] = inventory[i];
				if(slots[i].itemName != null) {
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					if(slotRect.Contains(e.mousePosition)) {
						CreateTooltip(slots[i]);
						showTooltip = true;
						if(e.button == 0 && e.type == EventType.mouseDrag && !draggingItem) {
							draggingItem = true;
							prevIndex = i;
							draggedItem = slots[i];
							inventory[i] = new Item();
							if(i == 0) {
								equippedItem = null;
							}
						}
						if(e.type == EventType.mouseUp && draggingItem) {
							inventory[prevIndex] = inventory[i];
							if(prevIndex == 0) {
								equippedItem = inventory[i];
							}
							inventory[i] = draggedItem;
							if(i == 0) {
								equippedItem = draggedItem;
							}
							draggingItem = false;
							draggedItem = null;
						}
						if(e.button == 1 && e.type == EventType.mouseDown && health.health != health.maxHealth) {
							if(inventory[i].itemID == 1) {
								health.ModifyHealth(1);
								inventory[i] = new Item();
								if(i == 0) {
									equippedItem = null;
								}
							}
						}
					}
				} else if(slotRect.Contains(e.mousePosition) && e.type == EventType.mouseUp && draggingItem) {
					inventory[i] = draggedItem;
					if(i == 0) {
						equippedItem = draggedItem;
					}
					draggingItem = false;
					draggedItem = null;
				}
				if(tooltip == "") {
					showTooltip = false;
				}
			}
		}
	}
	
	void CreateTooltip(Item item) {
		tooltip = item.itemName + "\n\n" + item.itemDesc;
	}
	
	public void AddItem(int id) {
		for(int i = 0; i < inventory.Count; i++) {
			if(inventory[i].itemName == null) {
				for(int j = 0; j < database.items.Count; j++) {
					if(database.items[j].itemID == id) {
						inventory[i] = database.items[j];
						if(i == 0) {
							equippedItem = database.items[j];
						}
						break;
					}
				}
				break;
			}
		}
	}
	
	void RemoveItem(int id) {
		for(int i = 0; i < inventory.Count; i++) {
			if(inventory[i].itemID == id) {
				inventory[i] = new Item();
				if(i == 0) {
					equippedItem = null;
				}
				break;
			}
		}
	}
	
	public bool InventoryContains(int id) {
		for(int i = 0; i < inventory.Count; i++) {
			if(inventory[i].itemID == id) {
				return true;
			}
		}
		return false;
	}
}
