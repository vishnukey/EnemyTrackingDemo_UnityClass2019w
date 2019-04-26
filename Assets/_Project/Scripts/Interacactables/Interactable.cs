using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	Item[,] inventory;

	public List<Item> possibleItems;

	void Start()
	{
		inventory = InventoryUIManager.instance.MakeInventory();
		int ENDX = inventory.GetLength(0) - 1;
		int ENDY = inventory.GetLength(1) - 1;

		inventory[0,ENDY] = possibleItems[0];
		inventory[1,ENDY - 1] = possibleItems[1];
		inventory[2,ENDY - 1] = possibleItems[1];
		inventory[3,ENDY - 1] = possibleItems[1];
		inventory[4,ENDY - 1] = possibleItems[1];
		inventory[5,ENDY - 1] = possibleItems[1];
		
	}


	public virtual void Interact()
	{
		InventoryUIManager.instance.ShowInventory(inventory);
	}
}
