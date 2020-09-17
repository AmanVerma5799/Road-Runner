using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;

    public GameObject road, grass, ground1, ground2, ground3, ground4, grassBottom, land1, land2, land3, land4, land5, bigGrass, bigGrassBottom, tree1, tree2, tree3, bigTree;
    public GameObject roadHolder, topNearSidewalkHolder, topFarSidewalkHolder, bottomNearSidewalkHolder, bottomFarSidewalkHolder;

    public int startRoadTile, startGrassTile, startGround3Tile, startLandTile; // Initial number of road, grass, ground3, land tiles

    public List<GameObject> roadTiles, topNearGrass, topFarGrass, bottomNearGrass, bottomFarLandF1, bottomFarLandF2, bottomFarLandF3, bottomFarLandF4, bottomFarLandF5;

    public int[] posForTopGround1; // Positions for ground1 on top from 0 to startGround3Tile
    public int[] posForTopGround2; // Positions for ground2 on top from 0 to startGround3Tile
    public int[] posForTopGround4; // Positions for ground4 on top from 0 to startGround3Tile

    public int[] posForTopBigGrass; // Positions for big grass with tree on top near grass from 0 to startGrassTile

    public int[] posForTopTree1; // Positions for tree1 on top near grass from 0 to startGrassTile
    public int[] posForTopTree2; // Positions for tree2 on top near grass from 0 to startGrassTile
    public int[] posForTopTree3; // Positions for tree3 on top near grass from 0 to startGrassTile

    public int[] posForBottomTree1; //Position for tree1 on bottom near grass from 0 to startGrassTile;
    public int[] posForBottomTree2; //Position for tree2 on bottom near grass from 0 to startGrassTile;
    public int[] posForBottomTree3; //Position for tree3 on bottom near grass from 0 to startGrassTile;

    public int[] posForBottomBigGrass; // Position for big grass with tree on bottomNearGrass from 0 to startGrassTile

    public int posForRoadTile1; // Position for roadTile1 on road from 0 to startRoadTile;
    public int posForRoadTile2; // Position for roadTile2 on road from 0 to startRoadTile;
    public int posForRoadTile3; // Position for roadTile3 on road from 0 to startRoadTile;

    [HideInInspector]
    public Vector3 lastPosOfRoadTile, lastPosOfTopNearGrass, lastPosOfTopFarGrass, lastPosOfBottomNearGrass, lastPosOfBottomFarLandF1, lastPosOfBottomFarLandF2, lastPosOfBottomFarLandF3, lastPosOfBottomFarLandF4, lastPosOfBottomFarLandF5;

    [HideInInspector]
    public int lastOrderOfRoad, lastOrderOfTopNearGrass, lastOrderOfTopFarGrass, lastOrderOfBottomNearGrass, lastOrderOfBottomFarLandF1, lastOrderOfBottomFarLandF2, lastOrderOfBottomFarLandF3, lastOrderOfBottomFarLandF4, lastOrderOfBottomFarLandF5;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        Initialize();    
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if( instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Initialize()
    {
        InitializePlatform(road, ref lastPosOfRoadTile, road.transform.position, startRoadTile, roadHolder, ref roadTiles, ref lastOrderOfRoad, new Vector3(1.5f, 0f, 0f));
        InitializePlatform(grass, ref lastPosOfTopNearGrass, grass.transform.position, startGrassTile, topNearSidewalkHolder, ref topNearGrass, ref lastOrderOfTopFarGrass, new Vector3(1.2f, 0f, 0f));
        InitializePlatform(ground3, ref lastPosOfTopFarGrass, ground3.transform.position, startGround3Tile, topFarSidewalkHolder, ref topFarGrass, ref lastOrderOfTopFarGrass, new Vector3(4.8f, 0f, 0f));
        InitializePlatform(grassBottom, ref lastPosOfBottomNearGrass, new Vector3(2f, grassBottom.transform.position.y, 0f), startGrassTile, bottomNearSidewalkHolder, ref bottomNearGrass, ref lastOrderOfBottomNearGrass, new Vector3(1.2f, 0f, 0f));

        InitializeBottomFarLand();
    }

    void InitializePlatform(GameObject prefab, ref Vector3 lastPos, Vector3 lastPosOfTile, int amountTile, GameObject holder, ref List<GameObject>listTile, ref int lastOrder, Vector3 offset)
    {
        int orderInlayer = 0;
        lastPos = lastPosOfTile;

        for (int i = 0; i < amountTile; i++)
        {
            GameObject clone = Instantiate(prefab, lastPos, prefab.transform.rotation) as GameObject;
            clone.GetComponent<SpriteRenderer>().sortingOrder = orderInlayer;

            if(clone.tag == Tags._topNearGrass)
            {
                SetNearScene(bigGrass, ref clone, ref orderInlayer, posForTopBigGrass, posForTopTree1, posForTopTree2, posForTopTree3);
            }
            else if(clone.tag == Tags._bottomNearGrass)
            {
                SetNearScene(bigGrassBottom, ref clone, ref orderInlayer, posForTopBigGrass, posForTopTree1, posForTopTree2, posForTopTree3);
            }
            else if(clone.tag == Tags._bottomFarLand2)
            {
                if(orderInlayer == 5)
                {
                    CreateTreeOrGround(bigTree, ref clone, new Vector3(-0.57f, -1.34f, 0f));
                }
            }
            else if(clone.tag == Tags._topFarGrass)
            {
                CreateGround(ref clone, ref orderInlayer);
            }

            clone.transform.SetParent(holder.transform);
            listTile.Add(clone);

            orderInlayer += 1;
            lastOrder = orderInlayer;

            lastPos += offset;
        }
    }

    void CreateScene(GameObject bigGrassPrefab, ref GameObject tileClone, int orderInLayer)
    {
        GameObject clone = Instantiate(bigGrassPrefab, tileClone.transform.position, bigGrassPrefab.transform.rotation) as GameObject;

        clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        clone.transform.SetParent(tileClone.transform);
        clone.transform.localPosition = new Vector3(-0.183f, 0.106f, 0f);

        CreateTreeOrGround(tree1, ref clone, new Vector3(0f, 1.52f, 0f));

        tileClone.GetComponent<SpriteRenderer>().enabled = false;
    }

    void CreateTreeOrGround(GameObject prefab, ref GameObject tileClone, Vector3 localPos)
    {
        GameObject clone = Instantiate(prefab, tileClone.transform.position, prefab.transform.rotation) as GameObject;

        SpriteRenderer tileCloneRenderer = tileClone.GetComponent<SpriteRenderer>();
        SpriteRenderer cloneRenderer = clone.GetComponent<SpriteRenderer>();

        cloneRenderer.sortingOrder = tileCloneRenderer.sortingOrder;
        clone.transform.SetParent(tileClone.transform);
        clone.transform.localPosition = localPos;

        if(prefab == ground1 || prefab == ground2 || prefab == ground4)
        {
            tileCloneRenderer.enabled = false;
        }
    }

    void CreateGround(ref GameObject clone, ref int orderInLayer)
    {
        for (int i = 0; i < posForTopGround1.Length; i++)
        {
            if(orderInLayer == posForTopGround1[i])
            {
                CreateTreeOrGround(ground1, ref clone, Vector3.zero);
                break;
            }
        }
        for (int i = 0; i < posForTopGround2.Length; i++)
        {
            if (orderInLayer == posForTopGround2[i])
            {
                CreateTreeOrGround(ground2, ref clone, Vector3.zero);
                break;
            }
        }
        for (int i = 0; i < posForTopGround4.Length; i++)
        {
            if (orderInLayer == posForTopGround4[i])
            {
                CreateTreeOrGround(ground4, ref clone, Vector3.zero);
                break;
            }
        }
    }

    void SetNearScene(GameObject bigGrassPrefab, ref GameObject clone, ref int orderInLayer, int[] posForBigGrass, int[] posForTree1, int[] posForTree2, int[] posForTree3)
    {
        for (int i = 0; i < posForBigGrass.Length; i++)
        {
            if(orderInLayer == posForBigGrass[i])
            {
                CreateScene(bigGrassPrefab, ref clone, orderInLayer);
                break;
            }
        }
        for (int i = 0; i < posForTree1.Length; i++)
        {
            if (orderInLayer == posForTree1[i])
            {
                CreateTreeOrGround(tree1, ref clone, new Vector3(0f, 1.15f, 0f));
                break;
            }
        }
        for (int i = 0; i < posForTree2.Length; i++)
        {
            if (orderInLayer == posForTree2[i])
            {
                CreateTreeOrGround(tree2, ref clone, new Vector3(0f, 1.15f, 0f));
                break;
            }
        }
        for (int i = 0; i < posForTree3.Length; i++)
        {
            if (orderInLayer == posForTree3[i])
            {
                CreateTreeOrGround(tree3, ref clone, new Vector3(0f, 1.15f, 0f));
                break;
            }
        }
    }

    void InitializeBottomFarLand()
    {
        InitializePlatform(land1, ref lastPosOfBottomFarLandF1, land1.transform.position, startLandTile, bottomFarSidewalkHolder, ref bottomFarLandF1, ref lastOrderOfBottomFarLandF1, new Vector3(1.6f, 0f, 0f));
        InitializePlatform(land2, ref lastPosOfBottomFarLandF2, land2.transform.position, startLandTile - 3, bottomFarSidewalkHolder, ref bottomFarLandF2, ref lastOrderOfBottomFarLandF2, new Vector3(1.6f, 0f, 0f));
        InitializePlatform(land3, ref lastPosOfBottomFarLandF3, land3.transform.position, startLandTile - 4, bottomFarSidewalkHolder, ref bottomFarLandF3, ref lastOrderOfBottomFarLandF3, new Vector3(1.6f, 0f, 0f));
        InitializePlatform(land4, ref lastPosOfBottomFarLandF4, land4.transform.position, startLandTile - 7, bottomFarSidewalkHolder, ref bottomFarLandF4, ref lastOrderOfBottomFarLandF4, new Vector3(1.6f, 0f, 0f));
        InitializePlatform(land5, ref lastPosOfBottomFarLandF5, land5.transform.position, startLandTile - 10, bottomFarSidewalkHolder, ref bottomFarLandF5, ref lastOrderOfBottomFarLandF5, new Vector3(1.6f, 0f, 0f));
    }
}