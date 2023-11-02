using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScroller : MonoBehaviour
{ 
    public WorldSegment groundLeft, groundRight;
    public WorldSegment[] prefabSegments;

    private void Update()
    {
        float speed = GameManager.Instance.worldSpeed;
        Vector3 d = new Vector3( -speed * Time.deltaTime, 0, 0);
        groundLeft.transform.position += d;
        groundRight.transform.Translate(d);

        if(groundRight.transform.position.x <= 0)
        {
            Destroy(groundLeft.gameObject);

            int randInd = Random.Range(0, prefabSegments.Length);
            WorldSegment newSeg = Instantiate(prefabSegments[randInd], transform);

            d.x = groundRight.SizeX * 0.5f + newSeg.SizeX * 0.5f;
            newSeg.transform.position = groundRight.transform.position + d;

            groundLeft = groundRight;
            groundRight = newSeg;
        }
    }
}
