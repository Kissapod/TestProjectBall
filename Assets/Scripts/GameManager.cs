using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text coinCounterText;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject endGameDisplay;

    private GameObject[] coins;
    private int coinCounter = 0;
    private Vector2 playerStartPosition;
    // Start is called before the first frame update
    void Start()
    {
        coinCounterText.text = coinCounter.ToString();
        playerStartPosition = player.transform.position;
        coins = GameObject.FindGameObjectsWithTag("Coin");
    }

    private void Update()
    {
        if (coinCounter == coins.Length)
        {
            endGameDisplay.GetComponentInChildren<Text>().text = "You win!";
            ViewEndGameDisplay();
        }
    }

    public void CoinCounter()
    {
        coinCounter++;
        coinCounterText.text = coinCounter.ToString();
    }

    public void Restart()
    {
        CoinRefresh();
        PlayerRefresh();
        CoinCounterRefresh();

        //закрываем экран конца игры
        endGameDisplay.SetActive(false);
    }

    private void CoinCounterRefresh()
    {
        //обнуляем счетчик монет
        coinCounter = 0;
        coinCounterText.text = coinCounter.ToString();
    }

    private void CoinRefresh()
    {
        //активируем собранные монеты
        foreach (GameObject coin in coins)
            if (coin.activeSelf == false)
                coin.SetActive(true);
    }

    private void PlayerRefresh()
    {
        //очищаем очередь позиций, ставим игрока на начальную точку и активируем
        player.GetComponent<Player>().ClearMovePoints();
        player.transform.position = playerStartPosition;
        player.SetActive(true);

        player.GetComponentInChildren<LineDraw>().LineReset();
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void ViewEndGameDisplay()
    {
        if (coinCounter < coins.Length)
            endGameDisplay.GetComponentInChildren<Text>().text = "You lose!";
        
        if (player.activeSelf)
            player.SetActive(false);
        
        endGameDisplay.SetActive(true);
    }
}
