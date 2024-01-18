using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    [SerializeField] private Bot bot;
    [SerializeField] private Character character;
    [SerializeField] private int numOfTeams;
    [SerializeField] private int numOfPlayers;

    private List<List<Player>> teams;

    public void StartGame() {
        for (int i = 0; i < numOfTeams; i++) {
            teams.Add(new List<Player>());
            for (int j = 0; j < numOfPlayers; j++) {
                teams[i].Add(Instantiate(bot).GetComponent<Bot>());
            }
        }

        // int playerTeam = 
        // teams[]
    }
}