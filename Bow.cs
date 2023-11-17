using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bow : Item
{
    public abstract override void Use();
    public GameObject arrowImpactPrefab;
}
