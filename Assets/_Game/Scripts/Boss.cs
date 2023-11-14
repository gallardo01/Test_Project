using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Character
{

    [SerializeField] float attackRange;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Player player;
    [SerializeField] Kunai kunaiPrefab;

    // Boss có các tính năng sau:
	// + Boss có thể bắn đạn vào người chơi nếu người chơi tiến lại gần trong 1 phạm vi nhất định
	// + Tầm chém xa hơn người chơi
	// + Người chơi chỉ có thể bắn phi tiêu 5 lần
	// + Trên bản đồ sẽ ngẫu nhiên xuất hiện phi tiêu, người chơi ăn sẽ được + 3 phi tiêu

    private void Start() {
        OnInit();
    }

    public override void OnInit() {
        hp = 300;
        healthBar.OnInit(100);
    }

    private void Update() {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange) Attack();
    }

    private void Attack() {
        Kunai kunai = Instantiate(kunaiPrefab, transform.position, Quaternion.identity);
    }

    public override void OnDespawn() {
        
    }

    protected override void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(LoadMenu), 2f);
    }

    private void LoadMenu() {
        PlayerPrefs.SetInt("stage", 0);
        SceneManager.LoadSceneAsync(0);
    }

}
