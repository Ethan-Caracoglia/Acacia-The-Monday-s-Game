using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionSorter : IComparer<Collider2D>
{
    /// <summary>
    /// Checks if x has a lower z value than y.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(Collider2D x, Collider2D y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return 1;
        if (y == null) return -1;

        return x.gameObject.transform.position.z.CompareTo(y.gameObject.transform.position.z);
    }
}
