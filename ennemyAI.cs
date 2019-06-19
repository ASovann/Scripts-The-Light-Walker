using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ennemyAI : MonoBehaviour
{

    //Distance entre le joueur et l'ennemi
    private float Distance;

    private float disapearTime;
    public float despawnTime;

    private float resetTime;
    public float respawnTime;

    public Text lootText;

    //Distance entre la position de base et l'ennemi
    private float DistanceBase;

    private Vector3 basePosition;

    public AudioClip deathSound;
    public AudioClip attackSound;
    private AudioSource source;

    private int nbrAttack;

    //cible ennemy
    public Transform Target;

    // distance de poursuite
    public float chaseRange = 10;

    //portée attaque
    public float attackRange = 2.2f;

    //cooldown attaque
    private float attackRepeatTime;
    public float attackTime;

    public float textTime;
    private float textTimeReset;

    //dégats infligés
    public float TheDamage;

    //agent de navigation
    private UnityEngine.AI.NavMeshAgent agent;

    //animations
    private Animation animations;

    //vie
    private float maxEnemyHealth;
    public float ennemyHealth;
    public bool isDead = false;

    //loot
    public GameObject[] loots;

    public List<GameObject> onSpellList = new List<GameObject>();
    questSystem quest;

    PlayerInventory playerInv;
    public int XpLoot;
    public int MoneyLoot;
    public string Name;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animations = gameObject.GetComponent<Animation>();
        playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        attackRepeatTime = attackTime;
        basePosition = transform.position;
        maxEnemyHealth = ennemyHealth;
        textTimeReset = textTime;
        source = GetComponent<AudioSource>();
        lootText = GameObject.Find("loot_text").GetComponent<Text>();
        quest = GameObject.FindGameObjectWithTag("Player").GetComponent<questSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //recherche du joueur
            Target = GameObject.Find("Player").transform;

            //calcul de la distance joueur ennemi 
            Distance = Vector3.Distance(Target.position, transform.position);

            //calcul de la distance base ennemi 
            DistanceBase = Vector3.Distance(basePosition, transform.position);

            //ennemi éloigné
            if (Distance > chaseRange && DistanceBase <= 1)
            {
                idle();

            }

            //ennemi proche mais pas a portée 
            if (Distance < chaseRange && Distance > attackRange)
            {
                chase();

            }

            //ennemi proche a portée
            if (Distance < attackRange)
            {
                
                attack();

            }

            //joueur échappé
            if (Distance > chaseRange && DistanceBase > 1 )
            {
                BackBase();
            }
        }
        else
        {
            resetTime -= Time.deltaTime;
            disapearTime -= Time.deltaTime;
            textTimeReset -= Time.deltaTime;


        }
        if (disapearTime <= 0)
        {
            disapearTime = despawnTime;
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            
        }
        if (resetTime <= 0)
        {
            resetTime = respawnTime;
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            isDead = false;
            Respawn();
        }
        if (textTimeReset <= 0)
        {
            textTimeReset = textTime;
            lootText.text = null;
        }
        


    }

    //poursuite
    void chase()
    {
        animations.Play("walk");
        
        
        agent.destination = Target.position;
    }

    //attaque
    void attack()
    {
        //ne traverse pas le joueur
        agent.destination = transform.position;
        switch (nbrAttack)
        {
            case 0:
                animations.Play("attack");
                
                source.PlayOneShot(attackSound);
                Target.GetComponent<PlayerInventory>().ApplyDamage(TheDamage);

                Debug.Log("L'ennemi a envoyé " + TheDamage + " points de dégats");
                nbrAttack++;
                break;
            default:
                attackRepeatTime -= Time.deltaTime;
                if (attackRepeatTime <= 0)
                {
                    animations.Play("attack");
                    source.PlayOneShot(attackSound);
                    Target.GetComponent<PlayerInventory>().ApplyDamage(TheDamage);
                    Debug.Log("L'ennemi a envoyé " + TheDamage + " points de dégats");
                    attackRepeatTime = attackTime;
                    nbrAttack++;
                }
                break;
        }
        
    }
    
    //idle
    void idle()
    {
        animations.Play("idle");
        nbrAttack = 0;

    }

    //subit des dégats
    public void ApplyDamage(float TheDamage)
    {
        if (!isDead)
        {
            ennemyHealth = ennemyHealth - TheDamage;

            print(gameObject.name + "à subit " + TheDamage + " points de dégats");

            if (animations.name == "hit")
            {
                animations.Play("hit");
            }
            if (ennemyHealth <= 0 && isDead == false)
            {
                Dead();
            }
        }
    }
    public void BackBase()
    {
        animations.Play("walk");
        nbrAttack = 0;
        agent.destination = basePosition;
    }
    public void Dead()
    {
        agent.destination = transform.position;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        isDead = true;
        animations.Play("death");
        source.PlayOneShot(deathSound);
        lootText.text = "XP +" + XpLoot + " " + "Gold +" + MoneyLoot;
        quest.nameKill = gameObject.name;
        

        //loot
        if (loots.Length > 0)
        { 
            int randomNumber = Random.Range(0, loots.Length);
            GameObject finalloot = loots[randomNumber];
            Instantiate(finalloot, transform.position, transform.rotation);
        }
        playerInv.currentXp += XpLoot;

        playerInv.currentMoney += MoneyLoot;
        //corps disparait au bout de 5 secondes
        //Destroy(transform.gameObject, 5);


    } 
    public void Respawn()
    {
        GameObject.FindGameObjectWithTag("Enemy").transform.position = basePosition;
        ennemyHealth = maxEnemyHealth;
    }

}
