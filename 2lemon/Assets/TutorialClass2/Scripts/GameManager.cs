using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ʹ���ֵ����洢��ҹ�����ʵ��
    public Dictionary<int, PlayerManager> playerManagers = new Dictionary<int, PlayerManager>();

    // �����ҹ��������ֵ���
    public void AddPlayer(int playerId, PlayerManager playerManager)
    {
        if (!playerManagers.ContainsKey(playerId))
        {
            playerManagers.Add(playerId, playerManager);
        }
    }

    // ���ֵ����Ƴ���ҹ�����
    public void RemovePlayer(int playerId)
    {
        if (playerManagers.ContainsKey(playerId))
        {
            playerManagers.Remove(playerId);
        }
    }

    // ͨ�����ID��ȡ��ҹ�����
    public PlayerManager GetPlayerManager(int playerId)
    {
        if (playerManagers.TryGetValue(playerId, out PlayerManager playerManager))
        {
            return playerManager;
        }
        return null;
    }
}