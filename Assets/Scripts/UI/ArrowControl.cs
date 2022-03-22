using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.AddListener(E_EventType.SHOW_ARROW, ShowArrow);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(E_EventType.SHOW_ARROW, ShowArrow);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            arrow.SetActive(false);
        }
    }

    void ShowArrow()
    {
        {
            arrow.SetActive(true);
            arrow.GetComponent<MyMouseArrow>().origin.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
}
