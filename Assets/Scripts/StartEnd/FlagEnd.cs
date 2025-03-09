using UnityEngine;

public class FlagEnd : MonoBehaviour
{
    public GameObject gameManagerGO;

    void OnTriggerEnter2D(Collider2D collided) {
        gameManagerGO.GetComponent<GameManager>().onEndTriggerEntered(collided.gameObject);
    }
}
