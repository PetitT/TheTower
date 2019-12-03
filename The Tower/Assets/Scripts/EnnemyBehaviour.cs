using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBehaviour : MonoBehaviour
{
    public float speed;
    private bool movingLeft;

    void Update()
    {
        transform.Translate((movingLeft ? Vector2.left : Vector2.right) * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
            movingLeft = !movingLeft;
    }
}
