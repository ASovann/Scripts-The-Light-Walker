using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellCollision : MonoBehaviour
{
    
    private int spellID;
    private float LightningSpellDamage;
    private float FireballSpellDamage;
    private float ElectricSpellDamage;
    private float IceSpellDamage;
    public int spellTime;
    // Start is called before the first frame update
    void Start()
    {
        
        FireballSpellDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>().FireBallDamage;
        LightningSpellDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>().LightningSpellDamage;
        IceSpellDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>().IceSpellDamage;
        
        Destroy(gameObject, spellTime); 
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (gameObject.name == "FireballSpell(Clone)")
            {
                col.gameObject.GetComponent<ennemyAI>().ApplyDamage(FireballSpellDamage);
            }
            if (gameObject.name == "LightningSpell(Clone)")
            {
                col.gameObject.GetComponent<ennemyAI>().ApplyDamage(LightningSpellDamage);
            }
            if (gameObject.name == "IceSpell(Clone)")
            {
                col.gameObject.GetComponent<ennemyAI>().ApplyDamage(IceSpellDamage);
            }
            
        }
        if (gameObject.name == "FireballSpell(Clone)")
        {
            if (col.gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }
        }
        if (gameObject.name == "LightningSpell(Clone)")
        {
            if (col.gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }
        }
        if (gameObject.name == "IceSpell(Clone)")
        {
            if (col.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
        
        
    }
    
}
