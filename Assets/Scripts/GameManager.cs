using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public List<Bird> birds;
    public List<Pig> pigs;
    private Vector3 originPos;

    public GameObject loseUI;
    public GameObject winUI;
    public GameObject[] starts;

    public int starNum = 0;

    //关卡数量
    private int totalNum = 10;

    private void Awake()
    {
        Instance = this;
        if (birds.Count > 0) {
            originPos = birds[0].transform.position;
        }
    }

    /// <summary>
    /// 初始化小鸟上弓
    /// </summary>
    public void Initiate()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].spJo2D.enabled = true;
                birds[i].canMove = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].spJo2D.enabled = false;
                birds[i].canMove = false;
            }
        }
    }

    // Use this for initialization
    void Start () {
        Initiate();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 判断是否打完猪
    /// </summary>
    public void NextBird()
    {
        if (GameManager.Instance.pigs.Count > 0)
        {
            if (GameManager.Instance.birds.Count > 0)
            {
                //有猪有鸟，下一只鸟
                Initiate();
            }
            else
            {
                //没鸟了，有猪输了
                loseUI.SetActive(true);
            }
        }
        else
        {
            //打完猪，赢了
            winUI.SetActive(true);
        }
    }

    public void ShowStar() {

        StartCoroutine("StarShow");
    }

    IEnumerator StarShow() {

        for (; starNum < birds.Count + 1; starNum++) {
            //保证鸟多于三个时，i不越界
            if (starNum > starts.Length) {
                break;
            }
            starts[starNum].SetActive(true);
            //保证星星一颗一颗出现
            yield return new WaitForSeconds(0.2f);
        }

        print(starNum);
    }

    /// <summary>
    /// 重新开始
    /// </summary>
    public void Retry()
    {
        SceneManager.LoadScene(2);
        SavaData();
    }

    /// <summary>
    /// 返回主菜单
    /// </summary>
    public void Home()
    {
        SceneManager.LoadScene(1);
        SavaData();
    }

    /// <summary>
    /// 播放声音
    /// </summary>
    /// <param name="clip"></param>
    public void PlayAudioClip(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, Vector3.zero);
    }

    public void SavaData() {

        if (starNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"))) {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starNum);
        }

        //获取所有关卡星星总数，并保存
        int sumStar = 0;
        for (int i = 0; i < totalNum; i++) {
            sumStar += PlayerPrefs.GetInt("level"+ i.ToString());
        }
        PlayerPrefs.SetInt("totalStars", sumStar);
    }
}
