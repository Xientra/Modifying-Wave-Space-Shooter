using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    void Start()
    {
        player = Player.Instance;
    }
    void Update()
    {
        if (player == null) return;
        if (ToFarAwayFromPlayer()) { transform.position = WaveController.Instance.GetRandomPositionAroundPlayer(); }
    }

    private Player player;
    public float distance;

    private bool ToFarAwayFromPlayer()
    { return (player.transform.position - transform.position).magnitude > distance; }

}
