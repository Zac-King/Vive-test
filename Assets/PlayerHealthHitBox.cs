using UnityEngine;
using System.Collections;

public class PlayerHealthHitBox : MonoBehaviour
{
    [SerializeField] PlayerHealth player;
    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<WeaponHitBox>().playerWeapon == false)
        {
            player.TakeDamage(c.GetComponent<WeaponHitBox>().weaponDamage);
        }
    }
}
