using HotelManagePro.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagePro.Features.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagePro.Features.Customers.Services;

public class CustomerService
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Customer CreateNewCustomer(Customer customer)
    {
        _dbContext.Add(customer);
        _dbContext.SaveChanges();
        return customer;
    }

   
    public Customer? GetCustomerById(int id)
    {
        return _dbContext.Customers
            .Include(c => c.Address)
            .FirstOrDefault(c => c.CustomerId == id);
    }

    public Customer? GetCustomerByEmail(string? email)
    {
        return _dbContext.Customers
            .Include(c => c.Address)
            .FirstOrDefault(c => c.Email == email);
    }


    public List<Customer> GetAllCustomers()
    {
        return _dbContext.Customers
            .Include(c => c.Address)
            .ToList();
    }

    public Customer? GetCustomerByPhone(int phoneNumber)
    {
        return _dbContext.Customers
            .Include(c => c.Address)
            .FirstOrDefault(c => c.PhoneNumber == phoneNumber);
    }
}
