using System;
using UnityEngine;

public class Unit : MonoBehaviour, IProvideUnitDetails
{
    [Header("Unit stats:")]
    [SerializeField]
    private string unitName;
    [SerializeField]
    private int maxHP = 3;
    [SerializeField]
    private int currentHP;
    [SerializeField]
    private bool isAlive = true;
    [SerializeField]
    private float speed = 5f;

    [Header("Unit setup:")]
    [SerializeField]
    private Vector2 targetCoordinate;
    private bool isMoving;
    private bool isFacingRight = true;
    //The following bool is set by animation events in Unity editor
    private bool isAttacking = false;
    [SerializeField]
    private GameObject selectionIndicator;
    private SpriteRenderer spriteRenderer;
    private int BLOOD_SORTING_LAYER = 2;
    private int CORPSE_SORTING_LAYER = 3;
    private Color REGULAR_COLOR = new Color(1f, 1f, 1f);
    private Color WOUNDED_COLOR = new Color(1f, 0.86f, 0.8f);
    private Color HEAVY_WOUNDED_COLOR = new Color(1f, 0.55f, 0.55f);
    [SerializeField]
    private SpriteRenderer[] bloodFX;
    public Action OnHit;
    public Action<Unit> OnDeath;

    [Header("Sound setup")]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip swingSFX;
    [SerializeField]
    private AudioClip hitSFX;
    [SerializeField]
    private AudioClip dieSFX;

    [Header("Animation parameters:")]
    private Animator animator;
    private string currentAnimaton;
    private string ANIM_IDLE = "Idle";
    private string ANIM_WALK = "Walk";
    private string ANIM_DIE = "Die";
    private string ANIM_ATT1 = "Attack1";
    private string ANIM_ATT2 = "Attack2";

    void Start()
    {
        selectionIndicator.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHP = maxHP;
        name = NameGenerator.GenerateRandomName();
        unitName = name;
    }

    void FixedUpdate()
    {
        if (!isAlive) { return; }
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
        if (isAttacking) { return; }
        if (HasReachedTargetCoordinate())
        {
            ChangeAnimationState(ANIM_IDLE);
            return;
        }

        Vector2 direction = targetCoordinate - (Vector2)transform.position;

        direction.Normalize();

        transform.Translate(direction * speed * Time.fixedDeltaTime);

        ChangeAnimationState(ANIM_WALK);

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
    void ChangeAnimationState(string newAnimation)
    {
        //if (!isAlive) { return; }
        if (currentAnimaton != newAnimation)
        {
            animator.Play(newAnimation);
            currentAnimaton = newAnimation;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive) { return; }
        int attackRoll = UnityEngine.Random.Range(0, 2);
        if (attackRoll == 0) { ChangeAnimationState(ANIM_ATT1); }
        if (attackRoll == 1) { ChangeAnimationState(ANIM_ATT2); }
        PlaySound(swingSFX);
        Unit u = collision.gameObject.GetComponent<Unit>();
        if (u != null)
        {
            u.GetHit();
        }
    }
    public void GetHit()
    {
        if (currentHP > 0) 
        {
            PlaySound(hitSFX);
            SplashBlood();
            currentHP--;
            SetWoundedColor();
            OnHit?.Invoke();
        }
        if (currentHP <= 0)
        {
            Die();
        }
    }
    private void SetWoundedColor()
    {
        //Percentage value introduced if for some reason other HP system is introduced later
        float currentHPPercentage = (float)currentHP / (float)maxHP;
        Debug.Log(currentHPPercentage);
        if (currentHPPercentage >= 1f)
        {
            spriteRenderer.color = REGULAR_COLOR;
        } else if (currentHPPercentage < 1f && currentHPPercentage >= 0.66f)
        {
            spriteRenderer.color = WOUNDED_COLOR;
        }
        else if (currentHPPercentage < 0.66f && currentHPPercentage >= 0.33f)
        {
            spriteRenderer.color = HEAVY_WOUNDED_COLOR;
        } else
        {            
            spriteRenderer.color = REGULAR_COLOR;
        }
    }
    private void Die()
    {
        if (!isAlive) { return; }
        isAlive = false;
        GetComponent<Rigidbody2D>().simulated = false;
        ChangeAnimationState(ANIM_DIE);
        PlaySound(dieSFX);
        spriteRenderer.sortingOrder = CORPSE_SORTING_LAYER;
        OnDeath?.Invoke(this);
    }
    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null) { return; }
        audioSource.PlayOneShot(clip);
    }
    private void SplashBlood()
    {
        if (bloodFX.Length <= 0) { return; }
        int index = UnityEngine.Random.Range(0, bloodFX.Length);
        SpriteRenderer blood = Instantiate(bloodFX[index], transform);
        blood.sortingOrder = BLOOD_SORTING_LAYER;
        blood.transform.parent = null;
    }
    public string GetName()
    {
        return unitName;
    }
    public string GetHP()
    {
        string hp = "HP: " + currentHP.ToString();
        return hp;
    }

    public void OnMouseDown()
    {
        SpriteSelector s = FindObjectOfType<SpriteSelector>();
        s.SelectUnit(this);
    }

    public void Select(bool value)
    {
        if (value) 
        {
            selectionIndicator.SetActive(true);
        } else
        {
            selectionIndicator.SetActive(false);
        }
    }
    //The following methods are used in the Animator despite being greyed out in VS Studio
    private void IsAttackingTrue()
    {
        isAttacking = true;        
    }
    private void IsAttackingFalse()
    {
        isAttacking = false;
    }
}