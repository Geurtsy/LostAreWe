using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetection {

    public GameObject GetClosestObjInRange(Vector2 pos, float range, int layers)
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(pos, range, layers);
        float dis = 0.0f;
        GameObject target = null;

        foreach(Collider2D col in cols)
        {
            float currentDis = (new Vector2(col.transform.position.x, col.transform.position.y) - pos).magnitude;

            if(currentDis > dis)
            {
                dis = currentDis;
                target = col.gameObject;
            }
        }

        return target;
    }
}
