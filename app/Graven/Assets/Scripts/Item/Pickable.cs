using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            switch (tag)
            {
                case "Bubble":
                    {
                        GetComponent<Bubble>().OnPick(collider);
                        break;
                    }
                case "Coin":
                    {
                        GetComponent<Coin>().OnPick(collider);
                        break;
                    }
            }
        }
    }
}