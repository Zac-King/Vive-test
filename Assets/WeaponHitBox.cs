using UnityEngine;
using System.Collections;

public class WeaponHitBox : MonoBehaviour
{
    public int weaponDamage = 5;
    public float critChance = 10f;
    public bool playerWeapon = false;

    float velocity = 0;
    Vector3 lastPos = Vector3.zero;
    Vector3 weaponVelocity = Vector3.zero;
    Transform handpos;

    Collider weaponCollider;

    void Awake()
    {
        weaponCollider = GetComponent<Collider>();

        if(playerWeapon)
            handpos = transform.parent;
    }

    void Update()
    {
        if (playerWeapon)
        {
            weaponVelocity = (handpos.localPosition - lastPos) / Time.deltaTime;
            lastPos = handpos.localPosition;
            if (weaponVelocity.magnitude > 3)
            {
                weaponCollider.enabled = true;
            }

            else
            {
                weaponCollider.enabled = false;
            }
        }
    }

}
