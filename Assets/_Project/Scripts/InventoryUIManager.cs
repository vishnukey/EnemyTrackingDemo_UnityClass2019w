using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour {

	[SerializeField] Player player;
	[SerializeField] Transform inventoryPanel;
	[SerializeField] GameObject inventorySlotPrefab;
	[SerializeField] List<MonoBehaviour> toDisable;

	Image[,] inventorySlots;
	bool shown = false;

	// Use this for initialization
	void Start () {
		inventorySlots = new Image[player.inventory.GetLength(0), player.inventory.GetLength(1)];
		int HEIGHT = Screen.height;
		int WIDTH = Screen.width;
		int xstep = WIDTH / player.inventory.GetLength(0);
		int ystep = HEIGHT / player.inventory.GetLength(1);
		for (int i = 0; i < player.inventory.GetLength(0); i++){
			for (int j = 0; j < player.inventory.GetLength(1); j++){
				GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
				RectTransform tform = slot.GetComponent<RectTransform>();
				int posX = i * xstep - WIDTH / 2 + 100;
				int posY = j * ystep - HEIGHT / 2 + 100;
				tform.localPosition = new Vector3(posX, posY,0);
				Image gfx = slot.transform.GetChild(0).GetComponent<Image>();
				inventorySlots[i, j] = gfx;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < player.inventory.GetLength(0); i++){
			for (int j = 0; j < player.inventory.GetLength(1); j++){
				if (player.inventory[i, j] != null){
					inventorySlots[i, j] = player.inventory[i, j].graphic;
				} else{
					inventorySlots[i, j] = null;
				}
			}
		}
	}

	public void ShowInventory(){
		foreach (MonoBehaviour component in toDisable) component.enabled = false;
		inventoryPanel.gameObject.SetActive(true);
		shown = true;
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	public void HideInventory(){
		foreach (MonoBehaviour component in toDisable) component.enabled = true;
		inventoryPanel.gameObject.SetActive(false);
		shown = false;
		Cursor.lockState = CursorLockMode.Locked;
    	Cursor.visible = false;
	}
}
