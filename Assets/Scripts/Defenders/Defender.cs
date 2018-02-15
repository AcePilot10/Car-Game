using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour {

    #region Variables
    public float price;
    public GameObject prefab;
    public PlaceArea placeArea;
    #endregion

    #region Puchasing Functionality
    public virtual void Purchase() {
        if (CanSpawn())
        {
            if (Game.instance.HasEnoughEnergy(price))
            {
                Game.instance.RemoveEnergy(price);
                Debug.Log("Spawning Defender...");
                SpawnDefender();
            }
            else {
                Debug.Log("Not enough energy!");
            }
        }
        else {
            Debug.Log("Can't place there!");
        }
    }

    private bool CanSpawn() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.down, Mathf.Infinity);
        if (hit.collider != null)
        {
            switch (hit.collider.tag)
            {
                case "Track":
                    if (placeArea == PlaceArea.ON_TRACK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "Dirt":
                    if (placeArea == PlaceArea.NOT_ON_TRACK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
        else {
            Debug.Log("Didn't detect any place area!");
            return true;
        }
        return false;
    }

    public virtual void SpawnDefender() {
        GameObject obj = Instantiate(prefab) as GameObject;
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        obj.transform.position = position;
        obj.transform.parent = GameObject.FindGameObjectWithTag("Defender Parent").transform;
    }
    #endregion
}