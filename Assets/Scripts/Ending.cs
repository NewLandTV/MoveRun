using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject textBarBkg;
    public GameObject restartButtonObject;
    private Text textBarText;

    private void Awake()
    {
        textBarText = textBarBkg.GetComponentInChildren<Text>();
    }

    private IEnumerator Start()
    {
        TextBarEdit(true, "축하합니다!");

        yield return new WaitForSeconds(3f);

        TextBarEdit(true, "게임을 클리어 하였습니다!");

        yield return new WaitForSeconds(5f);

        TextBarEdit(true, "우리는 지난 시간을 기억할 것입니다.");

        yield return new WaitForSeconds(5f);

        TextBarEdit(true, "이 게임으로 당신도 마음이 바뀔 수 있습니다.");

        yield return new WaitForSeconds(7f);

        TextBarEdit(true, "But I don't know what to do with this game right now....");

        yield return new WaitForSeconds(8f);

        TextBarEdit(true, "앞으로 여러 개발 활동을 성실히 하겠습니다.");

        yield return new WaitForSeconds(9f);

        TextBarEdit(true, "다시 한 번 게임을 클리어 하신 것을 축하합니다!");

        yield return new WaitForSeconds(11f);

        TextBarEdit(true, "게임 제작 날짜 : 2022년 3월 15일");

        yield return new WaitForSeconds(5f);

        restartButtonObject.SetActive(true);
    }

    #region System

    private void TextBarEdit(bool active, string text)
    {
        textBarBkg.SetActive(active);
        textBarText.text = text;
    }

    #endregion

    public void Restart_ButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
