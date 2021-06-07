using Smart.Entity;
namespace Smart.Data
{

    public partial class DDoctor :GenericRepository<Doctor>
	{
		SmartContext db;
		public DDoctor(SmartContext DbContext)
			: base(DbContext)
		{
			db = DbContext;
		}
	}
}
 