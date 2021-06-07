using Smart.Entity;
namespace Smart.Data
{

    public partial class DDepartment :GenericRepository<Department>
	{
		SmartContext db;
		public DDepartment(SmartContext DbContext)
			: base(DbContext)
		{
			db = DbContext;
		}
	}
}
 