using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {   
        Debug.Log("triggred");
        Destroy(other.gameObject);
    }
}
