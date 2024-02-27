using AutoMapper;
using Contracts;
using Service.Repo.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repo
{
    internal sealed class HighPurityMaterial : IHighPurityMaterial
    {
        private readonly IRepoManager _repo;
        private readonly ILoggerManager _log;
        private readonly IMapper _map;

        public HighPurityMaterial(IRepoManager repo, ILoggerManager log, IMapper map)
        {
            _repo = repo;
            _log = log;
            _map = map;
        }

        public async Task<IEnumerable<MaterialDTO>> GetAllMaterial()
        {
            throw new NotImplementedException();
        }

        public async Task<MaterialDTO> GetMaterialByMaterialNumber(int materialNumber)
        {
            throw new NotImplementedException();
        }
    }
}
