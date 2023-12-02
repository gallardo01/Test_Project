using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class BrickSpawner : MonoBehaviour
{

    [SerializeField] private Transform parent;

    private IObjectPool<Brick> objectPool;
    private Dictionary<ColorType, int> colorCount;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = ObjectPool.Instance.Pool;

        colorCount = new Dictionary<ColorType, int>();

        foreach (ColorType colorType in Stage.Instance.UsedColors) {
            colorCount[colorType] = 9;
        }

        for (float x = -4.75f; x <= 1.25f; x += 2) {
            for (float z = -4.75f; z <= 5.25f; z += 1.25f) {
                Brick brick = objectPool.Get();
                brick.transform.parent = parent;
                brick.transform.SetPositionAndRotation(new Vector3(x, 0, z), parent.rotation);
                for (int i = 0; i < colorCount.Count; i++)
                {
                    if (Random.Range(0, 100) > 50 || i == colorCount.Count - 1) {
                        ColorType color = colorCount.ElementAt(i).Key;
                        brick.ChangeColor(color);
                        if (--colorCount[color] == 0) colorCount.Remove(color);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
