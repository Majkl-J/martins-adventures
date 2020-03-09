using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class white_chalk_ctrl : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        

        if (player.CompareTag("Player"))
        {
            
            if (martin_ctrl.p_Health <= 2)
            {
                martin_ctrl.p_Health = martin_ctrl.p_Health + 1;
                Destroy(this.gameObject);
            }
        }
    }

}
