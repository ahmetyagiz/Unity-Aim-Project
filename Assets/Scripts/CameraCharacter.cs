using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class CameraCharacter : MonoBehaviour
{
    public float spawnHelper = 4.5f;
    public GameObject objPrefab;
    public float ballForce = 700;
    public float ballCount;
    private Camera cam;
    public float camMove;
    
    public Image cursor;
    public bool playState;
    Rigidbody rb;
    BallManager ballManager;
    public TextMeshProUGUI ballCountText;
    public TextMeshProUGUI mainText;
    public int hitCount;

    void Start()
    {
        ballManager = FindObjectOfType<BallManager>();
        cam = GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;

        playState = true;
        ballCountText.text = ballCount.ToString();
    }

    void Update()
    {
        if (playState == true)
        {
            cursor.transform.position = Input.mousePosition;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Quaternion targetRotation = Quaternion.LookRotation(ray.direction);

            rb.velocity = new Vector3(0, 0, camMove);

            float mousePosx = Input.mousePosition.x;
            float mousePosy = Input.mousePosition.y;
            Vector3 BallInstantiatePoint = cam.ScreenToWorldPoint(new Vector3(mousePosx, mousePosy, cam.nearClipPlane + spawnHelper));

            if (Input.GetMouseButtonDown(0) && ballCount > 0)
            {
                Vector3 targetloc = ray.direction;
                ballCount -= 1;

                GameObject ballRigid;
                ballRigid = Instantiate(objPrefab, BallInstantiatePoint, transform.rotation) as GameObject;
                ballRigid.GetComponent<Rigidbody>().AddForce(targetloc * ballForce);
                ballCountText.text = ballCount.ToString();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            playState = false;

            if (hitCount >= 10)
            {
                mainText.text = "Success!";
                mainText.transform.DOScale(1, 1).SetEase(Ease.OutBack);
            }
            else
            {
                mainText.text = "You Failed!";
                mainText.transform.DOScale(1, 1).SetEase(Ease.OutBack);
            }

            rb.isKinematic = true;
            Invoke(nameof(RestartLevel), 4);
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}