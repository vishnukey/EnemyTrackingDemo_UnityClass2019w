using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

	[SerializeField] TextMeshProUGUI healthBar;
	[SerializeField] Player player;
	[SerializeField] Image equipedItemDisplay;
	[SerializeField] Sprite defaultImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.text = "Health: " + player.health;
		if (player.equipedItem == null || player.inventory[player.equipedItem.i, player.equipedItem.j] == null) equipedItemDisplay.sprite = defaultImage;
		else equipedItemDisplay.sprite = player.inventory[player.equipedItem.i, player.equipedItem.j].graphic;
	}
}
