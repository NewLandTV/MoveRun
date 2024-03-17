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
        TextBarEdit(true, "�����մϴ�!");

        yield return new WaitForSeconds(3f);

        TextBarEdit(true, "������ Ŭ���� �Ͽ����ϴ�!");

        yield return new WaitForSeconds(5f);

        TextBarEdit(true, "�츮�� ���� �ð��� ����� ���Դϴ�.");

        yield return new WaitForSeconds(5f);

        TextBarEdit(true, "�� �������� ��ŵ� ������ �ٲ� �� �ֽ��ϴ�.");

        yield return new WaitForSeconds(7f);

        TextBarEdit(true, "But I don't know what to do with this game right now....");

        yield return new WaitForSeconds(8f);

        TextBarEdit(true, "������ ���� ���� Ȱ���� ������ �ϰڽ��ϴ�.");

        yield return new WaitForSeconds(9f);

        TextBarEdit(true, "�ٽ� �� �� ������ Ŭ���� �Ͻ� ���� �����մϴ�!");

        yield return new WaitForSeconds(11f);

        TextBarEdit(true, "���� ���� ��¥ : 2022�� 3�� 15��");

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
