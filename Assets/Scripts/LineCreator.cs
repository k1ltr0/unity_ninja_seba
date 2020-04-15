using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LineCreator : MonoBehaviour
{
    // Start is called before the first frame update

    public static LineCreator instance;

    public GameObject linePrefab;//,trailPrefab;
    Line activeLine;
    bool _can_draw;

    public Image _energy_bar;

    GameObject lineGo,trailGo;



    List<Line> _lines;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _can_draw = true;
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetMouseButtonDown(0))
        {
            //lineGo = Instantiate(linePrefab);
            CreateNewLine(false , Color.white);

        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }

        if (activeLine != null && _can_draw)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
            //trailGo.transform.position = mousePos;
        }*/

    }

    public void UpdateBar(float used) {

        _energy_bar.fillAmount = (100 - used) / 100;

    }

    public void CreateNewLine(bool from_bonus,Color _color) {

        //activeLine = null;
        if (from_bonus)
        {
            activeLine = null;
        }
        lineGo = Instantiate(linePrefab);
        //trailGo = Instantiate(trailPrefab);

        activeLine = lineGo.GetComponent<Line>();
        activeLine._line.SetColors(_color,_color);

    }



}
