using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionProjectile : Projectile {

    public int _intervals;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider != null)
        {
            if(collision.collider.GetComponent<IHealth>() != null)
            {
                collision.collider.gameObject.AddComponent<Posion>().PosionOverTime(_damage, _intervals);

                Destroy(gameObject);
            }
        }
    }
}