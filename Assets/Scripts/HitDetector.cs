using UnityEngine;

public class HitDetector : MonoBehaviour
{

    [SerializeField] GameObject[] hitTrashAnimationPrefab;
    GameObject item;


    void OnTriggerEnter2D(Collider2D collision)
    {
        item = collision.gameObject;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        item = null;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (item != null && item.CompareTag("Paper"))
            {
                Instantiate(hitTrashAnimationPrefab[0], this.transform.position, Quaternion.identity);
                GameManager.sharedInstance.Hit();
                item = null;
            }
            else
            {
                GameManager.sharedInstance.Miss();
                return;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (item != null && item.CompareTag("Plastic"))
            {
                Instantiate(hitTrashAnimationPrefab[1], this.transform.position, Quaternion.identity);
                GameManager.sharedInstance.Hit();
                item = null;
            }
            else
            {
                GameManager.sharedInstance.Miss();
                return;
            }
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            if (item != null && item.CompareTag("Aluminum"))
            {
                Instantiate(hitTrashAnimationPrefab[2], this.transform.position, Quaternion.identity);
                GameManager.sharedInstance.Hit();
                item = null;
            }
            else
            {
                GameManager.sharedInstance.Miss();
                return;
            }
        }
    }
}
