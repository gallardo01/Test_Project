using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int speed = 1;
    public PlayerController Mummy;
    public BulletController Bullet;
    public GameObject DefenderSide, StartO, EndO;
    public GameObject[] path1, path2;
    private int step = 0;
    // Start is called before the first frame update
    void Start()
    {
        MummySpawner();
        BulletSpawner();
    }

    public GameObject[] returnPath(){
        if(Random.Range(0, 2) == 0)
        {
            return path1;
        }
        return path2;
    }

    public GameObject returnStartO(){
        return StartO;
    }

    public GameObject returnEndO(){
        return EndO;

    }

    private IEnumerator Delay1s(){
        yield return new WaitForSeconds(1f);
        MummySpawner();
    }
    void MummySpawner()
    {
        Instantiate(Mummy, DefenderSide.transform.position, Quaternion.identity);
        StartCoroutine(Delay1s());
        Debug.Log("Spawn a Mummy");
    }

    void BulletSpawner()
    {
        Instantiate(Bullet, DefenderSide.transform.position, Quaternion.identity);
        StartCoroutine(Delay1s());
        Debug.Log("Spawn a Bullet");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
