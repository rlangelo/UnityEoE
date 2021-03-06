﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    public GameObject m_dirtPrefab;
    public bool m_OnCross; //true if box has been pushed on to a cross

	public bool Move(Vector2 direction)//Avoid ability to move diagonally
    {
        if (BoxBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);//Box not blocked so move it
            TestForOnBomb();
            TestForOnWater();
            return true;
        }
    }


    public bool BoxBlocked(Vector3 position, Vector2 direction) //Boxes blocked by other boxes and walls
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] gates = GameObject.FindGameObjectsWithTag("Gate");
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        GameObject[] dirts = GameObject.FindGameObjectsWithTag("Dirt");
        foreach (var wall in walls)
        {
            if (wall.transform.position.x == newPos.x && wall.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        foreach (var box in boxes)
        {
            if (box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        foreach (var gate in gates)
        {
            if (gate.transform.position.x == newPos.x && gate.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        foreach (var dirt in dirts)
        {
            if (dirt.transform.position.x == newPos.x && dirt.transform.position.y == newPos.y)
            {
                return true;
            }
        }
        return false;
    }

    void TestForOnCross()
    {
        GameObject[] crosses = GameObject.FindGameObjectsWithTag("Cross");
        foreach (var cross in crosses)
        {
            if (transform.position.x == cross.transform.position.x && transform.position.y == cross.transform.position.y)
            {
                //On a cross
                GetComponent<SpriteRenderer>().color = Color.yellow;
                m_OnCross = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        m_OnCross = false;
    }

    void TestForOnBomb()
    {
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (var bomb in bombs)
        {
            if (transform.position.x == bomb.transform.position.x && transform.position.y == bomb.transform.position.y)
            {
                //On a bomb
                Destroy(bomb);
                Destroy(this.gameObject);
                return;
            }
        }
        return;
    }

    void TestForOnWater()
    {
        GameObject[] waters = GameObject.FindGameObjectsWithTag("Water");
        foreach (var water in waters)
        {
            if (transform.position.x == water.transform.position.x && transform.position.y == water.transform.position.y)
            {
                //On water
                Destroy(water);
                Destroy(this.gameObject);
                Instantiate(m_dirtPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                return;
            }
        }
    }

    public bool IsOnPlate()
    {
        GameObject[] plates = GameObject.FindGameObjectsWithTag("Plate");
        foreach (var plate in plates)
        {
            if (transform.position.x == plate.transform.position.x && transform.position.y == plate.transform.position.y)
            {
                return true;
            }
        }
        return false;
    }
}
