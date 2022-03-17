using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUp : MonoBehaviour
{
    public Slider slider;

    public void SetMaxPoint(int point)
    {
        slider.maxValue = point;
        slider.value = 0;
    }
    public void SetLevelPoint(int point)
    {
        slider.value = point;
    }

    private void Update()
    {
        if (slider.value == slider.maxValue)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
