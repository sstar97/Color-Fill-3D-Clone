using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabTile;
    [SerializeField] private GameObject playerObj;
    [SerializeField] private GameObject wallObj;
    [SerializeField] private GameObject enemyObj;
    [SerializeField] private GameObject gameBoard;
    public int xSize, ySize;
    public List<Vector2> walls;
    public List<Vector2> enemy;
    public Vector2 playerStart;
    public GameObject[,] grid;
    void Start()
    {
        CreateBoard(xSize, ySize);
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                //player object instantiate to desired position
                if (x == playerStart.x && y == playerStart.y)
                {
                    playerObj.transform.position = new Vector2(gameBoard.transform.position.x + (GetSize(prefabTile).x * x), gameBoard.transform.position.y + (GetSize(prefabTile).y * y));
                }
                // wall object instantiate in the form of a frame
                if (x == 0 || x == xSize - 1)
                {
                    ColorTile(x, y, Color.black);
                    GameObject wall = Instantiate(wallObj);
                    wall.transform.parent = gameBoard.transform;
                    wall.transform.position = new Vector2(gameBoard.transform.position.x + (GetSize(prefabTile).x * x), gameBoard.transform.position.y + (GetSize(prefabTile).y * y));
                    
                }
                if (y == 0 || y == ySize - 1)
                {
                    ColorTile(x, y, Color.black);
                    GameObject wall = Instantiate(wallObj);
                    wall.transform.parent = gameBoard.transform;
                    wall.transform.position = new Vector2(gameBoard.transform.position.x + (GetSize(prefabTile).x * x), gameBoard.transform.position.y + (GetSize(prefabTile).y * y));
                    
                }
                for (int i = 0; i < walls.Count; i++)
                {
                    if (x == walls[i].x && y == walls[i].y)
                    {
                        ColorTile(x, y, Color.black);
                        GameObject wall = Instantiate(wallObj);
                        wall.transform.parent = gameBoard.transform;
                        wall.transform.position = new Vector2(gameBoard.transform.position.x + (GetSize(prefabTile).x * x), gameBoard.transform.position.y + (GetSize(prefabTile).y * y));
                        
                    }
                }
                //enemy blocks instantiate
                for (int i = 0; i < enemy.Count; i++)
                {
                    if (x == enemy[i].x && y == enemy[i].y)
                    {
                        ColorTile(x, y, Color.red);
                        GameObject wall = Instantiate(enemyObj);
                        wall.transform.parent = gameBoard.transform;
                        wall.transform.position = new Vector2(gameBoard.transform.position.x + (GetSize(prefabTile).x * x), gameBoard.transform.position.y + (GetSize(prefabTile).y * y));
                    }
                }
                //filled area
            }
        }
    }

    private void ColorTile(int x, int y, Color color)
    {
        grid[x, y].GetComponent<SpriteRenderer>().color = color;
    }

    private Vector2 GetSize(GameObject tile)
    {
        return new Vector2(tile.GetComponent<SpriteRenderer>().bounds.size.x, tile.GetComponent<SpriteRenderer>().bounds.size.y);
    }

    private void CreateBoard(int width, int height)
    {
        grid = new GameObject[width, height];
        Vector2 startPos = this.transform.position;
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject tile = Instantiate(prefabTile);
                tile.transform.parent = this.transform;
                tile.transform.position = new Vector2(startPos.x + (GetSize(prefabTile).x * x), startPos.y + (GetSize(prefabTile).y * y));
                grid[x, y] = tile;
            }
        }
    }
}
