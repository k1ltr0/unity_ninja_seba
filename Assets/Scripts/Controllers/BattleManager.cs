using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START,PLAYERTURN,ENEMYTURN,WON,LOST }

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public BattleState state;

    public List<CharacterStats> _enemy = new List<CharacterStats>();
    public List<PlayerController> _player = new List<PlayerController>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerAttack();
        }
    }

    IEnumerator SetupBattle() {
        yield return new WaitForSeconds(1f);
        BattleUI.instance.PlayerTurn();
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn() {
        state = BattleState.PLAYERTURN;
        yield return new WaitForSeconds(.5f);
        BattleUI.instance.PlayerTurn();
        LineCreator.instance._can_draw = true;
        LineCreator.instance.UpdateBar(0);
    }

    IEnumerator EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(2f);

        if (_enemy != null && _enemy.Count > 0)
        {
            for (int i = 0; i < _enemy.Count; i++)
            {
                if (_enemy[i])
                    if (_enemy[i].currectHealth > 0)
                    {
                        EnemyAttack(_enemy[i]);
                        _enemy[i].GetComponent<SimpleAttackAnim>().Attack();
                    }
                yield return new WaitForSeconds(1f);
            }
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(PlayerTurn());
    }

    public void PlayerAttack()
    {
        StartCoroutine(SendDamage());
    }

    IEnumerator SendDamage()
    {
        _player[0].Attack();

        yield return new WaitWhile(() => _player[0].attack);

        for (int i = 0; i < _enemy.Count; i++)
        {
            if (_enemy[i] && _enemy[i].currectHealth > 0)
                _enemy[i].TakeDamage(_player[0].GetDamage());
        }

        LineCreator.instance._can_draw = false;
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        BattleUI.instance.EnemyTurn();
        LineStraight.instance.ResetLines();
    }

    public void EnemyAttack(CharacterStats enemy)
    {
        if (enemy)
        {
            if (enemy.currectHealth > 0)
            {
                _player[0].TakeDamage(enemy.Attack());
            }
        }

        state = BattleState.PLAYERTURN;
        CameraController.instance.Shake(.3f, 5, .65f);
    }

}
