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

        if (_enemy[0].currectHealth > 0)
        {
            EnemyAttack();

        }
        BattleUI.instance.PlayerTurn();



    }


    public void PlayerAttack() {

        // Debo validar un area de ejecucion , ya que siempre que detecta el mouse button down genera una linea nueva 
        //aunque la verdad con la validacion de cuantas veces pasa una linea por el enemigo se arregla el ataque

        _enemy[0].TakeDamage(_player[0].Attack());

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        BattleUI.instance.EnemyTurn();


    }


    public void EnemyAttack()
    {
        _player[0].TakeDamage(_enemy[0].Attack());
        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

}
