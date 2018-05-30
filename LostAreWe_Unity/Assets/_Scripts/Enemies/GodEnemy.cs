using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodEnemy : MonoBehaviour, IHealth {

    [SerializeField] private HealthManager _healthManager;

    public HealthManager HealthManager
    {
        get { return _healthManager; }

        set { _healthManager = value; }
    }

    private void Update()
    {
        if(_healthManager._currentHealthPoints < 0)
        {
            Destroy(gameObject);
        }
    }
}