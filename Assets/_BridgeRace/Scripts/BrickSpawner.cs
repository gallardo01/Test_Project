using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class BrickSpawner : MonoBehaviour
{
    public static BrickSpawner Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] private Transform[] parent;

    private IObjectPool<Brick> objectPool;
    private List<Dictionary<ColorType, int>> colorCount;
    private List<List<Vector3>> availablePosition;

    public List<Dictionary<ColorType, int>> ColorCount { get => colorCount; }

    // Start is called before the first frame update
    void Start()
    {
        colorCount = new List<Dictionary<ColorType, int>>();

        availablePosition = new List<List<Vector3>>();
        
        objectPool = ObjectPool.Instance.Pool;

        colorCount.Add(new Dictionary<ColorType, int>());

        foreach (ColorType colorType in GameManager.Ins.UsedColors)
        {
            colorCount[0][colorType] = 9;
        }
        
        SpawnLevel(0);
    }

    public void StartLevel(int level, ColorType colorType) {
        if (colorCount.Count < level + 1) colorCount.Add(new Dictionary<ColorType, int>());
        var cc = colorCount[level];
        cc[colorType] = 9;
        SpawnLevel(level);
    }

    private void FillPosition(int level) {
        availablePosition.Add(new List<Vector3>());
        for (float x = -4.75f; x <= 1.25f; x += 2)
            for (float z = -4.75f; z <= 5.25f; z += 1.25f)
                availablePosition[level].Add(new Vector3(x, 0, z));
    }

    private void SpawnLevel(int level) {
        if (availablePosition.Count - 1 < level) FillPosition(level) ;

        var cc = colorCount[level];
        while (cc.Count > 0)
            Spawn(level);
    }

    private void PickColor(Brick brick, int level) {
        var cc = colorCount[level];
        for (int i = 0; i < cc.Count; i++)
        {
            if (Random.Range(0, 100) > 50 || i == cc.Count - 1)
            {
                ColorType color = cc.ElementAt(i).Key;
                brick.ChangeColor(color);
                if (--cc[color] == 0) cc.Remove(color);
                break;
            }
        }
    }

    // Spawn brick at level
    private void Spawn(int level) {
        var currentParent = parent[level];
        Brick brick = objectPool.Get();
        brick.transform.parent = currentParent;
        brick.transform.rotation = Quaternion.Euler(Vector3.zero);

        // random position at level
        var position = availablePosition[level][Random.Range(0, availablePosition[level].Count)];
        brick.transform.localPosition = position;

        // Make position occupied    
        availablePosition[level].Remove(position);

        PickColor(brick, level);
    }

    public void Consume(Brick brick) {
        StartCoroutine(ReSpawn(brick));
    }

    private IEnumerator ReSpawn(Brick brick) {
        
        // value
        var colorType = brick.colorType;
        int level = (int) (brick.transform.position.y / 3);

        // update empty position
        availablePosition[level].Add(brick.transform.localPosition);

        // update color
        var cc = colorCount[level];
        if (cc.ContainsKey(colorType)) cc[colorType]++;
        else cc[colorType] = 1;

        yield return new WaitForSecondsRealtime(5f);
        
        Spawn(level);
    }
}