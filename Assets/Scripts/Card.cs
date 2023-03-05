using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string Label { get; set; }
    public bool IsFaceUp => isFaceUp;

    [SerializeField]
    private SpriteRenderer face;
    [SerializeField]
    private SpriteRenderer back;

    [SerializeField]
    private bool isFaceUp;
    static private float rotationFaceUp = 0f;
    static private float rotationFaceDown = 180f;

    [SerializeField]
    private bool isRotating;
    private Vector3 startAngle;
    private Vector3 targetAngle;
    private float startTime;
    private float duration;

    public SpriteRenderer Face => face;
    public SpriteRenderer Back => back;

    public void MakeFaceUp(float duration = 1f)
    {
        if (!isFaceUp)
        {
            isFaceUp = true;
            StartRotate(rotationFaceUp, duration);
        }
    }

    public void MakeFaceDown(float duration = 1f)
    {
        if (isFaceUp)
        {
            isFaceUp = false;
            StartRotate(rotationFaceDown, duration);
        }
    }

    private void StartRotate(float targetAngle, float duration)
    {
        if (!isRotating)
        {
            isRotating = true;
            startAngle = this.transform.rotation.eulerAngles;
            this.targetAngle = new Vector3(0, targetAngle, 0);
            startTime = Time.time;
            this.duration = duration;
        }
    }

    void Awake()
    {
        //To make all cards facing down at the start
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));   
    }

    void Update()
    {
        if (isRotating)
        {
            float u = (Time.time - startTime) / duration;
            if (u >= 1)
            {
                isRotating = false;
                u = 1;
            }
            Vector3 rotation = this.transform.rotation.eulerAngles;
            rotation = Vector3.Lerp(startAngle, targetAngle, u);
            this.transform.rotation = Quaternion.Euler(rotation);
        }
    }

    private void OnMouseDown()
    {
        if (!isFaceUp && !Game.S.IsInputBlocked)
        {
            MakeFaceUp();
            Game.S.CardClicked(this);
        }
    }
}
