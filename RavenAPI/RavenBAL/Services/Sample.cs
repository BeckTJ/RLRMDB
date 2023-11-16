using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavenBAL.Services
{
    public class Sample
    {
        private readonly IRepoWrapper _repo;

        public Sample(IRepoWrapper repo)
        {
            _repo = repo;
        }   

    }
}
