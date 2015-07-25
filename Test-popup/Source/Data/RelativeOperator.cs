using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WpfApplication2.Data
{
   public class RelativeOperatoData
   {
      public RelativeOperator Data{get; set;}
      public RelativeOperatoData()
      {
         Data = new RelativeOperator()
           {
              CursorName = "Unfortunatly, there is functionality of a Popup that a Window doesn't have and is more important to me than freedom of motion. I guess I will have to restrict the movement so there is no chance of going beyond the screen bounds and thus no repositioning and no flickering. Thanks for the info",
              EstimatedCost = 5.5,
              EstimatedCostPercentage = 0.6,
              EstimatedCPU = 6.6,
              EstimatedCPUPercentage = 0.8,
              EstimatedTotalSubtreeCPU = 500,
              EstimatedTotalSubtreeCPUPercentage = 1,
              IsActualPlan = true,
              MissingIndexes = new MissingIndexesTypeDto()
              {
                 MissingIndexGroup = missingIndexesTypeDto()
              },
              NonParallelPlanReason = "NonParallelPlanReason11111",
              Operator = "Join",
              OrderBy = new OrderByTypeDto()
              {
                 OrderByColumn = new List<OrderByColumnTypeDto>() {
                 new OrderByColumnTypeDto(){ Ascending = true,  ColumnReference = new ColumnReferenceTypeDto(){ Alias = "dbo", Column = "emp_id", ComputedColumn=true, Database="dbo", Schema="sqlexp", Server=".", Table="emp" }} ,
                 new OrderByColumnTypeDto(){ Ascending = false,  ColumnReference = new ColumnReferenceTypeDto(){ Alias = "dbo", Column = "emp_name", ComputedColumn=true, Database="dbo", Schema="sqlexp", Server=".", Table="emp" }} 
              }
              }
           };

      }

      List<MissingIndexGroupTypeDto> missingIndexesTypeDto()
        {
            List<MissingIndexGroupTypeDto> dto = new List<MissingIndexGroupTypeDto>();
            List<MissingIndexTypeDto> index = new List<MissingIndexTypeDto>(){ 
                  new MissingIndexTypeDto(){ 
                      Database = "sqlexp", 
                      Schema="dbo", Table="EMP" , 
                      ColumnGroup = new List<ColumnGroupTypeDto>(){ 
                                         new ColumnGroupTypeDto(){ 
                                             Usage = UsageType.EQUALITY, 
                                             ColumnList = new List<ColumnTypeDto>(){
                                                 new ColumnTypeDto(){ 
                                                     ColumnId = 1, 
                                                     Name = "EMP_ID"}, 
                                                 new ColumnTypeDto(){ 
                                                     ColumnId = 2, 
                                                     Name = "EMP_Name"} } },
                     new ColumnGroupTypeDto(){ Usage = UsageType.INCLUDE, ColumnList = new List<ColumnTypeDto>(){new ColumnTypeDto(){ ColumnId = 1, Name = "EMP_Name"}, new ColumnTypeDto(){ ColumnId = 2, Name = "EMP_Gorop"} } }
                 }},
                 new MissingIndexTypeDto(){ Database = "sqlexp1", Schema="dbo1", Table="EMP1" , ColumnGroup = new List<ColumnGroupTypeDto>(){ 
                     new ColumnGroupTypeDto(){ Usage = UsageType.EQUALITY, ColumnList = new List<ColumnTypeDto>(){new ColumnTypeDto(){ ColumnId = 1, Name = "EMP_ID"}, new ColumnTypeDto(){ ColumnId = 2, Name = "EMP_Name"} } },
                     new ColumnGroupTypeDto(){ Usage = UsageType.INCLUDE, ColumnList = new List<ColumnTypeDto>(){new ColumnTypeDto(){ ColumnId = 1, Name = "EMP_Name"}, new ColumnTypeDto(){ ColumnId = 2, Name = "EMP_Gorop"} } },
                     new ColumnGroupTypeDto(){ Usage = UsageType.INEQUALITY, ColumnList = new List<ColumnTypeDto>(){new ColumnTypeDto(){ ColumnId = 3, Name = "EMP_Age"}} }
                 }}
          };
           dto.Add(new MissingIndexGroupTypeDto() { Impact = 12, MissingIndex = index });
            List<MissingIndexTypeDto> index2 = new List<MissingIndexTypeDto>(){ 
           new MissingIndexTypeDto(){ Database = "sqlexp2", Schema="dbo2", Table="EMP2" , ColumnGroup = new List<ColumnGroupTypeDto>(){ 
               new ColumnGroupTypeDto(){ Usage = UsageType.INEQUALITY, ColumnList = new List<ColumnTypeDto>(){new ColumnTypeDto(){ ColumnId = 3, Name = "EMP_Age"}} }
           }}
          };
            dto.Add(new MissingIndexGroupTypeDto() { Impact = 15, MissingIndex = index2 });
            return dto;
        }
   }
   public sealed class RelativeOperator
   {
      public RelativeOperator()
      {
      }
      [DataMember]
      public object Key { get; set; }
      [DataMember]
      public string Operator { get; set; }

      #region RelOpType

      #region actual plan
      [DataMember]
      [Description("Is Actual Plan")]
      public bool IsActualPlan { get; set; }

      [DataMember]
      [Description("Cursor Name")]
      public string CursorName { get; set; }
      #endregion actual plan

      #region estimated plan
      [DataMember]
      [Description("Estimated Operator Cost")]
      public double EstimatedCost { get; set; }

      [DataMember]
      [Description("Estimated Operator Cost Percentage")]
      public double EstimatedCostPercentage { get; set; }

      [DataMember]
      [Description("Estimated CPU Cost")]
      public double EstimatedCPU { get; set; }

      [DataMember]
      [Description("Estimated CPU Cost Percentage")]
      public double EstimatedCPUPercentage { get; set; }

      [DataMember]
      [Description("Estimated Operator Subtree CPU")]
      public double EstimatedTotalSubtreeCPU { get; set; }
      [DataMember]
      [Description("Estimated Operator Subtree CPU Percentage")]
      public double EstimatedTotalSubtreeCPUPercentage { get; set; }

      #endregion estimated plan

      #endregion



      [DataMember]
      public MissingIndexesTypeDto MissingIndexes { get; set; }

      [DataMember]
      public string NonParallelPlanReason { get; set; }

      [DataMember] 
      [Description("Order By")]          
      public OrderByTypeDto OrderBy { get; set; } 

      [DataMember] 
      public ColumnReferenceListTypeDto GroupBy { get; set; } 

      [DataMember] 
      [Description("Parameter List")] 
       public ColumnReferenceListTypeDto ParameterList { get; set; } 
  
         [DataMember] 
         [Description("Parameter List")] 
         public ScalarExpressionListTypeDto ParameterListSE { get; set; } 

         [DataMember] 
         [Description("Action Column")] 
         public SingleColumnReferenceTypeDto ActionColumn { get; set; } 
   }
   [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
   public sealed class DataMemberAttribute : Attribute
   {
      // Summary:
      //     Initializes a new instance of the System.Runtime.Serialization.DataMemberAttribute
      //     class.

      // Summary:
      //     Gets or sets a value that specifies whether to serialize the default value
      //     for a field or property being serialized.
      //
      // Returns:
      //     true if the default value for a member should be generated in the serialization
      //     stream; otherwise, false. The default is true.
      public bool EmitDefaultValue { get; set; }
      //
      // Summary:
      //     Gets or sets a value that instructs the serialization engine that the member
      //     must be present when reading or deserializing.
      //
      // Returns:
      //     true, if the member is required; otherwise, false.
      //
      // Exceptions:
      //   System.Runtime.Serialization.SerializationException:
      //     the member is not present.
      public bool IsRequired { get; set; }
      //
      // Summary:
      //     Gets or sets a data member name.
      //
      // Returns:
      //     The name of the data member. The default is the name of the target that the
      //     attribute is applied to.
      public string Name { get; set; }
      //
      // Summary:
      //     Gets or sets the order of serialization and deserialization of a member.
      //
      // Returns:
      //     The numeric order of serialization or deserialization.
      public int Order { get; set; }
   }

}
