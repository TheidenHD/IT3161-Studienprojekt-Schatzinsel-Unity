using UnityEngine;
using UnityEngine.Tilemaps;

public class Team : Map<TileStatus>
{
    private Map<TileStatus> map;
    private Tilemap hiden;
    public bool gefunden = false;

    public Team(int Width, int Height, Map<TileStatus> map, Tilemap hiden) : base(Width, Height)
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
            if (this[Width, Height] == TileStatus.Schatz)
            {
                this.gefunden = true;
            }
        }
        else if (this.testR(Width, Height))
        {
            hiden.SetTile(new Vector3Int(Width, Height, 0), null);
        }
    }

    public bool checkU(int Width, int Height)
    {
        return this.test(Width, Height) && this[Width, Height] == TileStatus.Unbekant;
    }

    public bool checkB(int Width, int Height)
    {
        return this.test(Width, Height) && (this[Width, Height] == TileStatus.Begehbar || this[Width, Height] == TileStatus.Schatz);
    }
}
