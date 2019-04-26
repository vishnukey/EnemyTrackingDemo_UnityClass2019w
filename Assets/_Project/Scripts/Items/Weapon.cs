using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Weapon", menuName="Items/New Melee Weapon")]
public class Weapon : Item {
	public float damageBoost;
	public float rangeBoost;

	public override void Attack(Player player)
	{
		RaycastHit hit;
        if (Physics.Raycast(player.transform.position, -player.transform.right, out hit, player.hitReach + rangeBoost)){
            if (hit.collider.CompareTag("Enemy")){
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null){
                    enemy.TakeDamage(player.strength + damageBoost);
                }
            }
        }
	}
}
