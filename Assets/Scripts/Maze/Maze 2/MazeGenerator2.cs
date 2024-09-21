using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator2 : MonoBehaviour
{
    public int l;
    public int w;

    private int unvisited;
    private Cell[,] cells;
    private Cell curr, prev;
    private List<Cell> neighborHits, path;

    public delegate void RemoveWall(GameObject wall);
    public static event RemoveWall removeWall;

    [SerializeField] private GameObject cell;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject player;

    private void setUp()
    {
        for(int i = 0; i < l; i++) //Cell
        {
            for(int j = 0; j < w; j++)
            {
                GameObject c;
                if(i == 0 && j == 0) c = Instantiate(start, new Vector3(i * 5, 0f, j * 5), Quaternion.identity);
                else if (i == l - 1 && j == w - 1) c = Instantiate(end, new Vector3(i * 5, 0f, j * 5), Quaternion.identity);
                else c = Instantiate(cell, new Vector3(i * 5, 0f, j * 5), Quaternion.identity);
                cells[i, j] = c.GetComponent<Cell>();
            }
        }

        for(int i = 0; i < l; i++) //East
        {
            Instantiate(wall, new Vector3(i * 5, 0f, w * 5), Quaternion.AngleAxis(90, Vector3.up));
        }

        for (int i = 0; i < w; i++) //North
        {
            Instantiate(wall, new Vector3(l * 5, 0f, i * 5), Quaternion.identity);
        }

        for (int i = 0; i < l; i++) //South & West
        {
            for (int j = 0; j < w; j++)
            {
                Instantiate(wall, new Vector3(i * 5, 0f, j * 5), Quaternion.AngleAxis(90, Vector3.up));
                Instantiate(wall, new Vector3(i * 5, 0f, j * 5), Quaternion.identity);
            }
        }
    }

    private Cell checkVisited(Transform c, Vector3 dir)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(c.position + new Vector3(2.5f, 2.5f, 2.5f), dir, 5, LayerMask.GetMask("Cell"));
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out Cell cell) && !cell.visited) return cell;
        }
        return null;
    }

    private List<Cell> neighborhoodCast(Transform c)
    {
        List<Cell> cellList = new List<Cell>();
        cellList.Add(checkVisited(c, Vector3.forward));
        cellList.Add(checkVisited(c, Vector3.back));
        cellList.Add(checkVisited(c, Vector3.right));
        cellList.Add(checkVisited(c, Vector3.left));
        cellList.RemoveAll(cell => cell == null);
        return cellList;
    }

    private IEnumerator tunnel()
    //private void tunnel()
    {
        yield return null;
        while(unvisited > 0)
        {
            neighborHits.Clear();
            neighborHits = neighborhoodCast(curr.transform);

            if(neighborHits.Count > 0) moveCell(neighborHits[Random.Range(0, neighborHits.Count)]);
            else
            {
                path.Remove(curr);
                path.TrimExcess();
                curr = path[path.Count - 1];
            }
            yield return null;
        }

        GameObject spawnpoint = GameObject.Find("StartPoint");
        player.transform.position = spawnpoint.transform.position + new Vector3(0f, 0.5f, 0f);
    }

    private void moveCell(Cell next)
    {
        prev = curr;
        curr = next;
        curr.visit();
        unvisited--;
        path.Add(curr);
        breakWall(curr, prev);
    }

    private void breakWall(Cell curr, Cell prev)
    {
        Vector3 dir = prev.transform.position - curr.transform.position;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(curr.transform.position + new Vector3(2.5f, 2.5f, 2.5f), dir, 5, LayerMask.GetMask("Wall"));
        foreach(RaycastHit hit in hits)
        {
            if(hit.collider.gameObject.GetComponent<Wall>() != null)
            {
                if(removeWall != null) removeWall(hit.collider.gameObject);
                break;
            }
        }
    }

    private void generateMaze()
    {
        curr = cells[0, 0];
        curr.visit();
        unvisited--;
        path.Add(curr);
        curr.GetComponent<BoxCollider>().enabled = false;

        StartCoroutine(tunnel());
        //tunnel();
    } 

    void Start()
    {
        neighborHits = new List<Cell>();
        path = new List<Cell>();
        cells = new Cell[l, w];
        unvisited = l * w;
        setUp();
        generateMaze();
    }
}
