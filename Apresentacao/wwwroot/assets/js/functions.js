function newGuid() {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
        var r = (Math.random() * 16) | 0,
            v = c === "x" ? r : (r & 0x3) | 0x8;
        return v.toString(16);
    });
}

function alertSuccess(message) {

    Swal.fire({
        title: '',
        text: message,
        icon: 'success',
        customClass: {
            confirmButton: 'btn btn-primary'
        },
        showClass: {
            popup: 'animate__animated animate__flipInX'
        },
        buttonsStyling: false
    });

}

function alertWarning(message) {
    Swal.fire({
        title: '',
        text: message,
        icon: 'warning',
        customClass: {
            confirmButton: 'btn btn-primary'
        },
        showClass: {
            popup: 'animate__animated animate__flipInX'
        },
        buttonsStyling: false
    });
}

function alertError(message) {
    Swal.fire({
        title: '',
        text: message,
        icon: 'error',
        customClass: {
            confirmButton: 'btn btn-primary'
        },
        showClass: {
            popup: 'animate__animated animate__shakeX'
        },
        buttonsStyling: false
    });
}

function showLoading() {
    Swal.fire({
        title: 'Carregando os dados...',
        didOpen: () => {
            Swal.showLoading()
        },
        allowOutsideClick: false
    })
}