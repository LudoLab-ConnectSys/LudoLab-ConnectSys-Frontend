(function () {
    "use strict";

    var messageBanner;

    // La función de inicialización se debe ejecutar cada vez que se cargue una página nueva.
    Office.initialize = function (reason) {
        $(document).ready(function () {
            var element = document.querySelector('.MessageBanner');
            messageBanner = new components.MessageBanner(element);
            messageBanner.hideBanner();

            $('#get-data-from-selection').click(getDataFromSelection);
        });
    };

    // Lee datos de la selección actual del documento y muestra una notificación.
    function getDataFromSelection() {
        if (Office.context.document.getSelectedDataAsync) {
            Office.context.document.getSelectedDataAsync(Office.CoercionType.Text,
                function (result) {
                    if (result.status === Office.AsyncResultStatus.Succeeded) {
                        showNotification('El texto seleccionado es:', '"' + result.value + '"');
                    } else {
                        showNotification('Error:', result.error.message);
                    }
                }
            );
        } else {
            app.showNotification('Error:', 'Esta aplicación host no admite leer datos de la selección.');
        }
    }
    
    // Función del asistente para mostrar notificaciones
    function showNotification(header, content) {
        $("#notificationHeader").text(header);
        $("#notificationBody").text(content);
        messageBanner.showBanner();
        messageBanner.toggleExpansion();
    }
})();