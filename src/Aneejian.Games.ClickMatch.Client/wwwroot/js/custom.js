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

function focusFirstInput() {
    const input = document.querySelector('input')
    if (input)
        input.focus()
}

function focusElementById(id) {
    focusElement(document.getElementById(id))
}

function focusElementByClassName(className) {
    focusElement(document.querySelector(className))
}

function focusElement(elementToFocus) {
    try {
        elementToFocus.focus();
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
        const body = document.querySelector('body')
        body.classList.remove('modal-open')
        body.style = ''

    } catch (e) {
        console.error(e)
    }
}

function openModal(modalId) {
    try {
        const myModalEl = document.getElementById(modalId);
        const modal = bootstrap.Modal.getOrCreateInstance(myModalEl)
        modal.show()
        setModalFocus(myModalEl)
    } catch (e) {
        console.error(e)
    }
}

function setModalFocus(myModalEl) {
    const inputElement = myModalEl.querySelector('input')
    if (inputElement) {
        inputElement.focus()
    }
}