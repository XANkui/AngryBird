using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Load", 2);
	}

    private void Load() {
        SceneManager.LoadSceneAsync(1);
    }
}
