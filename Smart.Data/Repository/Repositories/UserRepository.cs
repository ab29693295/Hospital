using Smart.Entity;
namespace Smart.Data
{

    public partial class DUser :GenericRepository<User>
	{
		SmartContext db;
		public DUser(SmartContext DbContext)
			: base(DbContext)
		{
			db = DbContext;
		}
	}
}
 