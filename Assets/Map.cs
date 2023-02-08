using UnityEngine;

public enum TileStatus
{
    Unbekant,
    Wasser,
    Begehbar,
    Objekt
}

public class Element
{
    public int Cost = -1;
    public int Belonung = -1;
    public Vector2Int Vorheriger;
}
public class Map<T>
{
    private T[,] Karte;
    private int ovsetWidth;
    private int ovsetHeight;

    public Map(int Width, int Height)
    {
        ovsetWidth = Width / 2;
        ovsetHeight = Height / 2;
        Karte = new T[Width, Height];
    }

    public T this[int Width, int Height]
    {
        get
        {
            return Karte[Width + ovsetWidth, Height + ovsetHeight];
        }

        set
        {
            Karte[Width + ovsetWidth, Height + ovsetHeight] = value;
        }
    }
    public bool test(int Width, int Height)
    {
        return Width + ovsetWidth >= 0 && Width + ovsetWidth < Karte.GetLength(0) && Height + ovsetHeight >= 0 && Height + ovsetHeight < Karte.GetLength(1);
    }

    public bool testR(int Width, int Height)
    {
        return Width + ovsetWidth == -1 || Width + ovsetWidth == Karte.GetLength(0) || Height + ovsetHeight == -1 || Height + ovsetHeight == Karte.GetLength(1);
    }
}
