// Ejecutar después de cargar la página
window.addEventListener('load', function () {
    // Cambiar el estilo activo de los enlaces del menú lateral
    var menuButtons = document.querySelectorAll('.docente-menu a');

    // Marcar activo basado en la URL actual si es necesario, o dejar que el HTML lo defina
    // En este caso, el HTML ya tendrá la clase 'activo' en el enlace correcto

    menuButtons.forEach(function (btn) {
        btn.addEventListener('click', function () {
            menuButtons.forEach(function (b) { b.classList.remove('activo'); });
            btn.classList.add('activo');
        });
    });
});
