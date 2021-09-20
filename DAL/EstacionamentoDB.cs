using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class EstacionamentoDB
    {
        public static Response Insert(Estacionamento vaga)
        {
            string insert = $"INSERT into dbo.Estacionamento (Placa,HoraEntrada) values ('{vaga.Placa}',{vaga.HoraEntrada.ToString(CultureInfo.InvariantCulture)})";
            try
            {
                SqlCommand cmd = new SqlCommand(insert, ConnectionString.Connection);
                ConnectionString.Open();
                cmd.ExecuteNonQuery();
                ConnectionString.Close();
                return new Response
                {
                    Executed = true
                };
            }
            catch (Exception e)
            {
                if (ConnectionString.Connection.State == System.Data.ConnectionState.Open)
                {
                    ConnectionString.Close();
                }
                return new Response
                {
                    Executed = false,
                    ErrorMessage = e.Message,
                    Exception = e
                };
            }
        }

        public static Response Select(int id, out Estacionamento vaga)
        {
            vaga = new Estacionamento();
            string select = $"SELECT * from dbo.Estacionamento WHERE idEstacionamento = {id}";
            try
            {
                SqlCommand cmd = new SqlCommand(select, ConnectionString.Connection);
                ConnectionString.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    vaga.IdEstacionamento = Convert.ToInt32(dr[0]);
                    vaga.Placa = dr[1].ToString();
                    vaga.HoraEntrada = Convert.ToDouble(dr[2]);
                }
                dr.Close();
                ConnectionString.Close();
                return new Response
                {
                    Executed = true
                };
            }
            catch (Exception e)
            {
                return Confer(e);
            }
        }

        private static Response Confer(Exception e)
        {
            if (ConnectionString.Connection.State == System.Data.ConnectionState.Open)
            {
                ConnectionString.Close();
            }
            return new Response
            {
                Executed = false,
                ErrorMessage = e.Message,
                Exception = e
            };
        }
    }
}
