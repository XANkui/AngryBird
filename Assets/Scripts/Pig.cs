using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float maxSpeed = 10;
    public float minSpeed = 4;
    private SpriteRenderer spriteRenderer;
    public Sprite spriteHurt;
    public GameObject boom;
    public GameObject score;
    public bool isPig = false;

    //声音
    public AudioClip hurtCollision;
    public AudioClip boomCollision;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > maxSpeed) {

            Dead();
        }
        else if(collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed){
            GameManager.Instance.PlayAudioClip(hurtCollision);
            spriteRenderer.sprite = spriteHurt;
        }
    }

    public void Dead() {
        if (isPig == true) {
            GameManager.Instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        GameObject goBoom = Instantiate(boom, transform.position, Quaternion.identity);
        GameManager.Instance.PlayAudioClip(boomCollision);
        Destroy(goBoom, 1f);
        GameObject goScore = Instantiate(score, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        Destroy(goScore, 1f);
    }
}
