using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using System.Threading.Tasks;

namespace UselessFacts
{
    internal class DBConnector
    {
        internal static async Task<List<Phrase>> GetPhrase(string connectionString)
        {
            List<Phrase> phrase = new List<Phrase>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlCommand = @"SELECT TOP 1 id, phrase, added FROM phrase ORDER BY NEWID()";
                using (SqlCommand command = new SqlCommand(sqlCommand, conn))
                {
                    await conn.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Phrase p = new Phrase();
                                p.id = (long) reader["id"];
                                p.phrase = (string) reader["phrase"];
                                p.added = (DateTime) reader["added"];

                                phrase.Add(p);
                            }
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            return phrase;
        }
    }
}
