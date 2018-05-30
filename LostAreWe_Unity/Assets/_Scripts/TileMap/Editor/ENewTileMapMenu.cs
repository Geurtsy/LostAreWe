using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NewTileMapMenu : EditorWindow {

    [MenuItem("GameObject/Tile Map")]
    public static void CreateTileMap()
    {
        GameObject _go = new GameObject("Tile Map");
        _go.AddComponent<TileMap>();
    }
}
