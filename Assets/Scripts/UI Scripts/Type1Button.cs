using UnityEngine;
using UnityEngine.SceneManagement;

public class Type1Button : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadType1Game()
    {
        SceneManager.LoadScene("Type 1 Game Instructions");
    }
}
