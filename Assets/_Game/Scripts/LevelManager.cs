using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private int botCount;
    [SerializeField] private Character character;
    [SerializeField] private List<Bot> bots;

    private void Awake() {
        // Create Character
        Instantiate(character, Vector3.zero, Quaternion.identity, null);

        // Create Bots
        for (int i = 0; i < botCount; i++) {
            Vector3 position = new Vector3(Random.Range(-47f, 47f), 0, Random.Range(-47f, 47f));
            Bot bot = BotPool.Get();
            bot.transform.position = position;
            bots.Add(bot);
        }
    }

}
