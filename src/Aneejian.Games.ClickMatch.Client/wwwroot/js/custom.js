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
        // ignore
    }

}


function closeModal(modalId) {
    try {
        var myModalEl = document.getElementById(modalId);
        var modal = bootstrap.Modal.getInstance(myModalEl)
        modal.hide();
    } catch (e) {
        // ignore
    }
}

