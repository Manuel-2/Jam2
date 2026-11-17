using UnityEngine;

public class HitDetector : MonoBehaviour
{

    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject[] hitTrashAnimationPrefab;
    [SerializeField] ParticleSystem hitParticles;
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
            playerAnimator.SetTrigger("swing");
            if (item != null && item.CompareTag("Paper"))
            {
                hitParticles.Play();
                GameObject hitItem = Instantiate(hitTrashAnimationPrefab[0], this.transform.position, Quaternion.identity);
                Destroy(hitItem, 1f);
                GameManager.sharedInstance.Hit();
                Destroy(item);
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
            playerAnimator.SetTrigger("swing");
            if (item != null && item.CompareTag("Plastic"))
            {
                hitParticles.Play();
                GameObject hitItem = Instantiate(hitTrashAnimationPrefab[1], this.transform.position, Quaternion.identity);
                Destroy(hitItem, 1f);
                GameManager.sharedInstance.Hit();
                Destroy(item);
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
            playerAnimator.SetTrigger("swing");
            if (item != null && item.CompareTag("Aluminum"))
            {
                hitParticles.Play();
                GameObject hitItem = Instantiate(hitTrashAnimationPrefab[2], this.transform.position, Quaternion.identity);
                Destroy(hitItem, 1f);
                GameManager.sharedInstance.Hit();
                Destroy(item);
                item = null;
            }
            else
            {
                GameManager.sharedInstance.Miss();
                Destroy(item);
                return;
            }
        }
    }
}
