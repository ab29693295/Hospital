using Smart.Entity;
namespace Smart.Data
{

    public partial class DPatient :GenericRepository<Patient>
	{
		SmartContext db;
		public DPatient(SmartContext DbContext)
			: base(DbContext)
		{
			db = DbContext;
		}
	}
}
 