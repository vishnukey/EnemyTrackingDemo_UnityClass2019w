using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float health = 100;
	public float strength = 10;
	public float hitReach = 1;

	public Item[,] inventory;
	public Index2D equipedItem = null;

	public List<Item> forTestingPurposesONLY;
	Camera camera;

	// Use this for initialization
	void Start () {
		inventory = InventoryUIManager.instance.MakeInventory();
		// inventory[0, 0] = forTestingPurposesONLY[0];
		// inventory[1, 1] = forTestingPurposesONLY[1];
		// inventory[2, 1] = forTestingPurposesONLY[1];
		// inventory[3, 1] = forTestingPurposesONLY[1];
		// inventory[3, 0] = forTestingPurposesONLY[1];
		camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) Time.timeScale = 0;

		if (Input.GetMouseButtonDown(0)){
			if (equipedItem == null){
				RaycastHit hit;
				if (Physics.Raycast(transform.position, camera.transform.forward, out hit, hitReach)){
					if (hit.collider.CompareTag("Enemy")){
						Enemy enemy = hit.collider.GetComponent<Enemy>();
						if (enemy != null){
							enemy.TakeDamage(strength);
						}
					}
				}
			}else{
				inventory[equipedItem.i, equipedItem.j].Attack(this);
			}
		}

		if (Input.GetMouseButtonDown(1)){
			if (equipedItem != null)
			{
				Use(equipedItem.i, equipedItem.j);
			}
		}

		if (Input.GetKeyDown("i")){
			if (InventoryUIManager.instance.shown)
				InventoryUIManager.instance.HideInventory();
			else
				InventoryUIManager.instance.ShowInventory(inventory, true);
		}

		if (Input.GetKeyDown("e")){
			RaycastHit hit;
			if (Physics.Raycast(transform.position, camera.transform.forward, out hit, hitReach)){
				if (hit.collider.CompareTag("Interactable")){
					Interactable thing = hit.collider.GetComponent<Interactable>();
					if (thing != null) thing.Interact();
				}
			}
		}
	}

	public void TakeDamage(float strength){
		health -= strength;
	}

	public void Use(int i, int j)
	{
		if (inventory[i, j] != null){
			bool consumed = inventory[i, j].Use(this);
			if (consumed) inventory[i, j] = null;
		}
	}

	public void Equip(int i, int j)
	{
		equipedItem = new Index2D{i=i, j=j};
	}

	public void Take(Item item)
	{
		for (int i = 0; i < inventory.GetLength(0); i++){
			for (int j = 0; j < inventory.GetLength(1); j++){
				if (inventory[i, j] != null) continue;
				inventory[i, j] = item;
				goto done;
			}
		}

		done:
			return;
	}

	public void Drop(int i, int j)
	{
		inventory[i, j] = null;
	}

	public class Index2D
	{
		public int i, j;
	}

	
}
