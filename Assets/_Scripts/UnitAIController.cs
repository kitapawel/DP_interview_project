using System.Collections.Generic;
using UnityEngine;

public class UnitAIController : MonoBehaviour
{
    [Header("Unit order settings:")]
    public List<Unit> units = new List<Unit>();
    public float topLimit = 4f;
    public float bottomLimit = -4f;
    public float leftLimit = -4f;
    public float rightLimit = 4f;
    public float OrderFrequency = 3f;

    [Header("Unit spawning frequency:")]
    public Unit[] unitsToSpawn;
    public int unitMaxCount = 30;
    public Transform[] spawnLocations;
    [Range(2f, 5.99f)]
    public float SpawnMinFrequency = 2f;
    [Range(6f, 10f)]
    public float SpawnMaxFrequency = 6f;

    void Start()
    {
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
}
