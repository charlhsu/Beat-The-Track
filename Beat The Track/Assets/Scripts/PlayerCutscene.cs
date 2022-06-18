using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutscene : MonoBehaviour
{

    private bool shouldWalk = false;
    public float distanceToCross;
    public float waitingTime;

    private Vector2 direction = new Vector2(0, 1);
    private Vector3 startPoint;
    private bool idle;
    private bool launchCamMove = false;


    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Player can't control character during cutscene
        if (shouldWalk)
        {
            CameraController.instance.Zoom(2, 10);
            PlayerController.instance.ForceSortBodyElement("back");
            PlayerController.instance.canMove = false;
            PlayerController.instance.theRB.velocity = PlayerController.instance.moveSpeed * direction;
            PlayerController.instance.anim.SetBool("isMoving", true);
            PlayerController.instance.anim.SetBool("isFront", false);
            if (PlayerController.instance.transform.position.y - transform.position.y  >= distanceToCross)
            {
                shouldWalk = false;
                

                PlayerController.instance.anim.SetBool("isFront", true);
                
                PlayerController.instance.ForceSortBodyElement("front");
                idle = true;
            }
            if (idle)
            {
                StartCoroutine(WaitCamMove());
                Debug.Log(launchCamMove);
                if (launchCamMove)
                {
                    CameraController.instance.Zoom(8, 4);
                }
                if (CameraController.instance.mainCamera.orthographicSize >= 8)
                {
                    StartCoroutine(WaitZoom());
                }
              
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shouldWalk = true;
        }
    }

    IEnumerator WaitCamMove()
    {
        
        yield return new WaitForSeconds(1);
        Debug.Log("turlututu chapo pointu");
        launchCamMove = true;

    }
    IEnumerator WaitZoom()
    {
        yield return new WaitForSeconds(1);
        idle = false;
        PlayerController.instance.canMove = true;
            
    }

    

}
