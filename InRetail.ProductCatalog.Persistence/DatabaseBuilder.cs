namespace InRetail.ProductCatalog.Persistence
{
    public class DatabaseBuilder : IDatabaseBuilder
    {

        private readonly IUnitOfWork _unitOfWork;

        public DatabaseBuilder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void RebuildDatabase()
        {
            CreateInitialData();
        }

        private void CreateInitialData()
        {
            _unitOfWork.Commit();
        }
    }
}