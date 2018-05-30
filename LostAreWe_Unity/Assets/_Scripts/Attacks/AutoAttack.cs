using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IAttack))]
public class AutoAttack : MonoBehaviour {

    [SerializeField] private float _attackCoolDown;
    [SerializeField] private float _attackNoticeRange;
    [SerializeField] private LayerMask _layersToTest;
    private IAttack _attackMech;

    private void Start()
    {
        _attackMech = GetComponent<IAttack>();
        StartCoroutine(AutomateRangeAttack());
    }

    private IEnumerator AutomateRangeAttack()
    {
        while(true)
        {
            if(Physics2D.OverlapCircle(transform.position, _attackNoticeRange, _layersToTest))
            {
                _attackMech.Attack();
                if(_attackCoolDown > 0)
                {
                    yield return new WaitForSeconds(_attackCoolDown);
                }
                else
                {
                    yield return null;
                }
            }

            yield return null;
        }
    }
}
