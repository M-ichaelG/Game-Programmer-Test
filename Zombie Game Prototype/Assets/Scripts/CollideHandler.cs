using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideHandler : MonoBehaviour
{
    [SerializeField]
    private EnemyNavMesh myObject;
    [SerializeField]
    private int damageOnCollide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            myObject.SetDamage(damageOnCollide);
            
        }
    }
}
