using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [Range(5f, 10f)]
    public float boardXSize = 10f;
    [Range(5f, 10f)]
    public float boardYSize = 10f;

    void Awake()
    {
        gameObject.transform.localScale = new Vector3(boardXSize, boardYSize, 1f);
    }
}
