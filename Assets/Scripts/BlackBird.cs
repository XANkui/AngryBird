using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird {

    private List<Pig> blocks = new List<Pig>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy")) {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }


    public override void ShowSkill()
    {
        base.ShowSkill();

        if (blocks.Count > 0 && blocks != null){
            for (int i = 0; i < blocks.Count; i++) {
                blocks[i].Dead();
            }
        }

        Clear();
    }

    private void Clear() {
        rdg2D.velocity = Vector3.zero;
        GameObject go = Instantiate(boom, transform.position, Quaternion.identity);
        GameManager.Instance.PlayAudioClip(boomCollision);
        Destroy(go, 1.0f);
        render.enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        mytrail.TrailEnd();
    }

    public override void Next()
    {
        if (render.enabled == true) {
            base.Next();
            return;
        }
        GameManager.Instance.birds.Remove(this);
        Destroy(gameObject);
        GameManager.Instance.NextBird();
    }
}
