using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class BrickStack : Singleton<BrickStack>
{
    // Start is called before the first frame update
    public Transform Player;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask whiteLayer;
    public List<GameObject> Sbrick = new List<GameObject>();
    public int brick = 0;
    void Start()
    {
        transform.position = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.position.x,3,Player.position.z);
    }
    public void check()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit,0.1f,brickLayer))
        {
            Sbrick.Add(hit.collider.gameObject);
            hit.collider.gameObject.transform.SetParent(gameObject.transform);
            hit.collider.gameObject.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, 3 + 0.25f * brick, hit.collider.gameObject.transform.position.z);
            brick++;
        }
    }
    public void check1()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.5f, whiteLayer))
        {
            if (brick > 0)
            {
                int k = 0;
                foreach(GameObject i in Sbrick)
                {
                    if(k==brick-1)
                    {
                        Destroy(i);
                       
                    }
                    else
                    {
                        k++;
                    }
                }
                brick--;
            }
        }
    }
}
