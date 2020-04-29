using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public ParticleSystem splash;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            this.OnHitAnimation();
        }
    }

    public void OnHitAnimation() {
        this.splash.Play();
    }
}
