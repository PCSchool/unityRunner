using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionManagement : MonoBehaviour {
    public Canvas canvas;
    public Animation animation;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            Destroy(collision.gameObject);
            canvas.GetComponent<ScoreManagement>().increaseScore(1);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            collision.gameObject.SetActive(false);
            canvas.GetComponent<ScoreManagement>().damageHealth(1);
        }
            
    }
}
