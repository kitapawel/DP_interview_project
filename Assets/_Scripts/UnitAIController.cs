using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAIController : MonoBehaviour
{
    [Header("Unit order settings:")]
    private List<Unit> units = new List<Unit>();
    private BoardController gameBoard;
    private float topLimit = 4f;
    private float bottomLimit = -4f;
    private float leftLimit = -4f;
    private float rightLimit = 4f;
    [SerializeField]
    [Range(1f, 5f)]
    private float OrderFrequency = 3f;

    [Header("Unit spawning frequency:")]
    [SerializeField]
    [Range(3, 5)]
    private int initialUnitsToSpawn = 4;
    [SerializeField]
    private Unit[] unitsToSpawn;
    [SerializeField]
    [Range(6,60)]
    private int unitMaxCount = 30;
    [SerializeField]
    private Transform[] spawnLocations;
    [Range(0.1f, 3f)]
    [SerializeField]
    private float SpawnMinFrequency = 2f;
    [Range(3.01f, 6f)]
    [SerializeField]
    private float SpawnMaxFrequency = 6f;

    void Start()
    {
        CalculateBoardLimits();
        SpawnInitialUnits();
        StartCoroutine(SpawnUnitsRepeatedly());
        StartCoroutine(GiveOrders());
    }
    private IEnumerator SpawnUnitsRepeatedly()
    {
        while (true)
        {
            yield return new WaitForSeconds(DetermineRandomSpawnFrequency(SpawnMinFrequency, SpawnMaxFrequency));
            SpawnUnitsRandomly();
        }
    }
    private IEnumerator GiveOrders()
    {
        while (true)
        {
            yield return new WaitForSeconds(OrderFrequency);
            foreach (Unit u in units)
            {
                u.SetTargetCoordinate(DetermineRandomCoordinate());
            }
        }
    }
    private void SpawnInitialUnits()
    {
        int i = initialUnitsToSpawn;
        
        foreach (Transform spawn in spawnLocations)
        {
            if (i > 0)
            {
                if (unitsToSpawn.Length <= 0) { return; }
                if (units.Count > unitMaxCount) { return; }
                int unitIndex = Random.Range(0, unitsToSpawn.Length);
                Unit u = Instantiate(unitsToSpawn[unitIndex], new Vector3(spawn.position.x, spawn.position.y, 1f), Quaternion.identity);
                units.Add(u);
                u.OnDeath += RemoveUnit;
                u.transform.parent = null;
                i--;
            }
        }
    }
    private void SpawnUnitsRandomly()
    {        
        if (unitsToSpawn.Length <= 0) { return; }
        if (units.Count >= unitMaxCount) { return; }   
        int unitIndex = Random.Range(0, unitsToSpawn.Length);
        int spawnLocIndex = Random.Range(0, spawnLocations.Length);
        Unit u = Instantiate(unitsToSpawn[unitIndex], new Vector3(spawnLocations[spawnLocIndex].transform.position.x, spawnLocations[spawnLocIndex].transform.position.y, 1f), Quaternion.identity);
        units.Add(u);
        u.OnDeath += RemoveUnit;
        u.transform.parent = null;
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
        if (unit != null)
        {
            unit.OnDeath -= RemoveUnit;
        }
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
