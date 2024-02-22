using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [Range(5f, 10f)]
    [SerializeField]
    private float boardXSize = 10f;
    [Range(5f, 10f)]
    [SerializeField]
    private float boardYSize = 10f;

    public float BoardXSize { get => boardXSize; set => boardXSize = value; }
    public float BoardYSize { get => boardYSize; set => boardYSize = value; }

    void Awake()
    {
        gameObject.transform.localScale = new Vector3(BoardXSize, BoardYSize, 1f);
    }
}
