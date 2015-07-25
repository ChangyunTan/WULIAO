using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WpfApplication2.Data
{
    [DataContract] 
     public class OrderByTypeDto 
     { 
         [DataMember] 
         public IEnumerable<OrderByColumnTypeDto> OrderByColumn { get; set; } 
     } 

   [DataContract] 
    public class OrderByColumnTypeDto 
    { 
        [DataMember] 
        public bool Ascending { get; set; } 
        [DataMember] 
        public ColumnReferenceTypeDto ColumnReference { get; set; } 
    } 
     [DataContract] 
     public class ColumnReferenceTypeDto 
     { 
         private const string C_ObjectSeparator = "."; 
         [DataMember] 
         public ScalarTypeDto ScalarOperator { get; set; } 
         [DataMember] 
         [Description("Internal Debugging Information")] 
         public string InternalInfo { get; set; } 
         [DataMember] 
         public string Server { get; set; } 
         [DataMember] 
         public string Database { get; set; } 
         [DataMember] 
         public string Schema { get; set; } 
         [DataMember] 
         public string Table { get; set; } 
         [DataMember] 
         public string Alias { get; set; } 
         [DataMember] 
         public string Column { get; set; } 
         [DataMember] 
         public bool? ComputedColumn { get; set; } 
         [DataMember] 
         public string ParameterCompiledValue { get; set; } 
         [DataMember] 
         public string ParameterRuntimeValue { get; set; } 
  
         public override string ToString() 
         { 
             // Join the object full name. "[Server].[Database].[Schema].[Table].[Column]" 
             var nameList = new[] { Server, Database, Schema, Table, Column }; 
             var nonEmptyList = nameList.All(s => string.IsNullOrEmpty(s) == false); 
             var objectFullName = string.Join(C_ObjectSeparator, nameList.Where(s => string.IsNullOrEmpty(s) == false).ToArray()); 
  
             return objectFullName; 
         } 
     } 
       [DataContract] 
    public class ScalarTypeDto 
    { 
        [DataMember] 
        public string InternalInfo { get; set; } 
        [DataMember] 
        public string ScalarString { get; set; } 
        
    } 

}
