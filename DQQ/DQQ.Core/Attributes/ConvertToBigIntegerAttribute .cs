using System.ComponentModel.DataAnnotations.Schema;

namespace DQQ.Attributes
{
  [AttributeUsage(AttributeTargets.Property)]
  public class ConvertToBigIntegerAttribute : ColumnAttribute
  {
    public ConvertToBigIntegerAttribute() : base()
    {
      base.TypeName = "varchar(max)"; // Set the column type as varchar(max)
    }
  }
}
