using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public enum TileStatus
    { 
        Unbekant,
        Wasser,
        Begehbar,
        Objekt
    }

    public class Element
    {
        public TileStatus Status = TileStatus.Unbekant;
        public int Cost = -1;
        public int Belonung = -1;
        public Vector2Int Vorheriger;

        public Element(TileStatus StatusIn) 
        {
            Status = StatusIn;
        }
    }

    private Element[,] Karte;
    private int ovsetWidth;
    private int ovsetHeight;

    public Map(int Width, int Height)
    {
        ovsetWidth = Width / 2;
        ovsetHeight = Height / 2;
        Karte = new Element[Width, Height];
    }

    public Element this[int Width, int Height]
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
}
