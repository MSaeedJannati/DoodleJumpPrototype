using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]Transform mTransform;
  
    void Update()
    {
        if (mTransform.position.y < Movement.downY)
        {
            ObstacleGenerator.instance.CreateNewObstacle();
            gameObject.SetActive(false);
           
        }
    }
  
}
