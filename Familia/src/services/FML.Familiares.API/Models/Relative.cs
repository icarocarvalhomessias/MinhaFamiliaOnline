﻿using FML.Core.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

[Serializable]
public class Relative : Entity, IAggregateRoot
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public Gender Gender { get; set; }
    public bool IsActive { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool Patriarch { get; set; } = false;
    public bool Matriarch { get; set; } = false;
    public bool IsAlive { get; set; } = true;
    public string? LinkName { get; set; }
    public bool SecretSanta { get; set; } = false;
    public Guid? Spouse { get; set; }
    public Guid FamilyId { get; set; }
    public Guid HouseId { get; set; }
    public Guid? FatherId { get; set; }
    public Guid? MotherId { get; set; }

    public Relative? SpouseObj { get; set; }
    public List<Relative>? Children { get; set; }
    public Family? Family { get; set; }
    public House? House { get; set; }

    public Relative()
    {
        
    }

    public Relative(Guid UsuarioId, string firstName, string lastName, DateTime birthDate, Gender gender)
    {
        Id = UsuarioId;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Gender = gender;
    }


        [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    [NotMapped]
    public int Idade
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}