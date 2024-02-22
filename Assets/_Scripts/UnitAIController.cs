using System.Collections.Generic;
using UnityEngine;

public class UnitAIController : MonoBehaviour
{
    [Header("Unit order settings:")]
    [SerializeField]
    private List<Unit> units = new List<Unit>();
    [SerializeField]
    private BoardController gameBoard;
    [SerializeField]
    private float topLimit = 4f;
    private float bottomLimit = -4f;
    private float leftLimit = -4f;
    private float rightLimit = 4f;
    [SerializeField]
    private float OrderFrequency = 3f;

    [Header("Unit spawning frequency:")]
    [SerializeField]
    private Unit[] unitsToSpawn;
    [SerializeField]
    private int unitMaxCount = 30;
    [SerializeField]
    private Transform[] spawnLocations;
    [Range(2f, 5.99f)]
    [SerializeField]
    private float SpawnMinFrequency = 2f;
    [Range(6f, 10f)]
    [SerializeField]
    private float SpawnMaxFrequency = 6f;

    void Start()
    {
        CalculateBoardLimits();
        InvokeRepeating("SpawnUnits", 0.1f, DetermineRandomSpawnFrequency(SpawnMinFrequency, SpawnMaxFrequency));
        InvokeRepeating("GiveOrders", 0.2f, OrderFrequency);
    }
    private void GiveOrders()
    {
        if (units.Count <= 0) { return; }

        foreach (Unit u in units)
        {            
            u.SetTargetCoordinate(DetermineRandomCoordinate());
        }
    }
    private void SpawnUnits()
    {        
        if (unitsToSpawn.Length <= 0) { return; }
        if (units.Count > unitMaxCount) { return; }   
        int index = Random.Range(0, unitsToSpawn.Length);
        Unit u = Instantiate(unitsToSpawn[index], transform);
        units.Add(u);
        u.OnDeath += RemoveUnit;
        u.transform.parent = null;
        u.SetTargetCoordinate(DetermineRandomCoordinate());
    }
    private float DetermineRandomSpawnFrequency(float min, float max)
    {
        float result = Random.Range(min, max);
        return result;
    }
    private Vector2 DetermineRandomCoordinate()
    {
        float xCoord = Random.Range(leftLimit, rightLimit);
        float yCoord = Random.Range(topLimit, bottomLimit);
        Vector2 result = new Vector2(xCoord, yCoord);
        return result;
    }
    private void RemoveUnit(Unit unit)
    {
        units.Remove(unit);
        unit.OnDeath -= RemoveUnit;
    }
    private void CalculateBoardLimits()
    {
        if (gameBoard == null)
        {
            gameBoard = FindObjectOfType<BoardController>();
            if (gameBoard == null)
            {
                Debug.LogError("No board for the game!");
            }
        }
        topLimit = gameBoard.BoardYSize / 2f - 0.6f;
        bottomLimit = (gameBoard.BoardYSize / 2f - 0.1f) * -1f;
        leftLimit = (gameBoard.BoardXSize / 2f - 0.3f) * -1f;
        rightLimit = gameBoard.BoardXSize / 2f - 0.3f;
    }
}
