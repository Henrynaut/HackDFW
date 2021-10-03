using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
}
