using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableObject : MonoBehaviour
{
   public virtual BaseSave Serialize()
   {
       return new BaseSave();
   }

   public virtual void Deserialize(BaseSave save)
   {
       
   }
}
