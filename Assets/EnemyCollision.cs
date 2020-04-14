using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public LineRenderer line;
    public GameObject collision_point;

    BoxCollider2D collider2D;

    private GameObject top_left_point;

    private Vector2 FindIntersection(Vector2 s1, Vector2 e1, Vector2 s2, Vector2 e2) {
        float a1 = e1.y - s1.y;
        float b1 = s1.x - e1.x;
        float c1 = a1 * s1.x + b1 * s1.x;

        float a2 = e2.y - s2.y;
        float b2 = s2.x - e2.x;
        float c2 = a2 * s2.x + b2 * s2.y;

        float delta = a1 * b2 - a2 * b1;
        //If lines are parallel, the result will be (NaN, NaN).
        return delta == 0 ? new Vector2(float.NaN, float.NaN)
            : new Vector2((b2 * c1 - b1 * c2) / delta, (a1 * c2 - a2 * c1) / delta);
    }

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        top_left_point = Instantiate(collision_point);
    }

    // Update is called once per frame
    void Update()
    {
        bool intersects = line.bounds.Intersects(collider2D.bounds);

        if (intersects)
        {
            Vector2 line_1_from = line.GetPosition(0);
            Vector2 line_1_to = line.GetPosition(1);

            Vector2 square_top_left = transform.TransformPoint(new Vector2(
                collider2D.offset.x - collider2D.size.x * .5f,
                collider2D.offset.y + collider2D.size.y * .5f
            ));
            Vector2 square_top_right = transform.TransformPoint(new Vector2(
                collider2D.offset.x + collider2D.size.x * .5f,
                collider2D.offset.y + collider2D.size.y * .5f
            ));
            Vector2 square_bottom_left = transform.TransformPoint(new Vector2(
                collider2D.offset.x - collider2D.size.x * .5f,
                collider2D.offset.y - collider2D.size.y * .5f
            ));
            Vector2 square_bottom_right = transform.TransformPoint(new Vector2(
                collider2D.offset.x + collider2D.size.x * .5f,
                collider2D.offset.y - collider2D.size.y * .5f
            ));

            Vector2 intersection = FindIntersection(
                line_1_from, line_1_to,
                square_top_right, square_bottom_right
            );

            collision_point.transform.position = intersection;

            Vector2 intersection2 = FindIntersection(
                line_1_from, line_1_to,
                square_top_left, square_bottom_left
            );

            top_left_point.transform.position = intersection2;
        }
    }
}
