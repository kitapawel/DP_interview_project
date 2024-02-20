using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarkoPolo : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Button solveButton;
    public Image blood;
    private int bloodValue = 0;
    public int sacrificeRequirement = 10;

    private void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component is not assigned!");
            return;
        }
    }

    private void AdvancedSolution()
    {
        textMeshPro.text = "";

        for (int i = 1; i <= 100; i++)
        {
            string output = "";

            if (i % 3 == 0) output += "Marco";
            if (i % 5 == 0) output += "Polo";

            if (output == "") output = i.ToString();

            textMeshPro.text += output + "\n";
        }

    }
    private void BasicSolution()
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
    public void SolveMarkoPolo()
    {
        AdvancedSolution();
    }
}
