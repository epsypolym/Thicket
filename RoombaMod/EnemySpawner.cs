using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Thicket
{
    [DefaultExecutionOrder(-100)]
    class EnemySpawner : MonoBehaviour
    {
        public EnemyEnum enemyVar;

        public enum EnemyEnum
        {
            Filth, Stray, Schism, Soldier, BigMinos, Stalker, Sisyphus, Swordsmachine, Drone, Streetcleaner, V2, Mindflayer, MaliciousFace, Cerberus, HideousMass, Gabriel, Virtue, SomethingWicked, FleshPrison, MinosPrime
        }
        void Awake() {
            var joe = GameObject.Instantiate(SceneConstructor.bestiary[(int)enemyVar].gameObject);
            joe.SetActive(false);
            // unneeded
            joe.transform.parent = transform.parent;
            /*joe.transform.localPosition = transform.localPosition;
            joe.transform.localRotation = transform.localRotation;*/
            
            //gameObject.SetActive(false);
            
            // get list of all components
            var components = GetComponents<Component>();
            
            foreach(Component component in components) {
                if (component is EnemySpawner) continue;
                Component c;
                if(!joe.TryGetComponent(component.GetType(), out c)) {
                    Debug.Log("Component not found: " + component.GetType());
                    c = joe.AddComponent(component.GetType());
                }

                if (c is Behaviour) {
                    Behaviour b = (Behaviour) c;
                    //b.enabled = ((Behaviour) component).enabled;
                    b.enabled = true;
                }
                // set all fields to match
                var fields = component.GetType().GetFields();
                foreach(var field in fields) {
                    field.SetValue(c, field.GetValue(component));
                }
                Debug.Log("Fields set for " + component.GetType());
            }
            Debug.Log("Stupid shit");
            Debug.Log($"{BossBarManager.Instance == null} {BossBarManager.Instance.GetPrivate<BossHealthBarTemplate>("template") == null}");

            joe.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
