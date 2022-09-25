using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;
    

    void OnMove (InputValue inputValue) {
        var vector2 = inputValue.Get<Vector2>();
        Debug.Log($"{vector2.ToString()}");

        //TODO May need some CLAMP
        Vector3 pos = transform.position;
        pos.x += vector2.x * Speed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;
        transform.position = pos;
    }
}
