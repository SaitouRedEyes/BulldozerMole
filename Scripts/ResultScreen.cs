using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    private AudioSource myAudio;

    private void Start()
    {
        myAudio = Camera.main.GetComponent<AudioSource>();
        myAudio.Play(0);
    }

    private void OnMouseDown()
    {
        myAudio.Stop();
        SceneManager.LoadScene("StartScreen");
    }
}
