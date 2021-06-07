using Smart.Entity;
namespace Smart.Data
{

    public partial class DOperationVideo :GenericRepository<OperationVideo>
	{
		SmartContext db;
		public DOperationVideo(SmartContext DbContext)
			: base(DbContext)
		{
			db = DbContext;
		}
	}
}
 