using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDamage : MonoBehaviour
{
    public bool isFire;
    [SerializeField] private int damage;
    [SerializeField] private int fireDamage;
    public int Damage
    {
        get
        {
            if (isFire) return damage + fireDamage;
            else return damage;
        }
    }
}
