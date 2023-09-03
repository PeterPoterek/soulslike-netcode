using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    [Header("Network Join")]
    [SerializeField] bool startGameAsClient;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (startGameAsClient)
        {
            startGameAsClient = false;

            //We must shutdown as host because we started as host
            NetworkManager.Singleton.Shutdown();
            //Then restart as a client
            NetworkManager.Singleton.StartClient();
        }
    }
}
