﻿using System;
using System.Collections.Generic;

class Contact
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
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to Address Book Program");
        Dictionary<string, List<Contact>> addressBooks = new Dictionary<string, List<Contact>>();

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
                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Address: {contact.Address}");
                Console.WriteLine($"City: {contact.City}");
                Console.WriteLine($"State: {contact.State}");
                Console.WriteLine($"ZIP: {contact.Zip}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine();
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
                        List<Contact> searchResults = SearchContacts(addressBooks, foundContact.City, foundContact.State);

                        if (searchResults.Count > 0)
                        {
                            Console.WriteLine("Search Results:");
                            foreach (Contact contact in searchResults)
                            {
                                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                                Console.WriteLine($"Address: {contact.Address}");
                                Console.WriteLine($"City: {contact.City}");
                                Console.WriteLine($"State: {contact.State}");
                                Console.WriteLine($"ZIP: {contact.Zip}");
                                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                                Console.WriteLine($"Email: {contact.Email}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No contacts found in the specified city or state.");
                        }
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
                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Address: {contact.Address}");
                Console.WriteLine($"City: {contact.City}");
                Console.WriteLine($"State: {contact.State}");
                Console.WriteLine($"ZIP: {contact.Zip}");
                Console.WriteLine($"Phone: {contact.PhoneNumber}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static List<Contact> SearchContacts(Dictionary<string, List<Contact>> addressBooks, string city, string state)
    {
        List<Contact> searchResults = new List<Contact>();

        foreach (var addressBookEntry in addressBooks)
        {
            List<Contact> contacts = addressBookEntry.Value;

            foreach (Contact contact in contacts)
            {
                if (contact.City.ToLower() == city.ToLower() || contact.State.ToLower() == state.ToLower())
                {
                    searchResults.Add(contact);
                }
            }
        }

        return searchResults;
    }
}
