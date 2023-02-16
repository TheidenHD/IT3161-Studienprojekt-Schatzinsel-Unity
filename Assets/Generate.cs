using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generate : MonoBehaviour
{
    public GameObject spawnobjekt;
    [SerializeField]
    public static int Height = 10;
    [SerializeField]
    public static int Width = 20;
    public static int pixel = 16;
    public static int zoom = 5;
    [SerializeField]
    public static int Teams = 1;
    [SerializeField]
    public static int Pirates = 1;
    [SerializeField]
    public static bool hide = true;
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private Tilemap Map;
    [SerializeField]
    private Tilemap Objekts;
    [SerializeField]
    private Tilemap Hiden;
    [SerializeField]
    private TileBase[] Tile;
    [SerializeField]
    private SpriteRenderer Renderer;
    private Map<TileStatus> nav;
    // Start is called before the first frame update
    void Start()
    {
        Map.ClearAllTiles();
        Objekts.ClearAllTiles();
        Hiden.ClearAllTiles();

        Renderer.size = new Vector2(Width, Height);
        nav = new Map<TileStatus>(Width - 2, Height - 2);
        this.Camera.orthographicSize = zoom;

        List<(Team, List<Pirate>)> team =  new List<(Team, List<Pirate>)>();

        IEnumerable<int> enI = Enumerable.Range((Width - 2) / -2, Width - 2);
        IEnumerable<int> enJ = Enumerable.Range((Height - 2) / -2, Height - 2);
        Vector2Int PiratTarget = new Vector2Int(Random.Range(enI.First(), enI.Last() + 1), Random.Range(enJ.First(), enJ.Last() + 1));

        for (int i = 0; i < Teams; i++)
        {
            Team t = new Team(Width - 2, Height - 2, nav, Hiden);
            List<Pirate> temp = new List<Pirate>();
            for (int j = 0; j < Pirates; j++)
            {
                Pirate ob = Instantiate(spawnobjekt, new Vector3(PiratTarget.x, PiratTarget.y), Quaternion.identity).GetComponent<Pirate>();
                ob.team = t;
                ob.map = new Map<Element>(Width - 2, Height - 2);
                temp.Add(ob);
            }
            team.Add((t, temp));
        }

        foreach (int i in enI)
        {
            foreach (int j in enJ)
            {
                Map.SetTile(new Vector3Int(i, j), Tile[3]);
                if (Random.Range(0, 100) > 95  && PiratTarget.x != i && PiratTarget.y != j)
                {
                    Objekts.SetTile(new Vector3Int(i, j), Tile[2]);
                    nav[i, j] = TileStatus.Objekt;
                }
                else
                {
                    nav[i, j] = TileStatus.Begehbar;
                }
                foreach ((Team, List<Pirate>) t in team)
                {
                    t.Item1[i, j] = TileStatus.Unbekant;
                    foreach (Pirate p in t.Item2)
                    {
                        p.map[i, j] = new Element(i, j);
                    }
                }
            }
        }
        Vector2Int target = PiratTarget;
        while (target == PiratTarget)
        {
            target = new Vector2Int(Random.Range(enI.First(), enI.Last() + 1), Random.Range(enJ.First(), enJ.Last() + 1));

        }
        Objekts.SetTile(new Vector3Int(target.x, target.y), Tile[8]);
        nav[target.x, target.y] = TileStatus.Schatz;
        if (hide)
        {
            foreach (int i in Enumerable.Range(Width / -2, Width))
            {
                foreach (int j in Enumerable.Range(Height / -2, Height))
                {
                    Hiden.SetTile(new Vector3Int(i, j, 0), Tile[7]);
                }
            }
        }
    }
}
