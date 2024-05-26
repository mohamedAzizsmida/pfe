





using System;
using System.Data.SqlClient;
using System.Web;
//using static ProjetPFEService.Service1.Adherent;



using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
namespace ProjetPFEService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        private string connectionString = "data source=LAPTOP-TPVOKOLL\\SQLEXPRESS02; initial catalog=pfe;persist security info=True;  Integrated Security=SSPI;";
        private object hashedPasswordToCheck;

        private static object HashPassword(object password)
        {
            throw new NotImplementedException();
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string Authentification(string login, string password)

        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM adherent WHERE email = @login AND mot_de_passe = @password";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //  string hashedPasswordFromDB = (string)reader["password"]; // assuming password stored as a string

                            //  if (password == hashedPasswordFromDB)
                            {
                                string nom = (string)reader["nom"];
                                string prenom = (string)reader["prénom"];
                                HttpContext.Current.Session["Id"] = reader["id_adhérent"];
                                HttpContext.Current.Session["Nom"] = nom;
                                HttpContext.Current.Session["prenom"] = prenom;
                                return "Welcome " + prenom + " " + nom + "!";

                                //  HttpContext.Current.Session["nom"] = nom;
                                // HttpContext.Current.Session["prenom"] = prenom;
                            }
                            //else
                            //{
                            //    return "Incorrect password!";
                            //}
                        }
                        else
                        {
                            return "User not found!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    return "An error occurred: " + ex.Message;
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }
            }
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public string inscription(string Nom, string Prénom, string login, string password, string Adresse, string Code_postal, string ville,
             string pays, string telephone, string Date_de_naissance)

        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    string query = "INSERT INTO Adherent (Nom,Prénom,email,mot_de_passe,Adresse,Code_postal,ville,pays,telephonne,Date_de_naissance) VALUES (@Nom,@Prénom,@email,@mot_de_passe,@Adresse,@Code_postal,@ville,@pays,@telephone,@Date_de_naissance )";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nom", Nom);
                        command.Parameters.AddWithValue("@Prénom", Prénom);
                        command.Parameters.AddWithValue("@email", login);
                        command.Parameters.AddWithValue("@mot_de_passe", password);
                        command.Parameters.AddWithValue("@Adresse", Adresse);
                        command.Parameters.AddWithValue("@code_postal", Code_postal);
                        command.Parameters.AddWithValue("@ville", ville);
                        command.Parameters.AddWithValue("@pays", pays);
                        command.Parameters.AddWithValue("@telephone", telephone);
                        command.Parameters.AddWithValue("@Date_de_naissance", Date_de_naissance);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }


                    return " update ";

                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    return "An error occurred: " + ex.Message;
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }
            }
        }



        [System.Web.Services.WebMethod(EnableSession = true)]
        public string modifierclient(string Nom, string Prénom, string login, string password, string Adresse, string Code_postal, string ville,
             string pays, string telephone, string Date_de_naissance)

        {
            if (HttpContext.Current.Session["Id"] != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    try
                    {
                        string query = "update Adherent set Nom=@Nom,Prénom=@Prénom,email=@email,mot_de_passe=@mot_de_passe,Adresse=@Adresse,Code_postal=@Code_postal,ville=@ville,pays=@pays,telephonne=@telephonne,Date_de_naissance=@Date_de_naissance where id_adhérent=@id";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Nom", Nom);
                            command.Parameters.AddWithValue("@Prénom", Prénom);
                            command.Parameters.AddWithValue("@email", login);
                            command.Parameters.AddWithValue("@mot_de_passe", password);
                            command.Parameters.AddWithValue("@Adresse", Adresse);
                            command.Parameters.AddWithValue("@code_postal", Code_postal);
                            command.Parameters.AddWithValue("@ville", ville);
                            command.Parameters.AddWithValue("@pays", pays);
                            command.Parameters.AddWithValue("@telephonne", telephone);
                            command.Parameters.AddWithValue("@Date_de_naissance", Date_de_naissance);
                            command.Parameters.AddWithValue("@id", HttpContext.Current.Session["Id"]);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        //select nom,prenom,....... FROM adherent where id_adhérent=@id
                        //  command.Parameters.AddWithValue("@id", HttpContext.Current.Session["Id"]);

                        return "updated successfully!";
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                        return "An error occurred: " + ex.Message;
                    }
                    finally
                    {
                        // Close the connection
                        connection.Close();
                    }
                }
            }
            else return "";
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public string selectclient()

        {
            if (HttpContext.Current.Session["Id"] != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    try
                    {
                        string query = "select   Nom,Prénom,email,mot_de_passe,Adresse,Code_postal,ville,pays,telephonne,Date_de_naissance from Adherent where id_adhérent=@id";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {

                            command.Parameters.AddWithValue("@id", HttpContext.Current.Session["Id"]);

                            connection.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            // Convertir le DataTable en JSON

                            string json = JsonConvert.SerializeObject(table);

                            return json;
                        }
                        //select nom,prenom,....... FROM adherent where id_adhérent=@id
                        //  command.Parameters.AddWithValue("@id", HttpContext.Current.Session["Id"]);

                        return "select successfully!";
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                        return "An error occurred: " + ex.Message;
                    }
                    finally
                    {
                        // Close the connection
                        connection.Close();
                    }
                }
            }
            else return "";
        }




        [System.Web.Services.WebMethod(EnableSession = true)]
        public string testsession()
        {
            try
            {
                if (HttpContext.Current.Session["Id"] != null)
                {
                    return    "Welcome " + HttpContext.Current.Session["prenom"].ToString() + " " + HttpContext.Current.Session["Nom"].ToString() + "!";//"1";//HttpContext.Current.Session["nom"].ToString();
                }
                else return "0";
            }
            catch(Exception ex)
            {
                return "0";
            }
        }


        [System.Web.Services.WebMethod(EnableSession = true)]
        public string mot_de_passe_oublié(string login, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM adherent WHERE email = @login";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@login", login);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Mise à jour du mot de passe
                            reader.Close();// Fermer le reader précédent avant d'exécuter une nouvelle requête

                            string updateQuery = "UPDATE adherent SET mot_de_passe = @newPassword WHERE email = @login";
                            SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                            updateCommand.Parameters.AddWithValue("@newPassword", newPassword);
                            updateCommand.Parameters.AddWithValue("@login", login);
                            updateCommand.ExecuteNonQuery();

                            return "Password updated successfully!";
                        }
                        else
                        {
                            return "updated not successfully!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Gérer les exceptions
                    return "An error occurred: " + ex.Message;
                }
                finally
                {
                    // Fermer la connexion
                    connection.Close();
                }
            }
        }


        [System.Web.Services.WebMethod(EnableSession = true)]
        public string portefeuille(string datePf)//(string date)

        {
            if (HttpContext.Current.Session["Id"] != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    try
                    {
                        // string query = "select  p.id_titre,Quantité_Titre,Cmp,Montant,T.Nom, case when T.type_titre='ACT' then 'ACTION' else type_titre end type_titre,t.cours from Table_portefeuille p  JOIN  titre t ON p.id_titre = t.id_titre WHERE  id_adhérent=@id";
                      //  string query = "exec GetPortefeuille @id,'01/05/2024'";

                        string query = "EXEC GetPortefeuille @id, @DATEPF";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {

                            command.Parameters.AddWithValue("@id", HttpContext.Current.Session["Id"]);
                            command.Parameters.AddWithValue("@DATEPF", datePf);

                            //  command.Parameters.AddWithValue("@date", date);

                            connection.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            // Convertir le DataTable en JSON

                            string json = JsonConvert.SerializeObject(table);

                            return json;
                        }
                        //select nom,prenom,....... FROM adherent where id_adhérent=@id
                        //  command.Parameters.AddWithValue("@id", HttpContext.Current.Session["Id"]);

                        return "select successfully!";
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                        return "An error occurred: " + ex.Message;
                    }
                    finally
                    {
                        // Close the connection
                        connection.Close();
                    }
                }
            }
            else return "";
        }
    }
    public class Adherent
        {
            public int Id { get; set; }
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public string email { get; set; }
            public string mot_de_passe { get; set; }
            public string Adresse { get; set; }
            public string code_postal { get; set; }
            public string ville { get; set; }
            public string pays { get; set; }
            public int Telephone { get; set; }
            public DateTime Date_de_naissance { get; set; }




            // Ajoutez d'autres propriétés selon vos besoins
        }
    }
        

    


    


