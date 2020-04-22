using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasScript : MonoBehaviour
{
    [System.Serializable]
    public struct GameplayCamera
    {
        public GameObject camera;
        public Direction direction;
    }
    public List<GameplayCamera> gameplayCameras = new List<GameplayCamera>();
    [SerializeField] GameObject introCamera;
    [SerializeField] GetClues getClues;
    [SerializeField] GameObject rightArrow;
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject upArrow;
    [SerializeField] GameObject downArrow;

    [SerializeField] float swipePercentage;
    float swipeHorizontalDistance;
    float swipeVerticalDistance;
    Vector2 swipeStart;
    Vector2 swipeEnd;
    Direction swipeDirection;

    // Start is called before the first frame update
    void Start()
    {
        swipeHorizontalDistance = (swipePercentage * Screen.width)/100f;
        swipeVerticalDistance = (swipePercentage * Screen.height)/100f;
        foreach(GameplayCamera gameplayCamera in gameplayCameras)
        {
            gameplayCamera.camera.SetActive(false);
        }
        StartCoroutine(Intro());
    }

    // Update is called once per frame
    void Update()
    {
        if(getClues.clueIsZoomed == false && Input.touchCount == 2)
        {
            CheckForSwipe();
        }
    }

    void CheckForSwipe()
    {
        Touch touch = Input.touches[0];

        if(touch.phase == TouchPhase.Began)
        {
            swipeStart = touch.position;
            swipeEnd = touch.position;
        }
        else if(touch.phase == TouchPhase.Moved)
        {
            swipeEnd = touch.position;
            Vector2 swipe = swipeEnd - swipeStart;
            if(Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
            {
                if(swipe.magnitude >= swipeHorizontalDistance){
                    if(swipe.x > 0)
                    {
                        swipeDirection = Direction.Left;
                    }
                    else if(swipe.x < 0)
                    {
                        swipeDirection = Direction.Right;
                    }
                    MoveToCam(swipeDirection);
                }
            }
            else if(Mathf.Abs(swipe.y) > Mathf.Abs(swipe.x))
            {
                if(swipe.magnitude >= swipeVerticalDistance){
                    if(swipe.y > 0)
                    {
                        swipeDirection = Direction.Down;
                    }
                    else if(swipe.y < 0)
                    {
                        swipeDirection = Direction.Up;
                    }
                    MoveToCam(swipeDirection);
                }
            }
        }
    }

    void MoveToCam(Direction direction)
    {
        switch(direction)
        {
            case Direction.Right:
                foreach(GameplayCamera gameplayCamera in gameplayCameras)
                {
                    gameplayCamera.camera.SetActive(false);
                    if(gameplayCamera.direction == Direction.Right)
                    {
                        gameplayCamera.camera.SetActive(true);
                    }
                }
            break;

            case Direction.Left:
                foreach(GameplayCamera gameplayCamera in gameplayCameras)
                {
                    gameplayCamera.camera.SetActive(false);
                    if(gameplayCamera.direction == Direction.Left)
                    {
                        gameplayCamera.camera.SetActive(true);
                    }
                }
            break;

            case Direction.Up:
                foreach(GameplayCamera gameplayCamera in gameplayCameras)
                {
                    gameplayCamera.camera.SetActive(false);
                    if(gameplayCamera.direction == Direction.Up)
                    {
                        gameplayCamera.camera.SetActive(true);
                    }
                }
            break;

            case Direction.Down:
                foreach(GameplayCamera gameplayCamera in gameplayCameras)
                {
                    gameplayCamera.camera.SetActive(false);
                    if(gameplayCamera.direction == Direction.Down)
                    {
                        gameplayCamera.camera.SetActive(true);
                    }
                }
            break;
        }
        UpdateArrows();
        StartCoroutine(BlockZoom());
    }

    public void CameraButton(int directionIndex)
    {
        switch(directionIndex)
        {
            case 0:
                MoveToCam(Direction.Right);
            return;

            case 1:
                MoveToCam(Direction.Left);
            return;

            case 2:
                MoveToCam(Direction.Up);
            return;

            case 3:
                MoveToCam(Direction.Down);
            return;
        }
    }
    
    void UpdateArrows()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
        upArrow.SetActive(false);
        downArrow.SetActive(false);

        foreach(GameplayCamera gameplayCamera in gameplayCameras)
        {
            if(gameplayCamera.camera.activeSelf == false)
            {
                switch(gameplayCamera.direction)
                {
                    case Direction.Right:
                        rightArrow.SetActive(true);
                    return;

                    case Direction.Left:
                        leftArrow.SetActive(true);
                    return;

                    case Direction.Up:
                        upArrow.SetActive(true);
                    return;

                    case Direction.Down:
                        downArrow.SetActive(true);
                    return;
                }
            }
        }
    }

    IEnumerator Intro()
    {
        introCamera.SetActive(true);
        yield return new WaitForSeconds(.5f);
        introCamera.SetActive(false);
        gameplayCameras[0].camera.SetActive(true);
        UpdateArrows();
    }

    IEnumerator BlockZoom()
    {
        getClues.canZoom = false;
        yield return new WaitForSeconds(1f);
        getClues.canZoom = true;
    }
}

public enum Direction
{
    Right, Left, Up, Down
}
