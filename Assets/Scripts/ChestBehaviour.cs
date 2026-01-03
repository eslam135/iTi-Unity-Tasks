using System.Collections;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    Animator anim;
    public static bool canOpen = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (canOpen)
            {
                StartCoroutine(PlayOpenCloseSequence());
            }
        }
    }
    private IEnumerator PlayOpenCloseSequence()
    {
        anim.Play("Open");

        yield return new WaitForSeconds(1f);

        anim.Play("Close");
    }
}
