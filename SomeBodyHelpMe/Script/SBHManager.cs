using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBHManager : MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject civilianPrefab;
    [SerializeField] GameObject positions;

    private List<bool> enemyPositionTable;
    private List<bool> civilianPositionTable;
    private List<bool> reservationEnemy;

    private bool isGameOver = false;

    private int positionsCount;
    private int difficultyLevle;
    private int killedEnemy;
    private int killedCivilian;
    private int headShot;
    private int leftBullet = 15;
    private int leftEnemy;
    private int leftTime = 10;
    private int score;
    private int randomAimedAvoidPercent = 40;

    private float enemyUpDownSpeed = 1.0f;
    private float enemySpeed = 1.0f;
    private float enemyWarigariSpeedPercent = 1.0f;

    private Vector3 civilianRevisonPosition;
    private Vector3 enemyRevisonPosition;

    private SBHUIManager sbhUIManger;

    public static SBHManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    void Start () {
        if(NewGameManager.Instance != null)
        {
            GameObject tutorialCanvas = GameObject.Find("TutorialCanvas");
            if (tutorialCanvas != null) // 튜토리얼
            {
                NewGameManager.Instance.StartGame();
                difficultyLevle = 1;
            }
            else // 본 미니게임
            {
                difficultyLevle = NewGameManager.Instance.StartGame();
            }

        } else
        {
            difficultyLevle = 8;
        }

        sbhUIManger = this.GetComponent<SBHUIManager>();
        

        killedEnemy = 0;
        killedCivilian = 0;

        civilianRevisonPosition = new Vector3(0.5f, 0.05f, 0);
        enemyRevisonPosition = new Vector3(0.2f, 0, 0);

        positionsCount = positions.transform.childCount;

        enemyPositionTable = new List<bool>();
        civilianPositionTable = new List<bool>();
        reservationEnemy = new List<bool>();

        for (int i = 0; i < positionsCount; i++)
        {
            enemyPositionTable.Add(true);
            civilianPositionTable.Add(true);
        }

        DifficultyLevelAdjust();

        sbhUIManger.RefreshLeftBullet(leftBullet);
        sbhUIManger.RefreshLeftTime(leftTime);
        sbhUIManger.RefreshLeftEnemy(leftEnemy + reservationEnemy.Count);

        StartCoroutine(Timer());
    }

    public int LeftBullet {
        get { return leftBullet; }
        set { leftBullet = value; }
    }

    public bool IsShoot()
    {
        if (leftBullet > 0)
            return true;
        else
            return false;
    }

    public void Shoot()
    {
        if (!isGameOver)
        {
            leftBullet -= 1;
            sbhUIManger.RefreshLeftBullet(leftBullet);
            if (LeftBullet == 0) GameOver();
            SBHSoundManger.Instance.PlayShootAudio();
            sbhUIManger.MuzzleFlash();
        }
    }

    public void KillCivilian()
    {
        //score -= 1;
        //sbhUIManger.RefreshNowScore(score);
        killedCivilian += 1;
        GameOver();
    }

    public void KillEnemy()
    {
        killedEnemy += 1;
        leftEnemy -= 1;
        score += 1;
        sbhUIManger.RefreshNowScore(score);
        sbhUIManger.RefreshLeftEnemy(leftEnemy + reservationEnemy.Count);

        //ExecuteResrvationCreateEnemy();

        if (leftEnemy == 0) GameOver();
    }

    public void KillEnmeyHead()
    {
        headShot += 1;
        KillEnemy();
        sbhUIManger.ActiveHeadShotText();
    }

    private void ReservationCreateEnemy(bool isMoveable)
    {
        reservationEnemy.Add(isMoveable);
    }

    private void ExecuteResrvationCreateEnemy()
    {
        if(reservationEnemy.Count > 0 && leftEnemy < 3)
        {
            int _index = reservationEnemy.Count - 1;
            CreateEnemy(reservationEnemy[_index]);
            reservationEnemy.RemoveAt(_index);
        }
    }

    private void CreateEnemy(bool isMoveable)
    {
        int _randNum;
        bool _isCreate = false;
        do
        {
            _randNum = Random.Range(0, positionsCount);
            if (enemyPositionTable[_randNum] == true)
            {
                enemyPositionTable[_randNum] = false;
                _isCreate = true;
            }
        }
        while (!_isCreate);

        GameObject tempObject = Instantiate(enemyPrefab, positions.transform.GetChild(_randNum).position + enemyRevisonPosition, Quaternion.identity);
        tempObject.GetComponent<SBHEnemyControl>().AllocationMovePositionIndex(_randNum);
        tempObject.GetComponent<SBHEnemyControl>().UpDownSpeedUp(enemyUpDownSpeed);
        tempObject.GetComponent<SBHEnemyControl>().SpeedUp(enemySpeed);
        tempObject.GetComponent<SBHEnemyControl>().WarigariSpeedPercent = enemyWarigariSpeedPercent;

        if (isMoveable)
        {
            tempObject.GetComponent<SBHEnemyControl>().AllowMove();
        }
        leftEnemy += 1;
    }

    private void CreateCivilian(bool isKidnap) // isKidnap은 인질범과 겹치는 것을 표시
    {
        int _randNum;
        bool _isCreate = false;
        do
        {
            _randNum = Random.Range(0, positionsCount);
            if(civilianPositionTable[_randNum] == true)
            {
                if (isKidnap)
                {
                    if (enemyPositionTable[_randNum] == false) // 인질범이 있는 곳에 생성
                    {
                        civilianPositionTable[_randNum] = false;
                        _isCreate = true;
                    }
                }
                else
                {
                    if (enemyPositionTable[_randNum] == true)
                    {
                        civilianPositionTable[_randNum] = false;
                        _isCreate = true;
                    }
                }
            }
        }
        while (!_isCreate);

        Instantiate(civilianPrefab, positions.transform.GetChild(_randNum).position - civilianRevisonPosition, Quaternion.identity);
    }

    private IEnumerator Timer()
    {
        while (leftTime > 0)
        {
            yield return new WaitForSeconds(1f);
            if (!isGameOver)
            {
                leftTime -= 1;
                sbhUIManger.RefreshLeftTime(leftTime);
            } else
            {
                break;
            }
        }
        GameOver();
        
    }

    private void GameOver()
    {
        if (!isGameOver)
        {
            Result _result = GameResult();
            isGameOver = true;
            sbhUIManger.ActiveGameOver(_result);
            if (NewGameManager.Instance != null)
            {
                NewGameManager.Instance.ClearGame(_result);
            }
            else
            {
                Debug.Log("NewGameManager is null");
            }
        }
    }

    public int AllocationMovePosition(int index)
    {
        if(index < enemyPositionTable.Count - 1)
        {
            if (enemyPositionTable[index + 1])
            {
                enemyPositionTable[index] = true;
                enemyPositionTable[index + 1] = false;
                return index + 1;
            }
        }
        if (index > 0)
        {
            if (enemyPositionTable[index - 1])
            {
                enemyPositionTable[index] = true;
                enemyPositionTable[index - 1] = false;
                return index - 1;
            }
        }

        return index;
    }

    public Vector3 IndexToFindPosition(int index, bool isEnemy)
    {
        Vector3 _result = Vector3.zero;

        if (isEnemy) _result = enemyRevisonPosition;
        else _result = civilianRevisonPosition;

        return positions.transform.GetChild(index).position + _result;
    }

    public void FreeEnemyPositionIndex(int index)
    {
        enemyPositionTable[index] = true;
    }

    private void DifficultyLevelAdjust()
    {
        switch (difficultyLevle)
        {
            case 1:
                CreateEnemy(false);
                CreateCivilian(false);
                break;
            case 2:
                CreateEnemy(false);
                CreateEnemy(false);
                CreateCivilian(false);
                break;
            case 3:
                CreateEnemy(false);
                CreateEnemy(false);
                CreateCivilian(false);
                break;
            case 4:
                CreateEnemy(false);
                CreateEnemy(false);
                CreateEnemy(false);
                CreateCivilian(false);
                break;
            case 5:
                CreateEnemy(false);
                CreateEnemy(false);
                CreateEnemy(true);
                CreateCivilian(false);
                break;
            case 6:
                CreateEnemy(false);
                CreateEnemy(false);
                CreateEnemy(false);
                CreateEnemy(true);
                CreateCivilian(true);
                break;
            case 7:
                enemyWarigariSpeedPercent = 0.9f;
                enemyUpDownSpeed += 0.3f;
                enemySpeed += 0.2f;
                CreateEnemy(false);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateCivilian(true);
                CreateCivilian(true);
                break;
            case 8:
                enemyWarigariSpeedPercent = 0.8f;
                enemyUpDownSpeed += 0.4f;
                enemySpeed += 0.3f;
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                randomAimedAvoidPercent += 10;
                break;
            case 9:
                enemyWarigariSpeedPercent = 0.7f;
                enemyUpDownSpeed += 0.6f;
                enemySpeed += 0.5f;
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                randomAimedAvoidPercent += 10;
                break;
            case 10:
                enemyWarigariSpeedPercent = 0.6f;
                enemyUpDownSpeed += 1.0f;
                enemySpeed += 1.0f;
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                randomAimedAvoidPercent += 20;
                break;
            case 11:
                enemyWarigariSpeedPercent = 0.5f;
                enemyUpDownSpeed += 1.2f;
                enemySpeed += 1.0f;
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                randomAimedAvoidPercent += 30;
                break;
            case 12:
                enemyWarigariSpeedPercent = 0.4f;
                enemyUpDownSpeed += 1.4f;
                enemySpeed += 1.2f;
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                randomAimedAvoidPercent += 30;
                break;
            case 13:
                enemyWarigariSpeedPercent = 0.4f;
                enemyUpDownSpeed += 1.6f;
                enemySpeed += 1.4f;
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                randomAimedAvoidPercent += 40;
                break;
            case 14:
                enemyWarigariSpeedPercent = 0.3f;
                enemyUpDownSpeed += 1.6f;
                enemySpeed += 1.6f;
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                randomAimedAvoidPercent += 40;
                break;
            case 15:
                enemyWarigariSpeedPercent = 0.3f;
                enemyUpDownSpeed += 1.6f;
                enemySpeed += 2.0f;
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateEnemy(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                CreateCivilian(true);
                randomAimedAvoidPercent += 50;
                break;
        }
    }

    private Result GameResult()
    {
        List<Result> _resultList = new List<Result>();
        _resultList.Add(Result.Fail);
        _resultList.Add(Result.Successful);
        _resultList.Add(Result.BigSuccessful);

        int _index = 2;
        
        if (killedCivilian > 0) _index = 0;
        else if (leftEnemy > 0)
        {
            _index = 0;
        }
        else if (leftTime == 0)
        {
            _index = 0;
        }
        else if (leftEnemy == 0)
        {
            float _value = headShot;
            _value = _value / (leftEnemy + killedEnemy);

            if(_value >= 0.4f)
            {
                _index = 2;
            }
            else
            {
                _index = 1;
            }
        }

        //switch (difficultyLevle)
        //{
        //    case 1:
        //        if(score < 1)
        //        {
        //            _index -= 1;
        //        }
        //        if(headShot < 1)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 2:
        //        if (score < 2)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 1)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 3:
        //        if (score < 2)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 1)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 4:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 1)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 5:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 1)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 6:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 1)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 7:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 2)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 8:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 2)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 9:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 3)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 10:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 3)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 11:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 3)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 12:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 3)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 13:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 3)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 14:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 3)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //    case 15:
        //        if (score < 3)
        //        {
        //            _index -= 1;
        //        }
        //        if (headShot < 3)
        //        {
        //            _index -= 1;
        //        }
        //        break;
        //}

        if (_index < 0) _index = 0;
        return _resultList[_index];
    }

    public int RandomAimedAvoidPercent {
        get { return randomAimedAvoidPercent; }
    }
}
