using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    internal class DataBaseService
    {
        private const string connectionString = "data source=DESKTOP-HDRGJGO\\SQLEXPRESS; initial catalog=AddressBookDB; integrated security=true;";

        public void CreateDataBase()
        {
            try
            {
                SqlConnection con = new SqlConnection("data source=DESKTOP-HDRGJGO\\SQLEXPRESS; initial catalog=master; integrated security=true;");
                con.Open();

                string query = "create database AddressBookDB;";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Data Base Created Sussfully...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }



        public void CreateTable()
        {
            try
            {
                SqlConnection con = new SqlConnection(@"data source=DESKTOP-HDRGJGO\SQLEXPRESS; initial catalog=AddressBookDB; integrated security=true;");
                con.Open();

                string query = "create table AddressBookData(" +
                 "Columns_Id int identity(1,1) Primary key," +
                 "FirstName varchar(200)," +
                 "LastName varchar(100)," +
                 "Address varchar(200)," +
                 "City varchar(200)," +
                 "State varchar(200)," +
                 "Zip int," +
                 "Phone varchar(500)," +
                 "Email varchar(500)" +
                 ")";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table Created Sussessfully...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }



        public void InsertContact(Contact contact)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "INSERT INTO AddressBookData (FirstName, LastName, Address, City, State, Zip, Phone, Email) " +
                                   "VALUES (@FirstName, @LastName, @Address, @City, @State, @Zip, @Phone, @Email)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", contact.LastName);
                    cmd.Parameters.AddWithValue("@Address", contact.Address);
                    cmd.Parameters.AddWithValue("@City", contact.City);
                    cmd.Parameters.AddWithValue("@State", contact.State);
                    cmd.Parameters.AddWithValue("@Zip", contact.Zip);
                    cmd.Parameters.AddWithValue("@Phone", contact.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", contact.Email);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Contact inserted successfully in the database!\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while inserting contact into the database: {ex.Message}");
            }
        }




        public void UpdateContact(string firstName, Contact newContact)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "UPDATE AddressBookData " +
                                   "SET FirstName = @NewFirstName, LastName = @LastName, Address = @Address, " +
                                   "City = @City, State = @State, Zip = @Zip, Phone = @Phone, Email = @Email " +
                                   "WHERE FirstName = @FirstName";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NewFirstName", newContact.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", newContact.LastName);
                    cmd.Parameters.AddWithValue("@Address", newContact.Address);
                    cmd.Parameters.AddWithValue("@City", newContact.City);
                    cmd.Parameters.AddWithValue("@State", newContact.State);
                    cmd.Parameters.AddWithValue("@Zip", newContact.Zip);
                    cmd.Parameters.AddWithValue("@Phone", newContact.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", newContact.Email);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Contact updated successfully in the database!\n");
                    }
                    else
                    {
                        Console.WriteLine("Contact not found. Update failed.\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating contact in the database: {ex.Message}");
            }
        }



        public void DeleteContact(string firstName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "DELETE FROM AddressBookData WHERE FirstName = @FirstName";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Contact with the first name '{firstName}' deleted successfully from the database!\n");
                    }
                    else
                    {
                        Console.WriteLine($"Contact with the first name '{firstName}' not found in the database.\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting contact from the database: {ex.Message}");
            }
        }




    }

}
