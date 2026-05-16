using UnityEngine;
using System.Collections;

public class BookAnimation : MonoBehaviour
{
   public IEnumerator AnimateBookToShelf(Vector2 startPos, Vector2 targetPos, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if ( this == null) yield break;

            float t = elapsedTime / duration;

            Vector2 basePos = Vector2.Lerp(startPos, startPos + targetPos, t);

            transform.position = new Vector2(basePos.x, basePos.y);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
   }
}
