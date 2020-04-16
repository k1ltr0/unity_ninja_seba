using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LineStraight : MonoBehaviour
{
    public static LineStraight instance;

    public LineRenderer _line;
    private Vector3 _mouse_pos;
    public Material _material;
    private int _current_lines;
    public Transform _pointer;



    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (_line == null)
            {
                CreateLine(false,Vector2.zero);
            }

            _mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mouse_pos.z = 0;
            _line.SetPosition(0, _mouse_pos);
            _line.SetPosition(1, _mouse_pos);


        }
        else if (Input.GetMouseButtonUp(0) && _line)
        {
            if (Vector2.Distance(_line.GetPosition(0), _line.GetPosition(1)) < 100)
            {

                _line.SetPosition(1, _mouse_pos);

            }

            _mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mouse_pos.z = 0;
            _line = null;
            _current_lines++;

        }
        else if (Input.GetMouseButton(0) && _line && Vector2.Distance(_line.GetPosition(0), _line.GetPosition(1)) < 100)
        {

            _mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mouse_pos.z = 0;
            _line.SetPosition(1, _mouse_pos);
            LineCreator.instance.UpdateBar(Vector2.Distance(_line.GetPosition(0), _line.GetPosition(1)));
            _pointer.transform.position = _mouse_pos;

        }

    }


    public void CreateLine(bool from_pointer, Vector2 ini) {

        if (from_pointer)
        {
            _line.SetPosition(1, ini);
            _line = null;

            //_line.SetPosition(1, _mouse_pos);
        }


        _line = new GameObject("Line" + _current_lines).AddComponent<LineRenderer>();
        _line.material = _material;
        _line.positionCount = 2;
        _line.startWidth = 1.15f;
        _line.endWidth = 1.15f;
        _line.useWorldSpace = true;
        _line.numCapVertices = 50;

        if (from_pointer)
        {
            _line.SetPosition(0, ini);
            _line.SetPosition(1, _mouse_pos);
        }
    }

}
