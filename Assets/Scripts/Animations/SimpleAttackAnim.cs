using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttackAnim : MonoBehaviour
{


    //Transform _target;
    Vector3 _ini;

    private void Start()
    {
        _ini = this.transform.localPosition;
    }

    public void Attack(Transform _target) {


        if (this.transform.localPosition.x > 0)
        {
            iTween.MoveTo(gameObject, iTween.Hash("x", _target.position.x , "easeType", "easeInCirc", "loopType", "none", "delay", 0, "time", .5f));
            iTween.MoveTo(gameObject, iTween.Hash("x", _ini.x * 1, "easeType", "linear", "loopType", "none", "delay", .6f, "time", .5f));
        }
        else {

            iTween.MoveTo(gameObject, iTween.Hash("x", _target.position.x, "easeType", "easeInCirc", "loopType", "none", "delay", 0, "time", .5f));
            iTween.MoveTo(gameObject, iTween.Hash("x", _ini.x, "easeType", "linear", "loopType", "none", "delay", .6f, "time", .5f));
        }

        
    }
}
