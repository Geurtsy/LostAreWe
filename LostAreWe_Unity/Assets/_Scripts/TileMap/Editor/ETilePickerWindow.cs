using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ETilePickerWindow : EditorWindow {

    public enum Scale
    {
        X1,
        X2,
        X3,
        x4,
        X5
    }

    private Scale _currentScale;
    private Vector2 _currentSelection = Vector2.zero;

    public Vector2 _scrollPos = Vector2.zero;

    [MenuItem("Window/TilePicker")]
    public static void OpenTilePickerWindow()
    {
        var window = EditorWindow.GetWindow(typeof(ETilePickerWindow));
        var title = new GUIContent();
        title.text = "Tile Picker";
        window.titleContent = title;
    }

    private void OnGUI()
    {
        if(Selection.activeGameObject == null)
            return;

        var selection = Selection.activeGameObject.GetComponent<TileMap>();

        if(selection != null)
        {
            var tex2D = selection._tex2D;

            if(tex2D != null)
            {

                _currentScale = (Scale)EditorGUILayout.EnumPopup("Zoom", _currentScale);
                var newScale = ((int)_currentScale) + 1;
                var newTexSize = new Vector2(tex2D.width, tex2D.height) * newScale;
                var offset = new Vector2(10, 25);

                var viewPort = new Rect(0, 0, position.width - 5, position.height - 5);
                var contentSize = new Rect(0, 0, newTexSize.x + offset.x, newTexSize.y + offset.y);

                _scrollPos = GUI.BeginScrollView(viewPort, _scrollPos, contentSize);
                GUI.DrawTexture(new Rect(offset.x, offset.y, newTexSize.x, newTexSize.y), tex2D);

                var tile = selection._tileSize * newScale;

                tile.x += selection._tilePadding.x * newScale;
                tile.y += selection._tilePadding.y * newScale;

                var grid = new Vector2(newTexSize.x / tile.x, newTexSize.y / tile.y);

                var selectionPos = new Vector2(tile.x * _currentSelection.x + offset.x,
                    tile.y * _currentSelection.y + offset.y);

                var boxTex = new Texture2D(1, 1);
                boxTex.SetPixel(0, 0, new Color(0, 0.5f, 1.0f, 0.4f));
                boxTex.Apply();

                var style = new GUIStyle(GUI.skin.customStyles[0]);
                style.normal.background = boxTex;

                GUI.Box(new Rect(selectionPos.x, selectionPos.y, tile.x, tile.y), "", style);


                var cEvent = Event.current;
                Vector2 mousePos = new Vector2(cEvent.mousePosition.x, cEvent.mousePosition.y);
                if(cEvent.type == EventType.MouseDown && cEvent.button == 0)
                {
                    _currentSelection.x = Mathf.Floor((mousePos.x + _scrollPos.x) / tile.x);
                    _currentSelection.y = Mathf.Floor((mousePos.y + _scrollPos.y) / tile.y);

                    _currentSelection.x = _currentSelection.x > grid.x - 1 ? grid.x - 1 : _currentSelection.x;
                    _currentSelection.y = _currentSelection.y > grid.y - 1 ? grid.y - 1 : _currentSelection.y;

                    selection._tileID = (int)(_currentSelection.x + (_currentSelection.y * grid.x) + 1);

                    Repaint();
                }

                GUI.EndScrollView();
            }
        }
    }
}
