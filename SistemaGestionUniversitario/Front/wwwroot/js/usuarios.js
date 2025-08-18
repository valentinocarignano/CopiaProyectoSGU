// =======================

// MODAL ELIMINAR USUARIO

// =======================

const confirmModal = document.getElementById('confirmDeleteModal');
const confirmCheckbox = document.getElementById('confirmCheckbox');
const btnAceptar = document.getElementById('btnAceptar');
const deleteForm = document.getElementById('deleteForm');
const deleteMessage = document.getElementById('deleteMessage');

// Configurar modal al abrirse
confirmModal.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget; // Botón que abrió el modal
    const dni = button.getAttribute('data-dni');
    const nombre = button.getAttribute('data-nombre');
    const apellido = button.getAttribute('data-apellido');

    // Mensaje personalizado
    deleteMessage.textContent = `¿Desea eliminar el usuario ${nombre} ${apellido} (DNI: ${dni})?`;

    // Acción del formulario
    deleteForm.setAttribute('action', `/Usuario/DeleteUsuario?dni=${dni}`);

    // Resetear checkbox y botón
    confirmCheckbox.checked = false;
    btnAceptar.disabled = true;
});

// Habilitar aceptar solo si checkbox está marcado
confirmCheckbox.addEventListener('change', function () {
    btnAceptar.disabled = !this.checked;
});

// =======================

// MODAL INFORMACION USUARIO

// =======================

const infoModal = document.getElementById('informacionUsuario');

infoModal.addEventListener('show.bs.modal', function (event) {
    const button = event.relatedTarget;

    const dni = button.getAttribute('data-dni');
    const nombre = button.getAttribute('data-nombre');
    const apellido = button.getAttribute('data-apellido');
    const localidad = button.getAttribute('data-localidad');
    const direccion = button.getAttribute('data-direccion');
    const telefono = button.getAttribute('data-telefono');
    const rol = button.getAttribute('data-rol');

    document.getElementById('infoUsuarioLabel').textContent = `${nombre} ${apellido}`;
    document.getElementById('infoDni').textContent = dni;
    document.getElementById('infoLocalidad').textContent = localidad;
    document.getElementById('infoDireccion').textContent = direccion;
    document.getElementById('infoTelefono').textContent = telefono;
    document.getElementById('infoRol').textContent = rol;
});
