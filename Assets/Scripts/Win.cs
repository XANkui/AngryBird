using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 过关之后的星级统计展示
    /// </summary>
    public void Show() {
        GameManager.Instance.ShowStar();
    }

    
}
