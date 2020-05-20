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

    List<ParticleSystem> _blood_particles = new List<ParticleSystem>();

    void Awake()
    {
        instance = this;
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

    public void  Attack()
    {
        _charge_particle.Stop();
        attack = true;
        StartCoroutine(AttackSecuence());
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

        foreach (ParticleSystem item in _blood_particles)
        {
            item.Play();
        }
    }
}
