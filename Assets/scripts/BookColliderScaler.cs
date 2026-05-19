using UnityEngine;
using System.Collections;
public class BookColliderScaler : MonoBehaviour
{
    private BoxCollider2D colliderRef;
    public float OpenColliderXScale;
    public float OpenColliderYScale;
    public float ClosedColliderXScale;
    public float ClosedColliderYScale;
    private void Start()
    {
        colliderRef = GetComponent<BoxCollider2D>();
        colliderRef.size = new Vector2(OpenColliderXScale, OpenColliderYScale);
    }

}
