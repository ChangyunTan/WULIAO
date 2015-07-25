using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication2.Data
{
  [DataContract] 
   public class ColumnReferenceListTypeDto 
    { 
       [DataMember] 
       public IEnumerable<ColumnReferenceTypeDto> ColumnReference { get; set; } 
    } 

   [DataContract] 
    public class ScalarExpressionListTypeDto 
    { 
        [DataMember] 
        public IEnumerable<ScalarTypeDto> ScalarOperator { get; set; } 
    } 

   [DataContract] 
     public class SingleColumnReferenceTypeDto 
     { 
         [DataMember] 
         public IEnumerable<ColumnReferenceTypeDto> ColumnReference { get; set; } 
     } 

}
