using UnityEngine;

public class OrcCombat : MonoBehaviour {
    public Transform attackPoint;
    public LayerMask knightLayers;
    public float attackRange = 1.2f;
    private OrcBrain brain;
    private bool _hasDealtDamageInThisAttack = false;

    void Start() => brain = GetComponent<OrcBrain>();

    public void DamageKnight() => CheckHit(15, 10.0f);
    public void hDamageKnight() => CheckHit(30, 20.0f);

    public void ResetAttackFlag() => _hasDealtDamageInThisAttack = false;

    void CheckHit(int damage, float reward) {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, knightLayers);

        if (hitEnemies.Length == 0) {
            if (!_hasDealtDamageInThisAttack) {
                brain.ApplyReward(-1.0f); 
                _hasDealtDamageInThisAttack = true;
            }
            return;
        }

        if (!_hasDealtDamageInThisAttack) {
            foreach (Collider2D enemy in hitEnemies) {
                KnightS knight = enemy.GetComponent<KnightS>();
                if (knight != null) {
                    knight.TakeDamage(damage);
                    brain.ApplyReward(reward); 
                }
            }
            _hasDealtDamageInThisAttack = true;
        }
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}