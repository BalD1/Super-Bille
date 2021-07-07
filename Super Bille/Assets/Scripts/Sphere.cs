using UnityEngine;

public class Sphere : MonoBehaviour
{
    public float timer;
    float resetTimer;
    float time = 1;
    bool falling = false;

    private void Awake()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameStates.InGame);
    }
    private void Start()
    {
        resetTimer = timer;
    }
    private void Update()
    {
        GameManager.Instance.AddScore(transform.position.magnitude);
        if(falling)
        {
            timer -= time * Time.deltaTime;
            if (timer < 0)
            {
                GameManager.Instance.ChangeGameState(GameManager.GameStates.GameOver);
                this.enabled = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
            falling = false;
            timer = resetTimer;
        
    }
    private void OnCollisionExit(Collision collision)
    {
        falling = true;
    }
}
