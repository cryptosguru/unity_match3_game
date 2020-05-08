using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loafwad.Ulto
{
    public class CustomTile : MonoBehaviour
    {
        [SerializeField] private bool _isBlocked = false;
        private Animal _animal = null;

        public bool IsBlocked
        {
            get
            {
                return _isBlocked;
            }
        }

        public Animal Animal
        {
            get
            {
                return _animal;
            }
            set
            {
                _animal = value;
            }
        }
    }
}