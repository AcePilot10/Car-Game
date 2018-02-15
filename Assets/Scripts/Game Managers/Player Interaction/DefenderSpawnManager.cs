using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawnManager : MonoBehaviour
{

    public GameObject currentlySelected;

    public void SetCurrentSelected(GameObject obj) {
        currentlySelected = obj;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentlySelected != null)
            {
                currentlySelected.GetComponent<Defender>().SpawnDefender();
            }
        }
    }
}