﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public GameObject collision_point;

    BoxCollider2D collider2D;

    private GameObject[] collision_points;
    private LineRenderer[] lines;

    Vector2 intersection_top = Vector2.zero;
    Vector2 intersection_right = Vector2.zero;
    Vector2 intersection_bottom = Vector2.zero;
    Vector2 intersection_left = Vector2.zero;

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

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        collision_points = new GameObject[] {
            Instantiate(collision_point),
            Instantiate(collision_point)
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (LineStraight.instance._line != null)
        {
            //GameObject[] lines = LineStraight.instance._line;

            //foreach (GameObject game_object in lines)
            //{
            LineRenderer line = LineStraight.instance._line;//game_object.GetComponent<LineRenderer>();
                bool intersects = line.bounds.Intersects(collider2D.bounds);

                if (intersects)
                {
                    Vector2 line_1_from = line.GetPosition(0);
                    Vector2 line_1_to = line.GetPosition(1);

                    Vector2 square_top_left = transform.TransformPoint(
                        collider2D.offset + Vector2.Reflect(collider2D.size, Vector2.left) * .5f
                    );
                    Vector2 square_top_right = transform.TransformPoint(
                        collider2D.offset + collider2D.size * .5f
                    );
                    Vector2 square_bottom_left = transform.TransformPoint(
                        collider2D.offset - collider2D.size * .5f
                    );
                    Vector2 square_bottom_right = transform.TransformPoint(
                        collider2D.offset + Vector2.Reflect(collider2D.size, Vector2.down) * .5f
                    );

                    List<Vector2> intersections = new List<Vector2>();

                    if (LineIntersection(
                        line_1_from, line_1_to, square_top_left, square_top_right,
                        ref intersection_top
                    ))
                    {
                        intersections.Add(intersection_top);
                    }
                    if (LineIntersection(
                        line_1_from, line_1_to, square_top_right, square_bottom_right,
                        ref intersection_right
                    ))
                    {
                        intersections.Add(intersection_right);
                    }
                    if (LineIntersection(
                        line_1_from, line_1_to, square_bottom_left, square_bottom_right,
                        ref intersection_bottom
                    ))
                    {
                        intersections.Add(intersection_bottom);
                    }
                    if (LineIntersection(
                        line_1_from, line_1_to, square_top_left, square_bottom_left,
                        ref intersection_left
                    ))
                    {
                        intersections.Add(intersection_left);
                    }

                    collision_point.transform.position = intersection_top;
                    Vector2[] intersection_array = intersections.ToArray();
                    for (int i = 0; i < intersection_array.Length; i++)
                    {
                        collision_points[i].transform.position = intersection_array[i];
                    }
                }
        }

    }
}
