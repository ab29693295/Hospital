using Smart.Entity;
namespace Smart.Data
{

    public partial class DSmartContext :GenericRepository<SmartContext>
	{
		SmartContext db;
		public DSmartContext(SmartContext DbContext)
			: base(DbContext)
		{
			db = DbContext;
		}
	}
}
 