using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoseStuffPos : MonoBehaviour
{
    public GameObject[] loseStuffs;
    public Transform minPos, maxPos;

    private void Start()
    {
        RandomizePositions();
    }

    private void RandomizePositions()
    {
        foreach (var stuff in loseStuffs)
        {
            float randomY = UnityEngine.Random.Range(minPos.position.y, maxPos.position.y);
            stuff.transform.position = new Vector2(stuff.transform.position.x, randomY);
        }
    }
}
