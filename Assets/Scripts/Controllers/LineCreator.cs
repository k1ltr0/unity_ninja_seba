using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LineCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public static LineCreator instance;

    public GameObject linePrefab;//,trailPrefab;
    bool _can_draw;

    public Image _energy_bar;

    GameObject lineGo, trailGo;

    List<Line> _lines;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _can_draw = true;
    }

    void Update()
    {
    }

    public void UpdateBar(float used)
    {
        _energy_bar.fillAmount = (100 - used) / 100;
    }
}
