using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IMove))]
public class Projectile : MonoBehaviour {

    private IMove _moveMech;
    public float _damage;

	private void Start ()
    {
        _moveMech = GetComponent<IMove>();
	}

    private void Update()
    {
        _moveMech.CurrentSpeedState = Movement.SpeedState.WALK;
        _moveMech.Move(transform.right);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider != null)
        {
            if(collision.collider.GetComponent<IHealth>() != null)
            {
                collision.collider.GetComponent<IHealth>().HealthManager.Hurt(_damage);
            }

            Destroy(gameObject);
        }
    }
}