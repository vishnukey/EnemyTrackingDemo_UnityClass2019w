using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float health = 100;
	[SerializeField] float strength = 10;
	[SerializeField] float hitReach = 1;

	[SerializeField] int inventoryWidth;
	[SerializeField] int inventoryHeight;

	[SerializeField] InventoryUIManager inventoryUI;

	public Item[,] inventory;

	// Use this for initialization
	void Awake () {
		inventory = new Item[inventoryWidth, inventoryHeight];
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) Time.timeScale = 0;

		if (Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			if (Physics.Raycast(transform.position, -transform.right, out hit, hitReach)){
				if (hit.collider.CompareTag("Enemy")){
					Attack(hit.collider.transform);
				}
			}
		}

		if (Input.GetKeyDown("i")){
			inventoryUI.ShowInventory();
		}
		if (Input.GetKeyDown("o")){
			inventoryUI.HideInventory();
		}
	}

	public void TakeDamage(float strength){
		health -= strength;
	}

	void Attack(Transform target){
		if (target.gameObject.CompareTag("Enemy")){
			Enemy enemy = target.GetComponent<Enemy>();
			if (enemy != null){
				enemy.TakeDamage(strength);
			}
		}
	}
}
