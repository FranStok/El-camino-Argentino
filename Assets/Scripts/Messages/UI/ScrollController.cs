using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    [SerializeField] ScrollRect rectTransform;
    [SerializeField]
    private bool resetScrollOnStart = true;
    private void Start()
    {
       if(resetScrollOnStart) StartCoroutine(ResetScroll());
    }

    public IEnumerator ResetScroll(float x=0f, float y=0f)
    {
        yield return new WaitForEndOfFrame();
        rectTransform.normalizedPosition=new Vector2(x,y);
    }


}
