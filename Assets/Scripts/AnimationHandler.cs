using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class AnimationHandler : MonoBehaviour
{
	private Animation anim;

    void Awake() {
        anim = GetComponent<Animation>();
    }

    void FixedUpdate()
    {
		Vector3 movement = InputManager.GetMovement();
		bool effectiveSpeed = (movement.x != 0f || movement.z != 0f);
		//Debug.Log(effectiveSpeed);
        if (effectiveSpeed == true) {
			anim.Play("Run");
		}
		else {
			anim.Play("Idle");
		}
    }
}
