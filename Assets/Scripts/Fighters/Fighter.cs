using System;
using System.Collections;
using UnityEngine;

abstract public class Fighter : MonoBehaviour
{
    const float MOVE_SPEED = 7f;
    const float DISTANCE_MARGIN = 1.5f;
    [SerializeField] protected int maxHP;
    [SerializeField] protected int dex;
    [SerializeField] protected int str;
    [SerializeField] protected HealthBar healthBar;

    protected int currentDex;
    [SerializeField] protected int currentStr;
    protected int currentDef;
    protected int currentHP;
    [SerializeField]protected bool isAlive = true;

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
    public MusicManager musicManager;

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
    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected virtual void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }
    protected virtual void OnEnable()
    {
        turnManager = FindObjectOfType<TurnManager>();
        animator = GetComponent<Animator>();
        
        targetingSystem = FindObjectOfType<TargetingSystem>();
        particleSpawner = FindObjectOfType<ParticleSpawner>();
    }
    public void Heal(float heal, bool spawnParticles = true) //Heal by %
    {
        currentHP = Math.Min(maxHP, (int)(currentHP * heal + currentHP));
        if(spawnParticles)
            particleSpawner.SpawnParticles(transform.position, ParticlesType.HEAL);
        healthBar.SetVal(currentHP);
        musicManager.PlaySound(SoundType.SPELL);
    }
    public void Heal(bool spawnParticles = true) //Heal by 35%
    {
        currentHP = Math.Min(maxHP, (int) Math.Round(currentHP * 0.35f + currentHP));
        if(spawnParticles)
        {
            musicManager.PlaySound(SoundType.SPELL);
            particleSpawner.SpawnParticles(transform.position, ParticlesType.HEAL);
        }
        healthBar.SetVal(currentHP);
    }
    public void AddBlock(int block)
    {
        currentDef = Math.Max(0,block + currentDex)+ currentDef; //in case current dex < 0
        musicManager.PlaySound(SoundType.BLOCK);
        animator.SetTrigger("block");
    }
    private void OnMouseDown()
    {
        if (isMarked)
        {
            targetingSystem.ChooseFighter(this);
        }
    }
    protected abstract void EndRunEvent(); //Diffrent behaviour of Enemy and Player. Enemy will end turn, player will unblock GUI
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
        musicManager.PlaySound(SoundType.SPELL);

    }

    public void BuffDex(int dex)
    {
        currentDex += dex;
        particleSpawner.SpawnParticles(transform.position, ParticlesType.BUFF_DEX);
        musicManager.PlaySound(SoundType.SPELL);
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
        MarkAsTarget(false);
        musicManager.PlaySound(SoundType.DIE);
        animator.SetTrigger("die");
        isAlive = false;
    }
    protected virtual void StopAnimator() //Caled from Die animation
    {
        animator.enabled = false;
        turnManager.EndFightCheck(this);
    }
    protected void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    public void MarkAsTarget(bool isMarking) //Making highlight visible and saving isMarked var
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

    public void ReturnFromAttack() //Called from animation
    {
        FlipSprite();
        targetMovePos = initialPos;
        isMoving = true;
    }

    public void EndTurnTrigger() //Called both from animation and code
    {
        turnManager.EndTurn();
    }

}
