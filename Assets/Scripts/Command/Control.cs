using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public Transform playerTranform;
    Command btnW,btnA,btnS,btnD;
    // Start is called before the first frame update
    void Start()
    {
        btnW = new MoveForward();
        btnA = new MoveLeft();
        btnS = new MoveBack();
        btnD = new MoveRight();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
         if(Input.GetKey(KeyCode.W)){
             btnW.Execute(playerTranform);   
         }else if(Input.GetKey(KeyCode.A)){  //horizon, verticle
             btnA.Execute(playerTranform);
         }else if(Input.GetKey(KeyCode.S)){
             btnS.Execute(playerTranform);
         }else if(Input.GetKey(KeyCode.D)){
             btnD.Execute(playerTranform);
         }
    }
}
