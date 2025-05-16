using AAU_BE.Database;
using DefaultNamespace;

namespace AAU_BE.Models;
using System.Collections.Generic;
using System.Data;

public class PetService
{
    private readonly string _connectionString;

     public List<Pet> GetPetsForUser(int userId)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "id", userId },
        };
        var userpets = new List<Pet>();
        DataTable dT = SQLDB.FetchDataTable(Queries.PETSBYUSER, bindparams);
        foreach (DataRow row in dT.Rows)
        {
            Pet pet = new Pet()
            {
                id = Convert.ToInt32(row["Id"]),
                userid = row["userid"].ToString(),
                name = row["name"].ToString(),
                breed =row["breed"].ToString(),
                age = Convert.ToInt32(row["age"]),
                sex = row["sex"].ToString(),
                species = row["species"].ToString(),
            };
            userpets.Add(pet);
        }
        return userpets;
    }

    public bool CreatePet(Pet pet)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "userid", pet.userid },
            { "age", pet.age },
            { "breed", pet.breed },
            { "name", pet.name },
            { "species", pet.species },
            { "sex", pet.sex },
        };
        if(Database.SQLDB.DoSqlCommand(Queries.INSERTPET, bindparams) > 0)
        {
            return true;
        }
        return false;
    }

    public bool UpdatePet(Pet pet)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "id", pet.id },
            { "age", pet.age },
            { "breed", pet.breed },
            { "name", pet.name },
            { "species", pet.species },
            { "sex", pet.sex },
        };
        if(Database.SQLDB.DoSqlCommand(Queries.UPDATEPET, bindparams) > 0)
        {
            return true;
        }
        return false;
    }
    public bool DeletePet(int id)
    {
        var bindparams = new Dictionary<string, dynamic>
        {
            { "id", id },
        };
        if (Database.SQLDB.DoSqlCommand(Queries.DELETEPET, bindparams) > 0)
        {
            return true;
        }
        return false;
    }
}