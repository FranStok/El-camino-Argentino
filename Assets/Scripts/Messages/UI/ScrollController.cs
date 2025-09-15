using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    [SerializeField] ScrollRect rectTransform;
    private void Start()
    {
       StartCoroutine(ResetScroll());
    }

    public IEnumerator ResetScroll()
    {
        yield return new WaitForEndOfFrame();
        rectTransform.normalizedPosition=new Vector2(0,0);
    }


}
