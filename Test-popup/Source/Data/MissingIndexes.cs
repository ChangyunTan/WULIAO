using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication2.Data
{
   public class MissingIndexesTypeDto
   {
      [DataMember]
      public IEnumerable<MissingIndexGroupTypeDto> MissingIndexGroup { get; set; }
   }

   public class MissingIndexGroupTypeDto
   {
      [DataMember]
      public double Impact { get; set; }
      [DataMember]
      public IEnumerable<MissingIndexTypeDto> MissingIndex { get; set; }
   }
   public class MissingIndexTypeDto
   {
      [DataMember]
      public string Database { get; set; }
      [DataMember]
      public string Schema { get; set; }
      [DataMember]
      public string Table { get; set; }

      [DataMember]
      public IEnumerable<ColumnGroupTypeDto> ColumnGroup { get; set; }
   }

   public class ColumnGroupTypeDto
   {
      [DataMember]
      public UsageType Usage { get; set; }
      [DataMember]
      public IEnumerable<ColumnTypeDto> ColumnList { get; set; }
   }

   public class ColumnTypeDto
   {
      [DataMember]
      public string Name { get; set; }
      [DataMember]
      public int ColumnId { get; set; }
   }

}
