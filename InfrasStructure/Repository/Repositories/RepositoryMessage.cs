using Domain.Interfaces;
using Entities.Entities;
using InfrasStructure.Configuration;
using InfrasStructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace InfrasStructure.Repository.Repositories
{
    public class RepositoryMessage : RepositoryGenerics<Menssage>, IMessage
    {

        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        public RepositoryMessage()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }




    }
}
