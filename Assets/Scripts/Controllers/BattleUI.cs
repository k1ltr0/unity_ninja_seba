using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleUI : MonoBehaviour
{
    // Start is called before the first frame update
    public static BattleUI instance;
    public GameObject playerHud;
    public Text battle_state;


    private void Awake()
    {
        instance = this;
    }

    public void PlayerTurn()
    {
        playerHud.SetActive(true);
        battle_state.text = "Player turn";
    }

    public void EnemyTurn()
    {
        playerHud.SetActive(false);
        battle_state.text = "Enemy turn";
    }
}
