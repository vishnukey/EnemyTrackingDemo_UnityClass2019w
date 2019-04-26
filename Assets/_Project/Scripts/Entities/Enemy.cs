using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour {

	[Range(0,360)] [SerializeField] public float fieldOfView = 60;
	[SerializeField] public float viewDistance;
	[SerializeField] LayerMask searchMask;
	[SerializeField] float attackDistance = 0;
	[SerializeField] float strength = 0;
	[SerializeField] float speed = 1;
	[SerializeField] float minDistance = 1f;
	[SerializeField] float health = 100;
	[Tooltip("Attack Cooldown Time in seconds")] [SerializeField] float attackCooldown = 1;
	[SerializeField] TextMeshProUGUI healthBar;

	Transform target = null;
	float lastAttackTime = 0;

	bool canSeeTarget(Transform target, LayerMask mask){
		Vector3 dirToTarget = (target.position - transform.position).normalized;
		if (Vector3.Angle (transform.forward, dirToTarget) < fieldOfView / 2) {
			float dstToTarget = Vector3.Distance (transform.position, target.position);
			if (dstToTarget < viewDistance &&
				!Physics.Raycast (transform.position, dirToTarget, dstToTarget, ~searchMask)) {
				return true;
			}
		}

		return false;
	}

	Transform findTarget(){
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewDistance, searchMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			
			if (canSeeTarget(target, searchMask)) return target;
		}

		return null;
	}

	void Attack(Transform target){
		if (target.gameObject.CompareTag("Player")){
			Player player = target.GetComponent<Player>();
			if (player != null){
				player.TakeDamage(strength);
				lastAttackTime = Time.time;
			}
		}
	}

	public void TakeDamage(float strength){
		health -= strength;
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) Destroy(gameObject);
		healthBar.text = health.ToString();

		// Make sure we have a target
		if (target == null){
			Transform tryFindTarget = findTarget();
			if (tryFindTarget == null) {
				// Look Around 
				//TODO: Do this better
				Vector3 rot = new Vector3(Mathf.PerlinNoise(Time.time, 0) * 2 - 1, 0, Mathf.PerlinNoise(0,Time.time) * 2 - 1).normalized;
				transform.rotation = Quaternion.Lerp(
					transform.rotation,
					Quaternion.LookRotation(
						rot,
						Vector3.up
					),
					Time.deltaTime
				);
			}else {
				target = tryFindTarget;
			}
			return;
		}

		// Make sure w#endife can still see the target
		if (!canSeeTarget(target, searchMask)){
			target = null;
			return;
		}

		float distToTarget = Vector3.Distance(transform.position, target.position);

		// Attack if we're close enough
		if (distToTarget < attackDistance && Time.time - lastAttackTime > attackCooldown){
			Attack(target);
		}

		// Move to target
		//TODO: move to NavMeshAgent maybe
		transform.LookAt(target.position);
		if (distToTarget > minDistance)
			transform.position += transform.forward * speed * Time.deltaTime;
	}

	/*void OnGUI()
    {

        Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);
		
		GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), health.ToString());

    }*/
}
