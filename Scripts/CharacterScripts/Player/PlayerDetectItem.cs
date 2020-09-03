using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectItem : MonoBehaviour
{

    public Camera playerCamera;
    RaycastHit HitInfo;
    public float detectionRange = 100;
    public ItemManager iManager;
    public PlayerInventory pInventory;
    // Start is called before the first frame update
    void Start()
    {
        pInventory = GetComponent<PlayerInventory>();
        iManager = GameObject.Find("GameManager").GetComponent<ItemManager>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out HitInfo, detectionRange))
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * detectionRange, Color.yellow);
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (HitInfo.transform.tag)
                {
                    case "item":
                        Item itemholder = iManager.GetItem(HitInfo.transform.gameObject.name);
                        if (itemholder != null)
                        {
                            pInventory.GiveItem(HitInfo.transform.gameObject.name);
                            Destroy(HitInfo.transform.gameObject);
                        }
                        break;
                    case "car":
                        Debug.Log("Enter car");
                        break;
                    default:
                        return;
                }

            }
        }
    }
}
