using Microsoft.Data.SqlClient;
using PrisPilot.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PrisPilot.Services.Peristence
{

    public class TemplateRepository : BaseRepository<Template>
    {
        public TemplateRepository() : base()
        {
        }
        public override Template Add(Template template)
        {
            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                using SqlCommand Cmd = new SqlCommand(@" INSERT INTO TEMPLATE (Introduction, TimeTable, AboutUs, Title, DrosselMail, DrosselPhoneNumber, DrosselLogo) "
                                                              + "VALUES (@Introduction, @TimeTable, @AboutUs, @Title, @DrosselMail, @DrosselPhoneNumber, @DrosselLogo)"
                                                              + "SELECT @@IDENTITY", con);

                Cmd.Parameters.Add("@Introduction", SqlDbType.NVarChar, 2000).Value = template.Introduction;
                Cmd.Parameters.Add("@TimeTable", SqlDbType.NVarChar, 2000).Value = template.TimeTable;
                Cmd.Parameters.Add("@AboutUs", SqlDbType.Bit).Value = template.AboutUs;
                Cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = template.Title;
                Cmd.Parameters.Add("@DrosselMail", SqlDbType.NVarChar, 150).Value = template.DrosselMail;
                Cmd.Parameters.Add("@DrosselPhoneNumber", SqlDbType.NVarChar, 20).Value = template.DrosselPhoneNumber;
                Cmd.Parameters.Add("@DrosselLogo", SqlDbType.VarBinary).Value = template.DrosselLogo;

                template.TemplateID = Convert.ToInt32(Cmd.ExecuteScalar());
            }

            return template;
        }

        public override List<Template> GetAll()
        {
            List<Template> templates = new List<Template>();

            using (SqlConnection con = CreateConnection())
            {
                con.Open();

                using SqlCommand cmd = new SqlCommand(@"SELECT TemplateID, Introduction, TimeTable, AboutUs, Title,
                                                        DrosselMail, DrosselPhoneNumber, DrosselLogo FROM TEMPLATE", con);

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Template template = new Template
                    {
                        TemplateID = reader.GetInt32(0),
                        Introduction = (string)reader["Introduction"],
                        TimeTable = (string)reader["TimeTable"],
                        AboutUs = (bool)reader["AboutUs"],
                        Title = (string)reader["Title"],
                        DrosselMail = (string)reader["DrosselMail"],
                        DrosselPhoneNumber = (string)reader["DrosselPhoneNumber"],
                        DrosselLogo = (byte[])reader["DrosselLogo"]
                    };

                    templates.Add(template);
                }
            }

            return templates;
        }
    }

}
