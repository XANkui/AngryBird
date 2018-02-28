using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird {

    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = rdg2D.velocity;
        speed.x *= -1;
        rdg2D.velocity = speed;
    }
}
