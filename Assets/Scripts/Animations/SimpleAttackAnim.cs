using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttackAnim : MonoBehaviour
{

    public void Attack() {


        if (this.transform.localPosition.x > 0)
        {
            iTween.MoveBy(gameObject, iTween.Hash("x", this.transform.position.x * -1, "easeType", "easeInCirc", "loopType", "none", "delay", 0, "time", .5f));
            iTween.MoveBy(gameObject, iTween.Hash("x", this.transform.position.x * 1, "easeType", "linear", "loopType", "none", "delay", .6f, "time", .5f));
        }
        else {

            iTween.MoveBy(gameObject, iTween.Hash("x", this.transform.position.x * 1, "easeType", "easeInCirc", "loopType", "none", "delay", 0, "time", .5f));
            iTween.MoveBy(gameObject, iTween.Hash("x", this.transform.position.x * -1, "easeType", "linear", "loopType", "none", "delay", .6f, "time", .5f));
        }

        
    }
}
