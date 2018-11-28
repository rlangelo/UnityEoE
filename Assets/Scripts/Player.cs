using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool p_OnCross;
    public bool p_OnBomb;

    public bool Move(Vector2 direction) //Avoid ability to move diagonally
    {
        if (Mathf.Abs(direction.x) < 0.5) //Will always set one of coordinates to 0
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize(); //Makes eitehr x or y = 1
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            TestForPlayerOnGoal();
            TestForPlayerOnBomb();
            return true;
        }
    }

    bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        GameObject[] gates = GameObject.FindGameObjectsWithTag("Gate");
        foreach (var gate in gates)
        {
            if (gate.transform.position.x == newPos.x && gate.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        return false;
    }

    void TestForPlayerOnGoal()
    {
        GameObject[] crosses = GameObject.FindGameObjectsWithTag("Cross");
        foreach (var cross in crosses)
        {
            if (transform.position.x == cross.transform.position.x && transform.position.y == cross.transform.position.y)
            {
                //On a cross
                GetComponent<SpriteRenderer>().color = Color.yellow;
                p_OnCross = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        p_OnCross = false;
    }

    void TestForPlayerOnBomb()
    {
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (var bomb in bombs)
        {
            if (transform.position.x == bomb.transform.position.x && transform.position.y == bomb.transform.position.y)
            {
                //om bomb
                GetComponent<SpriteRenderer>().color = Color.red;
                p_OnBomb = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        p_OnBomb = false;
    }

}
