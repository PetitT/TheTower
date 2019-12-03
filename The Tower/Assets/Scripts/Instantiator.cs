using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public GameObject block;
    public Transform instantiatePosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Char"))
        {
            Instantiation();
        }
    }

    private void Instantiation()
    {
        Debug.Log("hiku");
        Instantiate(block, instantiatePosition.position, instantiatePosition.rotation);
        gameObject.SetActive(false);
    }
}
