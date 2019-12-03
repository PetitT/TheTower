using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Movement playerMovement;
    public float offset;
    public float lerpTime;
    public float upLerpTime;
    public float maxDistance;

    private bool isMoving = false;

    private void Awake()
    {
        playerMovement.stuck += stuckHandler;
    }


    private void Update()
    {
        CheckMaxDistance();

        if (isMoving)
            Follow();
    }

    private void CheckMaxDistance()
    {
        float camYPos = gameObject.transform.position.y;
        float playerYPos = player.transform.position.y;
        if (playerYPos > camYPos + maxDistance)
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, playerYPos, transform.position.z), upLerpTime);
    }

    private void stuckHandler()
    {
        StartCoroutine("Move");
    }

    private IEnumerator Move()
    {
        isMoving = true;
        yield return new WaitForSeconds(lerpTime + 1f);
        isMoving = false;
    }

    private void Follow()
    {
        float camYPos = gameObject.transform.position.y;
        float playerYPos = player.transform.position.y;

        if (camYPos < playerYPos - offset)
            camYPos = playerYPos - offset;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, camYPos, transform.position.z), lerpTime);
    }
}
