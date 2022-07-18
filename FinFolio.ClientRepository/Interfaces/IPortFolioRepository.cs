using FinFolio.PortFolioRepository.Entities;

namespace FinFolio.PortFolioRepository.Interfaces
{
    public interface IPortFolioRepository
    {
        public PortFolio GetPortFolioById(int id);
        public PortFolio GetPortFolioByUserId(int userId);
        public PortFolio CreatePortFolio(PortFolio portFolio);
        public PortFolio UpdatePortFolio(PortFolio portFolio);
    }
}
