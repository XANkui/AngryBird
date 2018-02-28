using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

    public int starNum = 0;
    private bool isSelected = false;
    public GameObject lockUI;
    public GameObject starUI;

    public GameObject panel;
    public GameObject map;

    //显示该地图所获得的星星数量
    public Text starText;
    //该地图的关卡
    public int startLevel = 1;
    public int endLevel = 10;


    // Use this for initialization
    void Start() {
        //清除保存的数据，发布前清理一次注释掉即可，
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt("totalStars", 0) >= starNum) {
            isSelected = true;
        }

        if (isSelected == true) {
            starUI.SetActive(true);
            lockUI.SetActive(false);

            //统计该地图关卡的获得的星星总数
            int starsCount = 0;
            for (int i = startLevel; i <= endLevel; i++) {
                starsCount += PlayerPrefs.GetInt("level" + i.ToString(), 0);
            }

            starText.text = starsCount.ToString() + "/30";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSelected()
    {
        if (isSelected == true) {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }

    public void BackPanel() {
        if (isSelected == true)
        {
            panel.SetActive(false);
            map.SetActive(true);
        }
    }
}
