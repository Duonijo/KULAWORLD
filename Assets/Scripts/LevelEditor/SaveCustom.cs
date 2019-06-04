using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
   [System.Serializable]
   public class SaveCustom
   {
      private List<(string,float, float,float)> _saveList;

      public List<(string, float, float, float)> SaveList
      {
         get => _saveList;
         set => _saveList = value;
      }
   }
}