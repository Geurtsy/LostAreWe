using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public Vector2 _mapSize = new Vector2(20, 10);
    public Texture2D _tex2D;
    public Vector2 _tileSize = new Vector2();
    public Vector2 _tilePadding = new Vector2();
    public Vector2 _gridSize = new Vector2();
    public Object[] _spriteReferences;
    public int _pixelsToUnits = 100;
    public int _tileID = 0;
    public GameObject _tiles;

    public Sprite _currentTileBrush
    {
        get { return (Sprite)(_spriteReferences[_tileID]); }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmosSelected()
    {


        if(_tex2D != null)
        {
            Gizmos.color = Color.green;
            var row = 0;
            var pos = transform.position;
            var maxColumns = _mapSize.x;
            var total = _mapSize.x * _mapSize.y;
            var tile = new Vector3(_tileSize.x / _pixelsToUnits, _tileSize.y / _pixelsToUnits);
            var offset = new Vector2(tile.x / 2, tile.y / 2);

            for(var i = 0; i < total; i++)
            {
                var column = i % maxColumns;

                var newX = (column * tile.x) + offset.x + pos.x;
                var newY = -(row * tile.y) - offset.y + pos.y;

                Gizmos.DrawWireCube(new Vector2(newX, newY), tile);

                row += column == maxColumns - 1 ? 1 : 0;
            }

            // Draws the grid outskirts.
            Gizmos.color = Color.white;
            var centerX = pos.x + (_gridSize.x / 2);
            var centerY = pos.y - (_gridSize.y / 2);

            Gizmos.DrawWireCube(new Vector2(centerX, centerY), _gridSize);
        }
    }
}
