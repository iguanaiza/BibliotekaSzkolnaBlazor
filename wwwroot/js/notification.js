﻿window.ShowToastr = function (type, message, title) {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": type === "success" || type === "error",
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    switch (type) {
        case "success":
            toastr.success(message, title);
            break;
        case "error":
            toastr.error(message, title);
            break;
        case "warning":
            toastr.warning(message, title);
            break;
        case "info":
            toastr.info(message, title);
            break;
    }
}
