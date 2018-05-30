using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBrush : MonoBehaviour {

    public Vector2 _brushSize = Vector2.zero;
    public int _tileID = 0;
    public SpriteRenderer _sprRenderer;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _brushSize);
    }

    public void UpdateBrush(Sprite sprite)
    {
        _sprRenderer.sprite = sprite;
    }
}
