using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSegment : MonoBehaviour
{
    public SpriteRenderer mainSprite;
    public float SizeX => mainSprite.bounds.size.x;
}
