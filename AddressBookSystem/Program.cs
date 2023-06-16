using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

class Contact : IComparable<Contact>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Contact other = (Contact)obj;
        return string.Equals(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(LastName, other.LastName, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(FirstName) ^ StringComparer.OrdinalIgnoreCase.GetHashCode(LastName);
    }

    public int CompareTo(Contact other)
    {
        int result = string.Compare(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase);
        if (result == 0)
        {
            result = string.Compare(LastName, other.LastName, StringComparison.OrdinalIgnoreCase);
        }
        return result;
    }

    public override string ToString()
    {
        return $"Name: {FirstName} {LastName}\n" +
               $"Address: {Address}\n" +
               $"City: {City}\n" +
               $"State: {State}\n" +
               $"ZIP: {Zip}\n" +
               $"Phone: {PhoneNumber}\n" +
               $"Email: {Email}\n";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to Address Book Program");
        Dictionary<string, List<Contact>> addressBooks = new Dictionary<string, List<Contact>>();
        Dictionary<string, List<Contact>> cityDictionary = new Dictionary<string, List<Contact>>();
        Dictionary<string, List<Contact>> stateDictionary = new Dictionary<string, List<Contact>>();

        while (true)
        {
            Console.WriteLine("Enter a command ('new' to create a new address book, 'quit' to exit):");
            string command = Console.ReadLine().ToLower();

            if (command == "quit")
                break;

            if (command == "new")
            {
                Console.Write("Enter the name of the new address book: ");
                string addressBookName = Console.ReadLine();

                if (!addressBooks.ContainsKey(addressBookName))
                {
                    addressBooks[addressBookName] = new List<Contact>();
                    Console.WriteLine($"Address book '{addressBookName}' created successfully!\n");

                    while (true)
                    {
                        Console.WriteLine("Enter contact details (or 'quit' to exit):");
                        Console.Write("First Name: ");
                        string firstName = Console.ReadLine();

                        if (firstName.ToLower() == "quit")
                            break;

                        Console.Write("Last Name: ");
                        string lastName = Console.ReadLine();

                        Console.Write("Address: ");
                        string address = Console.ReadLine();

                        Console.Write("City: ");
                        string city = Console.ReadLine();

                        Console.Write("State: ");
                        string state = Console.ReadLine();

                        Console.Write("ZIP: ");
                        string zip = Console.ReadLine();

                        Console.Write("Phone Number: ");
                        string phoneNumber = Console.ReadLine();

                        Console.Write("Email: ");
                        string email = Console.ReadLine();

                        Contact contact = new Contact
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Address = address,
                            City = city,
                            State = state,
                            Zip = zip,
                            PhoneNumber = phoneNumber,
                            Email = email
                        };

                        if (!addressBooks[addressBookName].Contains(contact))
                        {
                            addressBooks[addressBookName].Add(contact);
                            Console.WriteLine("Contact added successfully!\n");

                            // Add contact to city dictionary
                            if (!cityDictionary.ContainsKey(city))
                            {
                                cityDictionary[city] = new List<Contact>();
                            }
                            cityDictionary[city].Add(contact);

                            // Add contact to state dictionary
                            if (!stateDictionary.ContainsKey(state))
                            {
                                stateDictionary[state] = new List<Contact>();
                            }
                            stateDictionary[state].Add(contact);
                        }
                        else
                        {
                            Console.WriteLine("Duplicate contact. Contact not added.\n");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Address book '{addressBookName}' already exists.\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid command. Try again.\n");
            }
        }

        Console.WriteLine("\nAddress Books:");
        foreach (var addressBookEntry in addressBooks)
        {
            string addressBookName = addressBookEntry.Key;
            List<Contact> contacts = addressBookEntry.Value;

            Console.WriteLine($"Address Book: {addressBookName}");
            foreach (Contact contact in contacts)
            {
                Console.WriteLine(contact.ToString());
            }
            Console.WriteLine();
        }

        Console.WriteLine("Enter the name of the address book to edit/delete or search (or 'quit' to exit):");
        string searchAddressBookName = Console.ReadLine();

        if (searchAddressBookName.ToLower() != "quit")
        {
            if (addressBooks.ContainsKey(searchAddressBookName))
            {
                Console.WriteLine("Enter the first name of the contact to edit/delete or search:");
                string searchFirstName = Console.ReadLine();

                Console.WriteLine("Enter the last name of the contact to edit/delete or search:");
                string searchLastName = Console.ReadLine();

                Contact foundContact = addressBooks[searchAddressBookName].Find(contact =>
                    contact.FirstName.ToLower() == searchFirstName.ToLower() &&
                    contact.LastName.ToLower() == searchLastName.ToLower());

                if (foundContact != null)
                {
                    Console.WriteLine("Contact found. Enter 'edit' to edit, 'delete' to delete, or 'search' to search again:");

                    string action = Console.ReadLine().ToLower();

                    if (action == "edit")
                    {
                        Console.WriteLine("Enter updated details:");

                        Console.Write("First Name: ");
                        foundContact.FirstName = Console.ReadLine();

                        Console.Write("Last Name: ");
                        foundContact.LastName = Console.ReadLine();

                        Console.Write("Address: ");
                        foundContact.Address = Console.ReadLine();

                        Console.Write("City: ");
                        foundContact.City = Console.ReadLine();

                        Console.Write("State: ");
                        foundContact.State = Console.ReadLine();

                        Console.Write("ZIP: ");
                        foundContact.Zip = Console.ReadLine();

                        Console.Write("Phone Number: ");
                        foundContact.PhoneNumber = Console.ReadLine();

                        Console.Write("Email: ");
                        foundContact.Email = Console.ReadLine();

                        Console.WriteLine("Contact updated successfully!\n");
                    }
                    else if (action == "delete")
                    {
                        addressBooks[searchAddressBookName].Remove(foundContact);
                        Console.WriteLine("Contact deleted successfully!\n");
                    }
                    else if (action == "search")
                    {
                        Console.WriteLine("Enter the sorting option ('city', 'state', 'zip'):");
                        string sortingOption = Console.ReadLine().ToLower();

                        List<Contact> sortedContacts = new List<Contact>();

                        if (sortingOption == "city")
                        {
                            if (cityDictionary.ContainsKey(foundContact.City))
                            {
                                sortedContacts = cityDictionary[foundContact.City];
                            }
                        }
                        else if (sortingOption == "state")
                        {
                            if (stateDictionary.ContainsKey(foundContact.State))
                            {
                                sortedContacts = stateDictionary[foundContact.State];
                            }
                        }
                        else if (sortingOption == "zip")
                        {
                            sortedContacts = addressBooks[searchAddressBookName];
                            sortedContacts.Sort((c1, c2) => c1.Zip.CompareTo(c2.Zip));
                        }

                        Console.WriteLine("Search Results:");
                        foreach (Contact contact in sortedContacts)
                        {
                            Console.WriteLine(contact.ToString());
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Invalid action. No changes made.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Contact not found.\n");
                }
            }
            else
            {
                Console.WriteLine("Address book not found.\n");
            }
        }

        Console.WriteLine("\nUpdated Address Books:");
        foreach (var addressBookEntry in addressBooks)
        {
            string addressBookName = addressBookEntry.Key;
            List<Contact> contacts = addressBookEntry.Value;

            Console.WriteLine($"Address Book: {addressBookName}");
            foreach (Contact contact in contacts)
            {
                Console.WriteLine(contact.ToString());
            }
            Console.WriteLine();
        }

        // Write address book data to a JSON file
        string path = @"C:\Users\Karan Depale\RFC285\AddressBookSystem\List-AddressBookSystem\AddressBookSystem\JSONDATA.json";
        WriteAddressBookDataToJsonFile(path, addressBooks);

        // Read address book data from the JSON file
        ReadAddressBookDataFromJsonFile(path);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void WriteAddressBookDataToJsonFile(string path, Dictionary<string, List<Contact>> addressBooks)
    {
        try
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(addressBooks);
                file.WriteLine(json);
            }

            Console.WriteLine($"Address book data written to the file: {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while writing address book data to the file: {ex.Message}");
        }
    }

    static void ReadAddressBookDataFromJsonFile(string path)
    {
        try
        {
            using (StreamReader file = new StreamReader(path))
            {
                string json = file.ReadToEnd();
                Dictionary<string, List<Contact>> addressBooks = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<Contact>>>(json);

                Console.WriteLine($"Reading address book data from the file: {path}");
                Console.WriteLine();

                foreach (var addressBookEntry in addressBooks)
                {
                    string addressBookName = addressBookEntry.Key;
                    List<Contact> contacts = addressBookEntry.Value;

                    Console.WriteLine($"Address Book: {addressBookName}");
                    foreach (Contact contact in contacts)
                    {
                        Console.WriteLine(contact.ToString());
                    }
                    Console.WriteLine();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while reading address book data from the file: {ex.Message}");
        }
    }

    static Dictionary<string, int> SearchContacts(Dictionary<string, List<Contact>> cityDictionary, Dictionary<string, List<Contact>> stateDictionary, string city, string state)
    {
        Dictionary<string, int> searchResults = new Dictionary<string, int>();

        if (cityDictionary.ContainsKey(city.ToLower()))
        {
            searchResults["City"] = cityDictionary[city.ToLower()].Count;
        }
        else
        {
            searchResults["City"] = 0;
        }

        if (stateDictionary.ContainsKey(state.ToLower()))
        {
            searchResults["State"] = stateDictionary[state.ToLower()].Count;
        }
        else
        {
            searchResults["State"] = 0;
        }

        return searchResults;
    }
}
