using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimation : MonoBehaviour
{
    GameObject[] lines;

    // Update is called once per frame
    void Update()
    {
    }

    public void Play()
    {
        lines = GameObject.FindGameObjectsWithTag("line");
        Vector2 far_away_position = gameObject.transform.position;
        // get points
        for (int i = 0; i < lines.Length; i++)
        {
            LineRenderer line = lines[i].GetComponent<LineRenderer>();
            for (int j = 0; j < line.positionCount; j++)
            {
                Vector2 position = line.GetPosition(j);
                if (far_away_position.x < position.x)
                {
                    far_away_position = position;
                }
            }
        }

        gameObject.transform.position = far_away_position;
    }
}
