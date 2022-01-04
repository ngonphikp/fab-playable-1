using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractArea : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider2D> m_OnTriggerEnter2D;
    [SerializeField] private UnityEvent<Collider2D> m_OnTriggerStay2D;
    [SerializeField] private UnityEvent<Collider2D> m_OnTriggerExit2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D");
        m_OnTriggerEnter2D?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerExit2D");
        m_OnTriggerExit2D?.Invoke(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerStay2D");
        m_OnTriggerStay2D?.Invoke(collision);
    }
}
