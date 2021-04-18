using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        //transform.position = new Vector3(Mathf.Clamp(player.position.x,diemcuoixtrai,diemcuoixhai), Mathf.Clamp(player.position.y(diemcuoiytrai, diemcuoiyhai),transform.position.z);
    }
}
