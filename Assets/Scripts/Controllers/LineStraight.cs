using System.Linq;
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
    public Gradient _gradient;

    public List<EnemyCollision> _enemies = new List<EnemyCollision>();

    BattleManager battle_state;

    List<GameObject> _lines = new List<GameObject>();

    public PlayerController player;

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
            player.Charge();

        }
        else if (Input.GetMouseButtonUp(0) && _line)
        {
            if (Vector2.Distance(_line.GetPosition(0), _mouse_pos ) < 100)
            {

                _line.SetPosition(1, _mouse_pos);

            }

            _mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mouse_pos.z = 0;
            ValidateLines();
            _line = null;

        }
        else if (Input.GetMouseButton(0) && _line)
        {

            _mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mouse_pos.z = 0;

            if (Vector2.Distance(_line.GetPosition(0), _mouse_pos) < 100)
            {
                _line.SetPosition(1, _mouse_pos);
                LineCreator.instance.UpdateBar(Vector2.Distance(_line.GetPosition(0), _mouse_pos));
                _pointer.transform.position = _mouse_pos;
            }  

        }



    }

    public void ResetLines() {


        foreach (GameObject item in _lines)
        {
            Destroy(item);
        }

        _lines.Clear();
    }

    public void ResetLineBar(float used) {

        LineCreator.instance.UpdateBar(used);

    }

    public void DestroyLine(GameObject _line)
    {
        Destroy(_line);
        _lines.Remove(_line);
        ResetLineBar(0);
    }

    public void ValidateLines() {

        Debug.Log(_enemies[0].FindCollisions(_line));

        bool _shoold_destroy = true;

        foreach (EnemyCollision enemy in _enemies)
        {
            if (enemy.FindCollisions(_line) == 2)
            {
                _shoold_destroy = false;
            }
  
        }

        if (_shoold_destroy)
        {
            DestroyLine(_line.gameObject);

        }

    }

    public void CreateLine(bool from_pointer, Vector2 ini)
    {

        if (from_pointer)
        {
            _line.SetPosition(1, ini);
            _line = null;

        }

        _line = new GameObject("Line" + _current_lines).AddComponent<LineRenderer>();
        _line.gameObject.AddComponent<Line>();
        _line.sortingOrder = 1;
        _line.tag = "line";
        _line.material = _material;
        _line.positionCount = 2;
        _line.startWidth = 1.15f;
        _line.endWidth = 1.15f;
        _line.useWorldSpace = true;
        _line.numCapVertices = 50;
        _line.colorGradient = _gradient;
        _current_lines++;

        if (from_pointer)
        {
            _line.SetPosition(0, ini);
            _line.SetPosition(1, _mouse_pos);
        }
     
        _lines.Add(_line.gameObject);
    }

}
