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

    public ParticleSystem _blood;

    public List<EnemyCollision> _enemies = new List<EnemyCollision>();

    BattleManager battle_state;

    public List<GameObject> _lines = new List<GameObject>();
    Transform _start, _end;

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
                CreateLine(false, _mouse_pos);
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
                
                //_end.position = _mouse_pos;

                //_line.GetComponent<Electric>().CalculatePoints(_line.GetPosition(0), _line.GetPosition(1), _line);


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

        //Debug.Log(_enemies[0].FindCollisions(_line));

        bool _shoold_destroy = true;

        foreach (EnemyCollision enemy in _enemies)
        {
            if (enemy.FindCollisions(_line) == 2)
            {
                _shoold_destroy = false;
                //Debug.Log(enemy._points_pos.Count);
                /*_blood.transform.position = enemy._points_pos[4].transform.position;
                _blood.Play();*/
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
        _line.startWidth = 1f;
        _line.endWidth = 1f;
        _line.useWorldSpace = true;
        _line.numCapVertices = 50;
        //_line.colorGradient = _gradient;
        _current_lines++;


        if (from_pointer)
        {
            _line.SetPosition(0, ini);
            _line.SetPosition(1, _mouse_pos);
        }
        //_line.gameObject.AddComponent<Electric>();
        _lines.Add(_line.gameObject);
    }

}
