using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 1f;
    
    private Route _currentRoute;
    private Vector3 _currentTarget;

    private bool _isMoving;

    private IObjectPool<EnemyController> _pool;

    public void OnInit(IObjectPool<EnemyController> pool)
    {
        _pool = pool;
    }
    
    public void OnSpawn(Route route)
    {
        _currentRoute = route;
        transform.position = _currentRoute.startPos;
        _currentTarget = _currentRoute.positions.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget, movementSpeed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, _currentTarget) < 0.1f)
        {
            if (_currentRoute.positions.Count > 0)
            {
                _currentTarget = _currentRoute.positions.Dequeue();
                return;
            }

            _pool.Release(this);
        }
    }

    public void OnHit()
    {
        GameController.OnScore();
        _pool.Release(this);
    }
}
