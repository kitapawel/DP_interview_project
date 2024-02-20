using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkoPolo : MonoBehaviour
{
    private void Start()
    {
        SolveMarkoPolo();
    }
    public void SolveMarkoPolo()
    {
        for (int i = 1; i <= 100; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
            {
                Debug.Log("MarkoPolo");
            }
            else if (i % 3 == 0)
            {
                Debug.Log("Marko");
            }
            else if (i % 5 == 0)
            {
                Debug.Log("Polo");
            }
            else
            {
                Debug.Log(i);
            }
        }
    }
}
