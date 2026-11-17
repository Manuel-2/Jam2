using UnityEngine;

public class FailDetector : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("FAIL!");
    }
}
