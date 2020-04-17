using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START,PLAYERTURN,ENEMYTURN,WON,LOST }

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public BattleState state;

    public List<CharacterStats> _enemy = new List<CharacterStats>();
    public List<CharacterStats> _player = new List<CharacterStats>();




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
        EnemyAttack();


    }


    public void PlayerAttack() {

        _enemy[0].TakeDamage(_player[0].Attack());
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }


    public void EnemyAttack()
    {
        _player[0].TakeDamage(_enemy[0].Attack());
        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

}
