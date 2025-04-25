using Datos.Repositories.Contracts;
using Entidades.DTOs;
using Entidades.Entities;
using Logica;
using Logica.Contracts;

namespace Negocio.Implementations
{
    public class MateriaLogic : IMateriaLogic
    {
        IMateriaRepository _materiaRepository;
        IProfesorRepository _profesorRepository;
        IDiaHorarioRepository _diaHorarioRepository;

        public MateriaLogic(IMateriaRepository materiaRepository, IProfesorRepository profesorRepository, IDiaHorarioRepository diaHorarioRepository)
        {
            _materiaRepository = materiaRepository;
            _profesorRepository = profesorRepository;
            _diaHorarioRepository = diaHorarioRepository;
        }
        
        public async Task AltaMateria(string nombre, List<int> listaProfesoresID, List<int> listaDiasHorariosID, string modalidad, string anio)
        {
            // Verifica si ya existe una materia con el mismo nombre
            Materia? materiaExistente = (await _materiaRepository.FindByConditionAsync(m => m.Nombre == nombre)).FirstOrDefault();
            if (materiaExistente != null)
            {
                throw new ArgumentException("Ya existe una materia con el nombre ingresado.");
            }

            List<string> camposErroneos = new List<string>();

            if (!ValidacionesCampos.TextoEsValido(nombre))
            {
                camposErroneos.Add("Nombre");
            }

            if (!Int32.TryParse(anio, out int anioParse) || anioParse < 1 || anioParse > 3)
            {
                camposErroneos.Add("Año");
            }

            if (!ValidacionesCampos.ModalidadMateriaEsValida(modalidad))
            {
                camposErroneos.Add("Modalidad");
            }

            if (listaProfesoresID.Count == 0)
            {
                camposErroneos.Add("Profesor/es");
            }

            if (listaDiasHorariosID.Count == 0)
            {
                camposErroneos.Add("Horarios");
            }

            if (camposErroneos.Count > 0)
            {
                throw new ArgumentException("Los siguientes campos son invalidos: ", string.Join(", ", camposErroneos));
            }

            // Valida que no haya dos materias del mismo año en el mismo horario
            List<Materia> listaMaterias = (await _materiaRepository.FindAllAsync()).ToList();
            List<DiaHorario> listaDiasHorarios = new List<DiaHorario>();
            foreach (int diaHorarioID in listaDiasHorariosID)
            {
                bool existeSolapamiento = listaMaterias.Any(m => m.Anio == anioParse && m.DiaHorario.Any(d => d.ID == diaHorarioID));
                if (existeSolapamiento)
                {
                    // Si existe una materia en el mismo año y horario, devuelve una excepcion
                    throw new ArgumentException($"Ya existe una materia para el año {anioParse} en el horario seleccionado.");
                }

                DiaHorario? diaHorario = (await _diaHorarioRepository.FindByConditionAsync(dh => dh.ID == diaHorarioID)).FirstOrDefault();
                if (diaHorario == null)
                {
                    throw new ArgumentException($"No se encontró el horario con ID {diaHorarioID}.");
                }

                listaDiasHorarios.Add(diaHorario);
            }

            //Valida que un profesor no este asignado a dos materias con el mismo horario
            List<Profesor> listaProfesores = new List<Profesor>();
            foreach (int profesorID in listaProfesoresID)
            {
                Profesor? profesor = (await _profesorRepository.FindByConditionAsync(p => p.ID == profesorID)).FirstOrDefault();
                if (profesor == null)
                {
                    throw new ArgumentException($"No se encontró el profesor con ID {profesorID}.");
                }

                // Verifica si hay solapamiento de horarios con el ID del profesor
                List<Materia> materiasDadasPorProfesor = (await _materiaRepository.FindAllAsync()).Where(m => m.Profesores.Any(p => p.ID == profesorID)).ToList();
                
                // Verifica solapamiento con los horarios nuevos
                bool haySolapamiento = materiasDadasPorProfesor.Any(m => m.DiaHorario.Any(dh => listaDiasHorariosID.Contains(dh.ID)));

                if (haySolapamiento)
                {
                    throw new ArgumentException($"El profesor con ID {profesor.ID} ya tiene asignada otra materia en uno de los horarios seleccionados.");
                }

                listaProfesores.Add(profesor);
            }

            Materia materia = new Materia()
            {
                Nombre = nombre,
                Profesores = listaProfesores,
                DiaHorario = listaDiasHorarios,
                Modalidad = modalidad,
                Anio = anioParse
            };

            await _materiaRepository.AddAsync(materia);
            await _materiaRepository.SaveAsync();
        }
        public async Task BajaMateria(string nombre)
        {
            Materia? materiaEliminar = (await _materiaRepository.FindByConditionAsync(m => m.Nombre == nombre)).FirstOrDefault();

            if (materiaEliminar == null)
            {
                throw new InvalidOperationException("La materia que se desea eliminar no existe.");
            }

            _materiaRepository.Remove(materiaEliminar);
            await _materiaRepository.SaveAsync();
        }
        public async Task<MateriaDTO> ActualizacionMateria(int id, string nombre, List<int> listaProfesoresID, List<int> listaDiasHorariosID, string modalidad, string anio)
        {
            Materia? materiaExistente = (await _materiaRepository.FindByConditionAsync(m => m.Nombre.ToLower() == nombre.ToLower())).FirstOrDefault();
            if (materiaExistente != null && materiaExistente.ID != id)
            {
                throw new ArgumentException("Ya existe una materia con el nombre ingresado.");
            }

            List<string> camposErroneos = new List<string>();

            if (!ValidacionesCampos.TextoEsValido(nombre))
            {
                camposErroneos.Add("nombre");
            }

            if (!Int32.TryParse(anio, out int anioParse) || anioParse < 1 || anioParse > 3)
            {
                camposErroneos.Add("Año");
            }

            if (!ValidacionesCampos.ModalidadMateriaEsValida(modalidad))
            {
                camposErroneos.Add("modalidad");
            }

            if (listaProfesoresID.Count == 0)
            {
                camposErroneos.Add("Profesor/es");
            }

            if (listaDiasHorariosID.Count == 0)
            {
                camposErroneos.Add("Horarios");
            }

            if (camposErroneos.Count > 0)
            {
                throw new ArgumentException("Los siguientes campos son invalidos: ", string.Join(", ", camposErroneos));
            }

            // Valida que no haya dos materias del mismo año en el mismo horario (excluyendo la misma materia que se está actualizando)
            List<Materia> listaMaterias = (await _materiaRepository.FindAllAsync()).Where(m => m.ID != materiaExistente.ID).ToList();
            List<DiaHorario> listaDiasHorarios = new List<DiaHorario>();
            foreach (int diaHorarioID in listaDiasHorariosID)
            {
                bool existeSolapamiento = listaMaterias.Any(m => m.Anio == anioParse && m.DiaHorario.Any(d => d.ID == diaHorarioID));
                if(existeSolapamiento)
                {
                    throw new ArgumentException($"Ya existe una materia para el año {anioParse} en el horario seleccionado.");
                }

                DiaHorario? diaHorario = (await _diaHorarioRepository.FindByConditionAsync(dh => dh.ID == diaHorarioID)).FirstOrDefault();
                if (diaHorario == null)
                {
                    throw new ArgumentException($"No se encontró el horario con ID {diaHorarioID}.");
                }

                listaDiasHorarios.Add(diaHorario);
            }

            // Valida que un profesor no esté asignado a dos materias con el mismo horario (excepto esta materia)
            List<Profesor> listaProfesores = new List<Profesor>();
            foreach (int profesorID in listaProfesoresID)
            {
                Profesor? profesor = (await _profesorRepository.FindByConditionAsync(p => p.ID == profesorID)).FirstOrDefault();

                if (profesor == null)
                {
                    throw new ArgumentException($"No se encontró el profesor con ID {profesorID}");
                }

                List<Materia> materiasDadasPorProfesor = (await _materiaRepository.FindAllAsync()).Where(m => m.ID != materiaExistente.ID && m.Profesores.Any(p => p.ID == profesorID)).ToList();

                bool haySolapamiento = materiasDadasPorProfesor.Any(m => m.DiaHorario.Any(dh => listaDiasHorariosID.Contains(dh.ID)));

                if (haySolapamiento)
                {
                    throw new ArgumentException($"El profesor con ID { profesor.ID } ya tiene asignada otra materia en uno de los horarios seleccionados.");
                }

                listaProfesores.Add(profesor);
            }

            materiaExistente.Profesores = listaProfesores;
            materiaExistente.DiaHorario = listaDiasHorarios;

            _materiaRepository.Update(materiaExistente);
            await _materiaRepository.SaveAsync();

            MateriaDTO materiaExistenteDTO = new MateriaDTO
            {
                ID = materiaExistente.ID,
                Nombre = materiaExistente.Nombre,
                Modalidad = materiaExistente.Modalidad,
                Anio = materiaExistente.Anio,
                NombresProfesores = materiaExistente.Profesores.Select(p => p.Usuario.Nombre).ToList(),
                DescripcionDiasHorarios = materiaExistente.DiaHorario.Select(dh => dh.GetDescripcionDiaHorario()).ToList(),
            };

            return materiaExistenteDTO;
        }
        public async Task<List<MateriaDTO>> ObtenerMaterias()
        {
            try
            {
                List<Materia> listaMaterias = (await _materiaRepository.FindAllAsync()).ToList();

                if (listaMaterias == null)
                {
                    return null;
                }

                List<MateriaDTO> listaMateriasDTO = listaMaterias.Select(t => new MateriaDTO
                {
                    ID = t.ID,
                    Nombre = t.Nombre,
                    Anio = t.Anio,
                    Modalidad = t.Modalidad,
                    NombresProfesores = t.Profesores.Select(p => p.Usuario.Nombre).ToList(),
                    DescripcionDiasHorarios = t.DiaHorario.Select(dh => dh.GetDescripcionDiaHorario()).ToList(),
                }).ToList();

                return listaMateriasDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            };
        }
        public async Task<MateriaDTO> ObtenerMateriaNombre(string nombre)
        {
            Materia? materia = (await _materiaRepository.FindByConditionAsync(t => t.Nombre == nombre)).FirstOrDefault();

            if (materia == null)
            {
                return null;
            }

            MateriaDTO materiaDTO = new MateriaDTO()
            {
                ID = materia.ID,
                Nombre = materia.Nombre,
                Anio = materia.Anio,
                Modalidad = materia.Modalidad,
                NombresProfesores = materia.Profesores.Select(p => p.Usuario.Nombre).ToList(),
                DescripcionDiasHorarios = materia.DiaHorario.Select(dh => dh.GetDescripcionDiaHorario()).ToList(),
            };

            return materiaDTO;
        }
    }
}