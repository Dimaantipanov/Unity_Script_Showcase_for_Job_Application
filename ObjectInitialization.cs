// Изготовитель скрипта: Антипанов Дмитрий
// Назначение: Инициализация и установка ссылок на основные компоненты (Camera, Goal, ObjectContainer) для ObjectCreator и ConversionObject.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD;

namespace LD
{
    public class ObjectInitialization : MonoBehaviour
    {
        public Transform Camera;
        public Transform Goal;
        public Transform ObjectContainer;
        public GameObject[] NullObject;

        // Метод инициализации и передачи ссылок компонентам
        public void Initialization()
        {
            var ObjectCreatorComp = gameObject.GetComponent<ObjectCreator>();
            ObjectCreatorComp.Camera = Camera;
            ObjectCreatorComp.Goal = Goal;
            ObjectCreatorComp.ObjectContainer = ObjectContainer;

            var ConvObject = gameObject.GetComponent<ConversionObject>();
            ConvObject.NullObject = NullObject;
        }
    }
}
