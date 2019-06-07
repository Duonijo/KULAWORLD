using System.Collections.Generic;

namespace LevelEditor
{
   [System.Serializable]
   public class SaveCustom
   {
      private List<(string,float,float,float,float,float,float,string,float,float,float)> _saveList;
      //name,XYZ,XYZ,link, XYZ
      public List<(string,float,float,float,float,float,float,string,float,float,float)> SaveList
      {
         get => _saveList;
         set => _saveList = value;
      }
      
      private List<(int, string, long)> _highScore;

      public List<(int, string, long)> HighScore
      {
         get => _highScore;
         set => _highScore = value;
      }
   }
}