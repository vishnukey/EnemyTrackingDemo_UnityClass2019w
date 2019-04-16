﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor {

	void OnSceneGUI() {
		Enemy fow = (Enemy)target;
		Handles.color = Color.white;
		Handles.DrawWireArc (fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewDistance);
		Vector3 viewAngleA = fow.DirFromAngle (-fow.fieldOfView / 2, false);
		Vector3 viewAngleB = fow.DirFromAngle (fow.fieldOfView / 2, false);

		Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleA * fow.viewDistance);
		Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleB * fow.viewDistance);

		/*Handles.color = Color.red;
		foreach (Transform visibleTarget in fow.visibleTargets) {
			Handles.DrawLine (fow.transform.position, visibleTarget.position);
		}*/
	}
}
