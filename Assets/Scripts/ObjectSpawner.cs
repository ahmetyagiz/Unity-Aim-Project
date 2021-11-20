using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;

    int position_z;

    void Start()
    {
        position_z = 20;

        ObjectSpawn(-7);

        position_z = 15;

        ObjectSpawn(7);
    }

    private void ObjectSpawn(int position_x)
    {
        for (int i = 0; i < 6; i++)
        {
            Instantiate(blockPrefab, new Vector3(position_x, 1f, position_z), blockPrefab.transform.rotation);
            position_z +=10;
        }
    }
}