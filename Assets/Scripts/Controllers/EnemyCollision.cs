using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public GameObject collision_point;

    BoxCollider2D collider2D;

    private Stack<GameObject> avalilable_points = new Stack<GameObject>();
    private Stack<GameObject> in_use_points = new Stack<GameObject>();
    private Vector2[] vertices = new Vector2[4];
    public bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 intersection)
    {
        float Ax, Bx, Cx, Ay, By, Cy, d, e, f, num, offset;
        float x1lo, x1hi, y1lo, y1hi;

        Ax = p2.x - p1.x;
        Bx = p3.x - p4.x;

        // X bound box test/
        if (Ax < 0)
        {
            x1lo = p2.x; x1hi = p1.x;
        }
        else
        {
            x1hi = p2.x; x1lo = p1.x;
        }

        if (Bx > 0)
        {
            if (x1hi < p4.x || p3.x < x1lo) return false;
        }
        else
        {
            if (x1hi < p3.x || p4.x < x1lo) return false;
        }

        Ay = p2.y - p1.y;
        By = p3.y - p4.y;

        // Y bound box test//
        if (Ay < 0)
        {
            y1lo = p2.y; y1hi = p1.y;
        }
        else
        {
            y1hi = p2.y; y1lo = p1.y;
        }

        if (By > 0)
        {
            if (y1hi < p4.y || p3.y < y1lo) return false;
        }
        else
        {
            if (y1hi < p3.y || p4.y < y1lo) return false;
        }

        Cx = p1.x - p3.x;
        Cy = p1.y - p3.y;
        d = By * Cx - Bx * Cy;  // alpha numerator//
        f = Ay * Bx - Ax * By;  // both denominator//

        // alpha tests//
        if (f > 0)
        {
            if (d < 0 || d > f) return false;
        }
        else
        {
            if (d > 0 || d < f) return false;
        }

        e = Ax * Cy - Ay * Cx;  // beta numerator//

        // beta tests //
        if (f > 0)
        {
            if (e < 0 || e > f) return false;
        }
        else
        {
            if (e > 0 || e < f) return false;
        }

        // check if they are parallel
        if (f == 0) return false;

        // compute intersection coordinates //
        num = d * Ax; // numerator //
        offset = same_sign(num, f) ? f * 0.5f : -f * 0.5f;   // round direction //
        intersection.x = p1.x + (num + offset) / f;

        num = d * Ay;
        offset = same_sign(num, f) ? f * 0.5f : -f * 0.5f;
        intersection.y = p1.y + (num + offset) / f;

        return true;
    }

    private bool same_sign(float a, float b)
    {
        return ((a * b) >= 0f);
    }

    private Vector2[] FindVertices()
    {
        this.vertices[0] = transform.TransformPoint(
            collider2D.offset + Vector2.Reflect(collider2D.size, Vector2.left) * .5f
        );
        this.vertices[1] = transform.TransformPoint(
            collider2D.offset + collider2D.size * .5f
        );
        this.vertices[2] = transform.TransformPoint(
            collider2D.offset + Vector2.Reflect(collider2D.size, Vector2.down) * .5f
        );
        this.vertices[3] = transform.TransformPoint(
            collider2D.offset - collider2D.size * .5f
        );

        return this.vertices;
    }

    public int FindCollisions(LineRenderer line)
    {
        bool intersects = false;
        Vector2 line_1_from = line.GetPosition(0);
        Vector2 line_1_to = line.GetPosition(1);
        Vector2[] vertices = FindVertices();
        List<Vector2> intersections = new List<Vector2>();
        Vector2 intersection = Vector2.zero;
        int j = 0;

        for (int i = 0; i < 4; i++)
        {
            int next = i < 3 ? i + 1 : 0;
            intersects = LineIntersection(
                line_1_from, line_1_to, vertices[i], vertices[next],
                ref intersection
            );
            if (intersects)
            {
                GetCollisionPoint().transform.position = intersection;
                j++;
            }
        }

        return j;
    }

    private void DismissCollisionPoints()
    {
        while (this.in_use_points.Count > 0)
        {
            this.avalilable_points.Push(this.in_use_points.Pop());
        }
    }

    private GameObject GetCollisionPoint()
    {
        GameObject instance;
        if (this.avalilable_points.Count == 0)
        {
            instance = Instantiate(collision_point);
            this.in_use_points.Push(instance);
            return instance;
        }

        instance = this.avalilable_points.Pop();
        this.in_use_points.Push(instance);
        return instance;
    }

    public int FindCollisions()
    {
        int collision_points = 0;
        GameObject[] lines = GameObject.FindGameObjectsWithTag("line");
        DismissCollisionPoints();

        foreach (GameObject game_object in lines)
        {
            LineRenderer line = game_object.GetComponent<LineRenderer>();
            if (line.bounds.Intersects(collider2D.bounds) && FindCollisions(line) > 1)
            {
                collision_points += 1;
            }
        }

        return collision_points;
    }
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        avalilable_points = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            int collisions = FindCollisions();
            //Debug.Log("take damage from " + collisions + " lines");
        }
     
    }
}
