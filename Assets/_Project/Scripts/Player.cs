using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float health = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) Time.timeScale = 0;
	}

	public void TakeDamage(float strength){
		health -= strength;
		Debug.Log("Took " + strength + " points of damage");
	}
}
