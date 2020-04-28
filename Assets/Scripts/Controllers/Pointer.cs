using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem _algo;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bonus")
        {
            LineStraight.instance.CreateLine(true, collision.transform.position);
            collision.gameObject.SetActive(false);
            _algo.Play();
            //Instantiate(_algo, collision.GetContacts(0). // [0].point, Quaternion.identity);
        }
    }
}
