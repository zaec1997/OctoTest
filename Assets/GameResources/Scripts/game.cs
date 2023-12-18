using Naninovel;
using UnityEngine;
using UnityEngine.SceneManagement;
using DTT.MinigameMemory;

using UnityEditor;
using UnityEngine.UI;


[CommandAlias("gamemem")]
public class GameMem : Command
{
    private MemoryGameManager gameManager;
    private Vector2 screen;
    private MemoryGameSettings gameSettings;
    private Scene scene;
    private GameObject gamemanager;
    private GameObject canvasGame;
    private Board _board;
    [ParameterAlias("Difficulty")]
    public StringParameter Difficulty;
    public ScriptPlayer player;


    public override UniTask ExecuteAsync (AsyncToken asyncToken = default)
    {
        
        scene = SceneManager.GetActiveScene();
        canvasGame = new GameObject();
        canvasGame.name = "Canvas";
        canvasGame.AddComponent<Canvas>();
        canvasGame.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGame.AddComponent<GraphicRaycaster>();
        canvasGame.AddComponent<CanvasScaler>();
        canvasGame.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGame.GetComponent<CanvasScaler>().referenceResolution = new Vector2 (1920, 1080);


        _board = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<Board>("Assets\\DTT\\Minigame - Memory\\Demo\\Prefabs\\BoardPrefab.prefab"));
        _board.name = "Board";
        _board.GetComponent<GridLayoutGroup>().cellSize = new Vector2(100, 100);
        _board.transform.SetParent(canvasGame.transform);

        gamemanager = new GameObject();
        gamemanager.name = "GameManager";
        gameManager = gamemanager.AddComponent<MemoryGameManager>();
        gameManager._board = _board;
       

        Debug.Log(scene.name);
        if (Difficulty == "Hard") {gameSettings = AssetDatabase.LoadAssetAtPath<MemoryGameSettings>("Assets\\DTT\\Minigame - Memory\\Demo\\ScriptableObjects\\Demo Hard.asset"); }
        else if(Difficulty == "Medium") { gameSettings = AssetDatabase.LoadAssetAtPath<MemoryGameSettings>("Assets\\DTT\\Minigame - Memory\\Demo\\ScriptableObjects\\Demo Medium.asset"); }
        else{ gameSettings = AssetDatabase.LoadAssetAtPath<MemoryGameSettings>("Assets\\DTT\\Minigame - Memory\\Demo\\ScriptableObjects\\Demo Easy.asset"); }


        Debug.Log(gameManager.IsGameActive);
        gameManager.StartGame(gameSettings);
        Debug.Log(gameManager.IsGameActive);
        _board.GetComponent<GridLayoutGroup>().cellSize = new Vector2(40, 40);
        _board.transform.localScale = new Vector2(6, 6);
        _board.GetComponent<RectTransform>().SetPosX(Screen.width / 2);
        _board.GetComponent<RectTransform>().SetPosY(Screen.height / 1.5f);
        player = Engine.GetService<ScriptPlayer>();

        _board.AllCardsMatched += endGame;
        Debug.Log("End");
        return UniTask.CompletedTask;
        
    }
    private void endGame()
    {
        Engine.GetService<IScriptPlayer>().Play(Engine.GetService<IScriptPlayer>().Playlist, Engine.GetService<IScriptPlayer>().PlayedIndex+1);
        GameObject.Destroy(canvasGame.gameObject);
        GameObject.Destroy(gamemanager.gameObject);

    }

}
