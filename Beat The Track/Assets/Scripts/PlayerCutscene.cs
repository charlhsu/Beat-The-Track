using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutscene : MonoBehaviour
{

    private bool cutsceneActive = false;
    public float distanceToCross;
    public float waitingTime;
    public float CutsceneMoveSpeed;
    private Vector3 direction = new Vector3(0, 1, 0);
    private Vector3 startPoint;
    private 


    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Player can't control character during cutscene
        if (cutsceneActive)
        {
            PlayerController.instance.canMove = false;
            PlayerController.instance.transform.position += CutsceneMoveSpeed * direction;
           /* if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) >= distanceToCross)
            {
                cutsceneActive = false;
                PlayerController.instance.canMove = true;
            }*/
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            cutsceneActive = true;
        }
    }


}
