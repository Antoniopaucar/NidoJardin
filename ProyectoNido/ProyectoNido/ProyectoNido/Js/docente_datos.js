// Ejecutar después de cargar la página
window.addEventListener('load', function () {
    // Ocultar el texto "Sin archivos seleccionados" de los FileUpload
    var fileUploads = document.querySelectorAll('.file-upload-custom');
    fileUploads.forEach(function (fu) {
        var spans = fu.getElementsByTagName('span');
        for (var i = 0; i < spans.length; i++) {
            if (spans[i].textContent.indexOf('Sin archivos seleccionados') !== -1) {
                spans[i].style.display = 'none';
            }
        }
    });

    // Cambiar el estilo activo de los enlaces del menú lateral
    var menuButtons = document.querySelectorAll('.docente-menu a');
    menuButtons.forEach(function (btn) {
        btn.addEventListener('click', function () {
            menuButtons.forEach(function (b) { b.classList.remove('activo'); });
            btn.classList.add('activo');
        });
    });
});
