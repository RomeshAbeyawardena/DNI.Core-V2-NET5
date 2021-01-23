using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests.Assets
{
    [MessagePack.MessagePackObject(true)]
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public StudentType Type { get; set; }
        public DateTime Created { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Student student)
                return Equals(student);

            return false;
        }

        public bool Equals(Student student)
        {
            return student.Id == Id
                && student.Name == Name
                && student.Created == Created;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Created);
        }
    }

}
