using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : ScriptableObject {
    public Sprite graphic;
    public string itemName;
    public string itemDescription;

    public virtual bool Use(Player player) 
    {
        return false;
    }

    public virtual void Attack(Player player){
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, -player.transform.right, out hit, player.hitReach)){
            if (hit.collider.CompareTag("Enemy")){
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null){
                    enemy.TakeDamage(player.strength);
                }
            }
        }
    }
}