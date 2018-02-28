using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseList : MonoBehaviour {

    private Animator anim;
    public GameObject buttonPause;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Pause() {
        anim.SetBool("IsPause", true);
        buttonPause.SetActive(false);

        //避免暂停时弹弓上未发射的鸟，暂停时仍能操作
        if (GameManager.Instance.birds.Count > 0 && GameManager.Instance.birds != null) {
            if (GameManager.Instance.birds[0].isReleased == false) {
                GameManager.Instance.birds[0].canMove = false;
            }
        }
    }

    public void Resume() {
        Time.timeScale = 1;
        anim.SetBool("IsPause", false);

        //取消暂停时弹弓上未发射的鸟，恢复操作
        if (GameManager.Instance.birds.Count > 0 && GameManager.Instance.birds != null)
        {
            if (GameManager.Instance.birds[0].isReleased == false)
            {
                GameManager.Instance.birds[0].canMove = true;
            }
        }
    }

    public void PauseAnimEnd() {
        Time.timeScale = 0;
    }

    public void ResumeAnimEnd() {
        buttonPause.SetActive(true);
    }

    public void Retry() {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void Home() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
