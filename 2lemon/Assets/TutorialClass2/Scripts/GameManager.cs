using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 使用字典来存储玩家管理器实例
    public Dictionary<int, PlayerManager> playerManagers = new Dictionary<int, PlayerManager>();

    // 添加玩家管理器到字典中
    public void AddPlayer(int playerId, PlayerManager playerManager)
    {
        if (!playerManagers.ContainsKey(playerId))
        {
            playerManagers.Add(playerId, playerManager);
        }
    }

    // 从字典中移除玩家管理器
    public void RemovePlayer(int playerId)
    {
        if (playerManagers.ContainsKey(playerId))
        {
            playerManagers.Remove(playerId);
        }
    }

    // 通过玩家ID获取玩家管理器
    public PlayerManager GetPlayerManager(int playerId)
    {
        if (playerManagers.TryGetValue(playerId, out PlayerManager playerManager))
        {
            return playerManager;
        }
        return null;
    }
}