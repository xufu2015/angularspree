using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApplication2.Models
{
    public interface IUserRepository
    {
        bool VerifyEmail(string email);
        bool VerifyName(string firstName, string lastName);
    }
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [ClassicMovie(1960)]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Range(0, 999.99)]
        public decimal Price { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public bool Preorder { get; set; }
    }
    public enum Genre
    {
        Classic,
        PostClassic,
        Modern,
        PostModern,
        Contemporary,
    }
    public class ClassicMovieAttribute : ValidationAttribute, IClientModelValidator
    {
        private int _year;

        public ClassicMovieAttribute(int year)
        {
            _year = year;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Movie movie = (Movie)validationContext.ObjectInstance;

            if (movie.Genre == Genre.Classic && movie.ReleaseDate.Year > _year)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-classicmovie", GetErrorMessage());

            var year = _year.ToString(CultureInfo.InvariantCulture);
            MergeAttribute(context.Attributes, "data-val-classicmovie-year", year);
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }

        private string GetErrorMessage()
        {
            return $"Classic movies must have a release year earlier than {_year}.";
        }
    }

    public class MovieIValidatable : IValidatableObject
    {
        private const int _classicYear = 1960;

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Range(0, 999.99)]
        public decimal Price { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public bool Preorder { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Genre == Genre.Classic && ReleaseDate.Year > _classicYear)
            {
                yield return new ValidationResult(
                    $"Classic movies must have a release year earlier than {_classicYear}.",
                    new[] { "ReleaseDate" });
            }
        }
    }

    public class MVCMovieContext
    {
        public IList<Movie> Movies { get; } = new List<Movie>();

        public void AddMovie(Movie movie)
        {
            movie.Id = Movies.Count;
            Movies.Add(movie);
        }

        public void SaveChanges()
        {
            // No-op. In real world, this would save to a backing store e.g. SQL Server.
        }
    }

    public class User
    {
        [Remote(action: "VerifyEmail", controller: "Users")]
        public string Email { get; set; }

        [Remote(action: "VerifyName", controller: "Users", AdditionalFields = nameof(LastName))]
        public string FirstName { get; set; }
        [Remote(action: "VerifyName", controller: "Users", AdditionalFields = nameof(FirstName))]
        public string LastName { get; set; }
    }

    public class UserRepository : IUserRepository
    {
        private readonly HashSet<string> _emailAddresses = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<Person> _persons = new HashSet<Person>();

        public bool VerifyEmail(string email)
        {
            // In real world, adding a new email address would be a separate step. This would be a contains check.
            return _emailAddresses.Add(email);
        }

        public bool VerifyName(string firstName, string lastName)
        {
            // In real world, adding a new person would be a separate step. This would be a contains check.
            return _persons.Add(new Person()
            {
                FirstName = firstName,
                LastName = lastName
            });
        }

        private struct Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }

}
