namespace Kallithea_NET_API
{
   /// <summary>
   /// Data structure used by:
   /// 
   /// get_repo
   /// </summary>
   public class Repository_Changeset
   {
      public string author;
      public string date;
      public string message;
      public int raw_id;
      public int revision;
      public int short_id;
   }
}