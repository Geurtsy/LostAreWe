using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posion : MonoBehaviour
{
    public void PosionOverTime(float damage, int intervals)
    {
        StartCoroutine(GetComponent<IHealth>().HealthManager.Posion(damage, intervals));
    }
}
