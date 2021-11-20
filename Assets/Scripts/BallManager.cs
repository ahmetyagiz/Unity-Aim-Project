using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallManager : MonoBehaviour
{
    CameraCharacter cameraCharacter;

    private void Start()
    {
        cameraCharacter = FindObjectOfType<CameraCharacter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            GameObject obj = other.gameObject;
            cameraCharacter.hitCount += 1;

            obj.GetComponent<MeshRenderer>().material.color = Color.green;
            obj.transform.DOMoveY(-6, 0.75f).SetEase(Ease.InBack).OnComplete(() =>
            {
                obj.SetActive(false);
            });
        }
    }
}
