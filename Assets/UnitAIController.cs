using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAIController : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    public float topLimit = 4f;
    public float bottomLimit = -4f;
    public float leftLimit = -4f;
    public float rightLimit = 4f;
    public float AIResolution = 3f;

    void Start()
    {
        InvokeRepeating("GiveOrders", 0.1f, AIResolution);
    }


    private void GiveOrders()
    {
        foreach (Unit u in units)
        {
            float xCoord = Random.Range(leftLimit, rightLimit);
            float yCoord = Random.Range(topLimit, bottomLimit);
            u.SetTargetCoordinate(new Vector2(xCoord, yCoord));
        }
    }

}
