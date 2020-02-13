using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ctrl : MonoBehaviour
{
    [SerializeField] Text ui_text_p_Health;
    [SerializeField] GameObject p_Player;
    int ui_p_Health;
    string ui_p_HealthString;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ui_p_Health = martin_ctrl.p_Health;
        ui_p_HealthString = ui_p_Health.ToString();
        ui_text_p_Health.text = ui_p_HealthString;
    }
}
