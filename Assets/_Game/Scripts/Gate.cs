using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gate : MonoBehaviour
{
    // Start is called before the first frame update
    private string currentAnimName;
    [SerializeField] private Animator anim;
    void Start()
    {
        ChangeAnim("idle");
    }
    IEnumerator Delay(float s)
    {
        yield return new WaitForSeconds(s);
        ChangeAnim("done");
        PlayerPrefs.SetInt("Stage", 3);
        SceneManager.LoadScene("Loading");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && GameController1.Instance.isOpen == true)
        {
            ChangeAnim("open");
            StartCoroutine(Delay(1f));
        }
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
