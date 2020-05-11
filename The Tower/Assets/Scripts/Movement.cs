using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 target;
    private Camera cam;
    private bool canJump;
    public event Action stuck;
    public float throwForce;
    public GameObject loseText;
    public GameObject arrow;
    private Vector2 leftConstraints = new Vector2(91, 269);
    private Vector2 rightConstraints = new Vector2(0, 89);
    private Vector2 secondRightConstraints = new Vector2(271, 359);
    private Vector2 currentConstraint;
    private Vector2 nullConstraint = new Vector2(0, 0);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        currentConstraint = nullConstraint;
    }

    private void Update()
    {
        if (canJump)
        {
            if (CheckForConstraints())
            {
                GetClick();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    private void GetClick()
    {
        if (Input.GetMouseButton(0))
        {
            DeStick();
            Throw();
        }
    }
    private Vector2 GetDirection()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        target = mousePos - gameObject.transform.position;
        target.Normalize();
        return target;
    }

    private bool CheckForConstraints()
    {
        float arrowZ = arrow.transform.rotation.eulerAngles.z;
        if (currentConstraint == leftConstraints)
        {
            if (arrowZ > currentConstraint.x && arrowZ < currentConstraint.y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if((arrowZ > currentConstraint.x && arrowZ < currentConstraint.y) || (arrowZ > secondRightConstraints.x && arrowZ < secondRightConstraints.y))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    private void Throw()
    {
        rb.AddForce(GetDirection() * throwForce, ForceMode2D.Impulse);
        canJump = false;
    }

    private void DeStick()
    {
        currentConstraint = nullConstraint;
        transform.parent.DetachChildren();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Stick(collision.gameObject);
        }
        if (collision.CompareTag("WallLeft"))
        {
            Stick(collision.gameObject);
            currentConstraint = leftConstraints;
        }
        if (collision.CompareTag("WallRight"))
        {
            Stick(collision.gameObject);
            currentConstraint = rightConstraints;
        }
        if (collision.CompareTag("LoseStuff"))
        {
            StartCoroutine("Lose");
        }
    }

    private IEnumerator Lose()
    {
        loseText.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        arrow.SetActive(false);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    private void Stick(GameObject wall)
    {
        gameObject.transform.parent = wall.transform;
        rb.bodyType = RigidbodyType2D.Static;
        canJump = true;
        stuck.Invoke();
    }
}
