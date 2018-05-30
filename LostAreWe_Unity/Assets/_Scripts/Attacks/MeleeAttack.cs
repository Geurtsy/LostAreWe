using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : BaseAttack, IAttack {

    [SerializeField] private float _damage;
    public override float Damage { get; set; }
    [SerializeField] private float _meleeRange;
    [SerializeField] private LayerMask _hitLayers;
    [SerializeField] private LayerMask _targetLayers;

    private void Start()
    {
        Damage = _damage;
    }

    public override void Attack()
    {
        RaycastHit2D _hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.right.x, transform.right.y), _meleeRange, _hitLayers);

        if(_hit)
        {
            if(_hit.collider.GetComponent<IHealth>() != null)
            {
                _hit.collider.GetComponent<IHealth>().HealthManager.Hurt(Damage);
                Debug.Log("Hit");
            }
        }
    }
}