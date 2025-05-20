using Datos.Repositories.Contracts;
using Entidades.DTOs.Respuestas;
using Entidades.Entities;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class UsuarioLogic : IUsuarioLogic
    {
        private IUsuarioRepository _usuarioRepository;
        private IProfesorLogic _profesorLogic;
        private IAlumnoLogic _alumnoLogic;
        private IRolUsuarioRepository _rolUsuarioRepository;

        public UsuarioLogic(IUsuarioRepository usuarioRepository, IProfesorLogic profesorLogic, IAlumnoLogic alumnoLogic, IRolUsuarioRepository rolUsuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _profesorLogic = profesorLogic;
            _alumnoLogic = alumnoLogic;
            _rolUsuarioRepository = rolUsuarioRepository;
        }

        public async Task AltaUsuario(string dni, string password, string nombre, string apellido, string caracteristicaTelefono, string numeroTelefono, string localidad, string direccion, string rolUsuario, DateTime? fechaContratoIngreso)
        {
            List<string>?camposErroneos = new List<string>();
            
            RolUsuario? rolExistente = (await _rolUsuarioRepository.FindByConditionAsync(r => r.Descripcion == rolUsuario)).FirstOrDefault();

            if (!ValidacionesCampos.DocumentoEsValido(dni) || (await _usuarioRepository.FindByConditionAsync(p => p.DNI == dni)).Count() != 0)
            {
                camposErroneos.Add("DNI");
            }

            if (!ValidacionesCampos.TextoEsValido(nombre))
            {
                camposErroneos.Add("Nombre");
            }

            if (!ValidacionesCampos.TextoEsValido(apellido))
            {
                camposErroneos.Add("Apellido");
            }

            if (!ValidacionesCampos.NumeroTelefonoEsValido(caracteristicaTelefono, numeroTelefono))
            {
                camposErroneos.Add("Característica/Teléfono");
            }

            if(!ValidacionesCampos.TextoEsValido(localidad))
            {
                camposErroneos.Add("Localidad");
            }

            if (direccion == null || direccion == "")
            {
                camposErroneos.Add("Dirección");
            }

            if (rolExistente == null)
            {
                camposErroneos.Add("Rol Usuario");
            }

            if (password == null)
            {
                camposErroneos.Add("Rol Usuario");
            }
            else
            {
                password = PasswordHelper.HashPassword(password);
            }

            if (camposErroneos.Count > 0)
            {
                throw new ArgumentException("Los siguientes campos son inválidos: ", string.Join(", ", camposErroneos));
            }

            Usuario usuarioNuevo = new Usuario()
            {
                DNI = dni,
                Password = password,
                Nombre = nombre,
                Apellido = apellido,
                CaracteristicaTelefono = caracteristicaTelefono,
                NumeroTelefono = numeroTelefono,
                Localidad = localidad,
                Direccion = direccion,
                RolUsuario = rolExistente
            };

            await _usuarioRepository.AddAsync(usuarioNuevo);

            if (usuarioNuevo.RolUsuario.ID == 2)
            {
                try
                {
                    await _profesorLogic.AltaProfesor(usuarioNuevo, fechaContratoIngreso);
                }
                catch (ArgumentNullException)
                {
                    throw new ArgumentException("La fecha de contrato es obligatoria para el rol de profesor.");
                }
            }
            else if (usuarioNuevo.RolUsuario.ID == 3)
            {
                try
                {
                    await _alumnoLogic.AltaAlumno(usuarioNuevo, fechaContratoIngreso);
                }
                catch (ArgumentNullException)
                {
                    throw new ArgumentException("La fecha de ingreso es obligatoria para el rol de alumno.");
                }
            }

            await _usuarioRepository.SaveAsync();
        }
        public async Task BajaUsuario(string documento)
        {
            if (string.IsNullOrEmpty(documento) || !ValidacionesCampos.DocumentoEsValido(documento))
            {
                throw new ArgumentException("El documento ingresado no es valido.");
            }

            Usuario? usuarioEliminar = (await _usuarioRepository.FindByConditionAsync(u => u.DNI == documento)).FirstOrDefault();

            if(usuarioEliminar == null)
            {
                throw new ArgumentException("Usuario no encontrado.");
            }
            
            if (usuarioEliminar.RolUsuario.ID == 2)
            {
                await _profesorLogic.BajaProfesor(documento);
            }
            else if (usuarioEliminar.RolUsuario.ID == 3)
            {
                await _alumnoLogic.BajaAlumno(documento);
            }

            _usuarioRepository.Remove(usuarioEliminar);
            await _usuarioRepository.SaveAsync();
        }
        public async Task<UsuarioDTO> ActualizacionUsuario(string documento, string nombre, string apellido, string caracteristicaTelefono, string numeroTelefono, string localidad, string direccion, string rolUsuario)
        {
            List<string>? camposErroneos = new List<string>();

            RolUsuario? rolExistente = (await _rolUsuarioRepository.FindByConditionAsync(r => r.Descripcion == rolUsuario)).FirstOrDefault();
            Usuario? usuarioExistente = (await _usuarioRepository.FindByConditionAsync(r => r.DNI == documento)).FirstOrDefault();

            if (string.IsNullOrEmpty(documento) || !ValidacionesCampos.DocumentoEsValido(documento) || usuarioExistente == null)
            {
                throw new ArgumentException("El documento ingresado no es válido o el usuario con dicho documento no existe.");
            }

            if (!ValidacionesCampos.DocumentoEsValido(documento) || usuarioExistente.DNI != documento)
            {
                camposErroneos.Add("DNI");
            }

            if (!ValidacionesCampos.TextoEsValido(nombre))
            {
                camposErroneos.Add("Nombre");
            }

            if (!ValidacionesCampos.TextoEsValido(apellido))
            {
                camposErroneos.Add("Apellido");
            }

            if (!ValidacionesCampos.NumeroTelefonoEsValido(caracteristicaTelefono, numeroTelefono))
            {
                camposErroneos.Add("Característica/Teléfono");
            }

            if (!ValidacionesCampos.TextoEsValido(localidad))
            {
                camposErroneos.Add("Localidad");
            }

            if (direccion == null)
            {
                camposErroneos.Add("Dirección");
            }

            if (rolExistente == null)
            {
                camposErroneos.Add("Rol Usuario");
            }

            if (camposErroneos.Count > 0)
            {
                throw new ArgumentException("Se encontraron errores en los siguientes campos: " + string.Join(", ", camposErroneos));
            }

            usuarioExistente.Nombre = nombre;
            usuarioExistente.Apellido = apellido;
            usuarioExistente.CaracteristicaTelefono = caracteristicaTelefono;
            usuarioExistente.NumeroTelefono = numeroTelefono;
            usuarioExistente.Localidad = localidad;
            usuarioExistente.Direccion = direccion;
            usuarioExistente.RolUsuario = rolExistente;

            _usuarioRepository.Update(usuarioExistente);

            if (usuarioExistente.RolUsuario.ID == 2)
            {
                await _profesorLogic.ActualizacionProfesor(usuarioExistente);
            }
            else if (usuarioExistente.RolUsuario.ID == 3)
            {
                await _alumnoLogic.ActualizacionAlumno(usuarioExistente);
            }

            await _usuarioRepository.SaveAsync();

            UsuarioDTO usuarioActualizadoDTO = new UsuarioDTO()
            {
                ID = usuarioExistente.ID,
                DNI = usuarioExistente.DNI,
                Nombre = usuarioExistente.Nombre,
                Apellido = usuarioExistente.Apellido,
                CaracteristicaTelefono = usuarioExistente.CaracteristicaTelefono,
                NumeroTelefono = usuarioExistente.NumeroTelefono,
                Localidad = usuarioExistente.Localidad,
                Direccion = usuarioExistente.Direccion,
                RolUsuarioDescripcion = usuarioExistente.RolUsuario.Descripcion
            };

            return usuarioActualizadoDTO;
        }
        public async Task ActualizacionPassword(string dni, string actualPassword, string nuevaPassword)
        {
            Usuario? usuarioExistente = (await _usuarioRepository.FindByConditionAsync(r => r.DNI == dni)).FirstOrDefault();

            if (string.IsNullOrEmpty(dni) || !ValidacionesCampos.DocumentoEsValido(dni) || usuarioExistente == null)
            {
                throw new ArgumentException("El documento ingresado no es válido o el usuario con dicho documento no existe.");
            }

            if (!PasswordHelper.VerifyPassword(actualPassword, usuarioExistente.Password))
            {
                throw new ArgumentException("La contraseña ingresada es incorrecta.");
            }

            usuarioExistente.Password = PasswordHelper.HashPassword(nuevaPassword);

            _usuarioRepository.Update(usuarioExistente);
            await _usuarioRepository.SaveAsync();
        }
        public async Task<List<UsuarioDTO>> ObtenerUsuarios()
        {
            try
            {
                List<Usuario> listaUsuarios = (await _usuarioRepository.FindAllAsync()).ToList();

                if (listaUsuarios == null)
                {
                    return null;
                }

                List<UsuarioDTO> listaUsuariosDTO = listaUsuarios.Select(t => new UsuarioDTO
                {
                    ID = t.ID,
                    Nombre = t.Nombre,
                    Apellido = t.Apellido,
                    CaracteristicaTelefono = t.CaracteristicaTelefono,
                    NumeroTelefono = t.NumeroTelefono,
                    Localidad = t.Localidad,
                    Direccion = t.Direccion,
                    RolUsuarioDescripcion = t.RolUsuario.Descripcion
                }).ToList();

                return listaUsuariosDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            };
        }
        public async Task<UsuarioDTO> ObtenerUsuarioPorDNI(string dni)
        {
            if (!ValidacionesCampos.DocumentoEsValido(dni))
            {
                throw new ArgumentException("El DNI ingresado no es valido.");
            }

            Usuario? usuario = (await _usuarioRepository.FindByConditionAsync(t => t.DNI == dni)).FirstOrDefault();

            if (usuario == null)
            {
                return null;
            }

            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                ID = usuario.ID,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                CaracteristicaTelefono = usuario.CaracteristicaTelefono,
                NumeroTelefono = usuario.NumeroTelefono,
                Localidad = usuario.Localidad,
                Direccion = usuario.Direccion,
                RolUsuarioDescripcion = usuario.RolUsuario.Descripcion
            };

            return usuarioDTO;
        }
    }
}