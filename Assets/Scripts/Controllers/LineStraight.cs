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
    public GameObject point;
    public ParticleSystem _blood;
    public List<EnemyCollision> _enemies = new List<EnemyCollision>();
    public List<GameObject> _lines = new List<GameObject>();
    public PlayerController player;

    BattleManager battle_state;
    Transform _start, _end;

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

        if (Input.GetMouseButtonDown(0) && !IsOverUI() && LineCreator.instance._can_draw)
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
        else if (Input.GetMouseButtonUp(0) && _line && !IsOverUI() )
        {
            if (Vector2.Distance(_line.GetPosition(0), _mouse_pos ) < 100)
            {
                _line.SetPosition(1, _mouse_pos);
            }
            LineCreator.instance._can_draw = false;
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

    public void ResetLines()
    {
        foreach (GameObject item in _lines)
        {
            Destroy(item);
        }

        _lines.Clear();
    }

    public void ResetLineBar(float used)
    {
        LineCreator.instance.UpdateBar(used);
    }

    public void DestroyLine(GameObject _line)
    {
        if (_lines.Count < 1)
        {
            LineCreator.instance._can_draw = true;

        }
        Destroy(_line);
        _lines.Remove(_line);
        ResetLineBar(0);

        Debug.Log("RESET");
    }

    public void ValidateLines()
    {
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
        _line.startWidth = 1f;
        _line.endWidth = 1f;
        _line.useWorldSpace = true;
        _line.numCapVertices = 50;
        _current_lines++;

        if (from_pointer)
        {
            _line.SetPosition(0, ini);
            _line.SetPosition(1, _mouse_pos);
        }
        _lines.Add(_line.gameObject);
    }

    void OnDrawGizmos()
    {
        GameObject[] ui = GameObject.FindGameObjectsWithTag("ui");
        Gizmos.color = Color.yellow;

        foreach (GameObject button in ui)
        {
            RectTransform transform = button.GetComponent<RectTransform>();
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 half = (transform.TransformVector(transform.sizeDelta) * 0.5f);
            Gizmos.DrawSphere(transform.position - half, 5);
            Gizmos.DrawSphere(transform.position + half, 5);
        }

        GameObject[] bonus = GameObject.FindGameObjectsWithTag("bonus");
        Gizmos.color = Color.red;

        foreach (GameObject button in bonus)
        {
            Transform transform = button.GetComponent<Transform>();
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 half = Vector3.one * 0.05f;
            Gizmos.DrawSphere(transform.position - half, 5);
            Gizmos.DrawSphere(transform.position + half, 5);
        }
    }

    public bool IsOverUI()
    {
        GameObject[] ui = GameObject.FindGameObjectsWithTag("ui");
        foreach (GameObject button in ui)
        {
            Transform transform = button.GetComponent<Transform>();
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 half = Vector3.one * 0.05f;
            Vector3 origin = transform.position - half;
            Vector3 dest = transform.position + half;

            if (
                    Input.mousePosition.x > origin.x &&
                    Input.mousePosition.x < dest.x &&
                    Input.mousePosition.y > origin.y &&
                    Input.mousePosition.y < dest.y
                )
            {
                return true;
            }
        }

        GameObject[] bonus = GameObject.FindGameObjectsWithTag("bonus");
        foreach (GameObject button in bonus)
        {
            Transform transform = button.GetComponent<Transform>();
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 half = Vector3.one * 0.05f;
            Vector3 origin = transform.position - half;
            Vector3 dest = transform.position + half;

            if (
                    Input.mousePosition.x > origin.x &&
                    Input.mousePosition.x < dest.x &&
                    Input.mousePosition.y > origin.y &&
                    Input.mousePosition.y < dest.y
                )
            {
                return true;
            }
        }

        return false;
    }
}
