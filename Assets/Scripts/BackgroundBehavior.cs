using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{
    [SerializeField] private float _bgSpeed;
    private Vector2 _offset;
    private Material _material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _offset = new Vector2(0, _bgSpeed * Time.deltaTime);
        _material.mainTextureOffset += _offset;

        if (_material.mainTextureOffset.y >= 1)
        {
            _material.mainTextureOffset -= new Vector2(0, 1);
        }
    }
}
