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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (canJump)
            GetClick();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
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

    private void Throw()
    {
        rb.AddForce(GetDirection() * throwForce, ForceMode2D.Impulse);
        canJump = false;
    }

    private void DeStick()
    {
        transform.parent.DetachChildren();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Stick(collision.gameObject);
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
        yield return new WaitForSecondsRealtime(3);
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
