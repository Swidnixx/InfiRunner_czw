using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScroller : MonoBehaviour
{ 
    public WorldSegment groundLeft, groundRight;

    private void Update()
    {
        float speed = GameManager.Instance.worldSpeed;
        Vector3 d = new Vector3( -speed * Time.deltaTime, 0, 0);
        groundLeft.transform.position += d;
        groundRight.transform.Translate(d);

        if(groundRight.transform.position.x <= 0)
        {
            d.x = groundRight.SizeX + groundLeft.SizeX;
            groundLeft.transform.Translate(d);

            var tmp = groundLeft;
            groundLeft = groundRight;
            groundRight = tmp;
        }
    }
}
