using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Item))]
public class DraggableItem : MonoBehaviour
{
    bool isSelected = false;
    Vector2 mousePos;
    Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        if(!GetComponent<Item>().burning&&Item.canClick()){
            isSelected = true;
            if (rb)
            {
                rb.gravityScale = 0.0f;
            }
        }
    }

    private void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }

    private void OnMouseUp()
    {
        if(!GetComponent<Item>().burning&&Item.canClick()){
            isSelected = false;
            if(rb)
            {
                rb.gravityScale = 0.5f;
            }

            // Set velocity to zero so that it removes any force once clicked.
            rb.velocity = Vector2.zero;

            Vector2 throwForce = mousePos - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            throwForce = Vector2.ClampMagnitude(throwForce, 6f);

            rb.AddForce(throwForce * 100);
            Debug.Log(throwForce.magnitude);
        }
    }

    private void FixedUpdate()
    {
        if (isSelected)
        {
            float step = 15 * Time.deltaTime; // calculate distance to move
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, mousePos, step);
        }

        if (gameObject.transform.position.y < -10 || gameObject.transform.position.y > 10)
        {
            rb.velocity = Vector2.zero;
            gameObject.transform.position = Vector3.zero;
        }
    }

    public bool ReturnIsSelected(){
        return isSelected;
    }
}
