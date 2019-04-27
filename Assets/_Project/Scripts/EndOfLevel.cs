using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour {

	public string sceneToLoad;
	

	void OnTriggerEnter(Collider other)
	{
		SceneManager.LoadScene(sceneToLoad);
	}
}
