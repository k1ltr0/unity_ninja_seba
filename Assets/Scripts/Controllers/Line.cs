using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Line : MonoBehaviour
{
    // Start is called before the first frame update

    public LineRenderer _line;
    public EdgeCollider2D _line_col;
    public int _large;
    

    List<Vector2> points;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLine(Vector2 mousePos) {

        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePos);
            return;
        }

        if (Vector2.Distance(points.Last(), mousePos) >.1f && _large > points.Count)
        {
            SetPoint(mousePos);
        }
    }


    void SetPoint(Vector2 point)
    {

        points.Add(point);

        _line.positionCount = points.Count;
        _line.SetPosition(points.Count-1,point);

        if (points.Count > 1)
        {
            _line_col.points = points.ToArray();
            LineCreator.instance.UpdateBar(points.Count);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bonus")
        {
            Debug.Log("PUEDE");
            collision.gameObject.SetActive(false);
            LineCreator.instance.CreateNewLine(true, collision.GetComponent<SpriteRenderer>().color);

        }

    }


}
