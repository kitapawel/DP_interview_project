using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Unit : MonoBehaviour
{
    [Header("Unit stats:")]
    public string unitName;
    public int maxHP = 3;
    public int currentHP;
    public bool isAlive = true;
    public float speed = 5f;

    [Header("Unit setup:")]
    public Vector2 targetCoordinate;
    public bool isMoving;
    public bool isFacingRight = true;


    void Start()
    {        
        currentHP = maxHP;        
        name = NameGenerator.GenerateRandomName();
        unitName = name;
    }

    void Update()
    {
        if (targetCoordinate != null)
        {
            MoveTowardsTarget();
        }
    }    
    public void SetTargetCoordinate(Vector2 target)
    {
        targetCoordinate = target;
    }

    
    private void MoveTowardsTarget()
    {
        if (HasReachedTargetCoordinate()) { return; }

        Vector2 direction = targetCoordinate - (Vector2)transform.position;

        direction.Normalize();

        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x > 0)
        {
            if (!isFacingRight) 
            { 
                FlipSprite(); 
            }
        }
        if (direction.x < 0) 
        {  
            if (isFacingRight)
            {
                FlipSprite();
            }
        }

    }
    private bool HasReachedTargetCoordinate()
    {
        if (Vector2.Distance(transform.position, targetCoordinate) < 0.1f)
        {
            return true;
        } 
        return false;
    }
    private void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
