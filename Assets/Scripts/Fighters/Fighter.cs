using System;
using System.Collections;
using UnityEngine;

abstract public class Fighter : MonoBehaviour
{
    const float MOVE_SPEED = 7f;
    const float DISTANCE_MARGIN = 1.5f;
    [SerializeField] private int maxHP;
    [SerializeField] private int dex;
    [SerializeField] private int str;
    [SerializeField] HealthBar healthBar;

    protected int currentDex;
    protected int currentStr;
    protected int currentDef;
    protected int currentHP;
    protected bool isAlive = true;

    protected bool isMoving;
    protected bool isMarked;
    protected Vector3 initialPos;
    protected Vector3 targetMovePos;

    [HideInInspector] public Animator animator;
    protected TurnManager turnManager;
    protected SpriteRenderer spriteRenderer;
    [HideInInspector] public TargetingSystem targetingSystem;
    [HideInInspector] public ParticleSpawner particleSpawner;

    [SerializeField] protected GameObject highlight;

    #region Getters And Setters
    public int CurrentHP { get => currentHP; set => currentHP = value; }

    public bool IsAlive { get => isAlive; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int Dex { get => dex; set => dex = value; }
    public int Str { get => str; set => str = value; }
    public int CurrentDex { get => currentDex; set => currentDex = value; }
    public int CurrentStr { get => currentStr; set => currentStr = value; }
    public int CurrentDef { get => currentDef; set => currentDef = value; }
    public Vector3 InitialPos { get => initialPos; set => initialPos = value; }
    #endregion

    protected virtual void Start()
    {
        turnManager = FindObjectOfType<TurnManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetingSystem = FindObjectOfType<TargetingSystem>();
        particleSpawner = FindObjectOfType<ParticleSpawner>();

        currentHP = maxHP;
        healthBar.SetMaxVal(maxHP);

    }
    public void Heal(int heal)
    {
        currentHP = Math.Max(maxHP, currentHP += heal);
        particleSpawner.SpawnParticles(transform.position, ParticlesType.HEAL);
        healthBar.SetVal(currentHP);
    }
    public void AddBlock(int block)
    {
        currentDef += block + dex;
        animator.SetTrigger("block");
    }
    private void OnMouseDown()
    {
        if (isMarked)
        {
            if (targetingSystem.chosenCard.isRunning)
                targetingSystem.chosenCard.owner.SetRunTarget(transform.position);
            targetingSystem.ChooseFighter(this);
        }
    }
    protected abstract void EndRunEvent();
    protected void Update()
    {
        Run();
    }

    private void Run()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetMovePos, MOVE_SPEED * Time.deltaTime);

            if (targetMovePos == initialPos)
            {
                if (Vector3.Distance(transform.position, targetMovePos) < float.Epsilon)
                {
                    isMoving = false;
                    animator.SetBool("isRunning", false);
                    EndRunEvent(); //Enemy will end turn, player will unblock GUI
                    FlipSprite();
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, targetMovePos) <= DISTANCE_MARGIN)
                {
                    isMoving = false;
                    animator.SetTrigger("attack");
                }
            }
        }
    }

    public void BuffStr(int str)
    {
        currentStr += str;
        particleSpawner.SpawnParticles(transform.position, ParticlesType.BUFF_STR);
    }

    public void BuffDex(int dex)
    {
        CurrentDex += dex;
        particleSpawner.SpawnParticles(transform.position, ParticlesType.BUFF_DEX);

    }

    abstract public void MakeTurn();
    public void TakeDmg(int dmg)
    {
        currentDef -= dmg;
        if (currentDef < 0)
        {
            currentHP += currentDef;
            currentDef = 0;
            particleSpawner.SpawnParticles(transform.position, ParticlesType.BLOOD);
            healthBar.SetVal(currentHP);
        }

        if (currentHP <= 0)
            Die();
    }
    protected void Die()
    {
        animator.SetTrigger("die");
        MarkAsTarget(false);
        isAlive = false;
        turnManager.EndFightCheck();
    }
    protected void StopAnimator()
    {
        animator.enabled = false;
    }
    protected void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    public void MarkAsTarget(bool isMarking)
    {
        if (isAlive)
            highlight.GetComponent<SpriteRenderer>().enabled = isMarked = isMarking;
    }

    public virtual void SetRunTarget(Vector3 target)
    {
        targetMovePos = target;
        isMoving = true;
        animator.SetBool("isRunning", true);
    }

    public void ReturnFromAttack()
    {
        FlipSprite();
        targetMovePos = initialPos;
        isMoving = true;
    }
    public void SpawnParticles()
    {
        particleSpawner.SpawnParticles(targetMovePos, ParticlesType.BLOOD);
    }

    public void EndTurnTrigger()
    {
        turnManager.EndTurn();
    }

}
