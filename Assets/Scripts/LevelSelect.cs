using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

    private bool isSelected = false;
    public Sprite levelBG;
    private Image image;
    //关卡的星星
    public GameObject[] stars;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
        //第一个关卡自动解锁
        if (transform.parent.GetChild(0).name == gameObject.name) {
            isSelected = true;            
        }

        //获取前一关卡的星星数是否大于1，大于则当前关卡解锁
        int beforeLevelNum = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("level"+ beforeLevelNum.ToString()) > 0) {
            isSelected = true;
        }

        if (isSelected == true) {
            image.overrideSprite = levelBG;
            transform.Find("Num").gameObject.SetActive(true);

            int starCount = PlayerPrefs.GetInt("level" + gameObject.name);//获取显示关卡星星数

            if (starCount > 0)
            {
                for (int i = 0; i < starCount; i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Selected()
    {
        if (isSelected == true) {
            //保存选择场景
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
            SceneManager.LoadScene(2);
        }       
    }
}
