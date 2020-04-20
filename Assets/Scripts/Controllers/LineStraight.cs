﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class LineStraight : MonoBehaviour
{
    public static LineStraight instance;

    public LineRenderer _line;
    private Vector3 _mouse_pos;
    public Material _material;
    public int _current_lines;
    public Transform _pointer;

    BattleManager battle_state;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        battle_state = BattleManager.instance;
    }

    private void Update()
    {
        if (battle_state.state != BattleState.PLAYERTURN)
        {
            return;
        }

        if (InventoryUI.instance.itemsParent.gameObject.activeSelf)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (_line == null)
            {
                CreateLine(false, Vector2.zero);
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

            //BattleManager.instance.PlayerAttack();

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

    public void CreateLine(bool from_pointer, Vector2 ini)
    {

        if (from_pointer)
        {
            _line.SetPosition(1, ini);
            _line = null;

            //_line.SetPosition(1, _mouse_pos);
        }

        _line = new GameObject("Line" + _current_lines).AddComponent<LineRenderer>();
        _line.tag = "line";
        _line.material = _material;
        _line.positionCount = 2;
        _line.startWidth = 1.15f;
        _line.endWidth = 1.15f;
        _line.useWorldSpace = true;
        _line.numCapVertices = 50;
        _current_lines++;
        if (from_pointer)
        {
            _line.SetPosition(0, ini);
            _line.SetPosition(1, _mouse_pos);
        }
    }

}