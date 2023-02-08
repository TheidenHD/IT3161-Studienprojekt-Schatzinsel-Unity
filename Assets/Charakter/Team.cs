using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Map;

public class Team : Map
{
    private Map map;
    private Tilemap hiden;

    public Team(int Width, int Height, Map map, Tilemap hiden) : base(Width, Height)
    {
        this.map = map;
        this.hiden = hiden;
    }

    public void check(int Width, int Height)
    {
        if (this.test(Width, Height) && this[Width, Height] == TileStatus.Unbekant)
        {
            this[Width, Height] = map[Width, Height];
            hiden.SetTile(new Vector3Int(Width, Height, 0), null);
        }
    }
}
