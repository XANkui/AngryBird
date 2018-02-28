using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird {

    public override void ShowSkill()
    {
        base.ShowSkill();
        rdg2D.velocity *= 2;
    }
}
