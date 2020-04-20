using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    // Start is called before the first frame update
    public static BattleUI instance;
    public GameObject playerHud;


    private void Awake()
    {
        instance = this;
    }

    public void PlayerTurn() {
        playerHud.SetActive(true);
    }

    public void EnemyTurn()
    {
        /*foreach (GameObject item in playerHud.GetComponentsInChildren<GameObject>())
        {
            item.SetActive(false);
        }*/

        playerHud.SetActive(false);


    }
}
