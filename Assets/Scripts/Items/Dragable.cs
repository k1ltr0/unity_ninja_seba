using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isDragging);
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(
                Input.mousePosition
            ) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    private bool isDragging;

    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;
    }

}
