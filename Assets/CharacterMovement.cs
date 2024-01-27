using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    PlayerManager PlayerManager;

    public bool isMoveable;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform visualDirection;


    private void Awake()
    {
        TryGetComponent(out PlayerManager);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Move(horizontalInput);
    }

    void Move(float param)
    {
        if (!isMoveable)
        {
            PlayerManager.anim.SetWalk(false);
            return;
        }

        Vector2 moveDir = new Vector2(param, 0) * moveSpeed * Time.deltaTime;

        this.transform.Translate(moveDir);

        if(param > 0)
        {
            visualDirection.localScale = new Vector3(1f, 1f, 1f);
            PlayerManager.anim.SetWalk(true);
        }
        else if (param < 0)
        {
            visualDirection.localScale = new Vector3(-1f, 1f, 1f);
            PlayerManager.anim.SetWalk(true);
        }
        else
        {
            PlayerManager.anim.SetWalk(false);
        }
    }
}
