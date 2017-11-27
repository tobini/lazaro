using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject soundsManager;
    private FloorSoundsManager floorSoundsManager;

    // Use this for initialization
    void Start()
    {
        floorSoundsManager = soundsManager.GetComponent<FloorSoundsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            floorSoundsManager.SetCurrentFloor(gameObject);
        }
    }

}
