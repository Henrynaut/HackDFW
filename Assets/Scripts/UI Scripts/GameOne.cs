using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOne : MonoBehaviour
{
    public void LoadGameOne()
    {
        SceneManager.LoadScene("SimpleAR", LoadSceneMode.Additive);
    }
}
