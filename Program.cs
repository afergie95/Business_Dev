using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Codat_Api_Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");

            StreamReader r = new StreamReader("C:/Users/944074/Documents/Codat/Test Data Extracts/CreditNotes.json");
            string json = r.ReadToEnd();

            List<String> records = new List<String>();

            JObject jsonObject = JObject.Parse(json);
            IEnumerable<JToken> jTokens = jsonObject.Descendants().Where(p => p.Count() == 0);
            Dictionary<string, string> results = jTokens.Aggregate(new Dictionary<string, string>(), (properties, jToken) =>
            {
                properties.Add(jToken.Path, jToken.ToString());
                records.Add(jToken.ToString());
                return properties;
            });

            string creditNotesInsert = "INSERT INTO dbo.Credit_Notes VALUES (@Id, @creditNoteNumber, @customerRefId, @companyName, @totalAmount, @remainingCredit, @status, @issueDate, @currency, @paymentAllocationsID, @paTotalAmount, @paCurrency, @paDate, @note, @pageNumber, @pageSize, @totalResults, @currentLink, @selfLink)";
            string creditNoteCheck = "SELECT Id FROM Credit_notes";

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\944074\\Documents\\Codat_API.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand cmdCNCheck = new SqlCommand(creditNoteCheck, connection);

            cmdCNCheck.CommandType = CommandType.Text;

            List<Int32> items = new List<Int32>();

            connection.Open();
            using (SqlDataReader objReader = cmdCNCheck.ExecuteReader())
            {
                if (objReader.HasRows)
                {
                    while (objReader.Read())
                    {
                        int item = objReader.GetInt32(objReader.GetOrdinal("Id"));
                        items.Add(item);
                    }
                }
            }

            if (items.Contains(Convert.ToInt32(records[0])))
            {
                Console.WriteLine("A record already exists with this ID!");

                Console.WriteLine("Would you like to overwrite this record? (Yes/No)");

                string response = Console.ReadLine();

                if (response == "No")
                {
                    Console.WriteLine("Data Will not be Updated, Process Complete!");
                }
                else
                {
                    string deleteDupeCN = "DELETE FROM Credit_Notes Where Id = @ID";
                    SqlCommand cmdCNDelete = new SqlCommand(deleteDupeCN, connection);
                    cmdCNDelete.Parameters.Add("@Id", SqlDbType.Int);

                    {
                        
                        cmdCNDelete.Parameters["@Id"].Value = Convert.ToInt32(records[0]);
                        cmdCNDelete.ExecuteNonQuery();
                        
                    }

                    SqlCommand cmd = new SqlCommand(creditNotesInsert, connection);
                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters.Add("@creditNoteNumber", SqlDbType.Int);
                    cmd.Parameters.Add("@customerRefId", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@companyName", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@totalAmount", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@remainingCredit", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@status", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@issueDate", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@currency", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@paymentAllocationsID", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@paTotalAmount", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@paCurrency", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@paDate", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@note", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@pageNumber", SqlDbType.Int);
                    cmd.Parameters.Add("@pageSize", SqlDbType.Int);
                    cmd.Parameters.Add("@totalResults", SqlDbType.Int);
                    cmd.Parameters.Add("@currentLink", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@selfLink", SqlDbType.NVarChar);

                    {
                        
                        cmd.Parameters["@Id"].Value = Convert.ToInt32(records[0]);
                        cmd.Parameters["@creditNoteNumber"].Value = Convert.ToInt32(records[1]);
                        cmd.Parameters["@customerRefId"].Value = records[2];
                        cmd.Parameters["@companyName"].Value = records[3];
                        cmd.Parameters["@totalAmount"].Value = records[4];
                        cmd.Parameters["@remainingCredit"].Value = records[5];
                        cmd.Parameters["@status"].Value = records[6];
                        cmd.Parameters["@issueDate"].Value = records[7];
                        cmd.Parameters["@currency"].Value = records[8];
                        cmd.Parameters["@paymentAllocationsID"].Value = records[9];
                        cmd.Parameters["@paTotalAmount"].Value = records[10];
                        cmd.Parameters["@paCurrency"].Value = records[11];
                        cmd.Parameters["@paDate"].Value = records[12];
                        cmd.Parameters["@note"].Value = records[13];
                        cmd.Parameters["@pageNumber"].Value = Convert.ToInt32(records[14]);
                        cmd.Parameters["@pageSize"].Value = Convert.ToInt32(records[15]);
                        cmd.Parameters["@totalResults"].Value = Convert.ToInt32(records[16]);
                        cmd.Parameters["@currentLink"].Value = records[17];
                        cmd.Parameters["@selfLink"].Value = records[18];


                        cmd.ExecuteNonQuery();
                        
                        Console.WriteLine("Complete!");
                    }

                }
            }

            else
            {

                SqlCommand cmd = new SqlCommand(creditNotesInsert, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int);
                cmd.Parameters.Add("@creditNoteNumber", SqlDbType.Int);
                cmd.Parameters.Add("@customerRefId", SqlDbType.NVarChar);
                cmd.Parameters.Add("@companyName", SqlDbType.NVarChar);
                cmd.Parameters.Add("@totalAmount", SqlDbType.NVarChar);
                cmd.Parameters.Add("@remainingCredit", SqlDbType.NVarChar);
                cmd.Parameters.Add("@status", SqlDbType.NVarChar);
                cmd.Parameters.Add("@issueDate", SqlDbType.NVarChar);
                cmd.Parameters.Add("@currency", SqlDbType.NVarChar);
                cmd.Parameters.Add("@paymentAllocationsID", SqlDbType.NVarChar);
                cmd.Parameters.Add("@paTotalAmount", SqlDbType.NVarChar);
                cmd.Parameters.Add("@paCurrency", SqlDbType.NVarChar);
                cmd.Parameters.Add("@paDate", SqlDbType.NVarChar);
                cmd.Parameters.Add("@note", SqlDbType.NVarChar);
                cmd.Parameters.Add("@pageNumber", SqlDbType.Int);
                cmd.Parameters.Add("@pageSize", SqlDbType.Int);
                cmd.Parameters.Add("@totalResults", SqlDbType.Int);
                cmd.Parameters.Add("@currentLink", SqlDbType.NVarChar);
                cmd.Parameters.Add("@selfLink", SqlDbType.NVarChar);


                {
                    
                    cmd.Parameters["@Id"].Value = Convert.ToInt32(records[0]);
                    cmd.Parameters["@creditNoteNumber"].Value = Convert.ToInt32(records[1]);
                    cmd.Parameters["@customerRefId"].Value = records[2];
                    cmd.Parameters["@companyName"].Value = records[3];
                    cmd.Parameters["@totalAmount"].Value = records[4];
                    cmd.Parameters["@remainingCredit"].Value = records[5];
                    cmd.Parameters["@status"].Value = records[6];
                    cmd.Parameters["@issueDate"].Value = records[7];
                    cmd.Parameters["@currency"].Value = records[8];
                    cmd.Parameters["@paymentAllocationsID"].Value = records[9];
                    cmd.Parameters["@paTotalAmount"].Value = records[10];
                    cmd.Parameters["@paCurrency"].Value = records[11];
                    cmd.Parameters["@paDate"].Value = records[12];
                    cmd.Parameters["@note"].Value = records[13];
                    cmd.Parameters["@pageNumber"].Value = Convert.ToInt32(records[14]);
                    cmd.Parameters["@pageSize"].Value = Convert.ToInt32(records[15]);
                    cmd.Parameters["@totalResults"].Value = Convert.ToInt32(records[16]);
                    cmd.Parameters["@currentLink"].Value = records[17];
                    cmd.Parameters["@selfLink"].Value = records[18];

                    cmd.ExecuteNonQuery();
                    
                    Console.WriteLine("Complete!");
                }
            }
            connection.Close();



        }
        }
    }


