using System.Collections;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    [SerializeField] MessageQueue messageQueue;

    float delayGameStartup = 0f;

    bool isPreGameComplete = false;
    bool isActiveGameComplete = false;
    bool isPostGameComplete = false;

    Coroutine gameLoopCoroutine;

    GameState _gameState;

    public GameState gameState
    {
        get => _gameState;
        set
        {
            if(_gameState != value)
            {
                _gameState = value;
                Debug.Log($"Game State changed to: {_gameState}");

                switch (_gameState)
                {
                    case GameState.PreGame:
                        HandlePreGame();
                        break;
                    case GameState.ActiveGame:
                        HandleActiveGame();
                        break;
                    case GameState.PostGame:
                        HandlePostGame();
                        break;
                }
            }
        }
    }

    void Start()
    {
        StartGameLoop();
    }


    void OnEnable()
    {
        messageQueue.Subscribe(GlobalSlugs.GAME_ENDED, HandleGameCompleted);
    }

    void OnDisable()
    {
        messageQueue.Unsubscribe(GlobalSlugs.GAME_ENDED, HandleGameCompleted);
    }

    void HandleGameCompleted(object obj)
    {
        CompleteActiveGame();
    }

    #region GameLoop
    public void StartGameLoop()
    {
        if (gameLoopCoroutine == null)
        {
            gameLoopCoroutine = StartCoroutine(GameLoop());
        }
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            gameState = GameState.PreGame;
            yield return new WaitForSeconds(delayGameStartup);

            CompletePreGame();
            yield return new YieldPreGame(() => isPreGameComplete);
            gameState = GameState.ActiveGame;

            yield return new YieldActiveGame(() => isActiveGameComplete);
            gameState = GameState.PostGame;

            yield return new YieldPostGame(() => isPostGameComplete);
        }
    }

    public void CompletePreGame() => isPreGameComplete = true;
    public void CompleteActiveGame() => isActiveGameComplete = true;
    public void CompletePostGame() => isPostGameComplete = true;
    #endregion

    void HandlePreGame()
    {
        Debug.Log("Handling PreGame setup...");
        messageQueue.Publish(GlobalSlugs.PRE_BATTLE_STARTED, "");
        messageQueue.Publish(GlobalSlugs.GAME_STATE_CHANGED, gameState);
    }

    void HandleActiveGame()
    {
        Debug.Log("Game has started!");
        messageQueue.Publish(GlobalSlugs.BATTLE_STARTED, "");
        messageQueue.Publish(GlobalSlugs.GAME_STATE_CHANGED, gameState);
    }

    void HandlePostGame()
    {
        Debug.Log("Handling PostGame cleanup...");
        messageQueue.Publish(GlobalSlugs.BATTLE_ENDED, "");
        messageQueue.Publish(GlobalSlugs.GAME_STATE_CHANGED, gameState);
    }
}