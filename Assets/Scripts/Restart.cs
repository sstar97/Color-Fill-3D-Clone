using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] Text txt;
    [SerializeField] GameObject GameOver;
    float timer = 10;
    void Start()
    {
        
    }
    private void Update()
    {
        if (GameOver.activeInHierarchy == true)
        {
            timer -= Time.deltaTime;
            txt.text = Mathf.RoundToInt(timer).ToString();
            if (timer <= 0)
                SceneManager.LoadScene(0);
        }
    }

    public void res()
    {
        SceneManager.LoadScene(0);
    }
}
