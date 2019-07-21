using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {
    [System.Serializable]
    public class Cell
    {
        public bool visited;
        public GameObject north;            //위 1
        public GameObject east;             //오른쪽 2
        public GameObject west;             //왼쪽 3
        public GameObject south;            //아래 4
        public GameObject floor;
    }
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject gift;
    [SerializeField] private GameObject background;
    private List<GameObject> floorList;
    private Cell[] cells;
    private float wallLength;                //벽 길이
    private int currentCell = 0;             //현재 셀 위치
    public GameObject wallHolder;           //빈 오브젝트(모든벽의 부모)

    private Vector3 initialPos;             //초기 벽 위치
    private int mazeLevel = 1;              //게임 난이도
    private int totalCells;                 //전체 셀크기
    private int visitedCells = 0;           //방문할 셀 인덱스
    private bool startedBuilding = false;   //현재 셀이 설치되었는지
    private int currentNeighbor = 0;
    private List<int> lastCells;
    private List<Vector3> playerPosList;
    private int backingUp = 0;
    private int wallToBreak = 0;
    private int playerPos = 0;
    private int giftPos = 0;
    private int mazeNum = 0;
    
    public int xSize = 0;                  //x 길이
    public int ySize = 0;                  //y 길이

    void Start () {
        wallLength = 1.2f;
        playerPosList = new List<Vector3>();
        SetLevel();
        for (int i = 0; i < mazeNum; i++)
        {
            CreateWalls(i);
            SetPlayerPos(i);
        }
        MazeSanta.instance.InputPlayerPos(playerPosList);
    }

    void CreateWalls(int mazeNum)
    {
        floorList = new List<GameObject>();
        floorList.Clear();
        //maze라는 오브젝트 하나로 생성시킴
        //벽은 모든 자식으로 생성
        wallHolder = new GameObject();
        wallHolder.name = "Maze";
        //초기 위치를 정중앙으로 초기화
        initialPos = new Vector3((-xSize / 2) + wallLength / 2 + mazeNum * 20, 0, (-ySize / 2) + wallLength / 2 - 0.3f);
        Instantiate(background, initialPos, Quaternion.identity);
        MazeTrees.instance.CreateTrees(initialPos.x, initialPos.y);
        Vector3 myPos = initialPos;
        GameObject tmpWall;
        //x 벽 생성
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                //위치 지정
                myPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2, 0, initialPos.z + (i * wallLength) - wallLength / 2);
                // 벽 생성
                tmpWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
                //모든 벽을 wallholder의 자식으로 생성
                tmpWall.transform.parent = wallHolder.transform;
            }
        }
        //y 벽 생성
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                //위치 지정
                myPos = new Vector3(initialPos.x + (j * wallLength), 0, initialPos.z + (i * wallLength) - wallLength);
                //벽 생성
                tmpWall = Instantiate(wall, myPos, Quaternion.Euler(0, 90, 0)) as GameObject;
                //모든 벽을 wallholder의 자식으로 생성
                tmpWall.transform.parent = wallHolder.transform;
            }
        }

        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                //위치 지정
                myPos = new Vector3(initialPos.x + (j * wallLength), 0, initialPos.z + (i * wallLength) - wallLength / 2 + 0.0001f);
                //벽 생성
                tmpWall = Instantiate(floor, myPos, Quaternion.Euler(0, 0, 90)) as GameObject;
                //모든 벽을 wallholder의 자식으로 생성
                floorList.Add(tmpWall);
            }
        }
        CreateCell();
    }

    //하나의 방을 셀로 만들어준다
    void CreateCell()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        totalCells = xSize * ySize;
        GameObject[] allWalls;
        int children = wallHolder.transform.childCount;
        allWalls = new GameObject[children];
        cells = null;
        cells = new Cell[xSize * ySize];            //방크기 만큼 설정
        int eastWestProcess = 0;                    //현재 좌우벽 위치저장변수
        int childProcess = 0;
        int termCount = 0;
        //모든 벽 가져오기
        for (int i = 0; i < children; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }
        for (int cellprocess = 0; cellprocess < cells.Length; cellprocess++)
        {
            // 제일 왼쪽 셀일때
            if (termCount == xSize)
            {
                //위쪽 제일 오른쪽 벽으로 인덱스 수정
                eastWestProcess++;
                termCount = 0;
            }
            cells[cellprocess] = new Cell();
            cells[cellprocess].east = allWalls[eastWestProcess];
            cells[cellprocess].south = allWalls[childProcess + (xSize + 1) * ySize];
            eastWestProcess++;
            termCount++;
            childProcess++;
            cells[cellprocess].west = allWalls[eastWestProcess];
            cells[cellprocess].north = allWalls[(childProcess + (xSize + 1) * ySize) + xSize - 1];

            cells[cellprocess].floor = floorList[cellprocess];
        }
        CreateMaze();
    }
    //DFS를 이용함
    void CreateMaze()
    {
        while(visitedCells < totalCells)
        {
            if(startedBuilding)
            {
                GiveMeNeighbor();
                //옆 방이 선택 되지 않은 상황이면
                if (cells[currentNeighbor].visited == false && cells[currentCell].visited == true) 
                {
                    BreakWall();
                    cells[currentNeighbor].visited = true;
                    visitedCells++;
                    lastCells.Add(currentCell);
                    //현재 위치를 이웃 위치로 바꾼다.
                    currentCell = currentNeighbor;
                    if(lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }
                }
            }
            else
            {
                currentCell = xSize/2;
                cells[currentCell].visited = true;
                Destroy(cells[currentCell].north);
                currentCell = xSize * ySize - xSize / 2;
                cells[currentCell].visited = true;
                visitedCells+=2;
                startedBuilding = true;
            }
        }
        //다음 미궁때문에 초기화
        currentCell = 0;
        currentNeighbor = 0;
        visitedCells = 0;
        startedBuilding = false;
        backingUp = 0;
    }

    void GiveMeNeighbor()
    {
        int length = 0;
        int[] neighbors = new int[4];
        int check = 0;
        int[] connectingWall = new int[4];
        check = (currentCell + 1) / xSize;
        check -= 1;
        check *= xSize;
        check += xSize;
        //check 가 이 공식 지나 치면 제일 오른쪽일 경우 빼고 currentCell + 1과 check는 같지 않는다
        //east
        if (currentCell + 1 < totalCells && (currentCell + 1) != check) 
        {
            if(cells[currentCell + 1].visited == false)
            {
                neighbors[length] = currentCell + 1;
                connectingWall[length] = 2;
                length++;
            }
        }
        //west
        if (currentCell - 1 >= 0 && currentCell != check)
        {
            if (cells[currentCell - 1].visited == false)
            {
                neighbors[length] = currentCell - 1;
                connectingWall[length] = 3;
                length++;
            }
        }

        //north
        if (currentCell + xSize < totalCells)
        {
            if (cells[currentCell + xSize].visited == false)
            {
                neighbors[length] = currentCell + xSize;
                connectingWall[length] = 1;
                length++;
            }
        }
        //south
        if (currentCell - xSize >= 0)
        {
            if (cells[currentCell - xSize].visited == false)
            {
                neighbors[length] = currentCell - xSize;
                connectingWall[length] = 4;
                length++;
            }
        }
        if (length !=0)
        {
            int theChoseOne = Random.Range(0, length);
            currentNeighbor = neighbors[theChoseOne];
            wallToBreak = connectingWall[theChoseOne];
        }
        else
        {
            if(backingUp > 0)
            {
                currentCell = lastCells[backingUp];
                backingUp--;
            }
        }
    }

    void BreakWall()
    {
        switch (wallToBreak)
        {
            case 1:
                Destroy(cells[currentCell].north);
                break;
            case 2:
                Destroy(cells[currentCell].west);
                break;
            case 3:
                Destroy(cells[currentCell].east);
                break;
            case 4:
                Destroy(cells[currentCell].south);
                break;
        }
    }
    //산타, 선물 위치 설정
    void SetPlayerPos(int checkMazeNum)
    {
        playerPos = Random.Range(0, 4);
        switch (playerPos)
        {
            case 0:
                playerObject.transform.position = new Vector3(
                    CenterPos(cells[0].east.transform.position.x, cells[0].west.transform.position.x),
                    0,
                   CenterPos(cells[0].north.transform.position.z, cells[0].south.transform.position.z));
                gift.transform.position = new Vector3(
                    CenterPos(cells[xSize * ySize - 1].east.transform.position.x, cells[xSize * ySize - 1].west.transform.position.x),
                    0,
                   CenterPos(cells[xSize * ySize - 1].north.transform.position.z, cells[xSize * ySize - 1].south.transform.position.z));
                playerPos = 0;
                giftPos = xSize * ySize - 1;
                break;
            case 1:
                playerObject.transform.position = new Vector3(
                    CenterPos(cells[xSize - 1].east.transform.position.x, cells[xSize - 1].west.transform.position.x),
                    0,
                   CenterPos(cells[xSize - 1].north.transform.position.z, cells[xSize - 1].south.transform.position.z));
                gift.transform.position = new Vector3(
                   CenterPos(cells[xSize * (ySize - 1)].east.transform.position.x, cells[xSize * (ySize - 1)].west.transform.position.x),
                   0,
                  CenterPos(cells[xSize * (ySize - 1)].north.transform.position.z, cells[xSize * (ySize - 1)].south.transform.position.z));
                playerPos = xSize - 1;
                giftPos = xSize * (ySize - 1);
                break;
            case 2:
                playerObject.transform.position = new Vector3(
                    CenterPos(cells[xSize * (ySize - 1)].east.transform.position.x, cells[xSize * (ySize - 1)].west.transform.position.x),
                    0,
                   CenterPos(cells[xSize * (ySize - 1)].north.transform.position.z, cells[xSize * (ySize - 1)].south.transform.position.z));
                gift.transform.position = new Vector3(
                   CenterPos(cells[xSize - 1].east.transform.position.x, cells[xSize - 1].west.transform.position.x),
                   0,
                  CenterPos(cells[xSize - 1].north.transform.position.z, cells[xSize - 1].south.transform.position.z));
                playerPos = xSize * (ySize - 1);
                giftPos = xSize - 1;
                break;
            case 3:
                playerObject.transform.position = new Vector3(
                    CenterPos(cells[xSize * ySize - 1].east.transform.position.x, cells[xSize * ySize - 1].west.transform.position.x),
                    0,
                   CenterPos(cells[xSize * ySize - 1].north.transform.position.z, cells[xSize * ySize - 1].south.transform.position.z));
                gift.transform.position = new Vector3(
                   CenterPos(cells[0].east.transform.position.x, cells[0].west.transform.position.x),
                   0,
                  CenterPos(cells[0].north.transform.position.z, cells[0].south.transform.position.z));
                playerPos = xSize * ySize - 1;
                giftPos = 0;
                break;
        }
        if (checkMazeNum == 0) Instantiate(playerObject);
        playerPosList.Add(playerObject.transform.position);
        Instantiate(gift);
    }
    float CenterPos(float pos1, float pos2)
    {
        float center = (pos1 + pos2) / 2;
        return center;
    }
    //레벨에 따른 가로세로 설정
    void SetLevel()
    {
        mazeLevel = MazeUIManager.instance.gameLevel;
        mazeNum = MazeUIManager.instance.totalGiftNum;
        xSize = (mazeLevel - 1) / 3 + 4;
        ySize = (mazeLevel - 1) / 3 + 4;
        CameraSet();
    }
    //라운드별 카메라 움직이기
    void CameraSet()
    {
        if(mazeLevel <4)
        {
            Camera.main.transform.position = new Vector3(0.2f, 5, -0.5f);
        }
        else if(mazeLevel >=4 && mazeLevel <7)
        {
            Camera.main.transform.position = new Vector3(1, 6, 0.1f);
        }
        else if(mazeLevel >=7 && mazeLevel <10)
        {
            Camera.main.transform.position = new Vector3(0.2f, 7, -0.3f);
        }
        else if(mazeLevel >=10 && mazeLevel <13)
        {
            Camera.main.transform.position = new Vector3(0.8f, 8, 0.3f);
        }
        else
        {
            Camera.main.transform.position = new Vector3(0.3f, 9, -0.1f);
        }
    }
}
