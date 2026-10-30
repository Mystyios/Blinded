
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DestroyWithTriggerBehavior : MonoBehaviour
{
    public UnityEvent BeforeDestroy;
    
    public bool destroyaftertime = true;

    public float destroytimer = 5.0f;

    

    private WaitForSeconds wfs;

    private WaitForSeconds wfs2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wfs = new WaitForSeconds(destroytimer * .95f);
        wfs2 = new WaitForSeconds(destroytimer * .05f);
       if (destroyaftertime)
       {
           StartCoroutine("destroyaftertimer");

       }
    }

    public void destroyNow()
    {
        //BeforeDestroy.Invoke();
        Destroy(gameObject);
    }

    public void DestroywithTrigger()
    {
        StartCoroutine("DestroyShortTrigger");
    }

    public IEnumerator DestroyShortTrigger()
    {
        BeforeDestroy.Invoke();
        yield return new WaitForSeconds(.2f);
        destroyNow();
    }
    public IEnumerator destroyaftertimer()
    {
        yield return wfs;
        BeforeDestroy.Invoke();
        yield return wfs2;
        destroyNow();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
