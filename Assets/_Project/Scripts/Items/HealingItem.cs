using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Healing Item", menuName="Items/New Healing Item")]
public class HealingItem : Item {
	public float recoveryAmount;

	public override bool Use(Player player)
	{
		player.health += recoveryAmount;
		return true;
	}
}
