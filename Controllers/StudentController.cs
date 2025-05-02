using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApiBusinessLayer;
using StudentDataAcessLayer;

namespace StudentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("AllStudent", Name = "GetAllStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudent()
        {
            try
            {
                var students = Student.GetAllStudents();
                if (students == null || !students.Any())
                {
                    return NotFound("No students found.");
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // Method to get all students who passed
        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()

        {
            // var passedStudents = StudentDataSimulation.StudentsList.Where(student => student.Grade >= 50).ToList();

            List<StudentDTO> PassedStudentsList = StudentApiBusinessLayer.Student.GetPassedStudents();
            if (PassedStudentsList.Count == 0)
            {
                return NotFound("No Students Found!");
            }

            return Ok(PassedStudentsList); // Return the list of students who passed.
        }


        [HttpGet("AVG", Name = "GetAVG")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAVG()
        {
            try
            {
                double avg = Student.GetAVG();
                if (avg == 0)
                {
                    return NotFound("No students found.");
                }
                return Ok(avg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetStudentById")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<StudentDTO> GetStudentById(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            //var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);
            //if (student == null)
            //{
            //    return NotFound($"Student with ID {id} not found.");
            //}
            StudentApiBusinessLayer.Student student = StudentApiBusinessLayer.Student.Find(id);

            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            //here we get only the DTO object to send it back.
            StudentDTO SDTO = student.SDTO;

            //we return the DTO not the student object.
            return Ok(SDTO);

        }

       
        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudentDTO)
        {
            //we validate the data here
            if (newStudentDTO == null || string.IsNullOrEmpty(newStudentDTO.Name) || newStudentDTO.Age < 0 || newStudentDTO.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }

            //newStudent.Id = StudentDataSimulation.StudentsList.Count > 0 ? StudentDataSimulation.StudentsList.Max(s => s.Id) + 1 : 1;

            Student student =  new Student(new StudentDTO(newStudentDTO.ID, newStudentDTO.Name, newStudentDTO.Age, newStudentDTO.Grade));
            student.Save();

            newStudentDTO.ID = student.ID;

            //we return the DTO only not the full student object
            //we dont return Ok here,we return createdAtRoute: this will be status code 201 created.
            return CreatedAtRoute("GetStudentById", new { id = newStudentDTO.ID }, newStudentDTO);

        }

        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDTO> UpdateStudent(int id, StudentDTO updatedStudentDTO)
        {
            if (id < 1 || updatedStudentDTO == null)
            {
                return BadRequest("Invalid student data.");
            }
            //var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);
            //if (student == null)
            //{
            //    return NotFound($"Student with ID {id} not found.");
            //}
            Student student = Student.Find(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            student.Name = updatedStudentDTO.Name;
            student.Age = updatedStudentDTO.Age;
            student.Grade = updatedStudentDTO.Grade;
            student.Save();
            return Ok(updatedStudentDTO);
        }

        [HttpDelete("{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid student ID.");
            }
            //var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);
            //if (student == null)
            //{
            //    return NotFound($"Student with ID {id} not found.");
            //}
            Student student = Student.Find(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            student.Delete();
            return Ok($"Student with ID {id} deleted successfully.");
        }

    }
}
