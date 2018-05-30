using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour, IAttack {

    public abstract float Damage { get; set; }

    public abstract void Attack();
}
