﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public Vector3 worldPos;
    public bool walkable; //avoid obstacles
    public int gridX;
    public int gridY;
    public int movementPenalty; //value certain terrain types more

    public double GCost { get; set; }
    public double HCost { get; set; }
    public double FCost => GCost + HCost;

    public Node parent { get; set; }


    public Node(Vector3 worldPos, bool walkable, int gridX, int gridY, int movementPenalty)
    {                                           
        this.worldPos = worldPos;
        this.walkable = walkable;
        this.gridX = gridX;
        this.gridY = gridY;
        this.movementPenalty = movementPenalty;
    }

    /* Heuristics for grid maps
     * octile distance allows to move in 8 directions
     * 
     * Can be written in various different ways:
     *      if (dx > dy) (D * (dx-dy) + D2 * dy) else (D * (dy-dx) + D2 * dx)
     *      D * max(dx, dy) + (D2-D) * min(dx, dy)
     *      D2 * min(dx, dy) + |dx - dy|
     * where D is 1 and D2 is √2
     * 
     * only called for neighbor nodes, so always 45-45-90 triangle
     *
     * read more at http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html#heuristics-for-grid-maps
     */

    public double DistanceToNode(Node node)
    {
        float dx = Mathf.Abs(gridX - node.gridX);
        float dy = Mathf.Abs(gridY - node.gridY);

        return Mathf.Sqrt(2) * Mathf.Min(dx, dy) + Mathf.Abs(dx - dy);
    }
}
