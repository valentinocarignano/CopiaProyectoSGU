using Datos.Repositories.Contracts;
using Entidades.DTOs.Respuestas;
using Entidades.Entities;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class ProfesorLogic : IProfesorLogic
    {
        private IProfesorRepository _profesorRepository;

        public ProfesorLogic(IProfesorRepository profesorRepository)
        {
            _profesorRepository = profesorRepository;
        }

        public async Task AltaProfesor(Usuario usuario, DateTime? fechaInicioContrato)
        {      
            if (fechaInicioContrato == null)
            {
                throw new ArgumentNullException("Se debe asignar una fecha de inicio de contrato.");
            }

            if (usuario == null)
            {
                throw new ArgumentNullException("El profesor debe estar vinculado a un usuario.");
            }
            
            Profesor profesorNuevo = new Profesor()
            {
                FechaInicioContrato = fechaInicioContrato.Value,
                Usuario = usuario,
            };

            await _profesorRepository.AddAsync(profesorNuevo);
            await _profesorRepository.SaveAsync();
        }
        public async Task BajaProfesor(string documento)
        {
            if (string.IsNullOrEmpty(documento) || !ValidacionesCampos.DocumentoEsValido(documento))
            {
                throw new ArgumentException("El documento ingresado no es valido.");
            }

            Profesor? profesorExistente = (await _profesorRepository.FindByConditionAsync(t => t.Usuario.DNI == documento)).FirstOrDefault();

            if (profesorExistente == null)
            {
                throw new ArgumentException("No hay ningun profesor vinculado con el documento ingresado..");
            }

            _profesorRepository.Remove(profesorExistente);
            await _profesorRepository.SaveAsync();
        }
        public async Task ActualizacionProfesor(Usuario usuario)
        {
            Profesor? profesorExistente = (await _profesorRepository.FindByConditionAsync(p => p.Usuario == usuario)).FirstOrDefault();

            if(profesorExistente == null)
            {
                throw new ArgumentException("El usuario ingresado no esta vinculado con ningun profesor.");
            }

            _profesorRepository.Update(profesorExistente);
            await _profesorRepository.SaveAsync();
        }
        public async Task<List<ProfesorDTO>> ObtenerProfesores()
        {
            try
            {
                List<Profesor> listaProfesores = (await _profesorRepository.FindAllAsync()).ToList();

                if (listaProfesores == null)
                {
                    return null;
                }

                List<ProfesorDTO> listaProfesoresDTO = listaProfesores.Select(t => new ProfesorDTO
                {
                    ID = t.ID,
                    DNI = t.Usuario.DNI,
                    Nombre = t.Usuario.Nombre,
                    Apellido = t.Usuario.Apellido,
                    FechaInicioContrato = t.FechaInicioContrato,
                }).ToList();

                return listaProfesoresDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            };
        }
        public async Task<ProfesorDTO> ObtenerProfesorDNI(string dni)
        {
            if (!ValidacionesCampos.DocumentoEsValido(dni))
            {
                throw new ArgumentException("El DNI ingresado no es valido.");
            }

            Profesor? profesor = (await _profesorRepository.FindByConditionAsync(t => t.Usuario.DNI == dni)).FirstOrDefault();

            if (profesor == null)
            {
                return null;
            }

            ProfesorDTO profesorDTO = new ProfesorDTO()
            {
                ID = profesor.ID,
                DNI = profesor.Usuario.DNI,
                Nombre = profesor.Usuario.Nombre,
                Apellido = profesor.Usuario.Apellido,
                FechaInicioContrato = profesor.FechaInicioContrato,
            };

            return profesorDTO;
        }
    }
}