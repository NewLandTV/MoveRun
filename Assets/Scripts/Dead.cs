using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    public void Button_ReStartButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
