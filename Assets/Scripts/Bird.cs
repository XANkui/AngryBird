using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour
{
    public Transform rightTran;
    public Transform leftTran;
    public LineRenderer rightLineRender;
    public LineRenderer leftLineRender;

    private bool isClicked = false;
    public float maxDis = 1.2f;
    [HideInInspector]
    public SpringJoint2D spJo2D;
    protected Rigidbody2D rdg2D;
    public GameObject boom;
    protected MyTrail mytrail;
    //当前小鸟发射后避免再移动小鸟
    [HideInInspector]
    public bool canMove = false;

    public float speedCameraFollow = 3f;

    //声音
    public AudioClip select;
    public AudioClip fly;
    public AudioClip boomCollision;

    //飞行状态
    private bool isFlying = false;
    [HideInInspector]
    public bool isReleased = false;

    protected SpriteRenderer render;

    private void Awake()
    {
        spJo2D = gameObject.GetComponent<SpringJoint2D>();
        rdg2D = gameObject.GetComponent<Rigidbody2D>();
        mytrail = gameObject.GetComponent<MyTrail>();
        render = gameObject.GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //避免点击UI释放技能
        if (EventSystem.current.IsPointerOverGameObject() == true) {
            return;
        }

        if (isClicked == true)
        {
            //把鼠标的屏幕坐标通过摄像机转为世界坐标
            //又因为使用摄像机转换，所以得把摄像机的坐标加回来
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            //小鸟的弹弓拉伸长读限定
            if (Vector3.Distance(transform.position, rightTran.position) > maxDis) {
                Vector3 pos = (transform.position - rightTran.position).normalized;
                transform.position = pos * maxDis + rightTran.position;
            }

            Line();
        }

        //相机跟随小鸟运动
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(
            Camera.main.transform.position, new Vector3(
                Mathf.Clamp(posX, 0, 15),
                Camera.main.transform.position.y,
                Camera.main.transform.position.z),
            speedCameraFollow * Time.deltaTime);

        //小鸟飞行中触发技能
        if (isFlying == true) {
            if (Input.GetMouseButtonDown(0)) {
                ShowSkill();
            }

        }
    }

    private void OnMouseDown()
    {
        if (canMove == true) {
            GameManager.Instance.PlayAudioClip(select);
            //启用划线，显示弹弓皮带
            rightLineRender.enabled = true;
            leftLineRender.enabled = true;

            isClicked = true;
            rdg2D.isKinematic = true;
        }
        
    }

    private void OnMouseUp()
    {
        if (canMove == true)
        {
            //禁用划线，不显示弹弓皮带
            rightLineRender.enabled = false;
            leftLineRender.enabled = false;

            isClicked = false;
            rdg2D.isKinematic = false;
            Invoke("Fly", 0.1f);
            canMove = false;
        }
      
    }

    private void Fly() {
        GameManager.Instance.PlayAudioClip(fly);
        spJo2D.enabled = false;
        Invoke("Next", 3f);
        mytrail.TrailStart();
        isFlying = true;
        isReleased = true;
    }

    /// <summary>
    /// 实现弹弓带子
    /// </summary>
    private void Line() {
        
        //右边的弹弓带子
        rightLineRender.SetPosition(0, rightTran.position);
        rightLineRender.SetPosition(1, transform.position);

        //左边的弹弓带子
        leftLineRender.SetPosition(0, leftTran.position);
        leftLineRender.SetPosition(1, transform.position);
    }

    public virtual void Next() {
        GameManager.Instance.birds.Remove(this);
        Destroy(gameObject);
        GameObject go = Instantiate(boom, transform.position, Quaternion.identity);
        GameManager.Instance.PlayAudioClip(boomCollision);
        Destroy(go,1.0f);
        GameManager.Instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        mytrail.TrailEnd();
        isFlying = false;
    }

    /// <summary>
    /// 技能飞行中只能使用一次
    /// </summary>
    public virtual void ShowSkill() {
        isFlying = false;
    }
}
