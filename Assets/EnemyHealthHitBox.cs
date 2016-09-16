using UnityEngine;
using System.Collections;

public class EnemyHealthHitBox : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] int multiplier = 1;

    void OnTriggerEnter(Collider c)
    {
        if(c.GetComponent<WeaponHitBox>())
        {
            enemyHealth.TakeDamage(c.GetComponent<WeaponHitBox>().weaponDamage * multiplier);
            
        }
    }
}
