using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Button_Skip : MonoBehaviour
{

    public GameObject ButtonSkip;
    public GameObject Black;

    public bool isSkip = false;
    public bool isEnd = false;

    public VideoPlayer vid;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(SkipButton()); 
        //ButtonSkip.SetActive(false);

        Black.SetActive(false);

        vid.loopPointReached += vidOver;

       
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSkip == true && Input.anyKey)
        {
            SceneManager.LoadScene("LV1");
        }
        
    }

    private IEnumerator SkipButton()
    {
        yield return new WaitForSeconds(0f);
        ButtonSkip.SetActive(true);
        isSkip = true;   
        
    }

    void vidOver(VideoPlayer vid)
    {
        Black.SetActive(true);
    }
}
