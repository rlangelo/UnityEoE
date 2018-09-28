using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool Move(Vector2 direction)
    {
        return true;
    }

    bool Blocked(Vector3 position, Vector2 direction)
    {
        return false;
    }
}
