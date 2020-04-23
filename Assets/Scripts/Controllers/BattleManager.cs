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

    IEnumerator SetupBattle() {
        yield return new WaitForSeconds(1f);
        BattleUI.instance.PlayerTurn();
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn() {
        state = BattleState.PLAYERTURN;
        yield return new WaitForSeconds(1f);
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

        yield return new WaitForSeconds(0f);

        BattleUI.instance.PlayerTurn();
    }


    public void PlayerAttack() {

        // Debo validar un area de ejecucion , ya que siempre que detecta el mouse button down genera una linea nueva
        //aunque la verdad con la validacion de cuantas veces pasa una linea por el enemigo se arregla el ataque


        for (int i = 0; i < _enemy.Count; i++)
        {
            if (_enemy[i])

                if (_enemy[i].currectHealth > 0)
                {
                    _enemy[i].TakeDamage(_player[0].Attack());
                }
        }


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
        StartCoroutine(PlayerTurn());
    }

}
