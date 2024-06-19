using System;
using System.Collections.Generic;
using System.Linq;

public class HorarioService
{
    public List<Grupo> EmparejarHorarios(List<Instructor> instructores, List<Estudiante> estudiantes, int numEstudiantesPorGrupo)
    {
        var grupos = new List<Grupo>();

        foreach (var instructor in instructores)
        {
            foreach (var horarioInstructor in instructor.HorariosDisponibles)
            {
                var estudiantesCoincidentes = estudiantes
                    .Where(e => e.HorariosDisponibles.Any(h => HorarioCoincide(h, horarioInstructor)))
                    .Take(numEstudiantesPorGrupo)
                    .ToList();

                if (estudiantesCoincidentes.Count == numEstudiantesPorGrupo)
                {
                    var grupo = new Grupo
                    {
                        Instructor = instructor,
                        Estudiantes = estudiantesCoincidentes,
                        Horario = horarioInstructor
                    };
                    grupos.Add(grupo);

                    // Eliminar los estudiantes emparejados de la lista de estudiantes
                    estudiantes = estudiantes.Except(estudiantesCoincidentes).ToList();
                }
            }
        }

        return grupos;
    }

    private bool HorarioCoincide(HorarioDisponible horario1, HorarioDisponible horario2)
    {
        return horario1.Dia == horario2.Dia &&
               horario1.HoraInicio < horario2.HoraFin &&
               horario1.HoraFin > horario2.HoraInicio;
    }
}

public class HorarioDisponible
{
    public int Id { get; set; }
    public DayOfWeek Dia { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
    public int? EstudianteId { get; set; }
    public Estudiante Estudiante { get; set; }
    public int? InstructorId { get; set; }
    public Instructor Instructor { get; set; }
}

public class Instructor
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<HorarioDisponible> HorariosDisponibles { get; set; } = new List<HorarioDisponible>();
}

public class Estudiante
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<HorarioDisponible> HorariosDisponibles { get; set; } = new List<HorarioDisponible>();
}

public class Grupo
{
    public Instructor Instructor { get; set; }
    public List<Estudiante> Estudiantes { get; set; }
    public HorarioDisponible Horario { get; set; }
}

public class Emparejamiento
{
    public Instructor Instructor { get; set; }
    public Estudiante Estudiante { get; set; }
    public HorarioDisponible Horario { get; set; }
}
