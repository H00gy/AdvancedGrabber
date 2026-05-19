using UnityEngine;
using System.Collections;


public class grabber : MonoBehaviour
{
    public Vector2 ClosedPos;
    public GameObject Book;
    //public GameObject OpenedBook;
    public Sprite OpenSprite;
    public Sprite ClosedSprite;

    public float AnimationSpeed = 5f;
    private BoxCollider2D colliderRef;
    public float OpenColliderXScale;
    public float OpenColliderYScale;
    public float ClosedColliderXScale;
    public float ClosedColliderYScale;
    private void Start()
    {
        //Book.SetActive(false);
        Book.transform.position = new Vector2(0, 0);
        colliderRef = Book.GetComponent<BoxCollider2D>();
        colliderRef.size = new Vector2(OpenColliderXScale, OpenColliderYScale);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Book)
        {
            Vector2 HitPoint = other.ClosestPoint(transform.position); // finds point of trigger
            Debug.Log(" the closesest point is " + HitPoint);
            SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
            StartCoroutine(SetColliderSize(ClosedColliderXScale, ClosedColliderYScale));
            sr.sprite = ClosedSprite; // changes sprite
            StartCoroutine(AnimateBookToShelf(HitPoint)); // animates
            
           

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Application.isPlaying) return;
        if (other.gameObject== Book)
        {
            
            Debug.Log("left");
            SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
           
            sr.sprite = OpenSprite; // changes sprite
            //Vector2 HitPoint = other.ClosestPoint(transform.position);
            StartCoroutine(SetColliderSize(OpenColliderXScale, OpenColliderYScale));
            //OpenedBook.SetActive(true);



        }
    }
    private void ColliderRef(bool state)
    {
        colliderRef = GetComponent<BoxCollider2D>();
        colliderRef.enabled = state;
    }
    private IEnumerator AnimateBookToShelf(Vector2 startPosition)
    {
        // Place the closed book exactly where the collision happened and turn it on
        Book.transform.position = startPosition;
        Book.SetActive(true);

        float timeElapsed = 0f;
        // You can adjust this duration to make the transition slower or faster
        float duration = AnimationSpeed/2f; // divide by 2 to split phases

        while (timeElapsed<duration) // moves X (Note to self, always use timeElapsed<duration)
        {
            float newX = Mathf.Lerp(startPosition.x, ClosedPos.x, timeElapsed / duration);
            Book.transform.position = new Vector2(newX, startPosition.y);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        timeElapsed = 0f;
        while (timeElapsed < duration) // moves Y
        {
            
            float newY = Mathf.Lerp(startPosition.y, ClosedPos.y, timeElapsed / duration);
            Book.transform.position = new Vector2(ClosedPos.x, newY);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        

        // Ensure it snaps perfectly to the final destination at the end
        //Book.transform.position = ClosedPos;
    }
    private IEnumerator SetColliderSize(float xSize, float ySize)
    {
        yield return new WaitForSeconds(1f);
        colliderRef = Book.GetComponent<BoxCollider2D>();
        colliderRef.size = new Vector2(xSize, ySize); // scales to new collider size
        
    }
    public void SwapSprites() // for changing collider size visually
    {
        if (Book.GetComponent<SpriteRenderer>().sprite == OpenSprite)
        {
            Book.GetComponent<SpriteRenderer>().sprite = ClosedSprite;
        }
        else
        {
            Book.GetComponent<SpriteRenderer>().sprite = OpenSprite;
        }
    }

}
