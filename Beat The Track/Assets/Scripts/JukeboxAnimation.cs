using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeboxAnimation : MonoBehaviour
{
    public static JukeboxAnimation instance;
    //Gestion du sprite du jukebox
    public SpriteRenderer jukeboxSR;
    public Animator anim;

    //Gestion des disques
    public float noShootRS = 100f;
    public float shootRS = 700f;
    [HideInInspector]
    public bool meleeFiring;
    [HideInInspector]
    public bool distanceFiring;

    //Stockage des disques
    public Disk[] currentDisks = new Disk[5];
    [HideInInspector]
    private Camera theCam;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        Debug.Log("pipi = caca");
    }

    // Update is called once per frame
    void Update()
    {
        //private Vector3 screenPoint = theCam.WorldToScreenPoint(temoin.transform.localPosition);

        //disque 1 (distance)
        if (currentDisks[0] != null)
        {
            if (Input.GetMouseButton(0))
            {
                anim.SetBool("isFiring", true);
                RotateDisk(0, shootRS);
            }
            else if(!Input.GetMouseButton(1)&& currentDisks[1] != null)
            {
                anim.SetBool("isFiring", false);
                RotateDisk(0, noShootRS);


            }
            else
            {
                RotateDisk(0, noShootRS);
            }
        }
        //disque 2 (melee)
        if (currentDisks[1] != null)
        {
            if (Input.GetMouseButton(1))
            {
                anim.SetBool("isFiring", true);
                RotateDisk(1, shootRS);
            }
            else if(!Input.GetMouseButton(0) && currentDisks[0] != null)
            {
                anim.SetBool("isFiring", false);
                RotateDisk(1, noShootRS);

            }
            else
            {
                RotateDisk(1, noShootRS);
            }
          
        }

        /*if(PlayerController.instance.moveInput.y == -1)
        {
            jukeboxSR.enabled = false;
            diskSR.enabled = false;
        }
        if(PlayerController.instance.moveInput.y == 1)
        {
            jukeboxSR.enabled = true;
            diskSR.enabled = true;
        }*/
    }
    public void RotateDisk(int diskSelected, float RS)
    {
        currentDisks[diskSelected].transform.Rotate(new Vector3(0, 0, Time.deltaTime * RS));
    }
}
