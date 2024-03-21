using System;
using System.Collections.Generic;
using UnityEngine;

public class IDamageble : MonoBehaviour
{
    [SerializeField] float myHP = 100;
  
    public Action<GameObject> DestroyMe; 
    public Action<KeyValuePair<Transform, int>> ITakeDamage;

    int takenDamage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            takenDamage = collision.gameObject.GetComponent<IDamage>().Damage;
            Destroy(collision.gameObject);

            myHP -= takenDamage; ITakeDamage?.Invoke(new KeyValuePair<Transform, int>(this.gameObject.transform, takenDamage));
            if (myHP <= 0) DestroyMe?.Invoke(this.gameObject);
        }
    }
}
