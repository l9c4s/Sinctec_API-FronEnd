using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Notifies
    {

        [NotMapped]
        public string NomePropriedade { get; set; }

        [NotMapped]
        public string Mensagem { get; set; }

        [NotMapped]
        public List<Notifies> Notificacoes { get; set; }

        public bool ValidaPropriedadeString(string Valor, string NomePropriedade)
        {
            if(string.IsNullOrWhiteSpace(Valor) || string.IsNullOrWhiteSpace(NomePropriedade))
            {
                Notificacoes.Add(new Notifies
                {
                    Mensagem = "Campo Obrigatorio",
                    NomePropriedade = NomePropriedade
                });
                return false;
            }
            return true;
        }

        public bool ValidaPropriedadeInt(int Valor, string NomePropriedade)
        {
            if (Valor < 1 || string.IsNullOrWhiteSpace(NomePropriedade))
            {
                Notificacoes.Add(new Notifies
                {
                    Mensagem = "Campo Obrigatorio",
                    NomePropriedade = NomePropriedade
                });
                return false;
            }
            return true;
        }

    }
}
