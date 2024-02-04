function enableToolTip() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    _ = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))
}

function inputFocus(modalId, elementId) {
    try {
        const myModal = document.getElementById(modalId)
        const myInput = document.getElementById(elementId)
        myModal.addEventListener('shown.bs.modal', () => {
            myInput.focus()
        })
    } catch (e) {
        console.log(e)
    }
}


function closeModal(modalId) {
    try {
        const myModalEl = document.getElementById(modalId);
        const modal = bootstrap.Modal.getInstance(myModalEl)
        modal.hide()
    } catch (e) {
        console.info('Failed to close modal as it is not found. Trying to remove the modal backdrop.' + e)
        closeModalBackdrop()
    }
}

function closeModalBackdrop() {
    try {
        var modalBackdrop = document.querySelector('.modal-backdrop')
        if (modalBackdrop) {
            modalBackdrop.remove();
        }
    } catch (e) {
        console.error(e)
    }
}

function openModal(modalId) {
    try {
        var myModalEl = document.getElementById(modalId);
        var modal = bootstrap.Modal.getOrCreateInstance(myModalEl)
        modal.show();
    } catch (e) {
        console.error(e)
    }
}
