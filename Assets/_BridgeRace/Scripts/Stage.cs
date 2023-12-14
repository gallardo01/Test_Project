using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class Stage : MonoBehaviour
{
    
    [SerializeField] private Transform brickParent;

    private IObjectPool<Brick> objectPool;
    private Dictionary<ColorType, int> colorCount;
    private List<Vector3> availablePosition;
    private List<Brick> bricks;

    public List<Brick> Bricks => bricks;

    private void Start() {
        colorCount = new Dictionary<ColorType, int>();

        bricks = new List<Brick>();

        objectPool = ObjectPool.Ins.Pool;
    }

    // Trigger when user crossing door
    public void StartLevel(ColorType colorType) {
        if (colorCount.ContainsKey(colorType)) return;
        if (availablePosition == null) FillPosition();
        colorCount[colorType] = 9;
        while (colorCount.Count > 0) {
            Spawn(PickColor());
        }
    }

    // Fill brick positions, once
    private void FillPosition() {
        availablePosition = new List<Vector3>();
        for (float x = -4.75f; x <= 1.25f; x += 2)
            for (float z = -4.75f; z <= 5.25f; z += 1.25f)
                availablePosition.Add(new Vector3(x, 0, z));
    }

    // Pick random color for bricks
    private ColorType PickColor()
    {
        ColorType color = colorCount.ElementAt(Random.Range(0, colorCount.Count)).Key;
        if (--colorCount[color] == 0) colorCount.Remove(color);
        return color;
    }

    // Spawn brick at level
    private void Spawn(ColorType color) {
        Brick brick = objectPool.Get();
        bricks.Add(brick);
        brick.transform.parent = brickParent;
        brick.transform.rotation = Quaternion.Euler(Vector3.zero);
        brick.Stage = this;
        Vector3 position = availablePosition[Random.Range(0, availablePosition.Count)];
        brick.transform.localPosition = position;
        brick.ChangeColor(color);
        // Make position occupied    
        availablePosition.Remove(position);
    }

    public void Consume(Brick brick) {
        StartCoroutine(ReSpawn(brick));
    }

    // Decrease remaining color before respawn, player crossing door cause bricks waiting for respawn to be respawn immediately due to color remaining in dictionary, respawn called after 5 seconds caused index out of range
    private IEnumerator ReSpawn(Brick brick) {
        
        // value
        var colorType = brick.ColorType;

        // update empty position
        availablePosition.Add(brick.transform.localPosition);
        bricks.Remove(brick);

        // update color
        if (colorCount.ContainsKey(colorType)) colorCount[colorType]++;
        else colorCount[colorType] = 1;

        colorType = PickColor();

        yield return new WaitForSecondsRealtime(5f);
        
        Spawn(colorType);
    }

}
