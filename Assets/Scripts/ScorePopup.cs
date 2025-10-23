using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] TextMeshPro textMesh;
    [SerializeField] float moveUpSpeed = 1f;
    [SerializeField] float fadeSpeed = 2f;
    [SerializeField] float lifetime = 1.5f;

    private Color textColor;
    private float timer = 0f;

    void Start()
    {
        if (textMesh == null)
            textMesh = GetComponent<TextMeshPro>();

        textColor = textMesh.color;
    }

    public void ShowPopup(string text)
    {
        textMesh.text = text;
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;

        textColor.a -= fadeSpeed * Time.deltaTime;
        textMesh.color = textColor;

        if (timer >= lifetime || textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
