using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MvcApplication2.Models
{
    public class Person
    {
        public static readonly Random Random = new Random();

        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public string Profile { get; set; }

        public static string[] Names = { "Khalid Abuhakmeh", "Nicole Miller", "Justin Rusbatch", "Oren Eini", "Rob Sullivan", "January Jones" };
        public static string[] Cities = { "Jerusalem", "Camp Hill", "Harrisburg", "Hollywood", "Oklahoma City" };

        public static Person New(int? id = null)
        {
            var phone = new StringBuilder();
            for (int i = 0; i < 11; i++)
                phone.Append(Random.Next(1, 9));

            return new Person
            {
                Id = id.HasValue ? string.Format("person/{0}", id) : null,
                Name = Names[Random.Next(0, Names.Length - 1)],
                Age = Random.Next(18, 80),
                City = Cities[Random.Next(0, Cities.Length - 1)],
                Phone = Convert.ToInt64(phone.ToString()).ToString("+# (###) ### ####"),
                Profile = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur vulputate leo eu arcu bibendum ut scelerisque massa interdum. Nam in sapien ultrices elit condimentum suscipit. Etiam sed dolor non arcu tincidunt fermentum ut ac ante. Nullam non sem eu lectus rutrum vulputate id sed justo. In iaculis convallis imperdiet. Aliquam dapibus viverra mollis. Vivamus quis augue turpis. Vestibulum dictum odio in odio eleifend viverra. In aliquam mi sit amet eros facilisis aliquet.Fusce volutpat viverra lectus, vitae vestibulum elit pretium quis. Mauris et augue ipsum, quis tristique tellus. Donec et semper sapien. Phasellus adipiscing nisl sed nisl malesuada tincidunt. Donec imperdiet fermentum quam at consequat. Cras lobortis tempus volutpat. Quisque non sagittis leo. In hac habitasse platea dictumst."
            };
        }
    }
}