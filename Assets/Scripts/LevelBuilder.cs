﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement //defines each item in a level by mapping a single character (ie #) to a prefab
{
    public string m_Character;
    public GameObject m_Prefab;
}



public class LevelBuilder : MonoBehaviour {

    public int m_CurrentLevel;
    public List<LevelElement> m_LevelElements;
    private Level m_Level;
    public int m_numberOfPlates;
    public Camera m_OrthographicCamera;

    public GameObject GetPrefab(char c)
    {
        LevelElement levelElement = m_LevelElements.Find(le => le.m_Character == c.ToString());
        if (levelElement != null)
        {
            return levelElement.m_Prefab;
        } else
        {
            return null;
        }
    }

    public void NextLevel()
    {
        m_CurrentLevel++;
        if (m_CurrentLevel >= GetComponent<Levels>().m_Levels.Count)
        {
            m_CurrentLevel = 0; //Wrap back to first level
        }
    }

    public void Build()
    {
        m_Level = GetComponent<Levels>().m_Levels[m_CurrentLevel];
        m_numberOfPlates = 0;
        //Offset coordinates so that center of level is roughly at 0,0
        int startx = -m_Level.Width / 2; //Save start x since needs to be reset in loop
        int x = startx;
        int y = m_Level.Height / 2;
        AdjustCameraSize(m_Level);
        foreach (var row in m_Level.m_Rows)
        {
            foreach (var ch in row)
            {
                Debug.Log(ch);
                if (ch.ToString() == "o")
                {
                    m_numberOfPlates++;
                }
                GameObject prefab = GetPrefab(ch);
                if (prefab)
                {
                    Debug.Log(prefab.name);
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                x++;
            }
            y--;
            x = startx;
        }
    }

    public int getNumberOfPlates()
    {
        return m_numberOfPlates;
    }

    public void AdjustCameraSize(Level p_Level)
    {
        if (p_Level.Height >= 12 && p_Level.Height < 20)
        {
            m_OrthographicCamera.orthographicSize = 10.0f;
        }
        else if (p_Level.Height > 20)
        {
            m_OrthographicCamera.orthographicSize = 13.0f;
        }
        else
        {
            m_OrthographicCamera.orthographicSize = 6.0f;
        }
        if (p_Level.Width >= 20 && p_Level.Height < 12)
        {
            m_OrthographicCamera.orthographicSize = 10.0f;
        }
    }
	
}
