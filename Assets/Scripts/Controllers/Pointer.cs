﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem _algo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bonus")
        {
            LineCreator.instance._can_draw = true;
            LineStraight.instance.CreateLine(true, collision.transform.position);
            collision.gameObject.SetActive(false);
            _algo.Play();
        }
    }
}
