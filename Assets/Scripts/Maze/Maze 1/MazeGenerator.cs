using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour
{
    public int l;
    public int w;
    private char[,,] map;
    private List<Node> nodeList;

    private int roomPosition;
    private int randomDirection;

    private int[] dirX = { 1, 0, -1, 0 };
    private int[] dirY = { 0, 1, 0, -1 };

    private System.Random rand = new System.Random();

    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject teleporter;
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject backWallLight;
    [SerializeField] private GameObject frontWallLight;
    [SerializeField] private GameObject leftWallLight;
    [SerializeField] private GameObject rightWallLight;
    [SerializeField] private GameObject backWallProp1;
    [SerializeField] private GameObject frontWallProp1;
    [SerializeField] private GameObject leftWallProp1;
    [SerializeField] private GameObject rightWallProp1;
    [SerializeField] private GameObject backWallProp2;
    [SerializeField] private GameObject frontWallProp2;
    [SerializeField] private GameObject leftWallProp2;
    [SerializeField] private GameObject rightWallProp2;
    [SerializeField] private GameObject backDoor;
    [SerializeField] private GameObject frontDoor;
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private GameObject endRoom;
    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject room3;
    [field: SerializeField] private NavMeshSurface navMeshSurface;

    [SerializeField] private GameObject player;

    private List<GameObject> walls;
    private List<List<GameObject>> hallWalls;
    private List<GameObject> doors;

    private void instantiateStartRoom(int i, int j, int y)
    {
        int[] roomX = { 0, 1, 1, 0 };
        int[] roomY = { 0, 0, 1, 1 };
        Instantiate(startRoom, new Vector3(i * 5, 0f, j * 5), Quaternion.identity);
        for (int k = 0; k < 4; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] == 'D')
                {
                    Instantiate(doors[l], new Vector3(i * 5 + roomX[k] * 5 + dirX[l] * 5, y * 5, j * 5 + roomY[k] * 5 + dirY[l] * 5), Quaternion.identity);
                }
                else if (map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] != 'S' && map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] != 's')
                {
                    Instantiate(walls[l], new Vector3(i * 5 + roomX[k] * 5 + dirX[l] * 5, y * 5, j * 5 + roomY[k] * 5 + dirY[l] * 5), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateEndRoom(int i, int j, int y)
    {
        int[] roomX = { 0, 1, 1, 0 };
        int[] roomY = { 0, 0, 1, 1 };
        Instantiate(endRoom, new Vector3(i * 5, y * 5, j * 5), Quaternion.identity);
        for (int k = 0; k < 4; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] == 'D')
                {
                    Instantiate(doors[l], new Vector3(i * 5 + roomX[k] * 5 + dirX[l] * 5, y * 5, j * 5 + roomY[k] * 5 + dirY[l] * 5), Quaternion.identity);
                }
                else if (map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] != 'E' && map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] != 'e')
                {
                    Instantiate(walls[l], new Vector3(i * 5 + roomX[k] * 5 + dirX[l] * 5, y * 5, j * 5 + roomY[k] * 5 + dirY[l] * 5), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateRoom1s(int i, int j, int y)
    {
        int[] roomX = { 0, 1, 0, 1, 0, 1 };
        int[] roomY = { 0, 0, 1, 1, 2, 2 };
        Instantiate(room1, new Vector3(i * 5, y * 5, j * 5), Quaternion.identity);
        for (int k = 0; k < 6; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] == 'D')
                {
                    Instantiate(doors[l], new Vector3(i * 5 + roomX[k] * 5 + dirX[l] * 5, y * 5, j * 5 + roomY[k] * 5 + dirY[l] * 5), Quaternion.identity);
                }
                else if (map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] != '!' && map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] != '1')
                {
                    Instantiate(walls[l], new Vector3(i * 5 + roomX[k] * 5 + dirX[l] * 5, y * 5, j * 5 + roomY[k] * 5 + dirY[l] * 5), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateRoom2s(int i, int j, int y)
    {
        int[] roomX = { 0, 0, 0, 2, 2, 2, 1, 1 };
        int[] roomY = { 0, 1, 2, 0, 1, 2, 0, 2 };
        Instantiate(room2, new Vector3(i * 5, y * 5, j * 5), Quaternion.identity);
        for (int k = 0; k < 8; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] == 'D')
                {
                    Instantiate(doors[l], new Vector3(i * 5 + roomX[k] * 5 + dirX[l] * 5, y * 5, j * 5 + roomY[k] * 5 + dirY[l] * 5), Quaternion.identity);
                }
                else if (map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] != '@' && map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] != '2')
                {
                    Instantiate(walls[l], new Vector3(i * 5 + roomX[k] * 5 + dirX[l] * 5, y * 5, j * 5 + roomY[k] * 5 + dirY[l] * 5), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateRoom3s(int i, int j, int y)
    {
        int[] roomX = { 0, 1 };
        int[] roomY = { 0, 0 };
        Instantiate(room3, new Vector3(i * 5, y * 5, j * 5), Quaternion.identity);
        for (int k = 0; k < 2; k++)
        {
            for (int l = 0; l < 4; l++)
            {
                if (map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] == 'D')
                {
                    Instantiate(doors[l], new Vector3(i * 5 + roomX[k] * 5 + dirX[l] * 5, y * 5, j * 5 + roomY[k] * 5 + dirY[l] * 5), Quaternion.identity);
                }
                else if (map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] != '$' && map[i + roomX[k] + dirX[l], j + roomY[k] + dirY[l], y] != '4')
                {
                    Instantiate(walls[l], new Vector3(i * 5 + roomX[k] * 5 + dirX[l] * 5, y * 5, j * 5 + roomY[k] * 5 + dirY[l] * 5), Quaternion.identity);
                }
            }
        }
    }

    private void instantiateWalls(int i, int j, int k, List<GameObject> hallWallList)
    {
        int n = rand.Next(101);
        if(n <= 60)
        {
            Instantiate(hallWallList[0], new Vector3(i * 5, k * 5f, j * 5), Quaternion.identity);
        }
        else if(n <= 90)
        {
            Instantiate(hallWallList[1], new Vector3(i * 5, k * 5f, j * 5), Quaternion.identity);
        }
        else if (n <= 95)
        {
            Instantiate(hallWallList[2], new Vector3(i * 5, k * 5f, j * 5), Quaternion.identity);
        }
        else
        {
            Instantiate(hallWallList[3], new Vector3(i * 5, k * 5f, j * 5), Quaternion.identity);
        }
    }

    private void instantiateMaze()
    {
        for (int i = 1; i < l - 1; i++)
        {
            for (int j = 1; j < w - 1; j++)
            {
                if (map[i, j, 0] == ' ' || map[i, j, 0] == 'D' || map[i, j, 0] == 'T')
                {
                    if (map[i, j, 0] == ' ' || map[i, j, 0] == 'D') Instantiate(floor, new Vector3(i * 5, 0f, j * 5), Quaternion.identity);
                    else Instantiate(teleporter, new Vector3(i * 5, 0f, j * 5), Quaternion.identity);
                    for (int k = 0; k < 4; k++)
                    {
                        if (map[i + dirX[k], j + dirY[k], 0] == '#')
                        {
                            instantiateWalls(i, j, 0, hallWalls[k]);
                        }
                    }
                }
                if (map[i, j, 0] == 'S')
                {
                    instantiateStartRoom(i, j, 0);
                }
                else if (map[i, j, 0] == '@')
                {
                    instantiateRoom2s(i, j, 0);
                }
                else if (map[i, j, 0] == '!')
                {
                    instantiateRoom1s(i, j, 0);
                }
                else if (map[i, j, 0] == '$')
                {
                    instantiateRoom3s(i, j, 0);
                }
            }
        }
        for (int i = 1; i < l - 1; i++)
        {
            for (int j = 1; j < w - 1; j++)
            {
                if (map[i, j, 1] == ' ' || map[i, j, 1] == 'D' || map[i, j, 0] == 'T')
                {
                    Instantiate(floor, new Vector3(i * 5, 5f, j * 5), Quaternion.identity);
                    for (int k = 0; k < 4; k++)
                    {
                        if (map[i + dirX[k], j + dirY[k], 1] == '#')
                        {
                            instantiateWalls(i, j, 1, hallWalls[k]);
                        }
                    }
                }
                if (map[i, j, 1] == 'E')
                {
                    instantiateEndRoom(i, j, 1);
                }
                else if (map[i, j, 1] == '@')
                {
                    instantiateRoom2s(i, j, 1);
                }
                else if (map[i, j, 1] == '!')
                {
                    instantiateRoom1s(i, j, 1);
                }
                else if (map[i, j, 1] == '$')
                {
                    instantiateRoom3s(i, j, 1);
                }
            }
        }
    }

    private void generateTeleporters()
    {
        int xRand1, xRand2, yRand1, yRand2, x, y;
        do
        {
            xRand1 = rand.Next(l - 5) + 2;
            yRand1 = rand.Next(w - 5) + 2;
            xRand2 = rand.Next(l - 5) + 2;
            yRand2 = rand.Next(w - 5) + 2;
            x = xRand1 - xRand2;
            y = yRand1 - yRand2;
        } while (map[xRand1, yRand1, 0] != '#' || map[xRand2, yRand2, 0] != '#' || map[xRand1, yRand1, 1] != '#' || map[xRand2, yRand2, 1] != '#' || Mathf.Sqrt(x* x + y * y) < Mathf.Sqrt(l * l + w * w) / 2);
        map[xRand1, yRand1, 0] = 'T';
        map[xRand1, yRand1, 1] = 'T';
        map[xRand2, yRand2, 0] = 'T';
        map[xRand2, yRand2, 1] = 'T';
    }

    private void generateStartRoom()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 1, 1, 0 };
        int[] roomY = { 0, 0, 1, 1 };
        bool detect;
        do{
            detect = false;
            xRand = rand.Next(l - 7) + 2;
            yRand = rand.Next(w - 7) + 2;
            for(int j = -2; j < 4; j++) for(int k = -2; k < 4; k++) if(map[xRand + j, yRand + k, 0] != '#') detect = true;
        }while(detect);
        for(int j = 0; j < 2; j++) for(int k = 0; k < 2; k++) map[xRand + j, yRand + k, 0] = 's';
        map[xRand, yRand, 0] = 'S';
        do{
            roomPosition = rand.Next(4);
            randomDirection = rand.Next(4);
            if(map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 0] == '#'){
                map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 0] = 'D';
                break;
            }
        }while(true);
    }

    private void generateEndRoom()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 1, 1, 0 };
        int[] roomY = { 0, 0, 1, 1 };
        bool detect;
        do
        {
            detect = false;
            xRand = rand.Next(l - 7) + 2;
            yRand = rand.Next(w - 7) + 2;
            for (int j = -2; j < 4; j++) for (int k = -2; k < 4; k++) if (map[xRand + j, yRand + k, 1] != '#') detect = true;
        } while (detect);
        for (int j = 0; j < 2; j++) for (int k = 0; k < 2; k++) map[xRand + j, yRand + k, 1] = 'e';
        map[xRand, yRand, 1] = 'E';
        do
        {
            roomPosition = rand.Next(4);
            randomDirection = rand.Next(4);
            if (map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 1] == '#')
            {
                map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 1] = 'D';
                break;
            }
        } while (true);
    }

    private void generateRoom1s()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 1, 0, 1, 0, 1 };
        int[] roomY = { 0, 0, 1, 1, 2, 2 };
        int roomPosition;
        int randomDirection;
        bool detect;
        for (int i = 0; i < 2; i++)
        {
            do
            {
                detect = false;
                xRand = rand.Next(l - 8) + 2;
                yRand = rand.Next(w - 7) + 2;

                for (int j = -2; j < 4; j++) for (int k = -2; k < 5; k++) if (map[xRand + j, yRand + k, 0] != '#') detect = true;
            } while (detect);
            for (int j = 0; j < 2; j++) for (int k = 0; k < 3; k++) map[xRand + j, yRand + k, 0] = '1';
            map[xRand, yRand, 0] = '!';
            do
            {
                roomPosition = rand.Next(6);
                randomDirection = rand.Next(4);
                if (map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 0] == '#')
                {
                    map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 0] = 'D';
                    break;
                }
            } while (true);
        }
        for (int i = 0; i < 3; i++)
        {
            do
            {
                detect = false;
                xRand = rand.Next(l - 8) + 2;
                yRand = rand.Next(w - 7) + 2;

                for (int j = -2; j < 4; j++) for (int k = -2; k < 5; k++) if (map[xRand + j, yRand + k, 1] != '#') detect = true;
            } while (detect);
            for (int j = 0; j < 2; j++) for (int k = 0; k < 3; k++) map[xRand + j, yRand + k, 1] = '1';
            map[xRand, yRand, 1] = '!';
            do
            {
                roomPosition = rand.Next(6);
                randomDirection = rand.Next(4);
                if (map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 1] == '#')
                {
                    map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 1] = 'D';
                    break;
                }
            } while (true);
        }
    }

    private void generateRoom2s()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 0, 0, 2, 2, 2, 1, 1 };
        int[] roomY = { 0, 1, 2, 0, 1, 2, 0, 2 };
        int roomPosition;
        int randomDirection;
        bool detect;
        for (int i = 0; i < 3; i++){
            do{
                detect = false;
                xRand = rand.Next(l - 8) + 2;
                yRand = rand.Next(w - 8) + 2;

                for(int j = -2; j < 5; j++) for(int k = -2; k < 5; k++) if(map[xRand + j, yRand + k, 0] != '#') detect = true;
            }while(detect);
            for(int j = 0; j < 3; j++) for(int k = 0; k < 3; k++) map[xRand + j, yRand + k, 0] = '2';
            map[xRand, yRand, 0] = '@';
            do{
                roomPosition = rand.Next(8);
                randomDirection = rand.Next(4);
                if(map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 0] == '#'){
                    map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 0] = 'D';
                    break;
                }
            }while(true);
        }
        for (int i = 0; i < 4; i++)
        {
            do
            {
                detect = false;
                xRand = rand.Next(l - 8) + 2;
                yRand = rand.Next(w - 8) + 2;

                for (int j = -2; j < 5; j++) for (int k = -2; k < 5; k++) if (map[xRand + j, yRand + k, 1] != '#') detect = true;
            } while (detect);
            for (int j = 0; j < 3; j++) for (int k = 0; k < 3; k++) map[xRand + j, yRand + k, 1] = '2';
            map[xRand, yRand, 1] = '@';
            do
            {
                roomPosition = rand.Next(8);
                randomDirection = rand.Next(4);
                if (map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 1] == '#')
                {
                    map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 1] = 'D';
                    break;
                }
            } while (true);
        }
    }

    private void generateRoom3s()
    {
        int xRand = 0;
        int yRand = 0;
        int[] roomX = { 0, 1 };
        int[] roomY = { 0, 0 };
        int roomPosition;
        int randomDirection;
        bool detect;
        for (int i = 0; i < 2; i++)
        {
            do
            {
                detect = false;
                xRand = rand.Next(l - 7) + 2;
                yRand = rand.Next(w - 6) + 2;

                for (int j = -2; j < 4; j++) for (int k = -2; k < 3; k++) if (map[xRand + j, yRand + k, 0] != '#') detect = true;
            } while (detect);
            for (int j = 0; j < 2; j++) for (int k = 0; k < 1; k++) map[xRand + j, yRand + k, 0] = '4';
            map[xRand, yRand, 0] = '$';
            do
            {
                roomPosition = rand.Next(2);
                randomDirection = rand.Next(4);
                if (map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 0] == '#')
                {
                    map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 0] = 'D';
                    break;
                }
            } while (true);
        }
        for (int i = 0; i < 2; i++)
        {
            do
            {
                detect = false;
                xRand = rand.Next(l - 7) + 2;
                yRand = rand.Next(w - 6) + 2;

                for (int j = -2; j < 4; j++) for (int k = -2; k < 3; k++) if (map[xRand + j, yRand + k, 1] != '#') detect = true;
            } while (detect);
            for (int j = 0; j < 2; j++) for (int k = 0; k < 1; k++) map[xRand + j, yRand + k, 1] = '4';
            map[xRand, yRand, 1] = '$';
            do
            {
                roomPosition = rand.Next(2);
                randomDirection = rand.Next(4);
                if (map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 1] == '#')
                {
                    map[xRand + roomX[roomPosition] + dirX[randomDirection], yRand + roomY[roomPosition] + dirY[randomDirection], 1] = 'D';
                    break;
                }
            } while (true);
        }
    }

    private void insert(Node node)
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].Price > node.Price)
            {
                nodeList.Insert(i, node);
                return;
            }
        }
        nodeList.Insert(nodeList.Count, node);
    }

    private char[,] getLayer(char[,,] map, int n)
    {
        int w = map.GetLength(0);
        int l = map.GetLength(1);

        char[,] layer = new char[w, l];

        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < l; j++)
            {
                layer[i, j] = map[i, j, n];
            }
        }

        return layer;
    }

    private void setLayer(char[,,] map, char[,] values, int n)
    {
        int w = map.GetLength(0);
        int l = map.GetLength(1);

        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < l; j++)
            {
                map[i, j, n] = values[i, j];
            }
        }
    }

    private void generateMaze()
    {
        map = new char[l + 1, w + 1, 2];
        for (int i = 0; i < l; i++)
        {
            for (int j = 0; j < w; j++)
            {
                for(int k = 0; k < 2; k++)
                {
                    map[i, j, k] = '#';
                }   
            }
        }

        generateStartRoom();
        generateEndRoom();
        generateRoom1s();
        generateRoom2s();
        generateRoom3s();
        generateTeleporters();

        List<Tile> tileList = new List<Tile>();
        for (int i = 0; i < l; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if (map[i, j, 0] == 'D' || map[i, j, 0] == 'T')
                {
                    tileList.Add(new Tile(i, j));
                }
            }
        }

        nodeList = new List<Node>();
        for (int i = 0; i < tileList.Count; i++)
        {
            for (int j = i + 1; j < tileList.Count; j++)
            {
                if (tileList[i] != tileList[j])
                {
                    insert(new Node(tileList[i], tileList[j]));
                }

            }
        }

        List<Tile> connected = new List<Tile>();
        List<Node> graphRes = new List<Node>();
        List<Node> extras = new List<Node>();
        bool first = true;

        foreach (Node node in nodeList)
        {
            if (first)
            {
                connected.Add(node.Dest);
                connected.Add(node.Src);
                graphRes.Add(node);
                first = false;
            }
            else if (connected.Contains(node.Dest) && !connected.Contains(node.Src))
            {
                connected.Add(node.Src);
                graphRes.Add(node);
            }
            else if (!connected.Contains(node.Dest) && connected.Contains(node.Src))
            {
                connected.Add(node.Dest);
                graphRes.Add(node);
            }
            else {
                extras.Add(node);
            }
        }

        foreach(Node node in extras)
        {
            if(rand.Next(4) == 0)
            {
                graphRes.Add(node);
            }
        }

        AStar astar = new AStar(l, w);

        foreach (Node node in graphRes)
        {
            setLayer(map, astar.trace(node.Src, node.Dest, getLayer(map, 0)), 0);
        }

        tileList = new List<Tile>();
        for (int i = 0; i < l; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if (map[i, j, 1] == 'D' || map[i, j, 1] == 'T')
                {
                    tileList.Add(new Tile(i, j));
                }
            }
        }

        nodeList = new List<Node>();
        for (int i = 0; i < tileList.Count; i++)
        {
            for (int j = i + 1; j < tileList.Count; j++)
            {
                if (tileList[i] != tileList[j])
                {
                    insert(new Node(tileList[i], tileList[j]));
                }

            }
        }

        connected = new List<Tile>();
        graphRes = new List<Node>();
        extras = new List<Node>();
        first = true;

        foreach (Node node in nodeList)
        {
            if (first)
            {
                connected.Add(node.Dest);
                connected.Add(node.Src);
                graphRes.Add(node);
                first = false;
            }
            else if (connected.Contains(node.Dest) && !connected.Contains(node.Src))
            {
                connected.Add(node.Src);
                graphRes.Add(node);
            }
            else if (!connected.Contains(node.Dest) && connected.Contains(node.Src))
            {
                connected.Add(node.Dest);
                graphRes.Add(node);
            }
            else
            {
                extras.Add(node);
            }
        }

        foreach (Node node in extras)
        {
            if (rand.Next(4) == 0)
            {
                graphRes.Add(node);
            }
        }

        astar = new AStar(l, w);

        foreach (Node node in graphRes)
        {
            setLayer(map, astar.trace(node.Src, node.Dest, getLayer(map, 1)), 1);
        }
    }


    private void Start()
    {
        generateMaze();
        
        walls = new List<GameObject>() { backWall, rightWall, frontWall, leftWall };
        hallWalls = new List<List<GameObject>>() { new List<GameObject>() { frontWall, frontWallLight, frontWallProp1, frontWallProp2 },
                                                   new List<GameObject>() { leftWall, leftWallLight, leftWallProp1, leftWallProp2 },
                                                   new List<GameObject>() { backWall, backWallLight, backWallProp1, backWallProp2 },
                                                   new List<GameObject>() { rightWall, rightWallLight, rightWallProp1, rightWallProp2 } };
        doors = new List<GameObject>() { backDoor, rightDoor, frontDoor, leftDoor };

        instantiateMaze();

        navMeshSurface.BuildNavMesh();

        //GameObject spawnpoint = GameObject.Find("StartPoint");
        //player.transform.position = spawnpoint.transform.position + new Vector3(0f, 0.5f, 0f);
    }


}
