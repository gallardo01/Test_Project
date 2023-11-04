using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;

public class GameController : MonoBehaviour
{
    public PlayerController Mummy;
    public GameObject DefenderSide;
    // Start is called before the first frame update
    void Start()
    {

    }

    private IEnumerator Delay1s(){
        yield return new WaitForSeconds(1f);
        StartCoroutine(Delay1s());
    }
    void Spawner()
    {
        Instantiate(Mummy, DefenderSide.transform.position, Quaternion.identity);
        StartCoroutine(Delay1s()); // Tưởng nó sẽ dừng mãi nhưng nó lỗi
        Debug.Log("Spawn");
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
