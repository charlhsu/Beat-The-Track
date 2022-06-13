using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gramophone : MonoBehaviour
{
    public Disk theDisk;
    public SpriteRenderer gramophoneSR;
    public Sprite emptyGramophone;
    public Animator gramoAnim;
    private bool canTakeDisk = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canTakeDisk)
        {
            //Association du disque au jukebox
            Disk diskClone = Instantiate(theDisk);
            diskClone.SetType();
            Debug.Log("slot number = "+diskClone.actualSlotNumber.ToString());
            //-0.05 -0.43
            diskClone.transform.parent = PlayerController.instance.slots[diskClone.actualSlotNumber];
            diskClone.transform.position = PlayerController.instance.slots[diskClone.actualSlotNumber].position;
            diskClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
            diskClone.transform.localScale = new Vector3(3.1f, 3.1f, 1f);

            JukeboxAnimation.instance.currentDisks[diskClone.actualSlotNumber] = diskClone;
            PlayerController.instance.disksSR[diskClone.actualSlotNumber] = diskClone.GetComponent<SpriteRenderer>();

            //PlayerController.instance.avaiableGuns.Add(gunClone);

            //PlayerController.instance.currentGun = PlayerController.instance.avaiableGuns.Count - 1;
            //PlayerController.instance.SwitchGun();


            //Association de l'instrument lié au disque au joeuur
            Instrument instrumentClone = Instantiate(diskClone.instrumentLinked);
            instrumentClone.transform.parent = PlayerController.instance.instrumentArm.transform;
            instrumentClone.transform.position = PlayerController.instance.instrumentArm.position;
            instrumentClone.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            diskClone.instrumentLinked = instrumentClone;//Should modifie this with a proper variable for the clone and not adding the clone to the attribute where the prefab was



            //PlayerController.instance.instrumentArm = diskClone.instrumentLinked.transform;

            //Désactivation des mains vides
            foreach (SpriteRenderer hand in PlayerController.instance.handsSR)
            {
                hand.enabled = false;
            }

            gramophoneSR.sprite = emptyGramophone;
            AudioManager.instance.PlaySFX(7);

            gramoAnim.enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canTakeDisk = true;
        }
       
    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            canTakeDisk = false;
        }

    }
}
