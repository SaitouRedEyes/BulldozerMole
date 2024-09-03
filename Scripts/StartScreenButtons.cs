using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenButtons : MonoBehaviour
{
    private AudioSource myAudio;
    void Start()
    {
        myAudio = Camera.main.GetComponent<AudioSource>();
        myAudio.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                myAudio.Stop();

                if (hit.collider.gameObject.tag.Equals("StartButton"))
                {
                    SceneManager.LoadScene("SampleScene");
                }
                
                if(hit.collider.gameObject.tag.Equals("QuitButton"))
                {
                    Application.Quit();
                }
            }
        }
    }    
}
