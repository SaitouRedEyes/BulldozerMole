using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speedY;
    private float rotationSpeed;
    private bool underground;
    private bool down;
    private bool falling;
    private int minersNumber;
    
    Animator animator;

    public Text minersNumbersText;
    public GameObject mainAudio;

    void Start()
    {
        speedY = 1.5f;
        rotationSpeed = 300;
        underground = true;
        down = true;
        falling = false;        
        animator = GetComponent<Animator>();
        animator.SetBool("Underground", true);
        minersNumber = 10;        
    }

    // Update is called once per frame
    void Update()
    {
        underground = CheckUnderground();

        if (underground)
        {
            falling = false;

            animator.SetBool("Underground", true);

            down = DigDownOrUp();

            UndergroundSpeedManager();
            UndergroundMovementManager();
        }
        else
        {
            down = true;

            animator.SetBool("Underground", false);

            SpeedManager();
            MovementManager();
        }

        RotationManager();

        minersNumbersText.text = "Miners Left: " + minersNumber;

        //Debug.Log(down + " " + transform.position.y + " " + speedY + " " + falling);
    }

    private bool CheckUnderground()
    {
        if (transform.position.y < 0) return true;
        else return false;
    }

    private bool DigDownOrUp()
    {
        if (transform.rotation.z < -0.7 || transform.rotation.z > 0.7) return false;        
        else return true;        
    }

    private void UndergroundSpeedManager()
    {
        if (speedY < 20)
        {
            if (down == true) speedY += 0.01f;
            else speedY += 0.1f;
        }
    }

    private void UndergroundMovementManager()
    {
        if (down == true) transform.position -= transform.up * Time.deltaTime * speedY;
        else transform.position += transform.up * Time.deltaTime * (-speedY);
    }

    private void SpeedManager()
    {
        if (speedY > 0 && falling == false) speedY -= 0.3f;
        else
        {
            if (speedY < 20) speedY += 0.1f;

            transform.rotation = new Quaternion(0, 0, 0, 0);            
            falling = true;            
        }        
    }

    private void MovementManager()
    {
        if (falling == true) transform.position -= transform.up * Time.deltaTime * speedY;
        else transform.position += transform.up * Time.deltaTime * (-speedY);
    }

    private void RotationManager()
    {
        if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
        {
            if (underground) transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed, 0);
            else
            {                
                transform.localPosition = new Vector3(transform.localPosition.x + 0.02f,
                                                        transform.localPosition.y,
                                                           transform.localPosition.z);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (underground) transform.Rotate(Vector3.back, Time.deltaTime * rotationSpeed, 0);
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x - 0.02f,
                                                        transform.localPosition.y,
                                                           transform.localPosition.z);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.tag.Equals("LeftWall"))
        {            
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z * -1);
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);            
        }

        if (collision.gameObject.tag.Equals("RightWall"))
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z * -1);
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }

        if (collision.gameObject.tag.Equals("BottomWall"))
        {
            transform.rotation = new Quaternion(0, 0, transform.rotation.z * -1, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }

        if (collision.gameObject.tag.Equals("Rock"))
        {
            mainAudio.GetComponent<AudioSource>().Stop();
            SceneManager.LoadScene("LoseScreen");
        }

        if (collision.gameObject.tag.Equals("Miner"))
        {
            Destroy(collision.gameObject);
            minersNumber--;

            if(minersNumber <= 0) SceneManager.LoadScene("WinScreen");
        }
    }
}










