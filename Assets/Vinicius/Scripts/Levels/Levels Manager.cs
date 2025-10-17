using System.Collections.Generic;
using System.Linq;
using Characters.Player.States;
using Objects.Interactables;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<Level> levels = new();

    private int currentLevelId => PlayerPrefs.GetInt("checkpointId");
    private Level currentLevel => levels[currentLevelId];

    private void Awake()
    {
        levels.OrderBy(l => l.GetCheckpointId());
    }

    void OnEnable()
    {
        Spawn.OnPlayerSpawned += PlayerSpawned;
        Respawn.OnPlayerRespawned += PlayerSpawned;
        Die.OnPlayerDied += PlayerDied;

        CheckpointCollider.OnCheckpointReached += LevelEntered;
    }

    void OnDisable()
    {
        Spawn.OnPlayerSpawned -= PlayerSpawned;
        Respawn.OnPlayerRespawned -= PlayerSpawned;
        Die.OnPlayerDied -= PlayerDied;

        CheckpointCollider.OnCheckpointReached -= LevelEntered;
    }

    // Quando o jogador SPAWNAR/RESPAWNAR:
    //// Restaura os inimigos/objetos com IRestorable da fase atual
    public void PlayerSpawned()
    {
        player.transform.position = currentLevel.GetSpawnPoint();

        currentLevel.RestoreObjects();
    }

    // Quando o jogador MORRER:
    //// Desativa os inimigos da fase atual
    public void PlayerDied()
    {
        currentLevel.DeactivateObjects();
    }

    // Quando o jogador ENTRAR em uma fase:
    //// Desativa os inimigos da fase anterior, se houver
    //// Ativa os inimigos da fase atual
    public void LevelEntered()
    {
        int id = currentLevelId;

        if (id > 0)
            levels[id - 1].DeactivateObjects();

        levels[id].ActivateObjects();
    }
}