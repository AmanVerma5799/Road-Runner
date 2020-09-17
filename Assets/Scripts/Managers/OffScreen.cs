using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class OffScreen : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(!GeometryUtility.TestPlanesAABB(planes, spriteRenderer.bounds))
        {
            if(transform.position.x - Camera.main.transform.position.x < 0f)
            {
                CheckTile();
            }
        }
    }

    void CheckTile()
    {
        if (this.tag == Tags._road)
        {
            Change(ref LevelGenerator.instance.lastPosOfRoadTile, new Vector3(1.5f, 0f, 0f), ref LevelGenerator.instance.lastOrderOfRoad);
        }
        else if (this.tag == Tags._topNearGrass)
        {
            Change(ref LevelGenerator.instance.lastPosOfTopNearGrass, new Vector3(1.2f, 0f, 0f), ref LevelGenerator.instance.lastOrderOfTopNearGrass);
        }
        else if (this.tag == Tags._topFarGrass)
        {
            Change(ref LevelGenerator.instance.lastPosOfTopFarGrass, new Vector3(4.8f, 0f, 0f), ref LevelGenerator.instance.lastOrderOfTopFarGrass);
        }
        else if (this.tag == Tags._bottomNearGrass)
        {
            Change(ref LevelGenerator.instance.lastPosOfBottomNearGrass, new Vector3(1.2f, 0f, 0f), ref LevelGenerator.instance.lastOrderOfBottomNearGrass);
        }
        else if (this.tag == Tags._bottomFarLand1)
        {
            Change(ref LevelGenerator.instance.lastPosOfBottomFarLandF1, new Vector3(1.6f, 0f, 0f), ref LevelGenerator.instance.lastOrderOfBottomFarLandF1);
        }
        else if (this.tag == Tags._bottomFarLand2)
        {
            Change(ref LevelGenerator.instance.lastPosOfBottomFarLandF2, new Vector3(1.6f, 0f, 0f), ref LevelGenerator.instance.lastOrderOfBottomFarLandF2);
        }
        else if (this.tag == Tags._bottomFarLand3)
        {
            Change(ref LevelGenerator.instance.lastPosOfBottomFarLandF3, new Vector3(1.6f, 0f, 0f), ref LevelGenerator.instance.lastOrderOfBottomFarLandF3);
        }
        else if (this.tag == Tags._bottomFarLand4)
        {
            Change(ref LevelGenerator.instance.lastPosOfBottomFarLandF4, new Vector3(1.6f, 0f, 0f), ref LevelGenerator.instance.lastOrderOfBottomFarLandF4);
        }
        else if (this.tag == Tags._bottomFarLand5)
        {
            Change(ref LevelGenerator.instance.lastPosOfBottomFarLandF5, new Vector3(1.6f, 0f, 0f), ref LevelGenerator.instance.lastOrderOfBottomFarLandF5);
        }
    }

    void Change(ref Vector3 pos, Vector3 offset, ref int orderInLayer)
    {
        transform.position = pos;
        pos += offset;

        spriteRenderer.sortingOrder = orderInLayer;
        orderInLayer++;
    }
}