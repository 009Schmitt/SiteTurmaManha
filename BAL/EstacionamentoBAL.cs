using DAL;
using System;

namespace BAL
{
    public static class EstacionamentoBAL
    {
        public static Response Insert(Estacionamento vaga)
        {
            if (Checker.StringChecker(vaga.Placa,7))
            {
                return EstacionamentoDB.Insert(vaga);
            }
            else
            {
                return new Response
                {
                    Executed = false,
                    ErrorMessage = "Placa Vazia ou Grande"
                };
            }
        }

        public static Response Select(int id, out Estacionamento vaga)
        {
            vaga = new Estacionamento();

            if (id > 0)
            {
                return EstacionamentoDB.Select(id,out vaga);
            }
            else
            {
                return new Response
                {
                    Executed = false,
                    ErrorMessage = "Id inválido"
                };
            }
        }



    }
}
