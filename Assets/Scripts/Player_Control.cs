using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    [SerializeField] private Swipe swipeControl;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform horizontalBorderLimits;
    [SerializeField] private GameObject Tail;
    [SerializeField] private GameObject Fill;
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private GameObject prefabTile;
    [SerializeField] private BoardManager board;
    [SerializeField] private float fillDelay = 0.001f;
    [SerializeField] private List<Vector2> paths;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private LevelUp levelUp;
    private Vector3 startPos;
    int levelPoint = 0;
    private int move;
    public bool moveC;
    public bool tailOpen;

    [SerializeField] public static float moveSpeed = 5f;

    private void Start()
    {
        levelUp.SetMaxPoint(Mathf.RoundToInt(horizontalBorderLimits.GetChild(1).position.x) * Mathf.RoundToInt(horizontalBorderLimits.GetChild(1).position.y));
        startPos = Player.position;
        moveC = false;
        tailOpen = true;
        move = 0;
    }
    private void Update()
    {

        if (swipeControl.SwipeLeft && move != 2)
        {
            Player.position = new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y), Player.position.z);
            paths.Add(new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y)));
            move = 1;
            moveC = true;
            Debug.Log("left");
        }
        else if (swipeControl.SwipeRight && move != 1)
        {
            Player.position = new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y), Player.position.z);
            paths.Add(new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y)));
            move = 2;
            moveC = true;
            Debug.Log("right");
        }
        else if (swipeControl.SwipeUp && move != 4)
        {
            Player.position = new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y), Player.position.z);
            paths.Add(new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y)));
            move = 3;
            moveC = true;
            Debug.Log("up");
        }
        else if (swipeControl.SwipeDown && move != 3)
        {
            Player.position = new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y), Player.position.z);
            paths.Add(new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y)));
            move = 4;
            moveC = true;
            Debug.Log("down");
        }
        int x = (int)Mathf.Round(Player.position.x);
        int y = (int)Mathf.Round(Player.position.y);
        if (board.grid[x, y].GetComponent<SpriteRenderer>().color != Color.cyan
            && board.grid[x, y].GetComponent<SpriteRenderer>().color != Color.black
            && board.grid[x, y].GetComponent<SpriteRenderer>().color != Color.green)
        {
            board.grid[x, y].GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        if (tailOpen == true)
            Tail_Follow(new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z), move);
        //Player.transform.position = Vector3.MoveTowards(Player.transform.position, desPos, moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {

        if (move == 1 && moveC == true)
        {
            this.transform.Translate(Time.deltaTime * -5f, 0, 0);
        }
        else if (move == 2 && moveC == true)
        {
            this.transform.Translate(Time.deltaTime * 5f, 0, 0);
        }
        else if (move == 3 && moveC == true)
        {
            this.transform.Translate(0, Time.deltaTime * 5f, 0);
        }
        else if (move == 4 && moveC == true)
        {
            this.transform.Translate(0, Time.deltaTime * -5f, 0);
        }
    }

    public void Tail_Follow(Vector3 tailPos, int dir)
    {
        if (dir == 1)
            Instantiate(Tail, new Vector3(Mathf.Round(tailPos.x + 0.3f), Mathf.Round(tailPos.y), tailPos.z), Quaternion.identity);
        if (dir == 2)
            Instantiate(Tail, new Vector3(Mathf.Round(tailPos.x - 0.3f), Mathf.Round(tailPos.y), tailPos.z), Quaternion.identity);
        if (dir == 3)
            Instantiate(Tail, new Vector3(Mathf.Round(tailPos.x), Mathf.Round(tailPos.y - 0.3f), tailPos.z), Quaternion.identity);
        if (dir == 4)
            Instantiate(Tail, new Vector3(Mathf.Round(tailPos.x), Mathf.Round(tailPos.y + 0.3f), tailPos.z), Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall" && moveC == true)
        {
            paths.Add(new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y)));
            control();
            tailOpen = false;
            moveC = false;

            move = 0;
        }
        if (other.gameObject.tag == "Fill" && moveC == true)
        {
            int x = (int)Mathf.Round(Player.position.x);
            int y = (int)Mathf.Round(Player.position.y);
            paths.Add(new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y)));
            if (board.grid[x, y].GetComponent<SpriteRenderer>().color != Color.green)
                control();
            
            tailOpen = false;
        }
        if (other.gameObject.tag == "Enemy" && moveC == true)
        {
            if (GameOver.activeInHierarchy == false)
            {
                GameOver.SetActive(true);
                Destroy(this.gameObject);
            }
            Debug.Log("DEAD");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fill" && moveC == true)
        {
            if (tailOpen == true)
                tailOpen = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        paths.Add(new Vector3((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y)));
        if (other.gameObject.tag == "Fill" && moveC == true)
        {
            paths.Add(new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y)));
            if (tailOpen == false)
            {
                tailOpen = true;
                if (board.grid[(int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y)].GetComponent<SpriteRenderer>().color != Color.cyan)
                    paths.Clear();
            }
        }
    }

    public IEnumerator Flood(int x, int y, Color oldColor, Color newColor)
    {
        WaitForSeconds wait = new WaitForSeconds(fillDelay);
        if (x >= 0 && x < board.xSize && y >= 0 && y < board.ySize)
        {
            yield return wait;
            if (board.grid[x, y].GetComponent<SpriteRenderer>().color == oldColor
                || board.grid[x, y].GetComponent<SpriteRenderer>().color == Color.red)
            {
                levelPoint++;
                board.grid[x, y].GetComponent<SpriteRenderer>().color = newColor;
                GameObject fill = Instantiate(Fill);
                fill.transform.position = new Vector2(gameBoard.transform.position.x + (GetSize(prefabTile).x * x), gameBoard.transform.position.y + (GetSize(prefabTile).y * y));
                StartCoroutine(Flood(x + 1, y, oldColor, newColor));
                StartCoroutine(Flood(x - 1, y, oldColor, newColor));
                StartCoroutine(Flood(x, y + 1, oldColor, newColor));
                StartCoroutine(Flood(x, y - 1, oldColor, newColor));
            }
            /*if (board.grid[x, y].GetComponent<SpriteRenderer>().color == Color.yellow)
            {
                board.grid[x, y].GetComponent<SpriteRenderer>().color = newColor;
            }*/
        }
    }
    private Vector2 GetSize(GameObject tile)
    {
        return new Vector2(tile.GetComponent<SpriteRenderer>().bounds.size.x, tile.GetComponent<SpriteRenderer>().bounds.size.y);
    }

    private void control()
    {
        int midX = Mathf.RoundToInt((paths[paths.Count - 1].x - paths[0].x));
        int midY = Mathf.RoundToInt((paths[paths.Count - 1].y - paths[0].y));
        paths.Add(new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y)));
        Debug.Log(paths[paths.Count - 1].x);
        Debug.Log(paths[paths.Count - 1].y);
        Player.position = new Vector3(Mathf.Round(Player.position.x), Mathf.Round(Player.position.y), Player.position.z);

        if (paths[0].x < paths[paths.Count - 1].x && paths[0].y == paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x - Mathf.Abs(midX) / 2), (int)Mathf.Round(Player.position.y), Color.white, Color.green));
            Debug.Log("midx1: " + midX);
        }
        else if (paths[0].x >= paths[paths.Count - 1].x && paths[0].y == paths[paths.Count - 1].y)
        {
            if (paths[0].x < horizontalBorderLimits.GetChild(1).position.y /2)
                StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y - 1), Color.white, Color.green));
            else
                StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y - 1), Color.white, Color.green));
            Debug.Log("midx2: " + midX);
        }
        else if (paths[0].x == paths[paths.Count - 1].x && paths[0].y < paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x + Mathf.Abs(midX) / 2), (int)Mathf.Round(Player.position.y - Mathf.Abs(midY) / 2), Color.white, Color.green));
            Debug.Log("x= y< midy: " + midY);
        }
        else if (paths[0].x == paths[paths.Count - 1].x && paths[0].y >= paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y + Mathf.Abs(midY) / 2), Color.white, Color.green));
            Debug.Log("x= y>= midy: " + midY);
        }
        else if (paths[0].x < paths[paths.Count - 1].x && paths[0].y < paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x - Mathf.Abs(midX) / 2), (int)Mathf.Round(Player.position.y - Mathf.Abs(midY) / 2), Color.white, Color.green));
            Debug.Log("x< y< midy: " + midY);
            Debug.Log("x< y< midx: " + midX);
        }
        else if (paths[0].x > paths[paths.Count - 1].x && paths[0].y < paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x + Mathf.Abs(midX) / 2), (int)Mathf.Round(Player.position.y - Mathf.Abs(midY) / 2), Color.white, Color.green));
            Debug.Log("x> y< midy: " + midY);
        }
        else if (paths[0].x < paths[paths.Count - 1].x && paths[0].y > paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x + Mathf.Abs(midX) / 2), (int)Mathf.Round(Player.position.y - Mathf.Abs(midY) / 2), Color.white, Color.green));
            Debug.Log("x> y> midy: " + midY);
        }
        else if (paths[0].x >= paths[paths.Count - 1].x && paths[0].y >= paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x + Mathf.Abs(midX) / 2), (int)Mathf.Round(Player.position.y + Mathf.Abs(midY) / 2), Color.white, Color.green));
            Debug.Log("x> y> midy: " + midY);
        }
        /*else if (paths[0].y > paths[paths.Count - 1].y && paths[0].y == paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x - midX / 2), (int)Mathf.Round(Player.position.y), Color.white, Color.green));
            Debug.Log("midx: " + midX);
        }*/

        /*if (paths[0].x < paths[paths.Count - 1].x && paths[0].y > paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y - 1), Color.white, Color.green));
            //StartCoroutine(Flood((int)Mathf.Round(Player.position.x + 1), (int)Mathf.Round(Player.position.y), Color.white, Color.green));
            Debug.Log(1);
        }
        else if (paths[0].x < paths[paths.Count - 1].x && paths[0].y < paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x + 1), (int)Mathf.Round(Player.position.y), Color.white, Color.green));
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y - 1), Color.white, Color.green));
            Debug.Log(2);
        }
        else if (paths[0].x > paths[paths.Count - 1].x && paths[0].y > paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x + 1), (int)Mathf.Round(Player.position.y), Color.white, Color.green));
            Debug.Log(3);
        }
        else if (paths[0].x > paths[paths.Count - 1].x && paths[0].y < paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y - 1), Color.white, Color.green));
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x - 1), (int)Mathf.Round(Player.position.y), Color.white, Color.green));
            Debug.Log(4);
        }
        else if (paths[0].x < paths[paths.Count - 1].x && paths[0].y == paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x - 1), (int)Mathf.Round(Player.position.y), Color.white, Color.green));
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y - 1), Color.white, Color.green));
            Debug.Log(5);
        }
        else if (paths[0].x > paths[paths.Count - 1].x && paths[0].y == paths[paths.Count - 1].y)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y - 1), Color.white, Color.green));
            Debug.Log(6);
        }
        else if (paths[0].x == paths[paths.Count - 1].x && paths[paths.Count - 1].y > horizontalBorderLimits.GetChild(1).position.y / 2)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y + 1), Color.white, Color.green));
            Debug.Log(7);
        }
        else if (paths[0].x == paths[paths.Count - 1].x && paths[paths.Count - 1].y <= horizontalBorderLimits.GetChild(1).position.y / 2)
        {
            StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y - 1), Color.white, Color.green));
            Debug.Log("sorun :" + 8);
        }*/
        StartCoroutine(Flood((int)Mathf.Round(Player.position.x), (int)Mathf.Round(Player.position.y), Color.cyan, Color.green));
        Debug.Log("startpos : " + startPos);
        Debug.Log(horizontalBorderLimits.GetChild(1).position.y / 2);
        levelUp.SetLevelPoint(levelPoint);
        paths.Clear();
        Debug.Log("trigger");
    }
}
