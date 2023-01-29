using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Map;

public class Generate : MonoBehaviour
{
    [SerializeField]
    public static int Height = 10;
    [SerializeField]
    public static int Width = 20;
    public static int pixel = 16;
    [SerializeField]
    private Tilemap Map;
    [SerializeField]
    private Tilemap Objekts;
    [SerializeField]
    private TileBase[] Tile;
    [SerializeField]
    private SpriteRenderer Renderer;
    private Map nav;
    // Start is called before the first frame update
    void Start()
    {
        Map.ClearAllTiles();
        Objekts.ClearAllTiles();
        Renderer.size = new Vector2(Width, Height);
        nav = new Map(Width - 2, Height - 2);
        foreach (int i in Enumerable.Range(-9, 18))
        {
            foreach (int j in Enumerable.Range(-4, 8))
            {
                Map.SetTile(new Vector3Int(i, j, 0), Tile[3]);
                if (Random.Range(0, 100) > 95)
                {
                    Objekts.SetTile(new Vector3Int(i, j, 0), Tile[2]);
                    nav[i, j] = new Element(TileStatus.Objekt);
                }
                else
                {
                    nav[i, j] = new Element(TileStatus.Begehbar);
                }
            }
        }
    }

}
