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
        return true
    } catch (e) {
        console.info('Failed to close modal as it is not found. Trying to remove the modal backdrop.' + e)
        closeModalBackdrop()
        return true
    }
}

function closeModalBackdrop() {
    try {
        const modalBackdrop = document.querySelector('.modal-backdrop')
        if (modalBackdrop) {
            modalBackdrop.remove();
        }
    } catch (e) {
        console.error(e)
    }
}

function openModal(modalId) {
    try {
        const myModalEl = document.getElementById(modalId);
        const modal = bootstrap.Modal.getOrCreateInstance(myModalEl)
        modal.show();
        const inputElements = myModalEl.querySelectorAll('input')
        if (inputElements.length > 1) {
            inputElements[0].classList.contains('visually-hidden') ? inputElements[1].focus() : inputElements[0].focus()
        }
    } catch (e) {
        console.error(e)
    }
}
