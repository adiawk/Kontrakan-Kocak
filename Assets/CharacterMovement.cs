using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public bool isMoveable;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform visualDirection;

    
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Move(horizontalInput);
    }

    void Move(float param)
    {
        if (!isMoveable)
            return;

        Vector2 moveDir = new Vector2(param, 0) * moveSpeed * Time.deltaTime;

        this.transform.Translate(moveDir);

        if(param > 0)
        {
            visualDirection.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (param < 0)
        {
            visualDirection.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
