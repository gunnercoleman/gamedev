using UnityEngine;

public class Teleporter : MonoBehaviour
{

    [SerializeField] GameObject TeleportDestination;

    [SerializeField] GameObject player;

    private Vector3 teleportCoords;

    private CharacterController characterController;

    private bool isTeleporting = false;


    void Start()

    {

        teleportCoords = TeleportDestination.transform.position;

        characterController = player.GetComponentInChildren<CharacterController>();

    }


    void OnTriggerEnter(Collider other)

    {

        if (!isTeleporting)

        {

            characterController.enabled = false;


            player.transform.position = teleportCoords;


            characterController.enabled = true;

            isTeleporting = true;

        }

    }


    private void OnTriggerExit(Collider other)

    {

        isTeleporting = false;

    }

}