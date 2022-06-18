using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform Target;

    public Camera mainCamera, bigMapCamera;
    private bool bigMapActive;
    // Start is called before the first frame update
    public void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.position.x, Target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!bigMapActive)
            {
                ActivateBigMap();
            }
            else
            {
                DeactivateBigMap();
            }
        }
    }
    public void ChangeTarget(Transform newTarget)
    {
        Target = newTarget;
    }

    public void ActivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = true;
            bigMapCamera.enabled = true;
            mainCamera.enabled = false;

            PlayerController.instance.canMove = false;

            Time.timeScale = 0f;

            UIController.instance.mapDisplay.SetActive(false);
            UIController.instance.bigMapText.SetActive(true);
        }
    }

    public void DeactivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        { 
        bigMapActive = false;
        bigMapCamera.enabled = false;
        mainCamera.enabled = true;

        PlayerController.instance.canMove = true;

        Time.timeScale = 1f;
        UIController.instance.mapDisplay.SetActive(true);
        UIController.instance.bigMapText.SetActive(false);
        }
    }
    public void Zoom(float targetZoom, float zoomRate)
    {
        mainCamera.orthographicSize = Mathf.MoveTowards(mainCamera.orthographicSize, targetZoom, zoomRate * Time.deltaTime);
    }


}

