using Smart.Entity;
namespace Smart.Data
{

    public partial class DOperationImage :GenericRepository<OperationImage>
	{
		SmartContext db;
		public DOperationImage(SmartContext DbContext)
			: base(DbContext)
		{
			db = DbContext;
		}
	}
}
 