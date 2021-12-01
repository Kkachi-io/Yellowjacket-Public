using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    #region Variables

        [SerializeField] private Room room;

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        var hero = other.GetComponent<Hero>();

        if(hero)
            room.ActivateRoomCamera();
    }

    public void SetRoom(Room newRoom)
    {
        room = newRoom;
    }


}
