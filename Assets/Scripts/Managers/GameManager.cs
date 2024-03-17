using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Rigidbody2D greenZone;

    public Text hpTxt;
    public Text lifeTimeTxt;
    public GameObject BkgGroup;

    public Player player;

    private Vector3 greenZoneDirection;

    public bool isGameStart;

    public int hour;
    public int minute;
    private float second;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 0f;

        second = 0f;

        if (instance == null)
        {
            instance = this;
        }
    }

    private IEnumerator Start()
    {
        StartCoroutine(GreenZoneControl());

        while (true)
        {
            TimeControl();

            greenZone.AddForce(greenZoneDirection, ForceMode2D.Impulse);

            hpTxt.text = string.Format("HP : {0}", player.HP);
            lifeTimeTxt.text = string.Format("{0:00}:{1:00}:{2:00}", hour, minute, second);

            yield return null;
        }
    }

    private IEnumerator GreenZoneControl()
    {
        while (true)
        {
            if (isGameStart)
            {
                if (Random.Range(0, 1025) % 2 == 1)
                {
                    greenZoneDirection = Vector3.zero;
                    greenZone.transform.position = Vector3.right * Random.Range(0, 5) + Vector3.up * Random.Range(0, 5);

                    yield return new WaitForSeconds(1.5f);
                }
                else
                {
                    greenZoneDirection = Vector3.right * Random.Range(-0.25f, 0.25f) + Vector3.up * Random.Range(-0.25f, 0.25f);
                }

                yield return new WaitForSeconds(0.25f);

                greenZone.gameObject.SetActive(Random.Range(0, 2) == 0 ? true : false);
            }

            yield return null;
        }
    }

    private void TimeControl()
    {
        second += Time.deltaTime;

        if (minute >= 60)
        {
            minute = 0;
            hour += 1;
        }
        if (second >= 60f)
        {
            second = 0f;
            minute += 1;

            if (minute >= 60)
            {
                minute = 0;
                hour += 1;
            }
        }

        // 1시 11분 11초가 지나면 승리
        if (hour >= 1 && minute >= 11 && second >= 11)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void StartButtonClick()
    {
        BkgGroup.SetActive(false);

        Time.timeScale = 1f;
        isGameStart = true;
    }
}
