using UnityEngine;

public class OrcCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask knightLayers;
    public float attackRange = 0.4f;
    public int attackDamage = 15;
    public int heavyAttackDamage = 30;
    
    public void DamageKnight()
    {
        Collider2D[] hitKnight = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, knightLayers);
        foreach (Collider2D KnightS in hitKnight )
        {
            KnightS.GetComponent<KnightS>().TakeDamage(attackDamage);
        }
    }
    
    public void hDamageKnight()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, knightLayers);
        foreach (Collider2D KnightS in hitEnemies )
        {
            KnightS.GetComponent<KnightS>().TakeDamage(heavyAttackDamage);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
