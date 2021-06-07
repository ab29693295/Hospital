using Smart.Entity;
namespace Smart.Data
{

    public partial class DOperationRecord :GenericRepository<OperationRecord>
	{
		SmartContext db;
		public DOperationRecord(SmartContext DbContext)
			: base(DbContext)
		{
			db = DbContext;
		}
	}
}
 