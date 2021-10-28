using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Thicket
{
    class EnemySpawner : MonoBehaviour
    {
        public EnemyEnum enemyVar;

        public enum EnemyEnum
        {
            Filth, Stray, Schism, Soldier, BigMinos, Stalker, Sisyphus, Swordsmachine, Drone, Streetcleaner, V2, Mindflayer, MaliciousFace, Cerberus, HideousMass, Gabriel, Virtue, SomethingWicked, FleshPrison, MinosPrime
        }
        
        void Start() 
        {
            var joe = GameObject.Instantiate(SceneConstructor.bestiary[(int)enemyVar].gameObject);
            joe.transform.parent = transform.parent;
            joe.transform.localPosition = transform.localPosition;
            joe.transform.localRotation = transform.localRotation;
        }

    }
}
