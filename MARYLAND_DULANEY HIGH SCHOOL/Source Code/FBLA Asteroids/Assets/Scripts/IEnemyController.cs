using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController { //interface
    void OnSpawn(Vector2 startPos, string answer, int health, bool isAnswer);
}
