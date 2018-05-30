using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : BaseAttack, IAttack {

    [SerializeField] private float _damage;
    [SerializeField] private GameObject _projectile;
    public override float Damage { get; set; }


    private void Start()
    {
        Damage = _damage;
    }

    public override void Attack()
    {
        Instantiate(_projectile, transform.position, transform.rotation).GetComponent<Projectile>()._damage = _damage;
    }
}