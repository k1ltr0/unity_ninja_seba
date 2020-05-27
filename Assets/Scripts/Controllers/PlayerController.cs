using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public PlayerAttackAnimation _player_attack_animation;
    public CharacterStats stats;
    public bool attack = false;
    public ParticleSystem _health_particle,_attack_particle, _back_particle, _charge_particle,_blood_particle;
    public TrailRenderer _trail;

    public CharacterStats _simple_target;

    bool simple;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (simple)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Target"));

                if (hit.collider != null)
                {
                    Debug.Log("ataque simple");

                    NormalAttack(hit.transform);
                    _simple_target = hit.transform.GetComponent<CharacterStats>();
                }
                else {
                    Debug.Log("nope");
                    //CancelAttack();

                }


            }
        }
    }

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }

    public void TakeHealth(int health)
    {
        _health_particle.Play();
        stats.TakeHealth(health);
    }

    public void TakeEnergy(int energy)
    {
        /*_health_particle.Play();
        stats.TakeHealth(health);*/
        Debug.Log("Use Energy");
    }

    public void Charge()
    {
        _charge_particle.Play();
    }

    public int GetDamage()
    {
        return stats.Attack();
    }

    public void  Attack(bool is_simple)
    {
        _charge_particle.Stop();
        attack = true;
        if (!is_simple)
        {
            StartCoroutine(AttackSecuence());
        }
        else {
            Debug.Log("No puede hacer lineas");
            LineCreator.instance._can_draw = false;
            simple = true;
        }
    }

    /*public void CancelSimpleAttack() {

        simple = false;
        LineCreator.instance._can_draw = true;
    }*/

    public void NormalAttack(Transform _target) {

        this.GetComponent<SimpleAttackAnim>().Attack(_target);
        Invoke("PlayBlood",  .5f);
    }

    void CancelAttack() {

        simple = false;
        _trail.gameObject.SetActive(false);
        attack = false;
        LineCreator.instance._can_draw = true;
        _charge_particle.Stop();
    }

    IEnumerator AttackSecuence()
    {
        float _last_delay = 0;
        _trail.gameObject.SetActive(true);
        this.GetComponent<SpriteRenderer>().enabled = false;

        if (LineStraight.instance._lines != null && LineStraight.instance._lines.Count > 0)
        {
            _attack_particle.Play();

            foreach (GameObject item in LineStraight.instance._lines)
            {
                iTween.MoveTo(gameObject, iTween.Hash("x", item.GetComponent<LineRenderer>().GetPosition(0).x, "y", item.GetComponent<LineRenderer>().GetPosition(0).y, "easeType", "linear", "loopType", "none", "delay", _last_delay + .1, "time", .1f));
                iTween.MoveTo(gameObject, iTween.Hash("x", item.GetComponent<LineRenderer>().GetPosition(1).x, "y", item.GetComponent<LineRenderer>().GetPosition(1).y, "easeType", "linear", "loopType", "none", "delay", _last_delay + .2, "time", .1f));
                _last_delay += .1f;
            }

        }

        _back_particle.startDelay = 1 + _last_delay;
        _back_particle.Play();

        iTween.MoveTo(gameObject, iTween.Hash("x", -120, "y", -70, "easeType", "linear", "loopType", "none", "delay", _last_delay + 1f, "time", .0f));
        CameraController.instance.Shake(.3f, 15, _last_delay + .5f);
        Invoke("PlayBlood", _last_delay + 1);

        yield return new WaitForSeconds(_last_delay+0.5f);

        this.GetComponent<SpriteRenderer>().enabled = true;
        _trail.gameObject.SetActive(false);
    }

    void PlayBlood()
    {
        attack = false;
        simple = false;
        foreach (ParticleSystem item in _blood_particles)
        {
            item.Play();
        }
    }
}
