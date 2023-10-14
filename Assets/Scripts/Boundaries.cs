using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 
            Camera.main.transform.position.y));
        objectWidth = transform.GetComponentInChildren<SpriteRenderer>().bounds.size.x / 4;
        objectHeight = transform.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 4;
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        transform.position = viewPos;
    }
}
