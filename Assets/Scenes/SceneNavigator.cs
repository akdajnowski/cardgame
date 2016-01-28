using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{

    public Object cardBattleScene;

    public void NavigateToCardBattle()
    {
        SceneManager.LoadScene(cardBattleScene.name);
    }
}
