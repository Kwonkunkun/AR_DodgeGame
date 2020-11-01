using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//level이 늘어날때마다 enemy가 하나씩 늘어남
//max level은 4
public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    private Joystick joystick;

    [Header("Text")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI LevelText;
    
    [Header("Time")]
    public float currentTime;
    public float nextLevelTime;

    [Header("Level")]
    [SerializeField] private int currentLevel;
    public float maxOfLevel = 4;

    [Header("Enemy")]
    public GameObject[] enemys;

    [Header("ETC")]
    private bool isPlayerDie = false;


    private void Start()
    {
        joystick = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Joystick>();
        joystick.go_Player = GameObject.Find("Player");
        currentTime = nextLevelTime;
    }

    void Update()
    {
        if(!isPlayerDie)
            GoNextLevel();
    }

    #region 싱글톤

    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    #endregion 

    #region 다음레벨로

    private void GoNextLevel()
    {
        //go next Level
        if (currentTime < 0)
        {
            //마지막 단계면 ending
            if (currentLevel == 4)
            {
                Debug.Log("Go Ending");
                SceneManager.LoadScene("EndingScene");
            }

            nextLevelTime += 5f;
            currentTime = nextLevelTime;
            currentLevel++;

            //change text
            LevelText.text = "단계 : " + currentLevel;

            //add enemy
            enemys[currentLevel].SetActive(true);
        }
        currentTime -= Time.deltaTime;

        //change text
        timeText.text = "시간 : " + Mathf.Round(currentTime);
    }

    #endregion

    #region 플레이어가 죽었을때

    public void PlayerDie()
    {
        StartCoroutine("Restart");
    }
    IEnumerator Restart()
    {
        //시간 멈춰놓기
        isPlayerDie = true;

        yield return new WaitForSeconds(3.0f);

        Debug.Log("Restart!!");
        SceneManager.LoadScene("MainScene");
    }

    #endregion
}
