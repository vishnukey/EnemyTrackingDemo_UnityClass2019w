using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

	[SerializeField] TextMeshProUGUI healthBar;
	[SerializeField] Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.text = "Health: " + player.health;	
	}
}
