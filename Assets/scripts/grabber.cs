using UnityEngine;
using System.Collections;


public class grabber : MonoBehaviour
{
    public Vector2 ClosedPos;
    public GameObject ClosedBook;
    public float AnimationSpeed = 5f;
    private void Start()
    {
        ClosedBook.SetActive(false);
        

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "manual")
        {
            Vector2 HitPoint = other.ClosestPoint(transform.position);
            Debug.Log(" the closesest point is " + HitPoint);
            other.gameObject.SetActive(false);
            StartCoroutine(AnimateBookToShelf(HitPoint));
           

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject== ClosedBook)
        {
            Debug.Log("left");
        }
    }
    private IEnumerator AnimateBookToShelf(Vector2 startPosition)
    {
        // Place the closed book exactly where the collision happened and turn it on
        ClosedBook.transform.position = startPosition;
        ClosedBook.SetActive(true);

        float timeElapsed = 0f;
        // You can adjust this duration to make the transition slower or faster
        float duration = AnimationSpeed/2f; // divide by 2 to split phases

        while (timeElapsed<duration) // moves X (Note to self, always use timeElapsed<duration)
        {
            float newX = Mathf.Lerp(startPosition.x, ClosedPos.x, timeElapsed / duration);
            ClosedBook.transform.position = new Vector2(newX, startPosition.y);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        timeElapsed = 0f;
        while (timeElapsed < duration) // moves Y
        {
            
            float newY = Mathf.Lerp(startPosition.y, ClosedPos.y, timeElapsed / duration);
            ClosedBook.transform.position = new Vector2(ClosedPos.x, newY);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        

        // Ensure it snaps perfectly to the final destination at the end
        //ClosedBook.transform.position = ClosedPos;
    }

}
