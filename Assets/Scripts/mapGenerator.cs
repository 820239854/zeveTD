using System.Collections.Generic;
using UnityEngine;

public class mapGenerator : MonoBehaviour
{
    public GameObject MapTile;
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;

    public static List<GameObject> mapTiles = new List<GameObject>();
    public static List<GameObject> pathTiles = new List<GameObject>();

    public static GameObject startTile;
    public static GameObject endTile;

    private bool reachedX = false;
    private bool reachedY = false;

    private GameObject currentTile;
    private int currIndex;
    private int nextIndex;

    public Color pathColor;
    public Color startColor;
    public Color endColor;

    private void Start()
    {
        generateMap();
    }

    private List<GameObject> getTopEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();

        for (int i = mapWidth * (mapHeight - 1); i < mapWidth * mapHeight; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }


    private List<GameObject> getBottomEdgeTiles()
    {
        List<GameObject> edgeTiles = new List<GameObject>();
        for (int i = 0; i < mapWidth; i++)
        {
            edgeTiles.Add(mapTiles[i]);
        }

        return edgeTiles;
    }


    private void moveDown()
    {
        pathTiles.Add(currentTile);
        currIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currIndex - mapWidth;
        currentTile = mapTiles[nextIndex];
    }

    private void moveLeft()
    {
        pathTiles.Add(currentTile);
        currIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currIndex - 1;
        currentTile = mapTiles[nextIndex];
    }

    private void moveRight()
    {
        pathTiles.Add(currentTile);
        currIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currIndex + 1;
        currentTile = mapTiles[nextIndex];
    }

    private void generateMap()
    {
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                GameObject newTile = Instantiate(MapTile);
                newTile.transform.position = new Vector2(x, y);

                mapTiles.Add(newTile);
            }
        }

        List<GameObject> topEdgeTiles = getTopEdgeTiles();
        List<GameObject> bottomEdgeTiles = getBottomEdgeTiles();

        startTile = topEdgeTiles[Random.Range(0, mapWidth)];
        endTile = bottomEdgeTiles[Random.Range(0, mapWidth)];

        currentTile = startTile;
        moveDown();

        int loopCount = 0;
        while (!reachedX)
        {
            loopCount++;
            if (loopCount > 100)
            {
                Debug.Log("Loop count exceeded");
                break;
            }

            if (currentTile.transform.position.x > endTile.transform.position.x)
            {
                moveLeft();
            }
            else if (currentTile.transform.position.x < endTile.transform.position.x)
            {
                moveRight();
            }
            else
            {
                reachedX = true;
            }
        }

        while (!reachedY)
        {
            loopCount++;
            if (loopCount > 100)
            {
                Debug.Log("Loop count exceeded");
                break;
            }

            if (currentTile.transform.position.y > endTile.transform.position.y)
            {
                moveDown();
            }
            else
            {
                reachedY = true;
            }
        }

        pathTiles.Add(endTile);

        foreach (GameObject pathTile in pathTiles)
        {
            pathTile.GetComponent<SpriteRenderer>().color = pathColor;
        }

        startTile.GetComponent<SpriteRenderer>().color = startColor;
        endTile.GetComponent<SpriteRenderer>().color = endColor;
    }
}