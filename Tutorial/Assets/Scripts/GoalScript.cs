using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public void OnEpisodeBegin()
    {
        float hor = Random.Range(-8.5f, 8.5f);
        float ver = Random.Range(-8.5f, 8.5f);

        transform.position = new Vector3(hor, 0.5f, ver);
    }
}
