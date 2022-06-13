using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public float waitToBeCollected = 0.5f;
    public Gun theGun;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Player" && waitToBeCollected <= 0)
        {
            bool hasGun = false;
            foreach(Gun gunToCheck in PlayerController.instance.avaiableGuns)
            {
      
                if(theGun.weaponName == gunToCheck.weaponName)
                {
                    hasGun = true;
                }
            }

            if (!hasGun)
            {
                Gun gunClone = Instantiate(theGun);
                gunClone.transform.parent = PlayerController.instance.instrumentArm;
                gunClone.transform.position = PlayerController.instance.instrumentArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;

                PlayerController.instance.avaiableGuns.Add(gunClone);

                PlayerController.instance.currentGun = PlayerController.instance.avaiableGuns.Count -1;
                PlayerController.instance.SwitchGun();

            }

            Destroy(gameObject);
            AudioManager.instance.PlaySFX(7);

        }
    }

}
