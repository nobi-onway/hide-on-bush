using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    [SerializeField] private int health;
    public int Health
    {
        get 
        {
            return health;
        }
        set
        {
            health = value;
            if(Health == 0)
            {
                GetComponent<CircleCollider2D>().enabled = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            Debug.Log(health);
        }
    }
    [SerializeField] private GameObject[] trees;

    private void Start()
    {
        trees = GameObject.FindGameObjectsWithTag("Tree");

        Health = trees.Length;
    }

    public void HealthChange()
    {
        Health--;
    }
}
