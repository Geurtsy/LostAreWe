using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
public class ETileMap : Editor
{
    public TileMap _map;

    TileBrush _brush;
    Vector3 _mouseHitPos;

    private bool MouseOnMap
    {
        get { return _mouseHitPos.x > 0 && _mouseHitPos.x < _map._gridSize.x && _mouseHitPos.y < 0 && _mouseHitPos.y > -_map._gridSize.y;  }
    }

    private void OnEnable()
    {
        _map = (TileMap)target; // Saves reference to current script extending.
        Tools.current = Tool.View; // Sets tool to normal mouse thingy.

        if(_map._tiles == null)
        {
            var go = new GameObject("Tiles");
            go.transform.SetParent(_map.transform);
            go.transform.position = Vector3.zero;

            _map._tiles = go;
        }

        if(_map._tex2D != null)
        {
            UpdateCalculations();
            NewBrush();
        }
    }

    private void OnDisable()
    {
        DestroyBrush();
    }

    private void OnSceneGUI()
    {
        if(_brush != null)
        {
            UpdateHitPosition();
            MoveBrush();

            if(_map._tex2D != null && MouseOnMap)
            {
                Event current = Event.current;
                if(current.alt)
                {
                    Draw();
                }
                else if(current.button == 1)
                {
                    RemoveTile();
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("TileMapEditor");

        #region "VerticalLayout"
        EditorGUILayout.BeginVertical();

        var oldSize = _map._mapSize;

        // Shows mapsize and a Tex2D in the inspector and assings them to the target script.
        _map._mapSize = EditorGUILayout.Vector2Field("Map Size:", _map._mapSize);

        if(_map._mapSize != oldSize)
            UpdateCalculations();

        var oldTex = _map._tex2D;
        _map._tex2D = (Texture2D)EditorGUILayout.ObjectField("Texture2D:", _map._tex2D, typeof(Texture2D), false);

        if(oldTex != _map._tex2D)
        {
            UpdateCalculations();
            _map._tileID = 1;
            CreateBrush();
        }

        // Warning message and other information.
        if(_map._tex2D == null)
        {
            EditorGUILayout.HelpBox("You must select an actual Texture2D dude!", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.LabelField("Tile Size:", _map._tileSize.x + " x " + _map._tileSize.y);
            _map._tilePadding = EditorGUILayout.Vector2Field("Tile Padding", _map._tilePadding);
            EditorGUILayout.LabelField("Grid Size In Units:", _map._gridSize.x + " x " + _map._gridSize.y);
            EditorGUILayout.LabelField("Pixels To Units:", _map._pixelsToUnits.ToString());
            UpdateBrush(_map._currentTileBrush);

            if(GUILayout.Button("Clear Dem Tiles"))
            {
                if(EditorUtility.DisplayDialog("You sure mate?", "You sure you wanted to clear ALL dem tiles on this tile map?", "Ugly! Clear it!", "NO! Computer DO. NOT. CLEAR."))
                {
                    ClearMap();
                }
            }
        }



        EditorGUILayout.EndVertical();
        #endregion
    }

    private void UpdateCalculations()
    {
        // Finds the asset in the AssetData base and saves all relative sprites.
        var path = AssetDatabase.GetAssetPath(_map._tex2D);
        _map._spriteReferences = AssetDatabase.LoadAllAssetsAtPath(path);

        // Sets default sprite to the first sprite.
        var sprite = (Sprite)_map._spriteReferences[1];
        var width = sprite.textureRect.width;
        var height = sprite.textureRect.height;

        // Sets tile size to size of spirte.
        _map._tileSize = new Vector2(width, height);

        // Sets pixelsToUnits to correct value according to value set in the inspector.
        _map._pixelsToUnits = (int)(sprite.rect.width / sprite.bounds.size.x);

        // Sets the grid size relative to world space and map size.
        _map._gridSize = new Vector2((width / _map._pixelsToUnits) * _map._mapSize.x, (height / _map._pixelsToUnits) * _map._mapSize.y);
    }

    private void CreateBrush()
    {
        var sprite = _map._currentTileBrush;
        if(sprite != null)
        {
            GameObject _go = new GameObject("Brush");
            _go.transform.SetParent(_map.transform);

            _brush = _go.AddComponent<TileBrush>();
            _brush._sprRenderer = _go.AddComponent<SpriteRenderer>();
            _brush._sprRenderer.sortingOrder = 1000;

            var pixelsToUnits = _map._pixelsToUnits;
            _brush._brushSize = new Vector2(sprite.textureRect.width / pixelsToUnits,
                sprite.textureRect.height / pixelsToUnits);

            _brush.UpdateBrush(sprite);
        }
    }

    private void NewBrush()
    {
        if(_brush == null)
            CreateBrush();
    }

    private void DestroyBrush()
    {
        if(_brush != null)
            DestroyImmediate(_brush.gameObject);
    }

    public void UpdateBrush(Sprite sprite)
    {
        if(_brush != null)
            _brush.UpdateBrush(sprite);
    }

    private void UpdateHitPosition()
    {
        var plane = new Plane(_map.transform.TransformDirection(Vector3.forward), Vector3.zero);
        var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        var hit = Vector3.zero;
        var dist = 0.0f;

        if(plane.Raycast(ray, out dist))
            hit = ray.origin + ray.direction.normalized * dist;

        _mouseHitPos = _map.transform.InverseTransformPoint(hit);
    }

    private void MoveBrush()
    {
        var tileSize = _map._tileSize.x / _map._pixelsToUnits;

        var x = Mathf.Floor(_mouseHitPos.x / tileSize) * tileSize;
        var y = Mathf.Floor(_mouseHitPos.y / tileSize) * tileSize;

        var row = x / tileSize;
        var column = Mathf.Abs(y / tileSize) - 1;

        if(!MouseOnMap)
            return;

        var id = (int)((column * _map._mapSize.x) + row);

        _brush._tileID = id;

        x += _map.transform.position.x + tileSize / 2;
        y += _map.transform.position.y + tileSize / 2;

        _brush.transform.position = new Vector3(x, y, _map.transform.position.z);


    }

    private void Draw()
    {
        var id = _brush._tileID.ToString();

        var posX = _brush.transform.position.x;
        var posY = _brush.transform.position.y;

        GameObject tile = GameObject.Find(_map.name + "/Tiles/tile_" + id);

        if(tile == null)
        {
            tile = new GameObject("tile_" + id);
            tile.transform.SetParent(_map._tiles.transform);
            tile.transform.position = new Vector3(posX, posY, 0);
            tile.AddComponent<SpriteRenderer>();
        }

        tile.GetComponent<SpriteRenderer>().sprite = _brush._sprRenderer.sprite;
    }

    private void RemoveTile()
    {
        var id = _brush._tileID.ToString();

        GameObject tile = GameObject.Find(_map.name + "/Tiles/tile_" + id);

        if(tile != null)
        {
            DestroyImmediate(tile);
        }

    }

    private void ClearMap()
    {
        for(var index = 0; index < _map._tiles.transform.childCount; index++)
        {
            Transform ctransform = _map._tiles.transform.GetChild(index);
            DestroyImmediate(ctransform.gameObject);
            index--;
        }
    }
}
