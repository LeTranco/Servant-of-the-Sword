using UnityEngine;

public class KnightCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 30;
    public int heavyAttackDamage = 60;

    public void DamageEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D Orc in hitEnemies )
        {
            Orc.GetComponent<Orc>().TakeDamage(attackDamage);
        }
    }
    
    public void hDamageEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D Orc in hitEnemies )
        {
            Orc.GetComponent<Orc>().TakeDamage(heavyAttackDamage);
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
