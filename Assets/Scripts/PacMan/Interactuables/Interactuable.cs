using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.PacMan
{
    public abstract class Interactuable : MonoBehaviour
    {
        protected abstract string[] CollidableTags { get; }

        protected abstract void EnterTrigger(Character character);

        protected static readonly Dictionary<GameObject, Character> characters = new();


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (CheckColision(collider))
            {
                var charAux = AddCharacter(collider);

                EnterTrigger(charAux);
            }
        }

        private Character AddCharacter(Collider2D collider)
        {
            var goAux = collider.transform.parent.gameObject;

            if (!characters.ContainsKey(goAux))
            {
                characters.Add(goAux, goAux.GetComponent<Character>());
            }

            return characters[goAux];
        }

        protected bool CheckColision(Collider2D collider)
        {
            for (int i = 0; i < CollidableTags.Length; ++i)
            {
                if (collider.CompareTag(CollidableTags[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}