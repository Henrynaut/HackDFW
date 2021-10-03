using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float fillSpeed = 0.5f;
    private float targetProgress = 0;
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
        if(slider.value >= 10)
        {
            //End Game
            
            SceneManager.LoadScene("Type 1 Game Instructions");
            slider.value = 0;
        }

    }
    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
}
