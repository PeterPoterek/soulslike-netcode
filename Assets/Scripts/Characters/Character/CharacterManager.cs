using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterManager : NetworkBehaviour
{
    public CharacterController characterController;
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterNetworkManager characterNetworkManager;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        characterNetworkManager = GetComponent<CharacterNetworkManager>();
    }

    protected virtual void Update()
    {
        if (IsOwner)
        {
            characterNetworkManager.networkPosition.Value = transform.position;
            characterNetworkManager.networkRotation.Value = transform.rotation;

        }
        else
        {
            transform.position = Vector3.SmoothDamp
            (transform.position,
            characterNetworkManager.networkPosition.Value,
            ref characterNetworkManager.networkPositionVelocity,
            characterNetworkManager.networkPositionSmoothTime);


            transform.rotation = Quaternion.Slerp
            (transform.rotation,
             characterNetworkManager.networkRotation.Value,
             characterNetworkManager.networkRotationSmoothTime);
        }
    }


    protected virtual void LateUpdate()
    {

    }
}
